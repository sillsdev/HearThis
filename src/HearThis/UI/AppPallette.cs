// --------------------------------------------------------------------------------------------
#region // Copyright (c) 2014, SIL International. All Rights Reserved.
// <copyright from='2011' to='2014' company='SIL International'>
//		Copyright (c) 2014, SIL International. All Rights Reserved.
//
//		Distributable under the terms of the MIT License (http://sil.mit-license.org/)
// </copyright>
#endregion
// --------------------------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using HearThis.Properties;
using L10NSharp;

namespace HearThis.UI
{
	public enum ColorScheme
	{
		Normal,
		HighContrast
	}

	public static class ColorSchemeExtensions
	{
		public static string ToLocalizedString(this ColorScheme colorScheme)
		{
			switch (colorScheme)
			{
				case ColorScheme.Normal:
					return LocalizationManager.GetString("ColorScheme.Normal", "Normal");
				case ColorScheme.HighContrast:
					return LocalizationManager.GetString("ColorScheme.HighContrast", "High Contrast");
				default:
					return null;
			}
		}
	}

	public static class AppPallette
	{
		public enum ColorSchemeElement
		{
			Background,
			MouseOverButtonBackColor,
			NavigationTextColor,
			ScriptFocusTextColor,
			ScriptContextTextColor,
			EmptyBoxColor,
			HilightColor,
			SecondPartTextColor,
			SkippedLineColor,
			Red,
			Blue,
			Green,
			Titles,
			LineBreakCommaActiveIcon,
			RecordInPartsIcon
		}

		private static readonly Dictionary<ColorScheme, Dictionary<ColorSchemeElement, Color>> ColorSchemes = new Dictionary<ColorScheme, Dictionary<ColorSchemeElement, Color>>
		{
			{
				ColorScheme.Normal, new Dictionary<ColorSchemeElement, Color>
				{
					{ColorSchemeElement.Background , Color.FromArgb(65,65,65) },
					{ColorSchemeElement.MouseOverButtonBackColor, Color.FromArgb(78,78,78) },
					{ColorSchemeElement.NavigationTextColor, Color.FromArgb(192, 192, 192) },
					{ColorSchemeElement.ScriptFocusTextColor, Color.FromArgb(252,202,1) },
					{ColorSchemeElement.ScriptContextTextColor, Color.FromArgb(192,192,192) },
					{ColorSchemeElement.EmptyBoxColor, Color.FromArgb(192,192,192) },
					{ColorSchemeElement.HilightColor, Color.FromArgb(251, 242, 0) },
					{ColorSchemeElement.SecondPartTextColor, Color.FromArgb(251, 242, 0) },
					{ColorSchemeElement.SkippedLineColor, Color.FromArgb(166,132,0) },
					{ColorSchemeElement.Red, Color.FromArgb(215,2,0) },
					{ColorSchemeElement.Blue, Color.FromArgb(00,8,118) },
					{ColorSchemeElement.Green, Color.FromArgb(57,165,0) },
					{ColorSchemeElement.Titles, Color.FromArgb(192, 192, 192) }

				}
			},
			{
				ColorScheme.HighContrast, new Dictionary<ColorSchemeElement, Color>
				{
					{ColorSchemeElement.Background, Color.FromArgb(0,0,0) },
					{ColorSchemeElement.MouseOverButtonBackColor, Color.FromArgb(0,0,0) },
					{ColorSchemeElement.NavigationTextColor, Color.FromArgb(192, 192, 192) },
					{ColorSchemeElement.ScriptFocusTextColor, Color.FromArgb(0,255,0) },
					{ColorSchemeElement.ScriptContextTextColor, Color.FromArgb(192, 192, 192) },
					{ColorSchemeElement.EmptyBoxColor, Color.FromArgb(192, 192, 192) },
					{ColorSchemeElement.HilightColor, Color.FromArgb(0,255,0) },
					{ColorSchemeElement.SecondPartTextColor, Color.FromArgb(0,255,0) },
					{ColorSchemeElement.SkippedLineColor, Color.FromArgb(0,255,0) },
					{ColorSchemeElement.Red, Color.FromArgb(255,0,0) },
					{ColorSchemeElement.Blue, Color.FromArgb(0,0,255) },
					{ColorSchemeElement.Green, Color.FromArgb(0,255,0) },
					{ColorSchemeElement.Titles, Color.FromArgb(192, 192, 192) }
				}
			}

		};

