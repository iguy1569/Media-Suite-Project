using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace DirectoryCompare
{
    public class TreeViewBuilder
    {
        private static List<TreeNodeBuilder> _ltnbNodeCollection = null;

        public static void AssembleTree(List<AllFileInfo> lafiInput, TreeView tvOutput, bool bMergeRoot)
        {
            _ltnbNodeCollection = new List<TreeNodeBuilder>();

            foreach (AllFileInfo afi in lafiInput)
                TraverseFilePath(afi, bMergeRoot);

            BuildTreeNodes(tvOutput);
        }

        public static void AssembleTree(List<AllFileInfo> lafiInput, TreeView tvOutput, string sSourceDir, string sDestinationDir, bool bMergeRoot)
        {
            _ltnbNodeCollection = new List<TreeNodeBuilder>();

            foreach (AllFileInfo afi in lafiInput)
                TraverseFilePath(afi, bMergeRoot);

            BuildTreeNodes(tvOutput);
        }

        private static void TraverseFilePath(AllFileInfo afi, bool bMergeRoot)
        {
            List<string> lsPath = BuildPathSplit(afi.fiFilesFound, bMergeRoot);

            for (int i = 0; i < lsPath.Count - 1; i++)
            {
               TreeNodeBuilder tnbTemp = new TreeNodeBuilder(lsPath[i], lsPath[i + 1]);
               tnbTemp.dcrRoot = afi.rootValue;

               if (_ltnbNodeCollection.Contains(tnbTemp, new TreeNodeBuilderMatch()))
               {
                   int index = _ltnbNodeCollection.FindIndex(
                        delegate(TreeNodeBuilder tnb) { return tnb.sPredecessor.Equals(tnbTemp.sPredecessor) && tnb.sSelf.Equals(tnbTemp.sSelf); });

                   tnbTemp = _ltnbNodeCollection[index];
               }
               else
                   _ltnbNodeCollection.Add(tnbTemp);

               if (i + 2 > lsPath.Count - 1)
                   tnbTemp.AddChild(afi.rootValue.ToString());
               else
                   tnbTemp.AddChild(lsPath[i + 2]);
            }
        }

        private static List<string> BuildPathSplit(FileInfo fi, bool bMergeRoot)
        {
            List<string> lsSplitPath = new List<string>();
            DirectoryInfo diTemp = fi.Directory;
            do
            {
                lsSplitPath.Add(diTemp.Name);
                diTemp = diTemp.Parent;
            }
            while (diTemp != null);

            lsSplitPath.Add("*");
            lsSplitPath.Reverse();

            if (bMergeRoot)
                lsSplitPath[1] = "All Content Combined";

            lsSplitPath.Add(fi.Name);

            return lsSplitPath;
        }

        private static void BuildTreeNodes(TreeView tv)
        {
            List<TreeNodeBuilder> ltnbRoots = _ltnbNodeCollection.FindAll(
                delegate(TreeNodeBuilder tnb) { return tnb.sPredecessor.Equals("*"); });
          
            foreach (TreeNodeBuilder tnbRoot in ltnbRoots)
                tv.Nodes.Add(RecursiveNodeCreation(tnbRoot.sPredecessor, tnbRoot.sSelf, tnbRoot.lsChildren));
        }

        private static TreeNode RecursiveNodeCreation(string sParent, string sSelf, List<string> lsChildren)
        {
            TreeNode tn = new TreeNode(sSelf);

            foreach (string sChild in lsChildren)
            {
                TreeNodeBuilder tnbTemp = _ltnbNodeCollection.Find(
                    delegate(TreeNodeBuilder tnb) { return (tnb.sPredecessor.Equals(sSelf) && tnb.sSelf.Equals(sChild)); });

                if (tnbTemp != null)
                    tn.Nodes.Add(RecursiveNodeCreation(sSelf, sChild, tnbTemp.lsChildren));
                else
                {
                    switch (sChild)
                    {
                        case "Source":
                            tn.ForeColor = Color.Green;
                            break;
                        
                        case "Destination":
                            tn.ForeColor = Color.Red;
                            break;

                        case "Match":
                            tn.ForeColor = Color.Blue;
                            break;
                    }
                }
            }
            return tn;
        }
    }

    public class TreeNodeBuilder
    {
        public string sPredecessor,
               sSelf;

        public List<string> lsChildren;

        public DirectoryCompare.Relation dcrRoot;

        public TreeNodeBuilder(string sPred, string sSelf)
        {
            sPredecessor = sPred;
            this.sSelf = sSelf;
            lsChildren = null;
            dcrRoot = DirectoryCompare.Relation.None;
        }

        public void AddChild(string sChild)
        {
            if (lsChildren == null)
                lsChildren = new List<string>();

            if (!lsChildren.Contains(sChild))
                lsChildren.Add(sChild);
        }
    }

    public class TreeNodeBuilderMatch : IEqualityComparer<TreeNodeBuilder>
    {
        public bool Equals(TreeNodeBuilder x, TreeNodeBuilder y)
        {
            if (x == null || y == null)
                return false;

            if (x.sPredecessor.Equals(y.sPredecessor) && x.sSelf.Equals(y.sSelf))
                return true;

            return false;
        }

        public int GetHashCode(TreeNodeBuilder obj)
        {
            return -1;
        }
    }    
}
