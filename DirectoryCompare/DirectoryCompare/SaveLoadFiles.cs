using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace DirectoryCompare
{
    public class SaveLoadFiles
    {
        // Xml tag for node, e.g. 'node' in case of <node></node>
        private const string XmlNodeTag = "node";

        // Xml attributes for node e.g. <node text="Asia" tag="" 
        // imageindex="1"></node>
        private const string XmlNodeTextAtt = "text";
        private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeImageIndexAtt = "imageindex";

        private Form sender;

        public delegate void UpdateMessage(string sMessage);
        public UpdateMessage SetMessage;

        public SaveLoadFiles(Form sender)
        { this.sender = sender; }

        public bool SaveFile(TreeView tv, List<AllFileInfo> lafi, string path, string sRootDirectory)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, sRootDirectory);
                    bf.Serialize(fs, lafi);
                    SerializeTreeView(tv, fs);

                    return true;
                }
            }
            catch (ArgumentNullException) { CallErrorCode(3); }
            catch (SerializationException) { CallErrorCode(2); }
            catch (IOException) { CallErrorCode(0); }
            catch (Exception) { CallErrorCode(-1); }

            return false;
        }

        public bool LoadFile(TreeView tv, ref List<AllFileInfo> lafi, string path, ref string sRootDirectory)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    tv.Nodes.Clear();

                    sRootDirectory = (string) bf.Deserialize(fs);
                    lafi = (List<AllFileInfo>) bf.Deserialize(fs);
                    DeserializeTreeView(tv, fs);
                    tv.ExpandAll();

                    return true;
                }
            }
            catch (ArgumentNullException) { CallErrorCode(3); }
            catch (SerializationException) { CallErrorCode(2); }
            catch (IOException) { CallErrorCode(1); }
            catch (Exception) { CallErrorCode(-1); }

            return false;
        }

        public void DeserializeTreeView(TreeView treeView, Stream stream)
        {
            XmlTextReader reader = null;
            try
            {
                // disabling re-drawing of treeview till all nodes are added
                treeView.BeginUpdate();
                reader = new XmlTextReader(stream);
                TreeNode parentNode = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            TreeNode newNode = new TreeNode();
                            bool isEmptyElement = reader.IsEmptyElement;

                            // loading node attributes
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount > 0)
                            {
                                for (int i = 0; i < attributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    SetAttributeValue(newNode,
                                                 reader.Name, reader.Value);
                                }
                            }
                            // add new node to Parent Node or TreeView
                            if (parentNode != null)
                                parentNode.Nodes.Add(newNode);
                            else
                                treeView.Nodes.Add(newNode);

                            // making current node 'ParentNode' if its not empty
                            if (!isEmptyElement)
                                parentNode = newNode;
                        }
                    }
                    // moving up to in TreeView if end tag is encountered
                    else if (reader.NodeType == XmlNodeType.EndElement)
                        if (reader.Name == XmlNodeTag)
                            parentNode = parentNode.Parent;

                    else if (reader.NodeType == XmlNodeType.XmlDeclaration)
                    { }

                    else if (reader.NodeType == XmlNodeType.None)
                    { return; }

                    else if (reader.NodeType == XmlNodeType.Text)
                    { parentNode.Nodes.Add(reader.Value); }
                }
            }
            finally
            {
                // enabling redrawing of treeview after all nodes are added
                treeView.EndUpdate();
                reader.Close();
            }
        }

        public void SerializeTreeView(TreeView treeView, Stream stream)
        {
            XmlTextWriter textWriter = new XmlTextWriter(stream,
                                          System.Text.Encoding.ASCII);
            // writing the xml declaration tag
            textWriter.WriteStartDocument();
            //textWriter.WriteRaw("\r\n");
            // writing the main tag that encloses all node tags
            textWriter.WriteStartElement("TreeView");

            // save the nodes, recursive method
            SaveNodes(treeView.Nodes, textWriter);

            textWriter.WriteEndElement();

            textWriter.Close();
            stream.Close();
        }

        private void SetAttributeValue(TreeNode node, string propertyName, string value)
        {
            if (propertyName == XmlNodeTextAtt)
            {
                node.Text = value;
            }
            else if (propertyName == XmlNodeImageIndexAtt)
            {
                node.ImageIndex = int.Parse(value);
            }
            else if (propertyName == XmlNodeTagAtt)
            {
                node.Tag = value;
            }
        }

        private void SaveNodes(TreeNodeCollection nodesCollection, XmlTextWriter textWriter)
        {
            for (int i = 0; i < nodesCollection.Count; i++)
            {
                TreeNode node = nodesCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                textWriter.WriteAttributeString(XmlNodeTextAtt,
                                                           node.Text);
                textWriter.WriteAttributeString(
                    XmlNodeImageIndexAtt, node.ImageIndex.ToString());
                if (node.Tag != null)
                    textWriter.WriteAttributeString(XmlNodeTagAtt,
                                                node.Tag.ToString());
                // add other node properties to serialize here  
                if (node.Nodes.Count > 0)
                {
                    SaveNodes(node.Nodes, textWriter);
                }
                textWriter.WriteEndElement();
            }
        }

        private void CallErrorCode(int i)
        {
            string sMessage = string.Empty;

            switch (i)
            {
                case 0:
                    sMessage = "Can not save file to this location. You may not have access to this folder.";
                    break;

                case 1:
                    sMessage = "Can not load file from this location. You may not have access to this folder.";
                    break;

                case 2:
                    sMessage = "A deserializing error has occured. File may be corrupt.";
                    break;

                case 3:
                    sMessage = "File not foud. File location may have moved.";
                    break;

                default:
                    sMessage = "A general error has occured"; 
                    break;
            }

            if (SetMessage != null) sender.Invoke(SetMessage, sMessage);
        }
    }
}
