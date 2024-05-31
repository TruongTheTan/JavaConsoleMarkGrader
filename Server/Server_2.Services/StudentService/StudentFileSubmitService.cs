using Microsoft.AspNetCore.Http;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;


namespace Services.StudentService;

public static class StudentFileSubmitService
{

	private static string? studentFileName;
	private static readonly string SAVE_FILE_LOCATION;
	private static readonly string EXTRACT_FILE_LOCATION;
	private static readonly string SOLUTION_PATH = Directory.GetParent(Environment.CurrentDirectory)!.FullName;

	public static string SplittedStudentFileName => studentFileName!.Split(".rar")[0];



	static StudentFileSubmitService()
	{
		SAVE_FILE_LOCATION = $@"{SOLUTION_PATH}\Server_5.Storage\StudentCompressedFiles\";
		EXTRACT_FILE_LOCATION = $@"{SOLUTION_PATH}\Server_5.Storage\StudentExtractedFiles\";
	}





	public static async Task<bool> SaveStudentFileAsync(IFormFile file)
	{
		studentFileName = file.FileName.Trim();
		string extension = Path.GetExtension(studentFileName).ToLower();

		// Only .rar extension allow
		if (extension != ".rar")
		{
			return false;
		}
		else
		{
			string filePath = Path.Combine(SAVE_FILE_LOCATION, studentFileName);

			using FileStream stream = new(filePath, FileMode.Create);

			await file.CopyToAsync(stream);
			stream?.Close();


			return File.Exists(SAVE_FILE_LOCATION + studentFileName);
		}
	}






	public static bool ExtractSavedFile()
	{
		string compressedFilePath = SAVE_FILE_LOCATION + studentFileName;

		RarArchive rarArchive = RarArchive.Open(compressedFilePath);

		rarArchive.WriteToDirectory(EXTRACT_FILE_LOCATION, new ExtractionOptions()
		{
			ExtractFullPath = true,
			Overwrite = true,
		});

		rarArchive.Dispose();

		return Directory.Exists($"{EXTRACT_FILE_LOCATION}{SplittedStudentFileName}");
	}



	public static async Task CleanUpAllFiles()
	{
		List<Task> tasks = new() { DeleteStudentCompressedFileAsync(), DeleteStudentExtractedFileAsync() };
		await Task.WhenAll(tasks);
	}



	private static Task DeleteStudentCompressedFileAsync()
	{
		return Task.Run(() =>
		{
			string studentCompressedFile = $"{SAVE_FILE_LOCATION}{studentFileName}";

			if (File.Exists(studentCompressedFile))
			{
				try
				{
					File.Delete(studentCompressedFile);
				}
				catch (Exception)
				{
					throw;
				}
			}
		});
	}




	private static Task DeleteStudentExtractedFileAsync()
	{
		return Task.Run(() =>
		{
			string studentExtractedFile = $"{EXTRACT_FILE_LOCATION}{SplittedStudentFileName}";

			try
			{
				Directory.Delete(studentExtractedFile, true);
			}
			catch (Exception)
			{
				throw;
			}
		});

	}
}

