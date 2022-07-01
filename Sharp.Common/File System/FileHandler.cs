using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sharp.Common.Logging;

namespace Sharp.Common.Files
{
    public static class FileHandler
    {
        /// <summary>
        /// Ensures a File exists by creating one if it does not already exist
        /// </summary>
        /// <param name="Path"> The File to ensure exists </param>
        /// <returns> Was the operation a success </returns>
        public static bool EnsureFileExists(string Path)
        {
            if (File.Exists(Path)) return true;
            try
            {
                File.Create(Path);
                return true;
            }
            catch (Exception e)
            {
                Logger.Log($"Failed to create File {Path}, encountered exception {e.Message}. {e.StackTrace}", Logger.ErrorLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// Ensures a Directory exists by creating one if it does not already exist
        /// </summary>
        /// <param name="Path"> The Directory to ensure exists </param>
        /// <returns> Was the operation a success </returns>
        public static bool EnsureDirectoryExists(string Path)
        {
            if (Directory.Exists(Path)) return true;
            try
            {
                Directory.CreateDirectory(Path);
                return true;
            }
            catch (Exception e)
            {
                Logger.Log($"Failed to create Directory {Path}, encountered exception {e.Message}. {e.StackTrace}", Logger.ErrorLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// Reads a file to an array
        /// </summary>
        /// <param name="Data"> The array to store the read file in </param>
        /// <param name="Path"> The full path to the file including filename and ext </param>
        /// <param name="ExpectedLength"> How long you expect the file to be for verification purposes
        /// set to -1 to disable </param>
        /// <returns> Was the operation a success </returns>
        public static bool ReadFromFile(ref string[] Data, string Path, int ExpectedLength)
        {
            if (!Directory.Exists(Path)) return false;
            try
            {
                Data = File.ReadAllLines(Path);
            }
            catch (Exception _ex)
            {
                Logger.Log($"Problem while reading {Path}, problem: {_ex.Message}", Logger.ErrorLevel.Error);
                return false;
            }

            if (Data.Count() < ExpectedLength)
            {
                Logger.Log($"Unspecified error reading {Path}.", Logger.ErrorLevel.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Writes an array to a file
        /// </summary>
        /// <param name="Data"> The array to write to the file </param>
        /// <param name="Path"> The path to the file, including it's filename and ext </param>
        /// <returns> Was the operation a success </returns>
        public static bool WriteFile(string[] Data, string Path)
        {
            try { File.Delete(Path); }
            catch (Exception _ex)
            {
                Logger.Log($"Problem while deleting old {Path} file problem: {_ex.Message}",
                  Logger.ErrorLevel.Error);
            }

            using (FileStream _stream = new FileStream(Path, FileMode.OpenOrCreate))
            using (StreamWriter _writer = new StreamWriter(_stream, Encoding.UTF8))
                for (int i = 0; i < Data.Count(); i++)
                    try
                    {
                        _writer.WriteLine(Data[i]);
                    }
                    catch (Exception _exc)
                    {
                        Logger.Log($"Problem while writing to {Path} file. Problem: {_exc.Message}", Logger.ErrorLevel.Error);
                        return false;
                    }

            return true;
        }

        /// <summary>
        /// Writes a message to a file on the hard drive
        /// </summary>
        /// <param name="Path"> The Directory the file should be created/written to in. </param>
        /// <param name="FileName"> The name of the file to be created/written to. </param>
        /// <param name="Data"> The data that should be written to the file. </param>
        /// <returns> Was the operation successfull? </returns>
        public static bool WriteToFileSingle(string Path, string FileName, string Data)
        {
            try
            {
                Directory.CreateDirectory(Path);
                using (FileStream _stream = new FileStream(Path + FileName, FileMode.OpenOrCreate))
                using (StreamWriter _writer = new StreamWriter(_stream))
                    _writer.WriteLine(Data + "\n");
            }
            catch
            {
                Logger.Log($"Failed to create or write {Data} to file: {FileName} in {Path}!", Logger.ErrorLevel.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds data on to a file on the hard drive, creating one if it does not exist
        /// </summary>
        /// <param name="Path"> The Directory the file should be created/written to in. </param>
        /// <param name="FileName"> The name of the file to be created/written to. </param>
        /// <param name="Data"> The data that should be written to the file. </param>
        /// <returns> Was the operation successfull? </returns>
        public static bool AddToFile(string Path, string FileName, string Data)
        {
            if (!File.Exists(Path + FileName)) return false;
            List<string> _file;
            try
            {
                string[] _unmodifiedFile = File.ReadAllLines(Path + FileName);

                _file = _unmodifiedFile.ToList();
                _file.Add(Data);
            }
            catch
            {
                // Careful! Can lock into recursion provided logger calls this method...
                Logger.Log($"Failed to read file {FileName} at {Path}", Logger.ErrorLevel.Warning);
                return false;
            }
            try
            {
                Directory.CreateDirectory(Path);
                using (FileStream _stream = new FileStream(Path + FileName, FileMode.OpenOrCreate))
                using (StreamWriter _writer = new StreamWriter(_stream))
                    for (int x = 0; x < _file.Count; x++)
                        _writer.WriteLine(_file[x]);
            }
            catch
            {
                Logger.Log($"Failed to create or write {Data} to file: {FileName} in {Path}!", Logger.ErrorLevel.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates file path and makes a call to Copy();
        /// </summary>
        /// <param name="SourceDirectory"> The directory you want to clone </param>
        /// <param name="TargetDirectory"> Where should the cloned files go </param>
        /// <returns> Did the operation suceed? </returns>
        public static bool CopyIntoFolder(string SourceDirectory, string TargetDirectory)
        {
            Directory.CreateDirectory(TargetDirectory); // Validate directory
            return Copy(SourceDirectory, TargetDirectory);
        }

        /// <summary>
        /// Copies all files including sub directories from the source dir to the target dir
        /// </summary>
        /// <param name="SourceDirectory"> Path to the files you want to copy </param>
        /// <param name="TargetDirectory"> Path to where the files should be copied to </param>
        /// <returns> Whether the operation was a success </returns>
        public static bool Copy(string SourceDirectory, string TargetDirectory)
        {
            if (!(Directory.Exists(SourceDirectory) && Directory.Exists(TargetDirectory)))
            {
                Logger.Log($"Failed to copy files, one or more directories does not exist!" +
                    $" Please validate the following directories: Source (What we're trying to copy from)" +
                    $" {SourceDirectory} Target (Where we're trying to copy to) {TargetDirectory}",
                    Logger.ErrorLevel.Error);
                return false;
            }
            DirectoryInfo _diSource = new DirectoryInfo(SourceDirectory);
            DirectoryInfo _diTarget = new DirectoryInfo(TargetDirectory);

            return CopyAll(_diSource, _diTarget);
        }

        /// <summary>
        /// Recursive function to actually copy files in a directory
        /// </summary>
        /// <param name="SourceDirectory"> Path to the files you want copied </param>
        /// <param name="TargetDirectory"> Path to where you want the files copied </param>
        /// <returns> Whether the operation was a success</returns>
        public static bool CopyAll(DirectoryInfo SourceDirectory, DirectoryInfo TargetDirectory)
        {
            if (Directory.Exists(TargetDirectory.FullName))
                Directory.Delete(TargetDirectory.FullName, true);

            Directory.CreateDirectory(TargetDirectory.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo t_file in TargetDirectory.GetFiles())
            {
                try
                {
                    t_file.CopyTo(Path.Combine(TargetDirectory.FullName, t_file.Name), true);
                }
                catch (Exception E)
                {
                    Logger.Log($"Failed to copy file {t_file.Name} in {t_file.FullName} to " +
                        $"{TargetDirectory.FullName} the following error occured: {E.Message}",
                        Logger.ErrorLevel.Error);
                    return false;
                }
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo _dirSourceSubDir in SourceDirectory.GetDirectories())
            {
                DirectoryInfo _nextTargetSubDir = TargetDirectory.CreateSubdirectory(_dirSourceSubDir.Name);
                if (!CopyAll(_dirSourceSubDir, _nextTargetSubDir))
                    return false;
            }

            return true;
        }
    }
}
