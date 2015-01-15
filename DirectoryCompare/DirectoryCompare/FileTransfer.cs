using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace DirectoryCompare
{
    public class FileTransfer
    {
        public delegate void UpdateProgress(long lPercent, long lTimeRemaining, long BytesPerSecond);
        public  UpdateProgress SetProgressBarInterval;

        public delegate void UpdateMessage(string sMessage);
        public UpdateMessage SetMessage;

        public delegate void EndTransfer();
        public EndTransfer CallEnd;

        private Form sender;

        private long _lCounter = 0,
                     _ltotalLength = 0,
                     _lLastCount = 0,
                     _lInteval = 0;

        private bool _bCancelThread;

        private string _sLastCreated;
        private List<string> _lsToDelete;

        Thread tFileTransfer;

        public string FileName { get; private set; }
        public string SourceDirectory { get; private set; }
        public string DestinationDirectory { get; private set; }

        public FileTransfer(Form sender) 
        { this.sender = sender; }

        public void CopyFileListToDestination(List<AllFileInfo> lfiTransfers, string sSourceRoot, string sDestinationRoot)
        {
            _bCancelThread = false;
            tFileTransfer = new Thread(delegate() { CopyFileListToDestinationThread(lfiTransfers, sSourceRoot, sDestinationRoot); });
            tFileTransfer.Start();
        }

        public void CancelCurrentOperation()
        {
            _bCancelThread = true;            
        }

        private void CopyFileListToDestinationThread(List<AllFileInfo> lafiTransfers, string sSourceRoot, string sDestinationRoot)
        {
            _lInteval = CreateProgressSystem(lafiTransfers);
            _lLastCount = 0;
            _ltotalLength = 0;
            _lCounter = 0;

            _lsToDelete = new List<string>();

            System.Timers.Timer MessageTimer = new System.Timers.Timer(1000);
            MessageTimer.Elapsed += new System.Timers.ElapsedEventHandler(MessageTimer_Elapsed);   
            
            foreach (AllFileInfo afi in lafiTransfers)
                _ltotalLength += afi.lFileByteLength;

            if (SetProgressBarInterval != null)
                sender.Invoke(SetProgressBarInterval, 0, 0, 0);

            try
            {
                foreach (AllFileInfo afi in lafiTransfers)
                {
                    SourceDirectory = afi.fiFilesFound.DirectoryName;
                    FileName = afi.fiFilesFound.Name;

                    DestinationDirectory = CreateDestinationPath(afi, sSourceRoot, sDestinationRoot);
                    if (!Directory.Exists(DestinationDirectory))
                        Directory.CreateDirectory(DestinationDirectory);

                    _sLastCreated = Path.Combine(sDestinationRoot, afi.fiFilesFound.FullName.Replace(SourceDirectory,""));

                    #region file exists check
                    if (File.Exists(_sLastCreated))
                    {
                        if (SetMessage != null)
                            sender.Invoke(SetMessage, _sLastCreated + " already exists");

                        _lCounter += afi.lFileByteLength;

                        long lBytesProcesed = _lCounter - _lLastCount,
                             lAmountLeft = _ltotalLength - _lCounter,
                             lPercentage = _lCounter / _lInteval;

                        if (SetProgressBarInterval != null)
                            sender.Invoke(SetProgressBarInterval, lPercentage, lAmountLeft / lBytesProcesed, lBytesProcesed);
                    }
                    #endregion

                    #region transfer file
                    else
                    {
                        using (FileStream source = new FileStream(afi.fiFilesFound.FullName, FileMode.Open, FileAccess.Read))
                        {
                            using (FileStream destination = new FileStream(_sLastCreated, FileMode.CreateNew, FileAccess.Write))
                            {
                                MessageTimer.Start();
                                WriteChunks(source, destination);

                                if (_bCancelThread)
                                    if (File.Exists(_sLastCreated) && !IsFileLocked(new FileInfo(_sLastCreated)))
                                        File.Delete(_sLastCreated);
                            }
                        }
                    }
                    #endregion
                }
            }

            catch (PathTooLongException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "Either the path or file name is too long (needs to be under 255 characters)\n" + ex.Message); }
            catch (DirectoryNotFoundException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "the target directory was not created / does not exists\n" + ex.Message); }
            catch (NotSupportedException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "Unknown error\n" + ex.Message); }
            catch (FileNotFoundException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "Source file was not found\n" + ex.Message); }
            catch (SecurityException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "Form of permission issue\n" + ex.Message); }
            catch (IOException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "General file error\n" + ex.Message); }
            catch (ArgumentOutOfRangeException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "An error was made while loading the file buffer\n" + ex.Message); }
            catch (ArgumentException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "There was an error crerating file in destination\n" + ex.Message); }
            catch (UnauthorizedAccessException ex) { if (SetMessage != null) sender.Invoke(SetMessage, "Destination directory has permission restrictions\n" + ex.Message); }
            finally
            {
                MessageTimer.Close();
                if (SetProgressBarInterval != null)
                    sender.Invoke(SetProgressBarInterval, 0, 0, 0);

                if (CallEnd != null)
                    sender.Invoke(CallEnd);

                foreach (string s in _lsToDelete)
                    if (File.Exists(_sLastCreated) && !IsFileLocked(new FileInfo(_sLastCreated)))
                        File.Delete(s);
            }
        }

        void MessageTimer_Elapsed(object o, System.Timers.ElapsedEventArgs e)
        {
            long lBytesProcesed = _lCounter - _lLastCount,
                 lAmountLeft = _ltotalLength - _lCounter,
                 lPercentage = _lCounter / _lInteval;

            _lLastCount = _lCounter;

            if (SetProgressBarInterval != null)
                sender.Invoke(SetProgressBarInterval, lPercentage, lAmountLeft / (lBytesProcesed.Equals(0) ? 1 : lBytesProcesed), lBytesProcesed);
        }

        private  long CreateProgressSystem(List<AllFileInfo> lafi)
        {
            long lByteInteval = 0;
            foreach (AllFileInfo afi in lafi)
                lByteInteval += afi.lFileByteLength;

            return lByteInteval / 100; // percentage breakdown
        }

        private  string CreateDestinationPath(AllFileInfo fi, string SourceRootPath, string DestinationRootPath)
        {
            return fi.fiFilesFound.DirectoryName.Replace(SourceRootPath, DestinationRootPath);
        }

        private void WriteChunks(FileStream source, FileStream destination)
        {
            BinaryReader reader = new BinaryReader(source);
            BinaryWriter writer = new BinaryWriter(destination);
            int chunkSize = 1024,
                iCount = 0;

            try
            {
                do
                {
                    writer.Write(reader.ReadBytes(chunkSize));

                    iCount += chunkSize;
                    _lCounter += chunkSize;

                    if (_bCancelThread)
                        break;
                }
                while (reader.BaseStream.Position < reader.BaseStream.Length);

                if (_bCancelThread)
                {
                    reader.Close();
                    writer.Close();
                }
            }

            catch (OutOfMemoryException)
            {
                _lCounter += reader.BaseStream.Length;

                _lsToDelete.Add(_sLastCreated);

                long lBytesProcesed = _lCounter - _lLastCount,
                     lAmountLeft = _ltotalLength - _lCounter,
                     lPercentage = _lCounter / _lInteval;

                if (SetProgressBarInterval != null)
                    sender.Invoke(SetProgressBarInterval, lPercentage, lAmountLeft / lBytesProcesed, lBytesProcesed);
            }

            finally
            {
                source.Close();
                destination.Close();
            }
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            { stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None); }
            catch (IOException)
            { return true; }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }
    }
}
