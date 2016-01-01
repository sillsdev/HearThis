// --------------------------------------------------------------------------------------------
#region // Copyright (c) 2014, SIL International. All Rights Reserved.
// <copyright from='2011' to='2014' company='SIL International'>
//		Copyright (c) 2014, SIL International. All Rights Reserved.
//
//		Distributable under the terms of the MIT License (http://sil.mit-license.org/)
// </copyright>
#endregion
// --------------------------------------------------------------------------------------------
using L10NSharp;
using SIL.CommandLineProcessing;
using SIL.IO;
using SIL.Progress;

namespace HearThis.Publishing
{
	/// <summary>
	/// this is just to ensure that megavoice gets the precise bit/rate it wants
	/// </summary>
	public class OggEncoder : IAudioEncoder
	{
		public void Encode(string sourcePath, string destPathWithoutExtension, IProgress progress)
		{
			progress.WriteMessage(LocalizationManager.GetString("OggEncoder.Progress", "   Converting to ogg format", "Appears in progress indicator"));
			string args = string.Format("-c 1 {0} \"{1}.ogg\"", sourcePath, destPathWithoutExtension);
			string exePath = FileLocator.GetFileDistributedWithApplication("sox","sox.exe");
			progress.WriteVerbose(exePath + " " + args);
			var result =CommandLineRunner.Run(exePath, args, "", 60, progress);
			if(result.StandardError.Contains("FAIL"))
				progress.WriteError(result.StandardError);
		}

		public string FormatName
		{
			get { return "ogg"; }
		}
	}
}