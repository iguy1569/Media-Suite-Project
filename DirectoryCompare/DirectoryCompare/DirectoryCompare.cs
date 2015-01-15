using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DirectoryCompare
{
    public partial class DirectoryCompare : Form
    {
        private SetSearchFilters _ssfSourceFilters,
                                 _ssfDestinationFilters;

        public enum Relation { Source, Destination, Match, None, Root }

        private int _iMessageRotator = 0;

        private KeyValuePair<TreeNode, bool> _kvpbtnTreeViewcheckChangeActive;

        private bool _bMessageInvokeUpdate = true,
                     _bDirInfoSource = false,
                     _bDirInfoDestination = false;
                    

        private List<AllFileInfo> _afiSourceFiles = null,
                                  _afiDestinationFiles = null,
                                  _afiSourceDiff = null,
                                  _afiDestinationDiff = null;

        private string _sRootPathSource,
                       _sRootpathDestination;

        private FileTransfer _ftFunctions;

        private Thread _tLoadAnimator;
        private Loading _lLoadAnimation;

        public DirectoryCompare()
        {
            InitializeComponent();
            _ssfSourceFilters = new SetSearchFilters(TB_SFilters, LBL_SFilterMessage);
            _ssfDestinationFilters = new SetSearchFilters(TB_DFilters, LBL_DFilterMessage);
            _ftFunctions = new FileTransfer(this);

            _ftFunctions.SetProgressBarInterval += new FileTransfer.UpdateProgress(UpdateProgressBar);
            _ftFunctions.SetMessage += new FileTransfer.UpdateMessage(MessageUpdate);
            _ftFunctions.CallEnd += new FileTransfer.EndTransfer(EndTransferCall);

            MessageTimer.Enabled = true;
            MessageTimer.Stop();
        }

        #region Control triggers and control supports

        #region Main operations
        private void BTN_TransferSourceDest_Click(object sender, EventArgs e)
        {
            List<AllFileInfo> lafiSelected = CreateTransferList(TV_SourceDiff.Nodes, _afiSourceDiff);

            LBL_Message.MouseEnter += new EventHandler(PB_CurrentTransfer_MouseEnter);
            LBL_Message.MouseLeave += new EventHandler(PB_CurrentTransfer_MouseLeave);

            DisableTransferButtons();
            _ftFunctions.CopyFileListToDestination(lafiSelected, _sRootPathSource, _sRootpathDestination);
        }

        private void BTN_DestToSource_Click(object sender, EventArgs e)
        {
            List<AllFileInfo> lafiSelected = CreateTransferList(TV_DestinationDiff.Nodes, _afiDestinationDiff);

            LBL_Message.MouseEnter += new EventHandler(PB_CurrentTransfer_MouseEnter);
            LBL_Message.MouseLeave += new EventHandler(PB_CurrentTransfer_MouseLeave);

            DisableTransferButtons();
            _ftFunctions.CopyFileListToDestination(lafiSelected, _sRootpathDestination, _sRootPathSource);
        }

        private void BTN_PrintDiffSource_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                PrintListToFile.PrintFile(_afiSourceDiff, saveFileDialog.FileName, false);
        }

        private void BTN_PrintDiffDest_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                PrintListToFile.PrintFile(_afiDestinationDiff, saveFileDialog.FileName, true);
        }
        #endregion

        #region update messages
        private void PB_CurrentTransfer_MouseEnter(object sender, EventArgs e)
        {
            MessageTimer.Start();
            LBL_Message.Text = "File: " + _ftFunctions.FileName;
            _iMessageRotator = 0;

            _bMessageInvokeUpdate = false;
        }

        private void PB_CurrentTransfer_MouseLeave(object sender, EventArgs e)
        {
            MessageTimer.Stop();
            _bMessageInvokeUpdate = true;
        }

        private void MessageTimer_Tick(object sender, EventArgs e)
        {
            string[] saTransferMessage = new string[] { "File: " + _ftFunctions.FileName, 
                                                        "From: " + _ftFunctions.SourceDirectory,
                                                        "To: " + _ftFunctions.DestinationDirectory };

            LBL_Message.Text = saTransferMessage[_iMessageRotator++ % saTransferMessage.Length];
        }
        #endregion

        #region closing / ending methods
        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            _ftFunctions.CancelCurrentOperation();
            BTN_Cancel.Enabled = false;
        }

        private void DirectoryCompare_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ftFunctions.CancelCurrentOperation();
        }
        #endregion

        #region Load Fileinfo into values
        private void BTN_GetSource_Click(object sender, EventArgs e)
        {
            BTN_PrintDiffSource.Enabled = false;
            BTN_PrintDiffDest.Enabled = false;

            _bDirInfoSource = ChangeDirectoryTrees(ref _afiSourceFiles, TV_Source, Relation.Source, ref _sRootPathSource);
            BTN_SaveSrc.Enabled = _bDirInfoSource;

            if (_afiSourceDiff != null && _afiDestinationDiff != null)
            {
                BTN_TransferSourceDest.Enabled = _bDirInfoSource;
                BTN_DestToSource.Enabled = _bDirInfoDestination;

                BTN_PrintDiffSource.Enabled = true;
                BTN_PrintDiffDest.Enabled = true;
            }
        }

        private void BTN_GetDestination_Click(object sender, EventArgs e)
        {
            BTN_PrintDiffSource.Enabled = false;
            BTN_PrintDiffDest.Enabled = false;

            _bDirInfoDestination = ChangeDirectoryTrees(ref _afiDestinationFiles, TV_Destination, Relation.Destination, ref _sRootpathDestination);
            BTN_SaveDst.Enabled = _bDirInfoDestination;

            if (_afiSourceDiff != null && _afiDestinationDiff != null)
            {
                BTN_DestToSource.Enabled = _bDirInfoDestination;
                BTN_TransferSourceDest.Enabled = _bDirInfoSource;

                BTN_PrintDiffSource.Enabled = true;
                BTN_PrintDiffDest.Enabled = true;

                BTN_LoadDst.Enabled = true;
                BTN_LoadSrc.Enabled = true;
            }
        }

        #region load/save to file
        private void BTN_SaveSrc_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveLoadFiles slf = new SaveLoadFiles(this);
                slf.SetMessage += new SaveLoadFiles.UpdateMessage(MessageUpdate);
                slf.SaveFile(TV_Source, _afiSourceFiles, saveFileDialog.FileName.ToString(), _sRootPathSource);
                BuildMasterTree();
            }
        }

        private void BTN_LoadSrc_Click(object sender, EventArgs e)
        {
            _afiSourceFiles = null;
            BTN_PrintDiffSource.Enabled = false;
            BTN_PrintDiffDest.Enabled = false;
            BTN_SaveSrc.Enabled = false;

            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK && folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                CallLoadAnimation();
                SaveLoadFiles slf = new SaveLoadFiles(this);
                slf.SetMessage += new SaveLoadFiles.UpdateMessage(MessageUpdate);
                slf.LoadFile(TV_Source, ref _afiSourceFiles, openFileDialog.FileName.ToString(), ref _sRootPathSource);

                foreach (AllFileInfo afi in _afiSourceFiles)
                    afi.rootValue = Relation.Source;

                _sRootPathSource = folderBrowserDialog.SelectedPath;

                BuildMasterTree();

                _bDirInfoSource = false;
                KillLoadAnimation();

                BTN_SaveSrc.Enabled = true;

                if (_afiSourceDiff != null && _afiDestinationDiff != null)
                {
                    BTN_DestToSource.Enabled = _bDirInfoDestination;
                    BTN_TransferSourceDest.Enabled = _bDirInfoSource;

                    BTN_PrintDiffDest.Enabled = true;
                    BTN_PrintDiffSource.Enabled = true;
                }
            }
        }

        private void BTN_SaveDst_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveLoadFiles slf = new SaveLoadFiles(this);
                slf.SetMessage += new SaveLoadFiles.UpdateMessage(MessageUpdate);
                slf.SaveFile(TV_Destination, _afiDestinationFiles, saveFileDialog.FileName.ToString(), _sRootpathDestination);
                BuildMasterTree();
            }
        }

        private void BTN_LoadDst_Click(object sender, EventArgs e)
        {
            _afiDestinationFiles = null;
            BTN_PrintDiffSource.Enabled = false;
            BTN_PrintDiffDest.Enabled = false;
            BTN_SaveDst.Enabled = false;

            openFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK && folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                CallLoadAnimation();
                SaveLoadFiles slf = new SaveLoadFiles(this);
                slf.SetMessage += new SaveLoadFiles.UpdateMessage(MessageUpdate);
                slf.LoadFile(TV_Destination, ref _afiDestinationFiles, openFileDialog.FileName.ToString(),ref _sRootpathDestination);

                foreach (AllFileInfo afi in _afiDestinationFiles)
                    afi.rootValue = Relation.Destination;

                _sRootpathDestination = folderBrowserDialog.SelectedPath;

                BuildMasterTree();

                _bDirInfoDestination = false;
                KillLoadAnimation();

                BTN_SaveDst.Enabled = true;

                if (_afiSourceDiff != null && _afiDestinationDiff != null)
                {
                    BTN_DestToSource.Enabled = _bDirInfoDestination;
                    BTN_TransferSourceDest.Enabled = _bDirInfoSource;

                    BTN_PrintDiffDest.Enabled = true;
                    BTN_PrintDiffSource.Enabled = true;
                }
            }
        }
        #endregion

        #region TreeView Checks
        private void TV_SourceDiff_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_kvpbtnTreeViewcheckChangeActive.Key.Equals(e.Node))
                _kvpbtnTreeViewcheckChangeActive = new KeyValuePair<TreeNode, bool>(null, false);
        }

        private void TV_DestinationDiff_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_kvpbtnTreeViewcheckChangeActive.Key.Equals(e.Node))
                _kvpbtnTreeViewcheckChangeActive = new KeyValuePair<TreeNode, bool>(null, false);
        }


        private void TV_SourceDiff_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!_kvpbtnTreeViewcheckChangeActive.Value)
            {
                _kvpbtnTreeViewcheckChangeActive = new KeyValuePair<TreeNode, bool>(e.Node, true);
                CheckTreeViewNode(e.Node, !e.Node.Checked);
                DeselectUpChain(e.Node);
            }
        }

        private void TV_DestinationDiff_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!_kvpbtnTreeViewcheckChangeActive.Value)
            {
                _kvpbtnTreeViewcheckChangeActive = new KeyValuePair<TreeNode, bool>(e.Node, true);
                CheckTreeViewNode(e.Node, !e.Node.Checked);
                DeselectUpChain(e.Node);
            }
        }

        private void DeselectUpChain(TreeNode tn)
        {
            if (tn.Checked)
            {
                do
                {
                    if (tn.Parent != null)
                    {
                        tn.Parent.Checked = false;
                        tn = tn.Parent;
                    }
                }
                while (tn.Parent != null);
            }
        }
        #endregion
        #endregion

        #endregion

        #region thread callbacks
        private void EndTransferCall()
        {
            MessageTimer.Stop();
            LBL_Message.MouseEnter -= PB_CurrentTransfer_MouseEnter;
            LBL_Message.MouseLeave -= PB_CurrentTransfer_MouseLeave;

            LBL_Message.Text = "Select an option";

            BTN_TransferSourceDest.Enabled = _bDirInfoSource;
            BTN_DestToSource.Enabled = _bDirInfoDestination;
            BTN_Cancel.Enabled = false;

            BTN_LoadDst.Enabled = true;
            BTN_LoadSrc.Enabled = true;
        }

        private void MessageUpdate(string sMessage)
        {
            LBL_Message.Text = sMessage;
        }

        private void UpdateProgressBar(long lPercentage, long lTimeRemaining, long lBytesPerSecond)
        {
            PB_CurrentTransfer.Value = (int)lPercentage;

            if (_bMessageInvokeUpdate)
                SetMessages(lPercentage, lTimeRemaining, lBytesPerSecond);
        }

        private void DisableTransferButtons()
        {
            BTN_TransferSourceDest.Enabled = false;
            BTN_DestToSource.Enabled = false;
            BTN_Cancel.Enabled = true;
            BTN_LoadSrc.Enabled = false;
            BTN_LoadDst.Enabled = false;
        }
        #endregion

        #region TreeViewbuilder
        private void ListDirectory(TreeView treeView, string path, List<string> searchPattern, List<AllFileInfo> lafiFilesfound, Relation assign)
        {
            treeView.Nodes.Clear();
            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(path);
            
            try
            {
                treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, searchPattern, lafiFilesfound, assign));
            }
            catch (UnauthorizedAccessException)
            {
                treeView.Nodes.Clear();
                MessageBox.Show("User does not have access to this folder.\n\n" +
                                "Please change the administrative rights on said folder \n" + 
                                "or select a new folder.", "Not accessable", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);               
            }
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, List<string> searchPattern, List<AllFileInfo> lfiFilesfound, Relation assign)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);

            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory, searchPattern, lfiFilesfound, assign));

            foreach (var file in directoryInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly).Where(s => searchPattern.Contains(s.Extension)))
            {
                TreeNode temp = new TreeNode(file.Name);
                temp.Name = file.FullName;
                directoryNode.Nodes.Add(temp);
                lfiFilesfound.Add(new AllFileInfo(file, assign));
            }

            return directoryNode;
        }
        #endregion

        #region Support methods
        private bool ChangeDirectoryTrees(ref List<AllFileInfo> afiFilesFound, TreeView tvTreeView, Relation assign, ref string sRootPath)
        {
            tvTreeView.Nodes.Clear();
            TV_CombinedResults.Nodes.Clear();

            afiFilesFound = null;

            if (folderBrowserDialog.ShowDialog().Equals(DialogResult.OK))
            {
                CallLoadAnimation();
                sRootPath = folderBrowserDialog.SelectedPath;
                afiFilesFound = new List<AllFileInfo>();
                ListDirectory(tvTreeView, folderBrowserDialog.SelectedPath, _ssfDestinationFilters._lsFilter, afiFilesFound, assign);
                tvTreeView.ExpandAll();

                BuildMasterTree();

                KillLoadAnimation();
                return true;
            }
            return false;
        }

        private void BuildMasterTree()
        {
            TV_DestinationDiff.Nodes.Clear();
            TV_SourceDiff.Nodes.Clear();
            TV_CombinedResults.Nodes.Clear();

            List<AllFileInfo> lafiCombined;

            if (_afiDestinationFiles != null && _afiSourceFiles != null)
            {
                _afiSourceDiff = AllFileInfoClone.Clone(_afiSourceFiles.Except(_afiDestinationFiles, new AllFileInfoIEquatable()).ToList());
                _afiDestinationDiff = AllFileInfoClone.Clone(_afiDestinationFiles.Except(_afiSourceFiles, new AllFileInfoIEquatable()).ToList());

                lafiCombined = AllFileInfoClone.Clone(_afiSourceFiles.Intersect(_afiDestinationFiles, new AllFileInfoIEquatable()).ToList());

                foreach (AllFileInfo afi in lafiCombined)
                    afi.rootValue = Relation.Match;
                
                lafiCombined.AddRange(_afiSourceDiff);
                lafiCombined.AddRange(_afiDestinationDiff);

                TreeViewBuilder.AssembleTree(_afiSourceDiff, TV_SourceDiff, false);
                TreeViewBuilder.AssembleTree(_afiDestinationDiff, TV_DestinationDiff, false);
                TreeViewBuilder.AssembleTree(lafiCombined, TV_CombinedResults, true);

                TV_SourceDiff.ExpandAll();
                TV_DestinationDiff.ExpandAll();

                TV_CombinedResults.Sort();
                TV_CombinedResults.ExpandAll();
            }
        }

        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0)
                {
                    this.CheckTreeViewNode(item, isChecked);
                }
            }
        }

        private List<AllFileInfo> CreateTransferList(TreeNodeCollection tncNodes, List<AllFileInfo> lafiInput)
        {
            List<AllFileInfo> lafiOutput = new List<AllFileInfo>();
            List<string> lsTemp = CheckedNames(tncNodes);

            foreach (string s in lsTemp)
            {
                AllFileInfo afiTemp = lafiInput.Find(
                    delegate(AllFileInfo afi) { return afi.fiFilesFound.Name.Equals(s); });

                if (afiTemp != null)
                    lafiOutput.Add(afiTemp);
            }
            return lafiOutput;
        }

        private List<string> CheckedNames(TreeNodeCollection theNodes)
        {
            List<string> aResult = new List<String>();

            if (theNodes != null)
            {
                foreach (TreeNode aNode in theNodes)
                {
                    if (aNode.Checked)
                        aResult.Add(aNode.Text);

                    aResult.AddRange(CheckedNames(aNode.Nodes));
                }
            }

            return aResult;
        }

        private void SetMessages(long lPercentage, long lTimeRemaining, long lBytesPerSecond)
        {
            LBL_Message.Text = string.Format("{0}%  @ {1} MBytes Per Second : Time Left {2}:{3}:{4}",
                lPercentage, (lBytesPerSecond / (1024.0 * 1024.0)).ToString("0.0"),
                (lTimeRemaining / 3600).ToString("00"), ((lTimeRemaining % 3600) / 60).ToString("00"), (lTimeRemaining % 60).ToString("00"));
        }

        private void CallLoadAnimation()
        {
            Point pCenter = new Point(Location.X + Width/2, Location.Y + Height/2);
            _tLoadAnimator = new Thread(delegate() { LoadAnimationThread(pCenter); });
            _tLoadAnimator.Start();
        }

        private void KillLoadAnimation()
        {
            if (_lLoadAnimation != null)
                _lLoadAnimation.KillForm();
        }

        private void LoadAnimationThread(Point pCenter)
        {
            _lLoadAnimation = new Loading();
            _lLoadAnimation.Size = new Size(100, 100);
            _lLoadAnimation.StartPosition = FormStartPosition.Manual;
            _lLoadAnimation.Location = new Point(pCenter.X - _lLoadAnimation.Width / 2,
                                                 pCenter.Y - _lLoadAnimation.Height / 2);

            _lLoadAnimation.ShowDialog();
        }
        #endregion
    }

    [Serializable]
    public class AllFileInfo
    {
        public FileInfo fiFilesFound;
        public long lFileByteLength;
        public DirectoryCompare.Relation rootValue;

        public AllFileInfo(FileInfo fiReturn, DirectoryCompare.Relation directorySource)
        {
            fiFilesFound = fiReturn;
            lFileByteLength = fiReturn.Length;
            rootValue = directorySource;
        }
    }

    public class AllFileInfoIEquatable : IEqualityComparer<AllFileInfo>
    {
        public bool Equals(AllFileInfo afi1, AllFileInfo afi2)
        {
            string sName1 = afi1.fiFilesFound.Name,
                   sName2 = afi2.fiFilesFound.Name;

            long lLength1 = afi1.lFileByteLength,
                 lLength2 = afi2.lFileByteLength;

            if (sName1.Equals(sName2) && lLength1.Equals(lLength2))
                return true;
            return false;
        }

        public int GetHashCode(AllFileInfo obj)
        {
            return -1;
        }
    }

    public static class AllFileInfoClone
    {
        public static AllFileInfo Clone<AllFileInfo>(AllFileInfo source)
        {
            if (!typeof(AllFileInfo).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(AllFileInfo);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (AllFileInfo)formatter.Deserialize(stream);
            }
        }
    }
    
}


