using System.Diagnostics;
using Repositories.DTOs;

namespace Services.StudentService;

public static class StudentMarkGraderService
{

	private static StreamWriter? streamWriter;
	private static StreamReader? streamReader;

	private static readonly Process myProcess;
	private static List<GetTestCaseDTO>? testCaseList;

	private static readonly string SOLUTION_PATH = Directory.GetParent(Environment.CurrentDirectory)!.FullName;



	public static List<GetTestCaseDTO>? TestCaseList
	{
		get => testCaseList;
		set
		{
			bool isTestCasesExistInDB = (value != null || value!.Count > 0);

			if (isTestCasesExistInDB)
				testCaseList ??= value;
			else
				testCaseList = new List<GetTestCaseDTO>();
		}
	}




	static StudentMarkGraderService()
	{
		myProcess = new Process();
		myProcess.StartInfo.UseShellExecute = false;
		myProcess.StartInfo.FileName = "java.exe";
		myProcess.StartInfo.RedirectStandardInput = true;
		myProcess.StartInfo.RedirectStandardOutput = true;
		myProcess.StartInfo.CreateNoWindow = false;
	}






	public static CreateStudentSubmissionDetailsDTO GradeStudentMark()
	{
		string questionDescription = "";
		string studentQuestionsFolder = $@"{SOLUTION_PATH}\Storage\StudentExtractedFiles".Trim();
		int studentQuestionTotalMark = 0, currentQuestionNumber = 1, currentTestCaseNumber = 1, tempMark = 0;


		CreateStudentSubmissionDetailsDTO createStudentSubmissionDetailsDTO = new();

		// Check student folder exist
		if (Directory.Exists(studentQuestionsFolder))
		{

			// Grade mark by each test case
			foreach (GetTestCaseDTO testCase in TestCaseList!)
			{
				// Plus 1 unit when is the last test case in order the second block if below can run
				if (TestCaseList.Last().Equals(testCase))
					currentTestCaseNumber++;


				// Change question number after 2 test cases
				if (currentTestCaseNumber >= 3)
				{
					currentTestCaseNumber = 1;

					tempMark += studentQuestionTotalMark;
					questionDescription += $"[QN={currentQuestionNumber}, Mark={studentQuestionTotalMark}]; ";
					studentQuestionTotalMark = 0;

					currentQuestionNumber++;
				}
				currentTestCaseNumber++;


				string studentFolderName = StudentFileSubmitService.SplittedStudentFileName;
				string studentJarPath = $@"{studentQuestionsFolder}\{studentFolderName}\Q{currentQuestionNumber}.jar";


				// Start grading mark if question number submitted
				if (File.Exists(studentJarPath))
				{
					myProcess.StartInfo.Arguments = "-jar " + studentJarPath; // run jar console command
					myProcess.Start();


					streamWriter = myProcess.StandardInput;
					streamReader = myProcess.StandardOutput;


					ProvideTestCaseInputToConsole(testCase);

					string studentOutputResult = GetStudentConsoleOutput();
					studentQuestionTotalMark += GetMarkByTestCaseOutput(testCase, studentOutputResult);
				}
			}
			streamWriter?.Close();
			streamReader?.Close();
			myProcess.Close();
		}
		else
		{
			createStudentSubmissionDetailsDTO.TotalMark = 0;
			createStudentSubmissionDetailsDTO.QuestionDescription = "Student did not submit the question";
		}

		createStudentSubmissionDetailsDTO.GradingTime = DateTime.Now;
		createStudentSubmissionDetailsDTO.TotalMark = (short?)tempMark;
		createStudentSubmissionDetailsDTO.QuestionDescription = questionDescription.Trim();

		return createStudentSubmissionDetailsDTO;
	}






	private static void ProvideTestCaseInputToConsole(GetTestCaseDTO testCase)
	{
		// Input multiple line
		if (testCase.IsInputArray)
			testCase.Inputs!.ForEach(input => streamWriter!.WriteLine(input)); // Insert each line in list to java console

		// Input 1 line
		else
			streamWriter!.WriteLine(string.Join(" ", testCase.Inputs!));
	}





	private static string GetStudentConsoleOutput()
	{
		string consoleOutput = streamReader!.ReadToEnd().Trim();
		string studentOutputResult = consoleOutput[(consoleOutput.IndexOf("OUTPUT:") + "OUTPUT:".Length)..].Trim();
		return studentOutputResult ??= "";
	}





	private static int GetMarkByTestCaseOutput(GetTestCaseDTO testCase, string studentOutputResult)
	{
		string studentOutputString = string.Join(" ", studentOutputResult.Split("\r\n").ToList());
		string testCaseOutputString = string.Join(testCase.IsInputByLine ? "\n" : " ", testCase.Outputs!);

		// Grade mark by output
		if (testCaseOutputString.Equals(studentOutputString))
			return (int)testCase.Mark!;

		return 0;
	}
}

