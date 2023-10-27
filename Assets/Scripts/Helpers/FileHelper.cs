using System;
using System.IO;
using UnityEngine;

namespace NecatiAkpinar.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// <para>Writes content to a file</para>
        /// </summary>
        /// <param name="filePath">File Name</param>
        /// <param name="fileContents">Content that will be written to the wanted file</param>
        public static bool WriteToFile(string filePath, string fileContents)
        {
            try
            {
                File.WriteAllText(filePath, fileContents);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to write to {filePath} with exception {e}");
                return false;
            }
        }

        public static bool IsThereFileByThatName(string fileName)
        {
            return File.Exists(Path.Combine(Application.persistentDataPath, fileName));
        }

        /// <summary>
        /// <para>Load content from a file</para>
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <param name="result">String that takes the loaded content from the file</param>
        public static bool LoadFromFile(string filePath, out string result)
        {
            if (!File.Exists(filePath))
            {
                Debug.Log("There is no file at the path");
                result = null;
                return false;
            }

            try
            {
                result = File.ReadAllText(filePath);
                //Debug.Log("There is a file and loaded.");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {filePath} with exception {e}");
                result = "";
                return false;
            }
        }

        /// <summary>
        /// <para>Changes path of a file</para>
        /// </summary>
        /// <param name="fileName">Current file name</param>
        /// <param name="newFileName">New file name</param>
        public static bool MoveFile(string fileName, string newFileName)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, fileName);
            var newFullPath = Path.Combine(Application.persistentDataPath, newFileName);

            try
            {
                if (File.Exists(newFullPath))
                {
                    File.Delete(newFullPath);
                }

                if (!File.Exists(fullPath))
                {
                    return false;
                }

                File.Move(fullPath, newFullPath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to move file from {fullPath} to {newFullPath} with exception {e}");
                return false;
            }

            return true;
        }
    }
}