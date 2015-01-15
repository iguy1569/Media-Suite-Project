using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Security;

namespace MediaDistributor
{
    static class ToolKit_MediaSeperator
    {
        private static List<string> Media_Filters = new List<string>(new string[] { ".wmv", ".mkv", ".avi", ".mp4", ".divx", ".xvid", ".mpeg", ".h264", ".x264", ".ts" });
        private static long FilesTotalLength, 
                            FilesCurrentLength;
        private static int iProgress;

        public delegate void PromptStart(object o, EventArgs e);
        public static event PromptStart TransferStarted;
        
        public delegate void PromptFinish(object o, EventArgs e);
        public static event PromptFinish TransferFinished;

        public static void GetTotalFileLength(List<FileInfo> lfi)
        {
            FilesTotalLength = 0;
            FilesCurrentLength = 0;
            foreach (FileInfo fi in lfi)
                FilesTotalLength += fi.Length;
        }

        #region ToolKit Variables
        public const string ConfigExtension = ".cfg";

        public struct PermissionsAccess
        {
            public string sEncPassword;
            public string Domain;
            public string User;
            public string Password
            {
                get
                { return DataProtector.DecryptData(sEncPassword); }
                set
                { sEncPassword = DataProtector.EncryptData(value); }
            }

            public PermissionsAccess(string Dom, string Use, string Pass)
            {
                Domain = Dom;
                User = Use;
                sEncPassword = DataProtector.EncryptData(Pass);
            }
        }

        public static PermissionsAccess SourcePermissions { private get; set; }
        public static PermissionsAccess DestinationPermissions { private get; set; }

        public static DirectoryInfo _SourceFolderConfig { get; private set; }
        public static DirectoryInfo _DestinationFolderConfig { get; private set; }

        public static string _TransferCurrent { get; private set; }
        public static int _TransferProgress 
        { 
            get { return iProgress; }
            private set 
            { 
                iProgress = value <= 100 ? value : 100;
                iProgress = iProgress >= 0 ? iProgress : 0;
            }
        }

        public static string MediaFilter
        {
            get
            {
                string sReturn = "";
                foreach (string s in Media_Filters)
                    sReturn += s + ", ";
                sReturn = sReturn.Trim().TrimEnd(',');
                return sReturn;
            }

            set
            {
                Regex reg = new Regex("^[.][a-zA-Z0-9]{2,4}$");

                string[] saMediaFilter = value.Split(',');

                List<string> lsReplacementFilters = new List<string>();

                try
                {
                    for (int i = 0; i < saMediaFilter.Length; i++)
                    {
                        string sTemp = saMediaFilter[i];
                        sTemp = sTemp.Trim();

                        if (Regex.Match(sTemp, "^[.][a-zA-Z0-9]{2,4}$", RegexOptions.IgnoreCase).Success)
                            lsReplacementFilters.Add(sTemp);
                    }

                    if (lsReplacementFilters.Count > 0)
                        Media_Filters = lsReplacementFilters;
                }
                catch (FormatException)
                { }
            }
        }

        public static char[] NameRange { get; private set; }
        public static bool KeepSubDir { get; private set; }

        #endregion

        /// <summary>
        /// Returns a list of all files in a directory based on media extension filters.
        /// Bool determines if searching all folders or top level only (default true, include).
        /// </summary>
        /// <param name="diDirectorySearch">Directory to search</param>
        /// <param name="bSubDirInclude">subdirectory include</param>
        /// <returns></returns>
        public static List<FileInfo> GetMediaFromFolders(DirectoryInfo diDirectorySearch, bool bSubDirInclude)
        {
            // check for directory existence before attempting grab
            if (!diDirectorySearch.Exists)
                return null;

            List<FileInfo> fiOutput = new List<FileInfo>();     // media files found in directory 
            SearchOption soSubs = SearchOption.AllDirectories;  // directory search options

            if (!bSubDirInclude)
                soSubs = SearchOption.TopDirectoryOnly;

            try
            {
                // check all files in directory where extension is included in the media filters 
                foreach (FileInfo file in diDirectorySearch.GetFiles("*.*", soSubs).Where(s => Media_Filters.Contains(s.Extension)))
                    fiOutput.Add(file);

                return fiOutput;
            }
            catch (ArgumentException)
            { return null; }
        }

        #region Configuration File Settings

