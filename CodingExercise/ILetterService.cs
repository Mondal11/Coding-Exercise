namespace CodingExercise
{
    public interface ILetterService
    {
        void CombineTwoLetters(string inputFile1, string inputFile2, string resultFile);
        
        void ArchiveFiles(string sourceFolder, string destinationFolder);
    }
}