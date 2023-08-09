using System.IO;

namespace CodingExercise
{
    /// <summary>
    /// Provides implementation for the interface to manage and process letters.
    /// </summary>
    public class LetterService : ILetterService
    {
        /// <summary>
        /// Combines the content of two text files and writes the result to another file.
        /// </summary>
        /// <param name="inputFile1">The path of the first input text file.</param>
        /// <param name="inputFile2">The path of the second input text file.</param>
        /// <param name="resultFile">The path of the output text file.</param>
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            string content1 = File.ReadAllText(inputFile1);
            string content2 = File.ReadAllText(inputFile2);
            
            string combinedContent = content1 + "\n\n" + content2;
            
            File.WriteAllText(resultFile, combinedContent);
        }
        
        
        /// <summary>
        /// Archives the letter files from the input folder to the archive folder.
        /// </summary>
        /// <param name="rootFolder">The root folder containing input and archive folders.</param>
        /// <param name="archiveFolder">The folder where archived letter files will be stored.</param>
        public void ArchiveFiles(string rootFolder, string archiveFolder)
        {
            ArchiveLetterType(rootFolder, archiveFolder, "Admission");
            ArchiveLetterType(rootFolder, archiveFolder, "Scholarship");
        }
        
        /// <summary>
        /// Archives the letter files of a specific type from the input folder to the archive folder.
        /// </summary>
        /// <param name="rootFolder">The root folder containing input and archive folders.</param>
        /// <param name="archiveFolder">The folder where archived letter files will be stored.</param>
        /// <param name="letterType">The type of letter to be archived (Example: "Admission" or "Scholarship").</param>
        private void ArchiveLetterType(string rootFolder, string archiveFolder, string letterType)
        {
            string inputFolder = Path.Combine(rootFolder, letterType);
            string[] datedFolders = Directory.GetDirectories(inputFolder);

            foreach (string datedFolder in datedFolders)
            {
                string datedFolderName = Path.GetFileName(datedFolder);
                string archiveDatedFolder = Path.Combine(archiveFolder, datedFolderName, letterType);

                Directory.CreateDirectory(archiveDatedFolder);

                string searchPattern = $"{letterType.ToLower()}-*.txt";
                string[] letterFiles = Directory.GetFiles(datedFolder, searchPattern);

                foreach (string letterFile in letterFiles)
                {
                    string fileName = Path.GetFileName(letterFile);
                    string destinationPath = Path.Combine(archiveDatedFolder, fileName);
                    File.Copy(letterFile, destinationPath, true); // true to overwrite if file already exists
                }
            }
        }

    }
}
