﻿////////////////////////////////////////////////////////////////////////////////
//  
// @module Quick Save for Unity3D 
// @author Michael Clayton
// @support clayton.inds+support@gmail.com 
//
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using CI.QuickSave.Core;

namespace CI.QuickSave
{
    public class QuickSaveRaw
    {
        /// <summary>
        /// Saves a string directly to the specified file, overwriting if it already exists
        /// </summary>
        /// <param name="filename">The file to save to</param>
        /// <param name="content">The string to save</param>
        public static void SaveString(string filename, string content)
        {
            SaveString(filename, content, new QuickSaveSettings());
        }

        /// <summary>
        /// Saves a string directly to the specified file using the specified settings, overwriting if it already exists
        /// </summary>
        /// <param name="filename">The file to save to</param>
        /// <param name="content">The string to save</param>
        /// <param name="settings">Settings</param>
        public static void SaveString(string filename, string content, QuickSaveSettings settings)
        {
            string encryptedText;

            try
            {
                encryptedText = Cryptography.Encrypt(content, settings.SecurityMode, settings.Password);
            }
            catch (Exception e)
            {
                throw new QuickSaveException("Encryption failed", e);
            }

            if (!FileAccess.SaveString(filename, true, encryptedText))
            {
                throw new QuickSaveException("Failed to write to file");
            }
        }

        /// <summary>
        /// Saves a byte array directly to the specified file, overwriting if it already exists
        /// </summary>
        /// <param name="filename">The file to save to</param>
        /// <param name="content">The byte array to save</param>
        public static void SaveBytes(string filename, byte[] content)
        {
            if (FileAccess.SaveBytes(filename, true, content))
            {
                throw new QuickSaveException("Failed to write to file");
            }
        }

        /// <summary>
        /// Loads the contents of the specified file into a string
        /// </summary>
        /// <param name="filename">The file to load from</param>
        /// <returns>The contents of the file as a string</returns>
        public static string LoadString(string filename)
        {
            return LoadString(filename, new QuickSaveSettings());
        }

        /// <summary>
        /// Loads the contents of the specified file into a string using the specified settings
        /// </summary>
        /// <param name="filename">The file to load from</param>
        /// <param name="settings">Settings</param>
        /// <returns>The contents of the file as a string</returns>
        public static string LoadString(string filename, QuickSaveSettings settings)
        {
            string content = FileAccess.LoadString(filename, true);

            if (content == null)
            {
                throw new QuickSaveException("Failed to load file");
            }

            string decryptedText;

            try
            {
                decryptedText = Cryptography.Decrypt(content, settings.SecurityMode, settings.Password);
            }
            catch (Exception e)
            {
                throw new QuickSaveException("Decryption failed", e);
            }

            return decryptedText;
        }

        /// <summary>
        /// Loads the contents of the specified file into a byte array
        /// </summary>
        /// <param name="filename">The file to load from</param>
        /// <returns>The contents of the file as a byte array</returns>
        public static byte[] LoadBytes(string filename)
        {
            byte[] content = FileAccess.LoadBytes(filename, true);

            if (content == null)
            {
                throw new QuickSaveException("Failed to load file");
            }

            return content;
        }

        /// <summary>
        /// Deletes the specified file if it exists
        /// </summary>
        /// <param name="filename">The file to delete</param>
        public static void Delete(string filename)
        {
            FileAccess.Delete(filename, true);
        }

        /// <summary>
        /// Determines if the specified file exists
        /// </summary>
        /// <param name="filename">The file to check</param>
        /// <returns>Does the file exist</returns>
        public static bool Exists(string filename)
        {
            return FileAccess.Exists(filename, true);
        }

        /// <summary>
        /// Gets the names of all files that have been saved
        /// </summary>
        /// <returns>A collection of file names</returns>
        public static IEnumerable<string> GetAllFiles()
        {
            return FileAccess.Files(false);
        }
    }
}