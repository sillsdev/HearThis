using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using L10NSharp;
using Palaso.CommandLineProcessing;
using Palaso.IO;
using Palaso.Extensions;
using Palaso.Progress;
using Palaso.Reporting;
using HearThis.Script;

namespace HearThis.Publishing
{
	/// <summary>
	/// Each script block is recorded and each clip stored as its own file.  This class manages that collection of files.
	/// </summary>
	public static class ClipRecordingRepository
	{
		private static string _sHearThisFolder;

		#region Retrieval and Deletion methods

		public static string GetPathToLineRecording(string projectName, string bookName, int chapterNumber, int lineNumber)
		{
			var chapter = GetChapterFolder(projectName, bookName, chapterNumber);
			return Path.Combine(chapter, lineNumber + ".wav");
		}

		public static bool GetHaveClip(string projectName, string bookName, int chapterNumber, int lineNumber)
		{
			var path = GetPathToLineRecording(projectName, bookName, chapterNumber, lineNumber);
			return File.Exists(path);
		}

		public static string GetChapterFolder(string projectName, string bookName, int chapterNumber)
		{
			var book = GetBookFolder(projectName, bookName);
			var chapter = CreateDirectoryIfNeeded(book, chapterNumber.ToString());
			return chapter;
		}

		private static string GetBookFolder(string projectName, string bookName)
		{
			var project = GetApplicationDataFolder(projectName);
			var book = CreateDirectoryIfNeeded(project, bookName.Trim());
			return book;
		}

		public static int GetCountOfRecordingsInFolder(string path)
		{
			if (!Directory.Exists(path))
				return 0;
			return GetSoundFilesInFolder(path).Length;
		}

		public static int GetCountOfRecordingsForBook(string projectName, string name)
		{
			var path = GetBookFolder(projectName, name);
			if (!Directory.Exists(path))
				return 0;
			return Directory.GetDirectories(path).Sum(directory => GetSoundFilesInFolder(directory).Length);
		}

		public static bool DeleteLineRecording(string projectName, string bookName, int chapterNumber,
			int lineNumber)
		{
			// just being careful...
			if (GetHaveClip(projectName, bookName, chapterNumber, lineNumber))
			{
				var path = GetPathToLineRecording(projectName, bookName, chapterNumber, lineNumber);
				try
				{
					File.Delete(path);
					return true;
				}
				catch (IOException err)
				{
					ErrorReport.NotifyUserOfProblem(err,
						LocalizationManager.GetString("LineRecordingRepository.DeleteLineRecordingProblem",
							"For some reason we are unable to delete that file. Perhaps it is locked up. Yes, this problem will need to be fixed."));
				}
			}
			return false;
		}

		#endregion

		#region AppData folder structure
		/// <summary>
		/// Get the folder %AppData%/SIL/HearThis where we store recordings and localization stuff.
		/// </summary>
		public static string ApplicationDataBaseFolder
		{
			get
			{
				if (_sHearThisFolder == null)
				{
					_sHearThisFolder = CreateDirectoryIfNeeded(
						Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
						Program.kCompany, Program.kProduct);
				}
				return _sHearThisFolder;
			}
		}

		/// <summary>
		/// Create (if necessary) and return the requested subfolder of the HearThis base AppData folder.
		/// </summary>
		/// <param name="projectName"></param>
		public static string GetApplicationDataFolder(string projectName)
		{
			return CreateDirectoryIfNeeded(ApplicationDataBaseFolder, projectName);
		}

		private static string CreateDirectoryIfNeeded(params string[] pathparts)
		{
			var path = Path.Combine(pathparts);
			Directory.CreateDirectory(path);
			return path;
		}

		#endregion

		#region Publishing methods

		public static void PublishAllBooks(PublishingModel publishingModel, string projectName, IPublishingInfoProvider infoProvider,
			string publishRoot, IProgress progress)
		{
			Directory.Delete(publishRoot, true);
			foreach (string dir in Directory.GetDirectories(GetApplicationDataFolder(projectName)))
			{
				if (progress.CancelRequested)
					return;
				string bookName = Path.GetFileName(dir);
				//var filePath = Path.Combine(publishPath, bookName);
				PublishAllChapters(publishingModel, projectName, infoProvider, bookName, publishRoot, progress);
				if (progress.ErrorEncountered)
					return;
			}
		}

