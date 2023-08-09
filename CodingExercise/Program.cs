using System;
using System.Collections.Generic;
using System.IO;

namespace CodingExercise
{
    class Program: LetterService
    {
        public static void Main(string[] args)
        {
            string rootFolder = @"/Users/suman/Desktop/Coding Challenge/CombinedLetters";
            string inputRootFolder = Path.Combine(rootFolder, "Input");
            string archiveRootFolder = Path.Combine(rootFolder, "Archive");
            
            
            ILetterService letterService = new LetterService();
            
            //Archive all the files from Input 
            letterService.ArchiveFiles(inputRootFolder, archiveRootFolder);
            MergeAndStoreLetters(rootFolder, letterService);
        }
        
        private static void MergeAndStoreLetters(string rootFolder, ILetterService letterService)
        {
            string inputAdmissionFolder = Path.Combine(rootFolder, "Input", "Admission");
            string inputScholarshipFolder = Path.Combine(rootFolder, "Input", "Scholarship");
            string outputFolder = Path.Combine(rootFolder, "Output");
            
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
                }
            }
        }
    }
}

