﻿using System.IO;
using System.Windows.Forms;

namespace FileSorter
{
    class SortFiles
    {
        /// <summary>
        ///  Main file sorting class.
        /// </summary>
        /// <remarks>
        /// Creates FolderBrowsereDialog object, sets its description so that user knows what to do
        /// and how the program works. Blocks user from creating any new folder.
        /// After user selects the path, it gets files from selected directory
        /// and performs methods of FileManager and DateService on each file.
        /// </remarks>
        public void Sort ()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                Description = "Choose a path where you want your files to be sorted. The program will sort your files based on their last access time."
            };
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                DateService dSInstance = DateService.GetInstance();
                FileManager fMInstance = FileManager.GetInstance();
                string directoryPath = fbd.SelectedPath;
                var filesInDirectory = Directory.EnumerateFiles(fbd.SelectedPath);
                
                foreach (string file in filesInDirectory)
                {
                    if (dSInstance.DAGreaterOrEqual(-7, file))
                    {
                        fMInstance.ManageFiles("Commonly Used", directoryPath, file);
                    }
                    else if (dSInstance.DaysAgo(-7, file) && dSInstance.MonthsAgo(-1, file))
                    {
                        fMInstance.ManageFiles("Rarely Used", directoryPath, file);
                    }
                    else if (dSInstance.MonthsAgo(-1, file) && dSInstance.MAGreaterOrEqual(-2, file))
                    {
                        fMInstance.ManageFiles("Barely Used", directoryPath, file);
                    }
                    else if (dSInstance.MonthsAgo(-2, file))
                    {
                        fMInstance.ManageFiles("Almost Never Used", directoryPath, file);
                    }
                }
            }
        }     
    }
}