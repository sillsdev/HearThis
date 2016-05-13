using System.Collections.Generic;
using Paratext;

namespace HearThis.Script
{
	/// <summary>
	/// This is the real implementation of IScripture which implements the interface by
	/// wrapping a real ScrText.
	/// </summary>
	class Scripture : IScripture
	{
		private ScrText _scrText;

		public Scripture(ScrText text)
		{
			_scrText = text;
		}

		#region IScripture Members

		public ScrVers Versification
		{
			get { return _scrText.Versification; }
		}

		public List<UsfmToken> GetUsfmTokens(VerseRef verseRef, bool singleChapter, bool doMapIn)
		{
			var parser = new ScrParser(_scrText, true);
			return parser.GetUsfmTokens(verseRef, false, true);

		}

		public IScrParserState CreateScrParserState(VerseRef verseRef)
		{
			return new ParserState(new ScrParserState(_scrText, verseRef));
		}

		public string DefaultFont
		{
			get { return _scrText.DefaultFont; }
		}

		public string EthnologueCode
		{
			get
			{
				var result = _scrText.EthnologueCode;
				if (result != null)
					return result;
				// Seems like the above SHOULD return Paratext's idea of the language identifier.
				// In some cases it does. But in others it does not.
				// In at least some cases where it does not, it really does know the identifier,
				// and seems to fairly consistently put it in the FullName,
				// surrounded by square brackets.
				var name = _scrText.FullName;
				int openBracket = name.IndexOf('[');
				int closeBracket = name.IndexOf(']');
				if (closeBracket > openBracket && openBracket > 0)
					return name.Substring(openBracket + 1, closeBracket - openBracket - 1);
				return result;
			}
		}

		#endregion
	}
}