        /// <summary>
        /// Used to rebuild a basic configuration file.
        /// Initialization of custom options
        /// </summary>
        public static string BuildDefaultConfigFile(string sConfigName)
        {
            List<string> lsDefaultBuildFile = new List<string>();
            
            #region basic configuration file
            lsDefaultBuildFile.AddRange(new string[] 
                {
                    @"/////////////////// Media Disribution Config File \\\\\\\\\\\\\\\\\\\\\\\",
                    @"",
                    @"- Media distribution configuration file",
                    @"- Do not change the hashtag enclosed words",
                    @"- Add edits after the hashtag words",
                    @"- Alpha range accepts either a '*' character (indicating any range)",
                    @"  or a range (of numbers to letters) '$-$'. ie. 'A-Z', 'B-N', '0-Z', '0-9' ",
                    @"",
                    @"#Media Filters# ",
                    @"",
                    @"#Source# ", 
                    @"#Source Permissions# ",
                    @"",
                    @"#Destination# ",
                    @"#Destination Permissions# ",
                    @"",
                    @"#Transfer Type# Move",
                    @"",
                    @"#Keep Subdirectory# True",
                    @"",
                    @"#Media Type# Series",
                    @"",
                    @"#Alpha Range# *",
                    @""
                }
            );
            #endregion

            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                File.Create(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)).Close();
            else
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), string.Empty);

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            try
            {
                using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
                {
                    // add all default lines to the config 
                    using (StreamWriter swConfig = new StreamWriter(fsConfig))
                    {
                        foreach (string s in lsDefaultBuildFile)
                            swConfig.WriteLine(s);
                    }
                }
                SetMediaExtensionFiltersForConfig(sConfigName);    // Set The Media filters based off list
            }
            catch (FileNotFoundException ex)
            { return ex.Message; }

            catch (IOException ex)
            { return ex.Message; }

            return "Success";
        }

        /// <summary>
        /// Delete config file from 
        /// </summary>
        /// <param name="sConfigName">Config File Name</param>
        /// <returns></returns>
        public static string DeleteConfigFile(string sConfigName)
        {

            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return "File not found";

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));

            try
            {
                File.Delete(fi.FullName);
            }
            catch (IOException ex)
            {
                return ex.Message;
            }
            catch(AccessViolationException ex)
            {
                return ex.Message;
            }

            return "Success";
        }

        /// <summary>
        /// Returns a list of all config files in the current directory
        /// </summary>
        /// <returns></returns>
        public static List<FileInfo> GetConfigFileList()
        {
            DirectoryInfo diTemp = new DirectoryInfo(Directory.GetCurrentDirectory().ToString());
            return diTemp.GetFiles("*" + ConfigExtension).ToList();
        }

        /// <summary>
        /// Reads into the media filter list the current file configuration of media filters.
        /// </summary>
        public static void GetMediaExtensionFiltersFromConfig(string sConfigName)
        {
            FileStream fsConfig = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), FileMode.Open, FileAccess.ReadWrite);

            using (StreamReader srConfig = new StreamReader(fsConfig))
            {
                string sTemp;
                while ((sTemp = srConfig.ReadLine()) != null)
                {
                    if (sTemp.Contains("#Media Filters#"))      // if on media filter line
                    {
                        MediaFilter = sTemp.Replace("#Media Filters#", "").Trim(); // clean up extension list before interpretation
                        return;
                    }
                }
                srConfig.Close();
            }
            fsConfig.Close();
        }

        /// <summary>
        /// Writes into the configuration file the current list of media filters.
        /// Rewrites file from scratch
        /// </summary>
        public static void SetMediaExtensionFiltersForConfig(string sConfigName)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite);

            #region Read Origional Config
            List<string> lsTemp = new List<string>();
            using (StreamReader sr = new StreamReader(fsConfig))
            {
                string sTemp;
                while ((sTemp = sr.ReadLine()) != null)
                    lsTemp.Add(sTemp);
                sr.Close();
            }
            fsConfig.Close();
            #endregion

            // overwrite as blank file
            File.WriteAllText(fi.FullName, string.Empty);

            #region Write new Config
            fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite);
            using (StreamWriter swConfig = new StreamWriter(fsConfig))
            {
                foreach (string s in lsTemp)
                    swConfig.WriteLine(s.Contains("#Media Filters#") ? "#Media Filters# " + MediaFilter : s);
                swConfig.Close();
            }
            fsConfig.Close();
            #endregion
        }

        /// <summary>
        /// Sets the source folder for the configuration file based off of path input.
        /// Returns 0 if successful, returns -1 if not
        /// </summary>
        /// <param name="diInput">input directory</param>
        public static int SetSourceDirectory(DirectoryInfo diInput, string sConfigName)
        {
            if (diInput.Exists)
            {
                _SourceFolderConfig = diInput;
                WriteDirectoryPath(diInput,sConfigName, "Source");
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// Sets the destination folder for the configuration file based off of path input.
        /// Returns 0 if successful, returns -1 if not
        /// </summary>
        /// <param name="diInput">input directory</param>
        public static int SetDestinationDirectory(DirectoryInfo diInput, string sConfigName)
        {
            if (diInput.Exists)
            {
                _SourceFolderConfig = diInput;
                WriteDirectoryPath(diInput, sConfigName, "Destination");
                return 0;
            }
            return -1;
        }

        /// <summary>
        /// writes to the config file the directory path to write to
        /// </summary>
        /// <param name="sConfigLine">config file to write to</param>
        /// <returns>directory path to write to config</returns>
        private static void WriteDirectoryPath(DirectoryInfo diInput, string sConfigName, string sConfigLine)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            List<string> lsTemp = new List<string>();

            using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        string sTarget = "#" + sConfigLine + "#";
                        if (sTemp.Contains(sTarget))
                            sTemp = sTarget + " " + diInput.FullName;
                        lsTemp.Add(sTemp);
                    }
                }
            }

            File.WriteAllText(fi.FullName, string.Empty);
            using (FileStream fsConfig = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter swConfig = new StreamWriter(fsConfig))
                {
                    foreach (string s in lsTemp)
                        swConfig.WriteLine(s);
                }
            }
        }

        /// <summary>
        /// Sets the source folder for the configuration file based off of path input.
        /// Returns 0 if successful, returns -1 if not
        /// </summary>
        /// <param name="diInput">input directory</param>
        public static void SetSourcePermissions(string sConfigName)
        {
            WriteDirectoryPermissions(sConfigName, "Source", SourcePermissions);
        }

        /// <summary>
        /// Sets the destination folder for the configuration file based off of path input.
        /// Returns 0 if successful, returns -1 if not
        /// </summary>
        /// <param name="diInput">input directory</param>
        public static void SetDestinationPermissions(string sConfigName)
        {
            WriteDirectoryPermissions(sConfigName, "Destination", DestinationPermissions);
        }


        /// <summary>
        /// writes to the config file the directory path permissions
        /// </summary>
        /// <param name="sConfigLine">config file to write to</param>
        /// <returns>directory path to write to config</returns>
        private static void WriteDirectoryPermissions(string sConfigName, string sConfigLine, PermissionsAccess paPermitFolders)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            List<string> lsTemp = new List<string>();

            using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        string sTarget = "#" + sConfigLine + " Permissions#";
                        if (sTemp.Contains(sTarget))
                            sTemp = sTarget + " " + paPermitFolders.Domain + " | " + paPermitFolders.User + " | " + paPermitFolders.sEncPassword;
                        lsTemp.Add(sTemp);
                    }
                }
            }

            File.WriteAllText(fi.FullName, string.Empty);
            using (FileStream fsConfig = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter swConfig = new StreamWriter(fsConfig))
                {
                    foreach (string s in lsTemp)
                        swConfig.WriteLine(s);
                }
            }
        }

        /// <summary>
        /// Returns the directory of the source folder from the configuration file.
        /// Additionally adds the directory info to the source property
        /// </summary>
        /// <returns>returns directory info</returns>
        public static DirectoryInfo GetSourceDirectory(string sConfigName)
        {
            _SourceFolderConfig = ReadDirectoryPath(sConfigName, "Source");
            return _SourceFolderConfig;
        }

        /// Returns the directory of the destination folder from the configuration file.
        /// Additionally adds the directory info to the destination property
        /// </summary>
        /// <returns>returns directory info</returns>
        public static DirectoryInfo GetDesinationDirectory(string sConfigName)
        {
            _DestinationFolderConfig = ReadDirectoryPath(sConfigName, "Destination");
            return _DestinationFolderConfig;
        }

        /// <summary>
        /// Reads through all lines of the config file for particular line
        /// returns the directory info if it actually exists
        /// </summary>
        /// <param name="sConfigLine">config file to read</param>
        /// <returns>returns directory info if exists</returns>
        private static DirectoryInfo ReadDirectoryPath(string sConfigName, string sConfigLine)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return null;

            using (FileStream fsConfig = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        string sTarget = "#" + sConfigLine + "#";
                        if (sTemp.Contains(sTarget))      // if on media filter line
                        {
                            sTarget = sTemp.Replace(sTarget, "").Trim(); // clean up extension list before interpretation
                            if (Directory.Exists(sTarget))
                                return new DirectoryInfo(sTarget);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the directory of the source folder from the configuration file.
        /// Additionally adds the directory info to the source property
        /// </summary>
        /// <returns>returns directory info</returns>
        public static bool GetSourcePermissions(string sConfigName)
        {
            PermissionsAccess paTemp = new PermissionsAccess();
            bool bTemp = ReadDirectoryPermssions(sConfigName, "Source", ref paTemp);
            SourcePermissions = paTemp;
            return bTemp;
        }

        /// Returns the directory of the destination folder from the configuration file.
        /// Additionally adds the directory info to the destination property
        /// </summary>
        /// <returns>returns directory info</returns>
        public static bool GetDesinationPermissions(string sConfigName)
        {
            PermissionsAccess paTemp = new PermissionsAccess();
            bool bTemp = ReadDirectoryPermssions(sConfigName, "Destination", ref paTemp);
            DestinationPermissions = paTemp;
            return bTemp;
        }

        private static bool ReadDirectoryPermssions(string sConfigName, string sConfigLine, ref PermissionsAccess paPermitFolders)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return false;

            using (FileStream fsConfig = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        string sTarget = "#" + sConfigLine + " Permissions#";
                        if (sTemp.Contains(sTarget))      // if on media filter line
                        {
                            sTarget = sTemp.Replace(sTarget, "").Trim(); // clean up extension list before interpretation
                            string[] saPermissions = sTarget.Split('|');

                            if (saPermissions.Length == 3)
                            {
                                paPermitFolders.Domain = saPermissions[0].Trim();
                                paPermitFolders.User = saPermissions[1].Trim();
                                paPermitFolders.sEncPassword = saPermissions[2].Trim();
                                return true;
                            }
                            else
                                return false;

                        }
                    }
                }
            }
            return false;
        }
        

        /// <summary>
        /// Reads the transfer type from the current file configuration.
        /// </summary>
        public static bool GetTransferFromConfig(string sConfigName)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return true;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));                   
            bool bReturn = true;

            using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        if (sTemp.Contains("#Transfer Type#"))      // if on media filter line
                        {
                            sTemp = sTemp.Replace("#Transfer Type# ", "").Trim(); // clean up extension list before interpretation
                            if (sTemp.ToLower().Contains("copy"))
                                bReturn = true;
                            else if (sTemp.ToLower().Contains("move"))
                                bReturn = false;
                        }
                    }
                }
            }
            return bReturn;
        }

        /// <summary>
        /// Writes into the configuration file the current transfer type.
        /// Rewrites file from scratch
        /// </summary>
        public static void SetTransferForConfig(string sConfigName, bool bKeepOrigional)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));          
            List<string> lsTemp = new List<string>();

            #region Read Origional Options
            using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        if (sTemp.Contains("#Transfer Type#"))
                            if (bKeepOrigional)
                                sTemp = "#Transfer Type# " + "Copy";
                            else
                                sTemp = "#Transfer Type# " + "Move";

                        lsTemp.Add(sTemp);
                    }
                }
            }
            #endregion

            File.WriteAllText(fi.FullName, string.Empty);

            #region Write New Options
            using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter swConfig = new StreamWriter(fsConfig))
                {
                    foreach (string s in lsTemp)
                        swConfig.WriteLine(s);
                }
            }
            #endregion
        }

        /// <summary>
        /// Reads the media search check from the current file configuration.
        /// returns byte value: 
        ///     0 - series, 1 - standalone
        /// </summary>
        public static byte GetMediaTypeFromConfig(string sConfigName)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return 0;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            byte bReturn = 0;

            try
            {
                using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (StreamReader srConfig = new StreamReader(fsConfig))
                    {
                        string sTemp;
                        while ((sTemp = srConfig.ReadLine()) != null)
                        {
                            if (sTemp.Contains("#Media Type#"))      // if on media filter line
                            {
                                sTemp = sTemp.Replace("#Media Type# ", "").Trim(); // clean up extension list before interpretation
                                if (sTemp.ToLower().Contains("series"))
                                    bReturn = 0;
                                else if (sTemp.ToLower().Contains("standalone"))
                                    bReturn = 1;
                                else if (sTemp.ToLower().Contains("any"))
                                    bReturn = 2;
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
            return bReturn;
        }

        /// <summary>
        /// Writes into the configuration file the current transfer type.
        /// Rewrites file from scratch
        /// </summary>
        public static void SetMediaTypeForConfig(string sConfigName, byte bOption)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            List<string> lsTemp = new List<string>();

            using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        if (sTemp.Contains("#Media Type#"))
                        {
                            if (bOption == 0)
                                sTemp = "#Media Type# " + "Series";
                            else if (bOption == 1)
                                sTemp = "#Media Type# " + "Standalone";
                            else if (bOption == 2)
                                sTemp = "#Media Type# " + "Any";
                        }

                        lsTemp.Add(sTemp);
                    }
                }
                
            }

            File.WriteAllText(fi.FullName, string.Empty);

            using (FileStream fsConfig = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter swConfig = new StreamWriter(fsConfig))
                {
                    foreach (string s in lsTemp)
                        swConfig.WriteLine(s);
                }
            }
        }

        /// <summary>
        /// Reads the file name ranges from the current file configuration.
        /// </summary>
        public static char[] GetAlphaRangeFromConfig(string sConfigName)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return NameRange; 

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            try
            {
                using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (StreamReader srConfig = new StreamReader(fsConfig))
                    {
                        string sTemp;
                        while ((sTemp = srConfig.ReadLine()) != null)
                        {
                            if (sTemp.Contains("#Alpha Range#"))      // if on media filter line
                            {
                                if (sTemp.Contains('*'))
                                    NameRange = new char[] { '0', 'Z' };

                                Match mat = Regex.Match(sTemp.Replace("#Alpha Range#", ""), "[0-9A-Za-z]-[0-9A-Za-z]", RegexOptions.IgnoreCase);
                                if (mat.Success)
                                    NameRange = mat.Value.Trim().Replace("-", "").ToUpper().ToCharArray();
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return NameRange; 
            }
            return NameRange;
        }

        /// <summary>
        /// Writes into the configuration file the file name limits.
        /// Rewrites file from scratch
        /// </summary>
        public static void SetAlphaRangeTypeForConfig(string sConfigName, char[] caAlphaRange)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            List<string> lsTemp = new List<string>();

            using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        if (sTemp.Contains("#Alpha Range#"))
                        {
                            if (caAlphaRange.Contains('0') && caAlphaRange.Contains('Z'))
                                sTemp = "#Alpha Range# *";
                            else
                                sTemp = "#Alpha Range# " + caAlphaRange[0] + "-" + caAlphaRange[1];
                        }

                        lsTemp.Add(sTemp);
                    }
                }
            }

            File.WriteAllText(fi.FullName, string.Empty);

            using (FileStream fsConfig = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter swConfig = new StreamWriter(fsConfig))
                {
                    foreach (string s in lsTemp)
                        swConfig.WriteLine(s);
                }
            }
        }

        /// <summary>
        /// Reads the sudirectory keep status from the current file configuration.
        /// </summary>
        public static bool GetSubDirectoryFromConfig(string sConfigName)
        {
            KeepSubDir = true;
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return KeepSubDir;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            try
            {
                using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (StreamReader srConfig = new StreamReader(fsConfig))
                    {
                        string sTemp;
                        while ((sTemp = srConfig.ReadLine()) != null)
                        {
                            if (sTemp.Contains("#Keep Subdirectory#"))      // if on media filter line
                                if (sTemp.Replace("#Keep Subdirectory#", "").Trim().ToLower().Contains("false"))
                                    KeepSubDir = false;
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                return KeepSubDir;
            }
            return KeepSubDir;
        }

        /// <summary>
        /// Writes into the configuration file the sub directory .
        /// Rewrites file from scratch
        /// </summary>
        public static void SetSubDirectoryForConfig(string sConfigName, bool bKeepSubDir)
        {
            if (!File.Exists(Path.Combine(Directory.GetCurrentDirectory(), sConfigName)))
                return;

            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), sConfigName));
            List<string> lsTemp = new List<string>();

            using (FileStream fsConfig = new FileStream(fi.FullName, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader srConfig = new StreamReader(fsConfig))
                {
                    string sTemp;
                    while ((sTemp = srConfig.ReadLine()) != null)
                    {
                        if (sTemp.Contains("#Keep Subdirectory#"))
                        {
                            if (bKeepSubDir)
                                sTemp = "#Keep Subdirectory# True";
                            else
                                sTemp = "#Keep Subdirectory# False";
                        }
                        lsTemp.Add(sTemp);
                    }
                }
            }

            File.WriteAllText(fi.FullName, string.Empty);

            using (FileStream fsConfig = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), sConfigName), FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter swConfig = new StreamWriter(fsConfig))
                {
                    foreach (string s in lsTemp)
                        swConfig.WriteLine(s);
                }
            }
        }

        #endregion

        #region File Transfer Functions

        /// <summary>
        /// Transfers all files from source folder to destination folder.
        /// Keeps all subdirectory information intact for the transfer.
        /// Must have both source and destination determined for use.
        /// returns message depending on results.
        /// Total progress is monitored using int (based out of 100 total).
        /// </summary>
        /// <param name="fiInput">file source</param>
        /// <returns>message string</returns>
        public static string MoveMediaContentOneToOne(List<FileInfo> fiInput, bool bKeepOrigional, bool bKeepSubdir, bool bFilterSeries)
        {
            _TransferProgress = 0;
            TransferStartedCall();

            #region Check for requirements
            if (!_SourceFolderConfig.Exists)
            {
                TransferFinsishedCall();
                return "Source folder has not been determined";
            }

            if (!_DestinationFolderConfig.Exists)
            {
                TransferFinsishedCall();
                return "Destination folder has not been determined";
            }

            if (fiInput.Count <= 0)
            {
                TransferFinsishedCall();
                return "No Files to Transfer";
            }
            #endregion

            #region Basic filtering of unwanted files
            for (int i = fiInput.Count - 1; i >= 0; i--)
            {
                if (File.Exists(fiInput[i].FullName) && fiInput[i].FullName.Contains(_SourceFolderConfig.FullName))
                {
                    string n;
                    int s, e;
                    if (ToolKit_MediaSeperator.DetermineSeriesInfo(fiInput[i].Name, out n, out s, out e))
                        if (bFilterSeries)
                            fiInput.RemoveAt(i);

                    else if (!ToolKit_MediaSeperator.DetermineTitleAlphaRange(fiInput[i].Name, ToolKit_MediaSeperator.NameRange[0], ToolKit_MediaSeperator.NameRange[1]))
                        fiInput.RemoveAt(i);                                                
                }
                else
                    fiInput.RemoveAt(i);

                continue;
            }
            #endregion

            GetTotalFileLength(fiInput);

            foreach (FileInfo fi in fiInput)
            {
                if (File.Exists(fi.FullName) && fi.FullName.Contains( _SourceFolderConfig.FullName))
                { 
                    string sTransferLocation;
                    if (bKeepSubdir)
                        sTransferLocation = Regex.Replace(fi.DirectoryName, _SourceFolderConfig.FullName, _DestinationFolderConfig.FullName);
                    else
                        sTransferLocation = Path.Combine(_DestinationFolderConfig.FullName);

                    
                    string sResult = CopyMoveFiles(fi, new DirectoryInfo(sTransferLocation), bKeepOrigional);
                    if (sResult != "Success")
                    return sResult;                
                }
                else
                    return "Error transfering file, " + fi.Name + " does not exist in source folder " + _SourceFolderConfig.Name;
            }
            TransferFinsishedCall();
            return "Transfer Successful";
        }

        /// <summary>
        /// Transfers all files from source folder to specific folder based off of contained words in title.
        /// Used mainly for sorting out TV content
        /// Must have both source and destination determined for use.
        /// returns message depending on results.
        /// Total progress is monitored using int (based out of 100 total).
        /// </summary>
        /// <param name="fiInput">file source</param>
        /// <returns>message string</returns>
        public static string MoveMediaContentOneToMany(List<FileInfo> fiInput, bool bKeepOrigional)
        {
            List<DirectoryInfo> ldiSubDirectory = _DestinationFolderConfig.GetDirectories("*.*", SearchOption.TopDirectoryOnly).ToList();
            List<KeyValuePair<DirectoryInfo, List<string>>> ddiSubSearches = GetSearchWordParameters(ldiSubDirectory);
            TransferStartedCall();

            #region Check for requirements
            if (!_DestinationFolderConfig.Exists)
            {
                TransferFinsishedCall();
                return "Destination folder has not been determined";
            }

            if (fiInput.Count <= 0)
            {
                TransferFinsishedCall();
                return "No Files to Transfer";
            }
            #endregion

            #region Basic filtering of unwanted files
            for (int i = fiInput.Count - 1; i >= 0; i--)
            {
                if (File.Exists(fiInput[i].FullName) && fiInput[i].FullName.Contains(_SourceFolderConfig.FullName))
                {
                    string n;
                    int s, e;
                    if (!ToolKit_MediaSeperator.DetermineSeriesInfo(fiInput[i].Name, out n, out s, out e))
                    {
                        fiInput.RemoveAt(i);
                        continue;
                    }

                    if (!ToolKit_MediaSeperator.DetermineTitleAlphaRange(fiInput[i].Name, ToolKit_MediaSeperator.NameRange[0], ToolKit_MediaSeperator.NameRange[1]))
                    {
                        fiInput.RemoveAt(i);
                        continue;
                    }
                }
                else
                {
                    fiInput.RemoveAt(i);
                    continue;
                }
            }
            #endregion

            GetTotalFileLength(fiInput);

            foreach (FileInfo fi in fiInput)
            {
                if (fi.Exists)
                {
                    foreach (KeyValuePair<DirectoryInfo, List<string>> kvpdi in ddiSubSearches)
                    {
                        string sRegexPattern = @"\b(";
                        foreach (string s in kvpdi.Value)
                            sRegexPattern += s + "|";
                        sRegexPattern = sRegexPattern.TrimEnd('|') + @")\b";

                        // check for match
                        if (Regex.Matches(fi.Name, sRegexPattern, RegexOptions.IgnoreCase).Count >= (int)Math.Ceiling(kvpdi.Value.Count / 2.0))
                        {
                            string sName,
                                   sResult = "File Skipped";
                            int iSeason,
                                iEpisode;

                            // Media content found to have series / episode format (season folder sub-classification)
                            if (DetermineSeriesInfo(fi.Name, out sName, out iSeason, out iEpisode))
                            {

                                if (!ToolKit_MediaSeperator.DetermineTitleAlphaRange(fi.Name, ToolKit_MediaSeperator.NameRange[0], ToolKit_MediaSeperator.NameRange[1]))
                                    continue;

                                DirectoryInfo diTemp = CreateSeasonDirectory(kvpdi.Key, iSeason);
                                sResult = CopyMoveFiles(fi, diTemp, bKeepOrigional);
                            }
                        }
                    }
                }
                else
                {
                    TransferFinsishedCall();
                    return "Error transfering file, " + fi.Name + " does not exist in source folder " + _SourceFolderConfig.Name;
                }
            }

            TransferFinsishedCall();
            return "Transfer Successful";
        }

        /// <summary>
        /// Searches folder for subdirectories and determines all words that are different between subfolders
        /// returns a list of both the origional subdirecotry info and the independendent search words that apply
        /// word search is organized by search word counts in ascending order
        /// </summary>
        /// <param name="ldiSubDirectory">List of subfolders to get search words from</param>
        /// <returns>keyvalue pair of search words and directory associated</returns>
        public static List<KeyValuePair<DirectoryInfo, List<string>>> GetSearchWordParameters(List<DirectoryInfo> ldiSubDirectory)
        {
            List<KeyValuePair<DirectoryInfo, List<string>>> ddiSubSearches = new List<KeyValuePair<DirectoryInfo, List<string>>>();
            List<string> lsUsedSearchWords = new List<string>();
            List<string> lsErrorConflicts = new List<string>();

            #region Get words to search for 
            foreach (DirectoryInfo di in ldiSubDirectory)
            {
                List<string> lsSearchWords = di.Name.TrimEnd(' ').Split(' ').ToList();
                lsSearchWords.RemoveAll(ConnectorWordRemoval);

                ddiSubSearches.Add(new KeyValuePair<DirectoryInfo, List<string>>(di, lsSearchWords));
            }
            #endregion

            ddiSubSearches.Sort(SearchWordCountAsc);       // sort search words into ascending count order

            #region check for search word autonomy, force removal if duplicate found, error message if unresolvable
            foreach (KeyValuePair<DirectoryInfo, List<string>> kvp in ddiSubSearches)
            {
                List<string> lsTemp = kvp.Value;
                for (int i = 0; i < lsTemp.Count; i++)
                {
                    if (lsUsedSearchWords.Contains(lsTemp[i]))
                    {
                        if (lsTemp.Count > 1)
                            lsTemp.Remove(lsTemp[i]);
                    }
                    else
                        lsUsedSearchWords.Add(lsTemp[i]);
                }
            }
            #endregion

            ddiSubSearches.Sort(SearchWordCountAsc);       // sort search words into ascending count order

            return ddiSubSearches;
        }

        private static int SearchWordCountAsc(KeyValuePair<DirectoryInfo, List<string>> a, KeyValuePair<DirectoryInfo, List<string>> b)
        {
            if (a.Value.Count > b.Value.Count)
                return 1;
            if (a.Value.Count < b.Value.Count)
                return -1;
            else
                return a.Value[0].CompareTo(b.Value[0]);
        }

        private static bool ConnectorWordRemoval(string s)
        {
            List<string> lsEliminate = new List<string>(new string[] { "of", "the", "and", "with", "life", "adventures",
                                                                       "i", "love", "my", "new", "to", "on", "world", 
                                                                       "family", "night", "man", "real", "series", "hour",
                                                                       "big", "me", "you", "star", "kids", "show",
                                                                       "time", "all", "for", "comedy", "day", "home", "is",
                                                                       "a", "if", "then", "from", "&", "its", "it", "in", "or" });

            if (lsEliminate.Contains(s.ToLower()))
                return true;
            return false;
        }

        /// <summary>
        /// moves or copies file from source to destination folder
        /// updates TransferProgress (0-100) to track progress
        /// </summary>
        /// <param name="fi">file input</param>
        /// <param name="di">file destination directory</param>
        /// <returns>error message</returns>
        public static string CopyMoveFiles(FileInfo fi, DirectoryInfo di, bool bKeepOrigional)
        {
            bool bSourceNet = false,
                 bDestNet = false;

            if (!fi.Exists)
                return "Source file has not been determined";

            if (!di.Exists)
                return "Destination folder has not been determined";

            try
            {
                FilesCurrentLength += fi.Length;
                _TransferProgress = (int)(((double)FilesCurrentLength / (double)FilesTotalLength) * 100.0);
                _TransferCurrent = fi.Name + "," + di.FullName;

                if (fi.FullName.Contains(@"\\"))
                    bSourceNet = true;

                if (di.FullName.Contains(@"\\"))
                    bDestNet = true;

                if (bSourceNet && bDestNet)
                {
                    ImpersonatedFileCopy source = new ImpersonatedFileCopy(SourcePermissions.Domain, SourcePermissions.User, SourcePermissions.Password);
                    ImpersonatedFileCopy dest = new ImpersonatedFileCopy(DestinationPermissions.Domain, DestinationPermissions.User, DestinationPermissions.Password);

                    //temporary holdover position
                    source.CopyMoveFile(fi.FullName, Path.Combine(Directory.GetCurrentDirectory(), fi.Name), bKeepOrigional);
                    dest.CopyMoveFile(Path.Combine(Directory.GetCurrentDirectory(), fi.Name), Path.Combine(di.FullName, fi.Name), false); 

                    source.Dispose();
                    dest.Dispose();
                }

                else if (bSourceNet)
                {
                    using (ImpersonatedFileCopy copy = new ImpersonatedFileCopy(SourcePermissions.Domain, SourcePermissions.User, SourcePermissions.Password))
                        copy.CopyMoveFile(fi.FullName, Path.Combine(di.FullName, fi.Name), bKeepOrigional);
                }

                else if (bDestNet)
                {
                using (ImpersonatedFileCopy copy = new ImpersonatedFileCopy(DestinationPermissions.Domain, DestinationPermissions.User, DestinationPermissions.Password))
                        copy.CopyMoveFile(fi.FullName, Path.Combine(di.FullName, fi.Name), bKeepOrigional);
                }

                else
                {
                    File.Copy(fi.FullName, Path.Combine(di.FullName, fi.Name));
                    if (!bKeepOrigional && File.Exists(Path.Combine(di.FullName, fi.Name)))                     
                        File.Delete(fi.FullName);
                }
            }

            catch (FileNotFoundException)
            { return "Error transfering file, file not found"; }
            catch (AccessViolationException)
            { return "Error transfering file, file could not be accessed"; }
            catch (UnauthorizedAccessException)
            { return "Error transfering file, insufficent access"; }
            catch (IOException ex)
            { return "Error transfering file, " + ex.Message; }

            finally
            { 
                _TransferProgress = 0;
                _TransferCurrent = "Awaiting Command";
                
            }

            return "Success";
        }

        private static void TransferStartedCall()
        {
            TransferStarted(new object(), EventArgs.Empty);
        }

        private static void TransferFinsishedCall()
        {
            TransferFinished(new object(), EventArgs.Empty);
        }

        /// <summary>
        /// Checks to see if a season folder exists for a show,
        /// creates said directory if not created yet.
        /// Returns the combined subdirectory path information
        /// </summary>
        /// <param name="di">parent path</param>
        /// <param name="iSeason">season number</param>
        /// <returns>full sub path</returns>
        public static DirectoryInfo CreateSeasonDirectory(DirectoryInfo di, int iSeason)
        {
            if (di.Exists)
            {
                DirectoryInfo[] dia = di.GetDirectories();
                foreach (DirectoryInfo d in dia)
                    if (d.Name.Contains("Season " + iSeason.ToString()))
                        return new DirectoryInfo(Path.Combine(di.FullName, d.Name));

                try
                {
                    return di.CreateSubdirectory("Season " + iSeason.ToString());
                }
                catch (AccessViolationException)
                { return null; }
            }
            return null;
        }

        #endregion

        #region Search Pattern Functions

        /// <summary>
        /// Used to check a file name title and determine if it is Series oriented
        /// returns true if series oriented
        ///     outs converted title, season #, episode #
        /// returns false if not series oriented
        ///     outs converted title, -1, -1
        /// </summary>
        /// <param name="sUnconverted">input file name</param>
        /// <param name="sConvert">title extraction</param>
        /// <param name="iSeason">season number</param>
        /// <param name="iEpisode">episode number</param>
        /// <returns></returns>
        public static bool DetermineSeriesInfo(string sUnconverted, out string sConvert, out int iSeason, out int iEpisode)
        {
            List<string> lsSearchFilters = new List<string>(new string[] { 
                                                                           "season [0-9][0-9] episode [0-9][0-9]", 
                                                                           "season [0-9] episode [0-9][0-9]", 
                                                                           "season [0-9][0-9] episode [0-9]", 
                                                                           "season [0-9] episode [0-9]", 
                                                                           "s[0-9][0-9]e[0-9][0-9]",
                                                                           "s[0-9]e[0-9][0-9]",
                                                                           "[0-9][0-9]x[0-9][0-9]", 
                                                                           "[0-9]x[0-9][0-9]",
                                                                           "[0-9][0-9].[0-9][0-9]", 
                                                                           "[0-9].[0-9][0-9]",
                                                                           "[0-9][0-9]-[0-9][0-9]", 
                                                                           "[0-9]-[0-9][0-9]",
                                                                           "[0-9][0-9][0-9][0-9]", 
                                                                           "[0-9][0-9][0-9]" 
                                                                         } 
                                                           );
            sConvert = sUnconverted.ToLower();
            iSeason = iEpisode = -1;

            foreach (string sSearchFilter in lsSearchFilters)
            {
                Match mStandard = Regex.Match(sConvert, sSearchFilter, RegexOptions.IgnoreCase);
                if (mStandard.Success)
                {
                    #region Get_Series_Title
                    sConvert = Regex.Replace(sConvert, sSearchFilter + ".+$", string.Empty);
                    sConvert = Regex.Replace(sConvert, "[._-]", " ");
                    sConvert = Regex.Replace(sConvert, @"[^A-Za-z0-9\s]", string.Empty);
                    sConvert = sConvert.TrimEnd();
                    #endregion

                    #region Split_Season_Episode
                    string sNumTemp = "";

                    foreach (char c in mStandard.Value)
                        if (Char.IsDigit(c) && sNumTemp.Length <= 4)
                            sNumTemp += c;

                    if (sNumTemp.Length == 4)
                    {
                        iSeason = int.Parse(sNumTemp.Substring(0, 2));
                        iEpisode = int.Parse(sNumTemp.Substring(2, 2));
                    }
                    else if (sNumTemp.Length == 3)
                    {
                        iSeason = int.Parse(sNumTemp.Substring(0, 1));
                        iEpisode = int.Parse(sNumTemp.Substring(1, 2));
                    }
                    #endregion

                    return true;
                }
            }
            return false;   
        }

        /// <summary>
        /// Used to determine if file name start char between range of characters
        /// returns true if in range (inclusive)
        /// </summary>
        /// <param name="sTitle">Title to check</param>
        /// <param name="cStartChar">start range</param>
        /// <param name="cEndChar">end range</param>
        /// <returns>in range bool</returns>
        public static bool DetermineTitleAlphaRange(string sTitle, char cStartChar, char cEndChar)
        {
            if (sTitle.Length > 0)
            {
                string sPattern;
                if (Char.IsDigit(cStartChar))
                    sPattern = string.Format("[{0}-9A-{1}]", cStartChar, cEndChar);
                else
                    sPattern = string.Format("[{0}-{1}]", cStartChar, cEndChar);

                Match mat = Regex.Match(sTitle[0].ToString(), sPattern, RegexOptions.IgnoreCase);
                if (mat.Success)
                    return true;
                else 
                    return false;
            }
            return false;
        }
        #endregion
    }

    /// <summary>
    /// used for encryption and decryption
    /// </summary>
    public static class DataProtector
    {
        private const string eEntropyValue = "1569";

        /// <summary>
        /// Encrypts a string using the DPAPI.
        /// </summary>
        /// <param name="stringToEncrypt">The string to encrypt.</param>
        /// <returns>The encrypted data.</returns>
        public static string EncryptData(string stringToEncrypt)
        {
            if (stringToEncrypt.Length > 0)
            {
                byte[] encryptedData = ProtectedData.Protect(Encoding.Unicode.GetBytes(stringToEncrypt), Encoding.Unicode.GetBytes(eEntropyValue), DataProtectionScope.LocalMachine);
                return Convert.ToBase64String(encryptedData);
            }
            else return "";
        }

        /// <summary>
        /// Decrypts a string using the DPAPI.
        /// </summary>
        /// <param name="stringToDecrypt">The string to decrypt.</param>
        /// <returns>The decrypted data.</returns>
        public static string DecryptData(string stringToDecrypt)
        {
            if (stringToDecrypt.Length > 0)
            {
                byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(stringToDecrypt), Encoding.Unicode.GetBytes(eEntropyValue), DataProtectionScope.LocalMachine);
                return Encoding.Unicode.GetString(decryptedData);
            }
            else
                return "";
        }
    }

    /// <summary>
    /// used to get network share access for transfers
    /// </summary>
    public class ImpersonatedFileCopy : IDisposable
    {
        #region Assembly Functions
        [DllImport("advapi32.dll")]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);
        #endregion

        #region Private Variables
        private IntPtr _TokenHandle = new IntPtr(0);
        private WindowsImpersonationContext _WindowsImpersonationContext;
        #endregion

        #region Constructors
        public ImpersonatedFileCopy(string domain, string username, string password)
        {
            Impersonate(domain, username, password);
        }
        #endregion

        #region Methods
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void Impersonate(string domain, string username, string password)
        {
            bool returnValue;

            try
            {
                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_INTERACTIVE = 2;

                _TokenHandle = IntPtr.Zero;

                //Call LogonUser to obtain a handle to an access token.
                returnValue = LogonUser(username, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref _TokenHandle);
                if (returnValue)
                {
                    WindowsIdentity newId = new WindowsIdentity(_TokenHandle);
                    _WindowsImpersonationContext = newId.Impersonate();
                }
            }
            catch (Exception ex)
            {
                UndoImpersonate();
                Console.WriteLine("Error" + ex.Message);
            }
        }

        private void UndoImpersonate()
        {
            if (_WindowsImpersonationContext != null)
            {
                _WindowsImpersonationContext.Undo();
                if (!_TokenHandle.Equals(IntPtr.Zero))
                {
                    CloseHandle(_TokenHandle);
                }
            }
        }

        public bool CopyMoveFile(string sSourceFileName, string destRemoteFilename, bool bKeepOrigional)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(destRemoteFilename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(destRemoteFilename));

                File.Copy(sSourceFileName, destRemoteFilename, true);
                if (!bKeepOrigional && File.Exists(destRemoteFilename))
                    File.Delete(sSourceFileName);

                return true;
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); return false; }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            UndoImpersonate();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
