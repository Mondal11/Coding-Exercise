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
        }
        
    }
}