		public static readonly Dictionary<ColorScheme, Dictionary<ColorSchemeElement, Image>> ColorSchemeIcons = new Dictionary<ColorScheme, Dictionary<ColorSchemeElement, Image>>
		{
			{
				ColorScheme.Normal, new Dictionary<ColorSchemeElement, Image>
				{
					{ColorSchemeElement.LineBreakCommaActiveIcon, Resources.linebreakCommaActive },
					{ColorSchemeElement.RecordInPartsIcon, Resources.recordInParts }
				}
			},
			{
				ColorScheme.HighContrast, new Dictionary<ColorSchemeElement, Image>
				{
					{ColorSchemeElement.LineBreakCommaActiveIcon, Resources.linebreakCommaActiveHC },
					{ColorSchemeElement.RecordInPartsIcon, Resources.recordInPartsHC }
				}
			}
		};

		public static ColorScheme CurrentColorScheme
		{
			get
			{
				var setScheme = Settings.Default.UserColorScheme;
				if (ColorSchemes.ContainsKey(setScheme))
				{
					return setScheme;
				}
				return ColorScheme.Normal;
			}
		}

		public static IEnumerable<KeyValuePair<ColorScheme, string>> AvailableColorSchemes
		{
			get
			{
				foreach (var colorScheme in Enum.GetValues(typeof(ColorScheme)).Cast<ColorScheme>())
				{
					yield return new KeyValuePair<ColorScheme, string>(colorScheme, colorScheme.ToLocalizedString());
				}
			}
		}

		public static Image LineBreakCommaActiveImage
		{
			get { return ColorSchemeIcons[CurrentColorScheme][ColorSchemeElement.LineBreakCommaActiveIcon]; }
		}

		public static Image RecordInPartsImage
		{
			get { return ColorSchemeIcons[CurrentColorScheme][ColorSchemeElement.RecordInPartsIcon]; }
		}

		public static Color Background
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.Background]; }
		}

		public static Color MouseOverButtonBackColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.MouseOverButtonBackColor]; }
		}

		public static Color NavigationTextColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.NavigationTextColor]; }
		}

		public static Color ScriptFocusTextColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.ScriptFocusTextColor]; }
		}

		public static Color ScriptContextTextColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.ScriptContextTextColor]; }
		}

		public static Color EmptyBoxColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.EmptyBoxColor]; }
		}

		public static Color HilightColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.HilightColor]; }
		}

		public static Color SecondPartTextColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.SecondPartTextColor]; }
		}

		public static Color SkippedLineColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.SkippedLineColor]; }
		}

		public static Color Red
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.Red]; }
		}

		public static Color Blue
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.Blue]; }
		}

		public static Color Green
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.Green]; }
		}

		public static Color TitleColor
		{
			get { return ColorSchemes[CurrentColorScheme][ColorSchemeElement.Titles]; }
		}

		public static Brush SkippedSegmentBrush = new SolidBrush(SkippedLineColor);
		public static Pen PartialProgressPen = new Pen(EmptyBoxColor, 3);
		public static Pen CompleteProgressPen =new Pen(HilightColor, 2);
		public static Brush DisabledBrush = new SolidBrush(EmptyBoxColor);
		public static Brush BackgroundBrush = new SolidBrush(Background);

		public static Pen ButtonMouseOverPen = new Pen(ScriptFocusTextColor, 3);
		public static Pen ButtonSuggestedPen = new Pen(ScriptFocusTextColor, 2);
		public static Brush ButtonRecordingBrush = new SolidBrush(Green);
		public static Brush ButtonWaitingBrush = new SolidBrush(Red);

		public static Brush ObfuscatedTextContextBrush = new SolidBrush(ControlPaint.Light(Background,(float) .3));
		public static Brush ScriptContextTextBrush = new SolidBrush(ScriptContextTextColor);

		private static Brush _blueBrush;
		public static Brush BlueBrush
		{
			get
			{
				if(_blueBrush==null)
				{
					_blueBrush = new SolidBrush(Blue);
				}
				return _blueBrush;
			}
		}

	}
}
