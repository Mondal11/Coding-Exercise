using System.IO;

namespace CodingExercise
{
    public class LetterService : ILetterService
    {
        public void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile)
        {
            // Read content from both input files
            string content1 = File.ReadAllText(inputFile1);
            string content2 = File.ReadAllText(inputFile2);
        
            // Combine content
            string combinedContent = content1 + "\n\n" + content2;
        
            // Write combined content to the result file
            File.WriteAllText(resultFile, combinedContent);
        }
        
        public void ArchiveFiles(string rootFolder, string archiveFolder)
        {
            ArchiveLetterType(rootFolder, archiveFolder, "Admission");
            ArchiveLetterType(rootFolder, archiveFolder, "Scholarship");
        }
        
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
