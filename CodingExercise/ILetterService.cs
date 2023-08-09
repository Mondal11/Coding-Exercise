namespace CodingExercise
{
    /// <summary>
    /// Represents a service for managing and processing letters.
    /// </summary>
    public interface ILetterService
    {
        /// <summary>
        /// Combines the content of two text files and writes the result to another file.
        /// </summary>
        /// <param name="inputFile1">The path of the first input text file.</param>
        /// <param name="inputFile2">The path of the second input text file.</param>
        /// <param name="resultFile">The path of the output text file.</param>
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
        
        
        /// <summary>
        /// Archives the letter files from the input folder to the archive folder.
        /// </summary>
        /// <param name="rootFolder">The root folder containing input and archive folders.</param>
        /// <param name="archiveFolder">The folder where archived letter files will be stored.</param>
        void ArchiveFiles(string rootFolder, string archiveFolder);
    }
}