		public static void PublishAllChapters(PublishingModel publishingModel, string projectName, IPublishingInfoProvider infoProvider,
			string bookName, string publishRoot, IProgress progress)
		{
			var bookFolder = GetBookFolder(projectName, bookName);
			foreach (var dirPath in Directory.GetDirectories(bookFolder))
			{
				if (progress.CancelRequested)
					return;
				var chapterNumber = int.Parse(Path.GetFileName(dirPath));
				PublishSingleChapter(publishingModel, projectName, infoProvider, bookName, chapterNumber, publishRoot,
									 progress);
				if (progress.ErrorEncountered)
					return;
			}
		}

		private static string[] GetSoundFilesInFolder(string path)
		{
			return Directory.GetFiles(path, "*.wav");
		}

		public static bool GetDoAnyClipsExistForProject(string projectName)
		{
			return Directory.GetFiles(GetApplicationDataFolder(projectName), "*.wav", SearchOption.AllDirectories).Any();
		}

		private static void PublishSingleChapter(PublishingModel publishingModel, string projectName, IPublishingInfoProvider infoProvider,
			string bookName, int chapterNumber, string rootPath, IProgress progress)
		{
			try
			{
				var verseFiles = GetSoundFilesInFolder(GetChapterFolder(projectName, bookName, chapterNumber));
				if (verseFiles.Length == 0)
					return;

				verseFiles = verseFiles.OrderBy(name =>
				{
					int result;
					if (Int32.TryParse(Path.GetFileNameWithoutExtension(name), out result))
						return result;
					return -1;
				}).ToArray();

				publishingModel.FilesInput += verseFiles.Length;
				publishingModel.FilesOutput++;

				progress.WriteMessage("{0} {1}", bookName, chapterNumber.ToString());

				string pathToJoinedWavFile = Path.GetTempPath().CombineForPath("joined.wav");
				using (TempFile.TrackExisting(pathToJoinedWavFile))
				{
					MergeAudioFiles(verseFiles, pathToJoinedWavFile, progress);

					PublishVerseIndexFiles(verseFiles, publishingModel, projectName, infoProvider, bookName, chapterNumber, rootPath, progress);

					publishingModel.PublishingMethod.PublishChapter(rootPath, bookName, chapterNumber, pathToJoinedWavFile,
						progress);
				}
			}
			catch (Exception error)
			{
				progress.WriteError(error.Message);
			}
		}

		internal static void MergeAudioFiles(IEnumerable<string> files, string pathToJoinedWavFile, IProgress progress )
		{
			if (files.Count() == 1)
			{
				File.Copy(files.First(), pathToJoinedWavFile);
			}
			else
			{
				var fileList = Path.GetTempFileName();
				File.WriteAllLines(fileList, files.ToArray());
				progress.WriteMessage(LocalizationManager.GetString("LineRecording.MergeAudioProgress","   Joining recorded clips", "Should have three leading spaces"));
				string arguments = string.Format("join -d \"{0}\" -F \"{1}\" -O always", Path.GetDirectoryName(pathToJoinedWavFile),
												 fileList);
				RunCommandLine(progress, FileLocator.GetFileDistributedWithApplication(false, "shntool.exe"), arguments);
			}
			if (!File.Exists(pathToJoinedWavFile))
			{
				throw new ApplicationException(
					"Um... shntool.exe failed to produce the file of the joined clips. Reroute the power to the secondary transfer conduit.");
			}
		}

		public static void RunCommandLine(IProgress progress, string exePath, string arguments)
		{
			progress.WriteVerbose(exePath + " " + arguments);
			ExecutionResult result = CommandLineRunner.Run(exePath, arguments, null, 60, progress);
			result.RaiseExceptionIfFailed("");
		}

		/// <summary>
		/// Publish Audacity Label Files or cue sheet to text files
		/// </summary>
		public static void PublishVerseIndexFiles(string[] verseFiles, PublishingModel publishingModel, string projectName, IPublishingInfoProvider infoProvider,
			string bookName, int chapterNumber, string rootPath, IProgress progress)
		{
			// get the output path
			var outputPath = Path.ChangeExtension(
				publishingModel.PublishingMethod.GetFilePathWithoutExtension(rootPath, bookName, chapterNumber), "txt");

			// clear the text files if they already exist
			File.Delete(outputPath);

			if (chapterNumber != 0)
			{
				if (publishingModel.verseIndexFormat == PublishingModel.VerseIndexFormat.AudacityLabelFile)
					PublishAudacityLabelFile(verseFiles, projectName, infoProvider, bookName, chapterNumber, outputPath, progress);

				else if (publishingModel.verseIndexFormat == PublishingModel.VerseIndexFormat.CueSheet)
					PublishCueSheet(verseFiles, projectName, infoProvider, bookName, chapterNumber, outputPath, progress);
			}
		}

