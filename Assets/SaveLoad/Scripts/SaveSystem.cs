/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/Achievements/";
    private const string SAVE_EXTENSION = "txt";

    public static void Init()
    {
        // Test if Save Folder exists
        if (!Directory.Exists(SAVE_FOLDER))
        {
            // Create Save Folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string fileName, string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + fileName + "." + SAVE_EXTENSION, saveString);
    }

    public static string Load(string fileName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        // Get all save files
        FileInfo[] saveFiles = directoryInfo.GetFiles("*." + SAVE_EXTENSION);
        // Cycle through all save files and identify the most recent one
        FileInfo needFile = null;
        foreach (FileInfo fileInfo in saveFiles)
        {
            Debug.Log("Get file: " + fileInfo.FullName);
            if (needFile == null)
            {
                needFile = fileInfo;

            }
            else
            {
                if (fileInfo.LastWriteTime > needFile.LastWriteTime)
                {
                    needFile = fileInfo;
                }
            }
        }

        // If theres a save file, load it, if not return null
        if (needFile != null)
        {
            string saveString = File.ReadAllText(needFile.FullName);
            return saveString;
        }
        else
        {
            return null;
        }
    }

}
