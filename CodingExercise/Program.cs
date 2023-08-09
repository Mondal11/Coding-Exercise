using System;
using System.Collections.Generic;
using System.IO;

namespace CodingExercise
{
    /// <summary>
    /// Contains the entry point and logic for the console application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the console application.
        /// </summary>
        /// <param name="args">Command-line arguments (not used in our case).</param>
        public static void Main(string[] args)
        {
            string inputRootFolder = @"/Users/suman/Desktop/Coding Challenge/CombinedLetters/Input";
            string archiveRootFolder = @"/Users/suman/Desktop/Coding Challenge/CombinedLetters/Archive";
            string rootFolder = @"/Users/suman/Desktop/Coding Challenge/CombinedLetters";
            
            ILetterService letterService = new LetterService();
            
            letterService.ArchiveFiles(inputRootFolder, archiveRootFolder);
            MergeAndStoreLetters(rootFolder, letterService);
        }
        
        /// <summary>
        /// Merges admission and scholarship letters for the same student and generates a report.
        /// </summary>
        /// <param name="rootFolder">The root folder containing input, archive, and output folders.</param>
        /// <param name="letterService">An instance of the ILetterService interface.</param>
        private static void MergeAndStoreLetters(string rootFolder, ILetterService letterService)
        {
            string inputAdmissionFolder = Path.Combine(rootFolder, "Input", "Admission");
            string inputScholarshipFolder = Path.Combine(rootFolder, "Input", "Scholarship");
            string outputFolder = Path.Combine(rootFolder, "Output");
            
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            int totalFilesCombined = 0;
            List<string> combinedStudentIds = new List<string>();

            string[] admissionDatedFolders = Directory.GetDirectories(inputAdmissionFolder);

            foreach (string admissionDatedFolder in admissionDatedFolders)
            {
                string datedFolderName = Path.GetFileName(admissionDatedFolder);
                string matchingScholarshipFolder = Path.Combine(inputScholarshipFolder, datedFolderName);

                if (!Directory.Exists(matchingScholarshipFolder))
                {
                    continue;
                }

                string[] admissionFiles = Directory.GetFiles(admissionDatedFolder, "admission-*.txt");

                foreach (string admissionFile in admissionFiles)
                {
                    string admissionFileName = Path.GetFileNameWithoutExtension(admissionFile);
                    string studentId = admissionFileName.Substring("admission-".Length);

                    string matchingScholarshipFile = Path.Combine(matchingScholarshipFolder, $"scholarship-{studentId}.txt");

                    if (!File.Exists(matchingScholarshipFile))
                    {
                        continue;
                    }

                    string outputFile = Path.Combine(outputFolder, $"{studentId}.txt");
                    letterService.CombineTwoLetters(admissionFile, matchingScholarshipFile, outputFile);

                    combinedStudentIds.Add(studentId);
                    totalFilesCombined++;
                }
            }
            
            ClearFolder(inputAdmissionFolder);
            ClearFolder(inputScholarshipFolder);
            
            string currentDate = DateTime.Now.ToString("MM-dd-yyyy");
            string fileNameBase = $"{currentDate} Report.txt";
            string combinedIdsFilePath = Path.Combine(rootFolder, fileNameBase);

            
            int counter = 1;
            while (File.Exists(combinedIdsFilePath))
            {
                string numberedFileName = $"{currentDate} Report ({counter}).txt";
                combinedIdsFilePath = Path.Combine(rootFolder, numberedFileName);
                counter++;
            }
            
            File.WriteAllText(combinedIdsFilePath, $"{DateTime.Now:MM/dd/yyyy}" + " Report\n");
            File.AppendAllText(combinedIdsFilePath, $"-----------------------------\n\n");
            File.AppendAllText(combinedIdsFilePath, $"Number of Combined Letters: {totalFilesCombined}\n\n");
            File.AppendAllLines(combinedIdsFilePath, combinedStudentIds);
        }
        
        /// <summary>
        /// Clears the contents of a specified folder by deleting all files and subdirectories.
        /// </summary>
        /// <param name="folderPath">The path of the folder to be cleared.</param>
        private static void ClearFolder(string folderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo subDirectory in directoryInfo.GetDirectories())
            {
                subDirectory.Delete(true);
            }
        }
    }
}


