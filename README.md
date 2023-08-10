# Testing my Application

Steps to follow for testing the Application.

1.	Copy the CombinedLetters folder from the Coding Exercise directory to your System.
2.	The folder layout is as mentioned in the problem Statement i.e, 


CombinedLetters<br>
------------ Input <br>
----------------- Admission <br>
--------------------------- 20220125 <br>
------------------------------------- Admission-98765432.txt <br>
------------------------------------- Admission-09876543.txt<br>
-----------------  Scholarship<br>
--------------------------- 20220125<br>
------------------------------------- Scholarship-98765432.txt<br>
-------------------------------------	Scholarship-09876543.txt<br>
------------ Archive<br>
------------ Output<br>


3.	Inside the Program.cs file, the main method drives the console app's operations. Before use, make sure to update the file paths in the code to match your system's folder structure. 
4.	Adjust paths for the Input, Output, and Archive folders as needed. This ensures smooth interaction with your file system, enabling the app to perform its tasks effectively.
5.	After successfully replacing the correct paths run the main method.

# Expected Result:

1.	You can see a new file being created named “Processing Date Report.txt”.
2.	You can also open the Report file and see the total number of letters combined along with the respective Student IDs.
3.	Take a look at the Input folder to verify that all files have been successfully archived in the Archive folder. 
4.	The archiving is organized by dates, making it easier to access files when needed in the future. This organized structure ensures better file management and accessibility.