		private static void PublishCueSheet(string[] verseFiles, string projectName,
			IPublishingInfoProvider infoProvider, string bookName, int chapterNumber, string outputPath, IProgress progress)
		{
			try
			{
				using (StreamWriter file = new StreamWriter(outputPath, true))
				{
					file.WriteLine(outputPath);
				}

				TimeSpan indextime = new TimeSpan(0, 0, 0, 0);

				for (int i = 0; i < verseFiles.Length; i++)
				{
					// 1st line
					string track;
					if (i < 9)
						track = "  TRACK 0" + (i + 1) + " AUDIO";
					else
						track = "  TRACK " + (i + 1) + " AUDIO";
					// 2nd line
					string title = "	TITLE 00000-" + bookName + chapterNumber + "-tnnC001 ";

					// 3rd line
					string index = "	INDEX 01 " + indextime;

					// write label file to the path
					using (StreamWriter file = new StreamWriter(outputPath, true))
					{
						file.WriteLine(track);
						file.WriteLine(title);
						file.WriteLine(index);
					}

					// get the length of the block
					using (var b = new NAudio.Wave.WaveFileReader(verseFiles[i]))
					{
						TimeSpan wavlength = b.TotalTime;

						//update the indextime for the verse
						indextime = indextime.Add(wavlength);
					}
				}
			}
			catch (Exception error)
			{
				progress.WriteError(error.Message);
			}
		}

		private static void PublishAudacityLabelFile(string[] verseFiles, string projectName, IPublishingInfoProvider infoProvider,
			string bookName, int chapterNumber, string outputPath, IProgress progress)
		{
			try
			{
				int headingsCounter = 0;
				bool checkPreviousIsHeadings = false;
				TimeSpan startTime = new TimeSpan(0, 0, 0, 0);
				TimeSpan endTime = new TimeSpan(0, 0, 0, 0);

				for (int i = 0; i < verseFiles.Length; i++)
				{
					// get the length of the block
					using (var b = new NAudio.Wave.WaveFileReader(verseFiles[i]))
					{
						TimeSpan wavlength = b.TotalTime;

						//update the endTime for the verse
						endTime = endTime.Add(wavlength);
					}

					string timeRange = startTime.TotalSeconds + "	" + endTime.TotalSeconds + "	";
					int lineNumber = Int32.Parse(Path.GetFileNameWithoutExtension(verseFiles[i]));
					ScriptLine block = infoProvider.GetBlock(bookName, chapterNumber, lineNumber);

					// current block is a heading
					string verseNumber;
					if (block.Heading)
					{
						// postpone appending and detect whether the next block is a heading
						if (i < verseFiles.Length - 1)
						{
							int nextLineNumber = Int32.Parse(Path.GetFileNameWithoutExtension(verseFiles[i + 1]));
							ScriptLine nextBlock = infoProvider.GetBlock(bookName, chapterNumber, nextLineNumber);
							if (nextBlock.Heading)
							{
								continue;
							}
						}

						// if current block is a heading and previous block is a heading
						if (checkPreviousIsHeadings)
						{
							startTime = endTime;
							continue;
						}

						// Current block is a heading but previous block is not a heading
						headingsCounter++;
						verseNumber = " s" + headingsCounter;
						checkPreviousIsHeadings = true;
					}

					// Current block is a normal verse
					else
					{
						// postpone appending and detect the next block whether if it's same verse number
						if (i < verseFiles.Length - 1)
						{
							int nextLineNumber = Int32.Parse(Path.GetFileNameWithoutExtension(verseFiles[i + 1]));
							ScriptLine nextBlock = infoProvider.GetBlock(bookName, chapterNumber, nextLineNumber);
							if (block.Verse == nextBlock.Verse)
							{
								continue;
							}
						}
						verseNumber = " " + block.Verse;
						checkPreviousIsHeadings = false;
					}

					AppendToLabelFile(outputPath, verseNumber, timeRange);

					// update start time for the next verse
					startTime = endTime;
				}
			}
			catch (Exception error)
			{
				progress.WriteError(error.Message);
			}
		}

		private static void AppendToLabelFile(string outputPath, string verseNumber, string timeRange)
		{
			using (StreamWriter file = new StreamWriter(outputPath, true))
			{
				file.WriteLine(timeRange + verseNumber);
			}
		}

		#endregion
	}
}
