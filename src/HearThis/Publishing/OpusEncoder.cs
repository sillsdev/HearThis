/*
OpusDocumentation: This file exports audio into .opus format. 
Opus is great for transmitting speech and music with minimal latency.
Make sure opusenc.exe is downloaded and in repository (it should be installed with the repository). This can be downloaded from https://opus-codec.org/downloads/ and is in the file opus tools 2.0. 
 ***NOTE*** Do not import the entire library into the project, only opusenc.exe (this goes for most encoders, keep the code clean and file size small) ***NOTE***
opusenc reads audio data in Wave, AIFF, FLAC, Ogg/FLAC, or raw PCM format and encodes it into an Ogg Opus stream.
Finally, verify that the FormatName and connecting Radio button (in PublishingDialogue.cs) have the same ID.
*/

// Use necessary namespaces and file directories
using System;
using System.IO;
using L10NSharp;
using SIL.CommandLineProcessing;
using SIL.IO;
using SIL.Progress;

namespace HearThis.Publishing
{
	// This line defines a public class named OpusEncoder and implements the IAudioEncoder interface.
	public class OpusEncoder : IAudioEncoder
	{
		// Define Encode with sourcePath, destPathWithoutExtension and IProgress progress
		public void Encode(string sourcePath, string destPathWithoutExtension, IProgress progress)
		{
			progress.WriteMessage("   " + LocalizationManager.GetString("OpusEncoder.Progress", "Converting to Opus format", "Appears in progress indicator"));

			// Specify the full path to opusenc.exe in the HearThis Project 
			// Possible string exePath = "\\src\\HearThis\\Publishing\\opusenc\\opusenc.exe";
			string exePath = FileLocationUtilities.GetFileDistributedWithApplication("opusenc", "opusenc.exe");

			// Starts at a bitrate of 64, pulling methods from opusenc.exe and taking the source paths from IAudioEncoder. It also specifies the file output as .opus.
			// Can modify the bitrate if needed
			string args = $"--bitrate 64 \"{sourcePath}\" \"{destPathWithoutExtension}.opus\"";

			// Write message and display the progress of the exporting audio
			progress.WriteVerbose(exePath + " " + args);

			/* This line invokes a method named Run from a CommandLineRunner class. It passes the executable path, arguments, an empty string for standard 
            input, a timeout of 10 minutes, and a progress interface.*/
			var result = CommandLineRunner.Run(exePath, args, "", 60 * 10, progress);
			if (result.StandardError.Contains("FAIL"))
				progress.WriteError(result.StandardError);
		}
		// This line creats the format name that connects to the opusRadio button in PublishingDialog.cs
		public string FormatName => "opus";
	}
}
