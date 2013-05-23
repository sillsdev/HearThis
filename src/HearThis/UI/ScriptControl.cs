using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using HearThis.Properties;
using HearThis.Script;
using Palaso.UI.WindowsForms.Widgets.Flying;

namespace HearThis.UI
{
	/// <summary>
	/// This class holds the text that the user is supposed to be reading (the main pane on the bottom left of the UI).
	/// </summary>
	public partial class ScriptControl : UserControl
	{
		private Animator _animator;
		private PointF _animationPoint;
		private Direction _direction;
		private static float _zoomFactor;
		private PaintData CurrentData { get; set; }
		private PaintData _outgoingData;
		private Brush _scriptFocusTextBrush;
		private Pen _focusPen;
		//private Brush _obfuscatedTextBrush;
		private bool _brightenContext;
		private bool _lockContextBrightness;
		private Rectangle _brightenContextMouseZone;

		public ScriptControl()
		{
			InitializeComponent();
			_brightenContextMouseZone = new Rectangle(0, 0, 10, 10); // We'll adjust this in OnSizeChanged();
			CurrentData = new PaintData();
			// Review JohnH (JohnT): not worth setting up for localization?
			CurrentData.Script = new ScriptLine(
				"The king’s scribes were summoned at that time, in the third month, which is the month of Sivan, on the twenty-third day. And an edict was written, according to all that Mordecai commanded concerning the Jews, to the satraps and the governors and the officials of the provinces from India to Ethiopia, 127 provinces");
			SetStyle(ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
			ZoomFactor = 1.0f;
			_scriptFocusTextBrush = new SolidBrush(AppPallette.ScriptFocusTextColor);


			_focusPen = new Pen(AppPallette.HilightColor,6);
		}

		private void ScriptControl_Load(object sender, EventArgs e)
		{

		}

		protected new bool ReallyDesignMode
		{
			get
			{
				return (base.DesignMode || GetService(typeof(IDesignerHost)) != null) ||
					(LicenseManager.UsageMode == LicenseUsageMode.Designtime);
			}
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			_brightenContextMouseZone = new Rectangle(0, 0, Bounds.Width / 2, Bounds.Height);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (ReallyDesignMode)
				return;

			if (CurrentData.Script == null)
				return;

			RectangleF r;
			if (_animator == null)
			{
				r = new RectangleF(0, 0, Bounds.Width, Bounds.Height);
				DrawScriptWithContext(e.Graphics, CurrentData, r);
			}
			else
			{
				if (_direction == Direction.Forwards)
				{
					int virtualLeft = Animator.GetValue(_animationPoint.X, 0,
														0 - Bounds.Width);
					r = new RectangleF(virtualLeft, 0, Bounds.Width, Bounds.Height);
					DrawScriptWithContext(e.Graphics, _outgoingData, r);

					virtualLeft = Animator.GetValue(_animationPoint.X, Bounds.Width, 0);
					r = new RectangleF(virtualLeft, 0, Bounds.Width*2, Bounds.Height);
					DrawScriptWithContext(e.Graphics, CurrentData, r);
				}
				else
				{
					int virtualLeft = Animator.GetValue(_animationPoint.X, 0,
														0 + Bounds.Width);
					r = new RectangleF(virtualLeft, 0, Bounds.Width, Bounds.Height*2);
					DrawScriptWithContext(e.Graphics, _outgoingData, r);

					virtualLeft = Animator.GetValue(_animationPoint.X, 0 - Bounds.Width, 0);
					r = new RectangleF(virtualLeft,0, Bounds.Width, Bounds.Height*2);
					DrawScriptWithContext(e.Graphics, CurrentData, r);
				}
			}
		}

		/// <summary>
		/// Draw the specified data in the specified rectangle. This is used to draw both the current data
		/// and the previous data (as part of animating the move from one line to another).
		/// </summary>
		private void DrawScriptWithContext(Graphics graphics, PaintData data, RectangleF rectangle)
		{
			const int verticalPadding = 10;
			const int kfocusIndent = 0;// 14;
			const int whiteSpace = 3; // pixels of space between context lines.

			if (data.Script == null)
				return;
			var mainPainter = new ScriptLinePainter(this, graphics, data.Script, rectangle, data.Script.FontSize, false);
			var mainHeight = mainPainter.Measure();
			var maxPrevContextHeight = rectangle.Height - mainHeight - whiteSpace;
			var prevContextPainter = new ScriptLinePainter(this, graphics, data.PreviousLine, rectangle, data.Script.FontSize, true);
			var top = rectangle.Top;
			var currentRect = rectangle;
			top += prevContextPainter.PaintMaxHeight(maxPrevContextHeight) + whiteSpace;
			top += verticalPadding;
			currentRect = new RectangleF(currentRect.Left + kfocusIndent, top, currentRect.Width, currentRect.Bottom - top);
			mainPainter.Rectangle = currentRect;
			var focusHeight = mainPainter.Paint() + whiteSpace;
			top += focusHeight;
			//graphics.DrawLine(_focusPen, rectangle.Left, focusTop, rectangle.Left, focusTop+focusHeight);

			top += verticalPadding;
			currentRect = new RectangleF(currentRect.Left - kfocusIndent, top, currentRect.Width, currentRect.Bottom - top);
			DrawOneScriptLine(graphics, data.NextLine, currentRect, data.Script.FontSize, true);
		}

		class ScriptLinePainter
		{
			private ScriptControl _control;
			private Graphics _graphics;
			private ScriptLine _script;
			public RectangleF Rectangle { get; set; }
			private int _mainFontSize;
			private bool _context; // true to paint context lines, false for the main text.

			readonly char[] clauseSeparators = new char[] { ',', ';', ':' };

			public ScriptLinePainter(ScriptControl control, Graphics graphics, ScriptLine script, RectangleF rectangle, int mainFontSize, bool context)
			{
				_control = control;
				_graphics = graphics;
				_script = script;
				Rectangle = rectangle;
				_mainFontSize = mainFontSize;
				_context = context;
			}

			public float PaintMaxHeight(float maxHeight)
			{
				if (maxHeight <= 10 || _script == null || string.IsNullOrEmpty(_script.Text))
					return 0; // assume we can't draw any context in less than 10 pixels.
				if (Measure() < maxHeight)
					return Paint(); // it all fit.
				int badSplit = 0; // everything after this did not fit.
				int goodSplit = _script.Text.Length; // everything after this fit.
				while (badSplit < goodSplit - 1)
				{
					int trySplit = (goodSplit + badSplit)/2;
					// try to split at non-letter
					if (!MoveToBreak(ref trySplit, badSplit, goodSplit))
						break;
					if (Measure(TextAtSplit(trySplit)) <= maxHeight)
					{
						goodSplit = trySplit;
					}
					else
					{
						badSplit = trySplit;
					}
				}
				if (goodSplit >= _script.Text.Length)
					return 0;  // can't fit any context.
				return Paint(TextAtSplit(goodSplit));
			}

			/// <summary>
			/// Try to move trySplit to a place that is not in the middle of a word,
			/// but between the limits. Return false if we can't find a suitable spot.
			/// </summary>
			/// <param name="trySplit"></param>
			/// <param name="min"></param>
			/// <param name="max"></param>
			/// <returns></returns>
			bool MoveToBreak(ref int trySplit, int min, int max)
			{
				if (IsGoodBreak(trySplit))
					return true;

				int maxDelta = (max - min)/2;  // rounded
				for (int delta = 1; delta < maxDelta; delta++ )
				{
					if (trySplit - delta > min && IsGoodBreak(trySplit - delta))
					{
						trySplit -= delta;
						return true;
					}
					if (trySplit + delta < max && IsGoodBreak(trySplit + delta))
					{
						trySplit += delta;
						return true;
					}
				}
				return false; // can't find any other good break point.
			}

			// Is it good to break at index? Don't pass 0 or length.
			bool IsGoodBreak(int index)
			{
				if (!Char.IsLetterOrDigit(_script.Text[index]))
					return true; // break before non-word-forming is good.
				return !Char.IsLetterOrDigit(_script.Text[index - 1]);
			}

			string TextAtSplit(int split)
			{
				return "..." + _script.Text.Substring(split);
			}

			public float Paint()
			{
				if (_script == null)
					return 0;
				return
					Paint(_script.Text);
			}

			public float Paint(string input)
			{
				return
					MeasureAndDo(input,
						(text, font, brush, lineRect, alignment) => _graphics.DrawString(text, font, brush, lineRect, alignment));
			}

			public float Measure()
			{
				if (_script == null)
					return 0;
				return
					MeasureAndDo(_script.Text,
						(text, font, brush, lineRect, alignment) => { });
			}

			public float Measure(string input)
			{
				return
					MeasureAndDo(input,
						(text, font, brush, lineRect, alignment) => { });
			}

			// Measure the height it will take to paint our script with all the current settings.
			// Also does the drawString action with the arguments needed to actually draw the text.
			internal float MeasureAndDo(string input, Action<string, Font, Brush, RectangleF, StringFormat> drawString)
			{
				if (_script == null || _mainFontSize == 0) // mainFontSize guard enables Shell designer mode
					return 0;

				FontStyle fontStyle = default(FontStyle);
				if (_script.Bold)
					fontStyle = FontStyle.Bold;
				StringFormat alignment = new StringFormat();
				if (_script.Centered)
					alignment.Alignment = StringAlignment.Center;

				// Base the size on the main Script line, not the context's own size. Otherwise, a previous or following
				// heading line may dominate what we really want read.
				var zoom = (float) (_control.ZoomFactor*(_context ? 0.9 : 1.0));

				//We don't let the context get big... for fear of a big heading standing out so that it doesn't look *ignorable* anymore.
				// Also don't let main font get too tiny...for example it comes up 0 in the designer.
				var fontSize = _context ? 12 : Math.Max(_mainFontSize, 8);
				using (var font = new Font(_script.FontName, fontSize*zoom, fontStyle))
				{
					if (Settings.Default.BreakLinesAtClauses && !_context)
					{
						// Draw each 'clause' on a line.
						float offset = 0;
						foreach (var chunk in SentenceClauseSplitter.BreakIntoChunks(input, clauseSeparators))
						{
							var text = chunk.Text.Trim();
							var lineRect = new RectangleF(Rectangle.X, Rectangle.Y + offset, Rectangle.Width,
														  Rectangle.Height - offset);
							drawString(text, font, _control._scriptFocusTextBrush,
									   lineRect, alignment);
							offset += _graphics.MeasureString(text, font, Rectangle.Size).Height;
						}
						return offset;
					}
					else
					{
						// Normal behavior: draw it all as one string.
						drawString(input, font, _context ? _control.CurrentScriptContextBrush : _control._scriptFocusTextBrush,
								   Rectangle, alignment);
						return _graphics.MeasureString(input, font, Rectangle.Size).Height;
					}
				}
			}
		}

		/// <summary>
		/// Draw one script line. It may be the main line (context is false)
		/// or a context line (context is true).
		/// </summary>
		private float DrawOneScriptLine(Graphics graphics, ScriptLine script, RectangleF rectangle, int mainFontSize, bool context)
		{
			return new ScriptLinePainter(this, graphics, script, rectangle, mainFontSize, context).Paint();
		}

		protected Brush CurrentScriptContextBrush
		{
			get
			{
				if (_brightenContext)
					return AppPallette.ScriptContextTextBrush;
				return AppPallette.ObfuscatedTextContextBrush;
			}

		}

		public float ZoomFactor
		{
			get {
				return _zoomFactor;
			}
			set {
				_zoomFactor = value;
				Invalidate();
			}
		}

		public enum Direction
		{
			Backwards,
			Forwards
		}

		public void GoToScript(Direction direction, ScriptLine previous, ScriptLine script, ScriptLine next)
		{
			_direction = direction;
			_outgoingData = CurrentData;
			_animator = new Animator();
			_animator.Animate += new Animator.AnimateEventDelegate(animator_Animate);
			_animator.Finished += new EventHandler((x, y) => { _animator = null;
																 _outgoingData = null;
			});
			_animator.Duration = 300;
			_animator.Start();
			CurrentData = new PaintData() {Script = script, PreviousLine = previous, NextLine = next};
			Invalidate();
		}

		void animator_Animate(object sender, Animator.AnimatorEventArgs e)
		{
			_animationPoint = e.Point;
			Invalidate();
		}

		private void ScriptControl_MouseMove(object sender, MouseEventArgs e)
		{
			var newMouseLocIsInTheZone = _lockContextBrightness || _brightenContextMouseZone.Contains(e.Location);
			if (_brightenContext == newMouseLocIsInTheZone)
				return; // do nothing (as quickly as possible)

			var oldBrightenContext = _brightenContext;
			_brightenContext = !_brightenContext || _lockContextBrightness;
			if (oldBrightenContext != _brightenContext)
				this.Invalidate();
		}

		private void ScriptControl_MouseLeave(object sender, EventArgs e)
		{
			if (!_brightenContext || _lockContextBrightness)
				return;
			_brightenContext = false;
			this.Invalidate();
		}

		private void ScriptControl_Click(object sender, MouseEventArgs e)
		{
			if (!_brightenContextMouseZone.Contains(e.Location))
				return;
			_lockContextBrightness = !_lockContextBrightness;

			//this tweak makes it more obvious that your click when context was locked on is going to turn it off
			if (_brightenContext & !_lockContextBrightness)
				_brightenContext = false;
			this.Invalidate();
		}
	}

	/// <summary>
	/// The data needed to call DrawScript. Used to paint both the current data and the animation.
	/// </summary>
	class PaintData
	{
		public ScriptLine Script { get; set; }
		// Preceding context; may be null.
		public ScriptLine PreviousLine { get; set; }
		// Following context; may be null.
		public ScriptLine NextLine { get; set; }
	}

}
