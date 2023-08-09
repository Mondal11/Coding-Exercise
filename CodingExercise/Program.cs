using System;
using System.Collections.Generic;
using System.IO;

namespace CodingExercise
{
    class Program: LetterService
    {
        public static void Main(string[] args)
        {
            string inputRootFolder = @"/Users/suman/Desktop/Coding Challenge/CombinedLetters/Input";
            string archiveRootFolder = @"/Users/suman/Desktop/Coding Challenge/CombinedLetters/Archive";
            string rootFolder = @"/Users/suman/Desktop/Coding Challenge/CombinedLetters";
            
            ILetterService letterService = new LetterService();
            
            letterService.ArchiveFiles(inputRootFolder, archiveRootFolder);
            MergeAndStoreLetters(rootFolder, letterService);
        }
        
        private static void MergeAndStoreLetters(string rootFolder, ILetterService letterService)
        {
            string inputAdmissionFolder = Path.Combine(rootFolder, "Input", "Admission");
            string inputScholarshipFolder = Path.Combine(rootFolder, "Input", "Scholarship");
            string outputFolder = Path.Combine(rootFolder, "Output");

            int totalFilesCombined = 0;
            List<string> combinedStudentIds = new List<string>();

            string[] admissionDatedFolders = Directory.GetDirectories(inputAdmissionFolder);

            foreach (string admissionDatedFolder in admissionDatedFolders)
            {
                string datedFolderName = Path.GetFileName(admissionDatedFolder);
                string matchingScholarshipFolder = Path.Combine(inputScholarshipFolder, datedFolderName);

                if (!Directory.Exists(matchingScholarshipFolder))
                {
                    continue; // No matching scholarship folder found for this date
                }

                string[] admissionFiles = Directory.GetFiles(admissionDatedFolder, "admission-*.txt");

                foreach (string admissionFile in admissionFiles)
                {
                    string admissionFileName = Path.GetFileNameWithoutExtension(admissionFile);
                    string studentId = admissionFileName.Substring("admission-".Length);

                    string matchingScholarshipFile = Path.Combine(matchingScholarshipFolder, $"scholarship-{studentId}.txt");

                    if (!File.Exists(matchingScholarshipFile))
                    {
                        continue; // No matching scholarship file found for this student ID
                    }

                    string outputFile = Path.Combine(outputFolder, $"{studentId}.txt");
                    letterService.CombineTwoLetters(admissionFile, matchingScholarshipFile, outputFile);

                    combinedStudentIds.Add(studentId);
                    totalFilesCombined++;
                }
            }

            // Clear the contents of the Admission and Scholarship folders in Input
            ClearFolder(inputAdmissionFolder);
            ClearFolder(inputScholarshipFolder);

            // Save combined student IDs to a text file with the current date as the name
            string combinedIdsFilePath = Path.Combine(rootFolder, $"{DateTime.Now:MM-dd-yyyy}") + " Report.txt";

            
            File.WriteAllText(combinedIdsFilePath, $"{DateTime.Now:MM/dd/yyyy}" + " Report\n");
            File.AppendAllText(combinedIdsFilePath, $"-----------------------------\n\n");
            File.AppendAllText(combinedIdsFilePath, $"Number of Combined Letters: {totalFilesCombined}\n\n");
            File.AppendAllLines(combinedIdsFilePath, combinedStudentIds);
            
        }
        
        
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


