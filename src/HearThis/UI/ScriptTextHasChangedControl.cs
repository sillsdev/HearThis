// --------------------------------------------------------------------------------------------
#region // Copyright (c) 2022, SIL International. All Rights Reserved.
// <copyright from='2020' to='2022' company='SIL International'>
//		Copyright (c) 2022, SIL International. All Rights Reserved.
//
//		Distributable under the terms of the MIT License (http://sil.mit-license.org/)
// </copyright>
#endregion
// --------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HearThis.Publishing;
using HearThis.Script;
using HearThis.StringDifferences;
using L10NSharp;
using SIL.Reporting;
using static System.IO.File;
using static System.String;
using DateTime = System.DateTime;
using FileInfo = System.IO.FileInfo;

namespace HearThis.UI
{
	/// <summary>
	/// This class holds the text that the user is supposed to be reading (the main pane on the bottom left of the UI).
	/// </summary>
	public partial class ScriptTextHasChangedControl : UserControl, IMessageFilter
	{
		private Project _project;
		private bool _updatingDisplay;
		private static float s_zoomFactor;
		private string _standardProblemText;
		private string _okayResolutionText;
		private string _standardDeleteExplanationText;
		private string _fmtRecordedDate;
		private int _indexIntoExtraRecordings;
		private ScriptLine _lastNullScriptLineIgnored;
		private ScriptLine CurrentScriptLine { get; set; }
		private IReadOnlyList<ExtraRecordingInfo> ExtraRecordings { get; set; }
		private ChapterInfo CurrentChapterInfo { get; set; }
		public event EventHandler ProblemIgnoreStateChanged;
		public event EventHandler ExtraClipCountChanged;
		public event EventHandler NextClick;
		private ShiftClipsViewModel _shiftClipsViewModel;

		public ScriptTextHasChangedControl()
		{
			InitializeComponent();
			_txtThen.BackColor = AppPalette.Background;
			_txtNow.BackColor = AppPalette.Background;
			_txtThen.ForeColor = AppPalette.TitleColor;
			_txtNow.ForeColor = AppPalette.TitleColor;
			_lblProblemSummary.ForeColor = _tableProblem.ForeColor =
				_nextButton.RoundedBorderColor = AppPalette.HilightColor;
			_btnPlayClip.FlatAppearance.MouseDownBackColor =
				_btnPlayClip.FlatAppearance.MouseOverBackColor =
				_btnShiftClips.FlatAppearance.MouseDownBackColor =
				_btnShiftClips.FlatAppearance.MouseOverBackColor =
					AppPalette.Background;

			Program.RegisterStringsLocalized(HandleStringsLocalized);

			_btnAskLater.CorrespondingRadioButton = _rdoAskLater;
			_btnUseExisting.CorrespondingRadioButton = _rdoUseExisting;
			_btnDelete.CorrespondingRadioButton = _rdoReRecord;

			HandleStringsLocalized();
		}

		private void HandleStringsLocalized()
		{
			_standardProblemText = _lblProblemSummary.Text;
			_okayResolutionText = _lblResolution.Text;
			_standardDeleteExplanationText = _btnDelete.Text;
			_fmtRecordedDate = _lblRecordedDate.Text;
			if (_rdoAskLater.Checked)
				UpdateDisplay();
			else
			{
				// TODO
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (Visible && !_updatingDisplay)
				UpdateState();
		}

		private void OnNextButton(object sender, EventArgs e)
		{
			NextClick?.Invoke(this, e);
		}

		public void SetData(Project project, IReadOnlyList<ExtraRecordingInfo> extraClips)
		{
			_project = project;
			CurrentScriptLine = _project.ScriptOfSelectedBlock;
			SetScriptFonts();
			ExtraRecordings = extraClips;
			CurrentChapterInfo = _project.SelectedChapterInfo;
			_indexIntoExtraRecordings = _project.SelectedScriptBlock - _project.LineCountForChapter;
			UpdateState();
		}

		private void SetScriptFonts()
		{
			if (_project == null)
				return;

			_txtNow.Font = _txtThen.Font = new Font(_project.FontNameForSelectedBlock,
				_project.FontSizeForSelectedBlock * ZoomFactor);
		}

		public void UpdateState()
		{
			_lastNullScriptLineIgnored = null;

			if (InvokeRequired)
				Invoke(new Action(UpdateDisplay));
			else
				UpdateDisplay();

			Task.Run(UpdateAudioButtonsControl);
		}

		private void UpdateAudioButtonsControl()
		{
			// We do this asynchronously to prevent deadlock on the control.
			// Nothing should keep the control locked for long, so it should always get
			// done very quickly. Since we don't lock _project, it is conceivable that
			// it could change in the middle of this operation, but it's practically
			// impossible. If it were to change, another background worker would get
			// kicked off right away. That one wouldn't be able to enter until we
			// released the lock on _audioButtonsControl, so everything would end up
			// in a consistent state.
			lock (_audioButtonsControl)
			{
				if (Visible && _project != null)
				{
					_audioButtonsControl.Path = _indexIntoExtraRecordings >= 0 && _indexIntoExtraRecordings < ExtraRecordings.Count ?
						ExtraRecordings[_indexIntoExtraRecordings].ClipFile :
						_project.GetPathToRecordingForSelectedLine();
					_audioButtonsControl.ContextForAnalytics = new Dictionary<string, string>
					{
						{"book", _project.SelectedBook.Name},
						{"chapter", CurrentChapterInfo.ChapterNumber1Based.ToString()},
						{"scriptBlock", _project.SelectedScriptBlock.ToString()},
						{"wordsInLine", CurrentScriptLine?.ApproximateWordCount.ToString()},
						{"_indexIntoExtraRecordings", _indexIntoExtraRecordings.ToString()}
					};
				}
				else
					_audioButtonsControl.Path = null;
			}

			if (InvokeRequired)
				Invoke(new Action(() => _audioButtonsControl.UpdateDisplay()));
			else
				_audioButtonsControl.UpdateDisplay();
		}

		private bool HaveScript => CurrentScriptLine?.Text?.Length > 0;

		private ScriptLine CurrentRecordingInfo => CurrentScriptLine == null ? null :
			CurrentChapterInfo?.Recordings.FirstOrDefault(r => r.Number == CurrentScriptLine.Number) ??
			CurrentChapterInfo?.DeletedRecordings?.FirstOrDefault(r => r.Number == CurrentScriptLine.Number);

		private DateTime ActualFileRecordingTime => GetActualClipRecordingTime(_project.SelectedScriptBlock);

		private DateTime GetActualClipRecordingTime(int lineNumber0Based) =>
			new FileInfo(ClipRepository.GetPathToLineRecording(_project.Name, _project.SelectedBook.Name,
				CurrentChapterInfo.ChapterNumber1Based, lineNumber0Based, _project.ScriptProvider)).CreationTime;

		private string ActualFileRecordingDateForUI => ActualFileRecordingTime.ToLocalTime().ToShortDateString();

		private string ReadyToReRecordMessage => LocalizationManager.GetString("ScriptTextHasChangedControl.ReadyForRerecording",
			"This block is ready to be re-recorded.");

		private void UpdateDisplay()
		{
			if (_project == null)
			{
				Hide();
				return; // Not ready yet
			}

			var currentRecordingInfo = CurrentRecordingInfo;

			if (!HaveScript)
			{
				if (ExtraRecordings.Count > _indexIntoExtraRecordings)
				{
					Debug.Assert(_indexIntoExtraRecordings >= 0, "Figure out when we can not have a script but be on a real block!");
					UpdateDisplayForExtraRecording();
					DeterminePossibleClipShifts();
					ShowNextButtonIfThereAreMoreProblemsInChapter();
				}
				else
					Hide(); // Not ready yet
				return;
			}

			if (currentRecordingInfo != null && currentRecordingInfo.Number - 1 != _project.SelectedScriptBlock)
				return; // Initializing during restart to change color scheme... not ready yet

			_updatingDisplay = true;
			SuspendLayout();
			_tableBlockText.SuspendLayout();

			ResetDisplayToProblemState();
			var haveRecording = GetHasRecordedClip(_project.SelectedScriptBlock);
			var haveBackup = !haveRecording && ClipRepository.GetHaveBackupFile(_project.Name, _project.SelectedBook.Name,
				CurrentChapterInfo.ChapterNumber1Based, _project.SelectedScriptBlock);
			_pnlPlayClip.Visible = haveRecording;
			_btnUseExisting.Visible =_btnDelete.Visible = 
				_panelThen.Visible = _txtThen.Visible = haveRecording || haveBackup;
			_btnDelete.Text = _standardDeleteExplanationText;
			_tableOptions.Visible = _lblNow.Visible = _txtNow.Visible = true;
			ShowNextButtonIfThereAreMoreProblemsInChapter();

			_txtNow.Text = CurrentScriptLine.Text;

			DeterminePossibleClipShifts();

			if (CurrentScriptLine.Skipped)
			{
				if (haveRecording)
				{
					SetThenInfo(currentRecordingInfo);

					_lblProblemSummary.Text = LocalizationManager.GetString("ScriptTextHasChangedControl.BlockSkippedButHasClip",
						"This block has been skipped, but it has a clip recorded.");
					SetDisplayForDeleteCleanupAction();
				}
				else
				{
					ShowNoProblemState(LocalizationManager.GetString(
						"ScriptTextHasChangedControl.BlockSkipped", "This block has been skipped."));
				}
			}
			else if (currentRecordingInfo == null || (!haveRecording && !haveBackup))
			{
				if (haveRecording)
				{
					// We have a clip, but we don't know anything about the script at the time it was recorded.
					_problemIcon.Image = AppPalette.ScriptUnknownIcon;
					_lblProblemSummary.Text = Format(LocalizationManager.GetString("ScriptTextHasChangedControl.ScriptTextAtTimeOfRecordingUnknown",
						"The clip for this block was recorded using an older version of {0} that did not save the version of the text at the time of recording.",
						"Param 0: \"HearThis\" (product name)"), ProductName);
					_lblRecordedDate.Text = Format(_fmtRecordedDate, ActualFileRecordingDateForUI);
				}
				else
				{
					if (haveBackup)
					{
						// REVIEW: In this case, do we still want to display the trash can icon? They have apparently
						// come back to this block sometime after making the decision to re-record. Since the old clip
						// has been deleted (backed up), showing the trash can icon probably makes some sense and it
						// is more consistent, but it *might* seem weird. Would it be better to just show the check
						// mark icon or no icon at all?
						Debug.Fail("REVIEW: I don't think we can get here!");
						ShowResolution(ReadyToReRecordMessage, _btnDelete);
					}
					else
					{
						ShowNoProblemState(LocalizationManager.GetString(
							"ScriptTextHasChangedControl.NotRecorded", "This block has not yet been recorded."));
					}
				}
			}
			else
			{
				SetThenInfo(currentRecordingInfo);

				if (_txtNow.Text != currentRecordingInfo.Text ||
				    currentRecordingInfo.OriginalText != null ||
				    haveBackup)
				{
					_lblProblemSummary.Text = _standardProblemText;
					if (haveBackup)
					{
						// REVIEW: In this case, do we still want to display the trash can icon? They have apparently
						// come back to this block sometime after making the decision to re-record. Since the old clip
						// has been deleted (backed up), showing the trash can icon probably makes some sense and it
						// is more consistent, but it *might* seem weird. Would it be better to just show the check
						// mark icon or no icon at all?
						ShowResolution(ReadyToReRecordMessage, _btnDelete);
					}
					else if (currentRecordingInfo.OriginalText != null && _txtNow.Text != currentRecordingInfo.OriginalText)
					{
						ShowResolution(_okayResolutionText, _btnUseExisting);
					}
				}
				else
				{
					_lblNow.Visible = _panelThen.Visible = false;

					Debug.Assert(_lastNullScriptLineIgnored == null);
					ShowNoProblemState(LocalizationManager.GetString(
						"ScriptTextHasChangedControl.NoProblem", "No problems"));
					_txtNow.Visible = false;
				}
			}

			ResumeLayout(true);
			Show();

			UpdateThenVsNowTableLayout();
			_tableBlockText.ResumeLayout();

			// REVIEW: Focus a specific control?
			if (_tableOptions.Visible)
				_tableOptions.Focus();
			else if (_audioButtonsControl.Enabled)
				_audioButtonsControl.Focus();

			_audioButtonsControl.UpdateDisplay();
			_updatingDisplay = false;
		}

		private void ShowNoProblemState(string stateDescription)
		{
			_tableOptions.Visible = _lblNow.Visible = false;
			_lblProblemSummary.Text = stateDescription;
			_problemIcon.Image = null;
		}

		private void ResetDisplayToProblemState()
		{
			_problemIcon.Image = AppPalette.AlertCircleIcon;
			_lblResolution.Visible = false;
			_rdoAskLater.Checked = true;
		}

		private void SetThenInfo(ScriptLine recordingInfo)
		{
			if (recordingInfo?.TextAsOriginallyRecorded == null)
				_txtThen.Visible = _panelThen.Visible = false;
			else
			{
				// If the two texts are the same, we will be hiding the "now" text in the calling code.
				if (_txtNow.Text.Length > 0 && _txtNow.Text != recordingInfo.TextAsOriginallyRecorded)
				{
					var diffs = new StringDifferenceFinder(recordingInfo.TextAsOriginallyRecorded, _txtNow.Text);
					SetRichText(_txtThen, diffs.OriginalStringDifferences);
					SetRichText(_txtNow, diffs.NewStringDifferences);
				}
				else
					_txtThen.Text = recordingInfo.TextAsOriginallyRecorded;
				_lblRecordedDate.Text = Format(_fmtRecordedDate, recordingInfo.RecordingTime.ToLocalTime().ToShortDateString());
				_txtThen.Visible = true;
			}
		}

		private void SetRichText(RichTextBox box, IEnumerable<StringDifferenceSegment> segments)
		{
			box.Text = Empty;
			int start = 0;

			foreach (var seg in segments)
			{
				// append the text to the RichTextBox control
				box.AppendText(seg.Text);
				int end = box.TextLength;

				// select the new text
				box.Select(start, end - start);
				// set the attributes of the new text
				box.SelectionColor = GetSegmentColor(seg.Type, box.ForeColor);
				// unselect
				box.Select(end, 0);
				start = end;
			}
		}

		private Color GetSegmentColor(DifferenceType segType, Color defaultColor)
		{
			switch (segType)
			{
				case DifferenceType.Same:
					return defaultColor;
				case DifferenceType.Addition:
					return AppPalette.Recording;
				case DifferenceType.Deletion:
					return AppPalette.Red;
				default:
					throw new ArgumentOutOfRangeException(nameof(segType), segType, null);
			}
		}

		private void ShowNextButtonIfThereAreMoreProblemsInChapter()
		{
			_nextButton.Visible = CurrentChapterInfo.GetIndexOfNextUnfilteredBlockWithProblem(
				_project.SelectedScriptBlock) > _project.SelectedScriptBlock;
			_nextButton.Invalidate();
		}

		private void SetDisplayForDeleteCleanupAction()
		{
			_rdoUseExisting.Visible = _btnUseExisting.Visible = false;
			_btnDelete.Text = LocalizationManager.GetString("ScriptTextHasChangedControl.DeleteExtraClipExplanation",
				"Delete clip");
			_rdoReRecord.Visible = _btnDelete.Visible = true;
		}

		private void ShowResolution(string labelText, RadioButtonHelperButton buttonWithIcon)
		{
			Debug.Assert(!IsNullOrWhiteSpace(labelText ));
			_lblResolution.Visible = true;
			_lblResolution.Text = labelText;
			// This will usually be the case, but if we are called from UpdateDisplay (i.e.,
			// initially setting up the state for the block and the block is already in a
			// resolved state, this will ensure the corretc option is selected.
			buttonWithIcon.CorrespondingRadioButton.Checked = true;
			_problemIcon.Image = AppPalette.CurrentColorScheme == ColorScheme.HighContrast ?
				buttonWithIcon.HighContrastMouseOverImage : buttonWithIcon.MouseOverImage;
			_pnlPlayClip.Visible = HaveScript ? GetHasRecordedClip(_project.SelectedScriptBlock) :
				Exists(ExtraRecordings[_indexIntoExtraRecordings].ClipFile);
			ProblemIgnoreStateChanged?.Invoke(this, EventArgs.Empty);
		}

		private void UpdateDisplayForExtraRecording()
		{
			SetDisplayForDeleteCleanupAction();

			Show();
			ResetDisplayToProblemState();

			var extraRecording = ExtraRecordings[_indexIntoExtraRecordings];

			Debug.Assert(Exists(extraRecording.ClipFile));
			_pnlPlayClip.Visible = true;
			_lblProblemSummary.Text = LocalizationManager.GetString("ScriptTextHasChangedControl.ExtraClip",
				"This is an extra clip that does not correspond to any block in the current script.");

			SetThenInfo(extraRecording.RecordingInfo);

			_lblNow.Visible = _txtNow.Visible = false;

			UpdateThenVsNowTableLayout();
		}

		private void UpdateThenVsNowTableLayout()
		{
			if (!_txtThen.Visible)
				_txtThen.Text = "";
			if (!_txtNow.Visible)
				_txtNow.Text = "";

			if (_txtThen.Text.Length == 0)
			{
				if (_txtNow.Text.Length == 0)
				{
					return;
				}

				_tableBlockText.ColumnStyles[0].Width = 0;
			}
			else
			{
				_tableBlockText.ColumnStyles[0].Width = 50;
			}

			_tableBlockText.ColumnStyles[1].Width = _txtNow.Text.Length == 0 ?
				0 : 50;
		}

		/// <summary>
		/// This invokes the message filter that allows the control to interpret various keystrokes as button presses.
		/// </summary>
		public void StartFilteringMessages()
		{
			Application.AddMessageFilter(this);
		}

		public void StopFilteringMessages()
		{
			Application.RemoveMessageFilter(this);
		}

		/// <summary>
		/// Filter out all keystrokes except the few that we want to handle.
		/// We handle Space, TAB, PageUp, PageDown, Delete and Arrow keys.
		/// </summary>
		/// <remarks>This is invoked because we implement IMessageFilter and call Application.AddMessageFilter(this)</remarks>
		public bool PreFilterMessage(ref Message m)
		{
			const int WM_KEYDOWN = 0x100;

			if (m.Msg != WM_KEYDOWN)
				return false;

			switch ((Keys)m.WParam)
			{
				case Keys.OemPeriod:
				case Keys.Decimal:
					RecordingToolControl.ShowPlayShortcutMessage();
					break;

				case Keys.Tab:
					_audioButtonsControl.OnPlay(this, null);
					break;

				case Keys.Delete:
					if (_rdoReRecord.Visible && !_rdoReRecord.Checked)
						_rdoReRecord.Checked = true;
					break;

				default:
					return false;
			}

			return true;
		}

		public float ZoomFactor
		{
			get => s_zoomFactor;
			set
			{
				s_zoomFactor = value;
				SetScriptFonts();
			}
		}

		private void DeleteClip()
		{
			try
			{
				_audioButtonsControl.ReleaseFile();
				_project.DeleteClipForSelectedBlock(ExtraRecordings);
				ShowResolution((_rdoUseExisting.Visible) ?
						LocalizationManager.GetString("ScriptTextHasChangedControl.Decision.ReRecord", "Decision: Need to re-record this block.") :
						LocalizationManager.GetString("ScriptTextHasChangedControl.DeletedExtraRecording",
							"This problem has been resolved (extra clip deleted)."),
					_btnDelete);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
				ErrorReport.ReportNonFatalException(exception);
				_updatingDisplay = true; // Prevent side-effect of selecting "Ask later"
				_rdoAskLater.Checked = true;
				ResetDisplayToProblemState();
				ProblemIgnoreStateChanged?.Invoke(this, EventArgs.Empty);
				_updatingDisplay = false;
			}
		}

		private void UseExistingClip()
		{
			// "Ignore" this problem.
			var scriptLine = CurrentRecordingInfo;
			if (scriptLine == null)
			{
				scriptLine = CurrentScriptLine;
				scriptLine.RecordingTime = ActualFileRecordingTime;
				_lastNullScriptLineIgnored = CurrentScriptLine;
			}
			else
			{
				if (!GetHasRecordedClip(_project.SelectedScriptBlock))
				{
					if (!_project.UndeleteClipForSelectedBlock())
					{
						Debug.Fail("Either we are in a bad state, or the Move failed.");
						_updatingDisplay = true; // Prevent side-effect of selecting "Ask later"
						_rdoReRecord.Checked = true;
						_updatingDisplay = false;
						return;
					}
				}

				scriptLine.OriginalText = scriptLine.Text;
				scriptLine.Text = CurrentScriptLine.Text;
				CurrentChapterInfo.OnScriptBlockRecorded(scriptLine);
			}

			ShowResolution(_okayResolutionText, _btnUseExisting);
		}

		private void RevertToProblemState()
		{
			ResetDisplayToProblemState();
			ProblemIgnoreStateChanged?.Invoke(this, EventArgs.Empty);

			if (_project.UndeleteClipForSelectedBlock())
				return;

			// Un-ignore.
			if (_lastNullScriptLineIgnored == CurrentScriptLine)
			{
				CurrentChapterInfo.RemoveRecordingInfo(_lastNullScriptLineIgnored);
				_lastNullScriptLineIgnored = null;
			}
			else
			{
				var scriptLine = CurrentRecordingInfo;
				scriptLine.Text = scriptLine.OriginalText;
				scriptLine.OriginalText = null;
				CurrentChapterInfo.OnScriptBlockRecorded(scriptLine);
			}
		}

		private bool GetHasRecordedClip(int i) => ClipRepository.GetHaveClipUnfiltered(_project.Name, _project.SelectedBook.Name,
			CurrentChapterInfo.ChapterNumber1Based, i);

		private void DeterminePossibleClipShifts()
		{
			if (!GetHasRecordedClip(_project.SelectedScriptBlock))
			{
				_shiftClipsViewModel = null;
				_btnShiftClips.Visible = _flowNearbyText.Visible = false;
				return;
			}

			_shiftClipsViewModel = new ShiftClipsViewModel(_project);
			_btnShiftClips.Visible = _flowNearbyText.Visible = _shiftClipsViewModel.CanShift;
		}

		private void _btnShiftClips_Click(object sender, EventArgs e)
		{
			using (var dlg = new ShiftClipsDlg(_shiftClipsViewModel))
			{
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					if (_indexIntoExtraRecordings >= 0 && _indexIntoExtraRecordings < ExtraRecordings.Count)
					{
						var lineNumberOfLastRealBlock = _project.SelectedChapterInfo.GetScriptBlockCount() - 1;
						var scriptLine = _project.ScriptProvider.GetBlock(_project.SelectedBook.BookNumber,
							_project.SelectedChapterInfo.ChapterNumber1Based, lineNumberOfLastRealBlock);
						scriptLine.RecordingTime = GetActualClipRecordingTime(lineNumberOfLastRealBlock);
						CurrentChapterInfo.OnScriptBlockRecorded(scriptLine);
						ExtraClipCountChanged?.Invoke(this, EventArgs.Empty);
					}
					else
					{
						ProblemIgnoreStateChanged?.Invoke(this, EventArgs.Empty);
						UpdateState();
					}
				}
			}
		}

		private void PaintRoundedBorder(object sender, PaintEventArgs e)
		{
			var ctrl = (Control)sender;
			DrawRoundedRectangle(ctrl, e, ctrl.ForeColor);
		}

		private void _pnlPlayClip_Paint(object sender, PaintEventArgs e)
		{
			DrawRoundedRectangle(_pnlPlayClip, e, _btnPlayClip.ForeColor);
		}

		private void DrawRoundedRectangle(Control ctrl, PaintEventArgs e, Color color)
		{
			var rect = ctrl.ClientRectangle;
			rect.Width--;
			rect.Height--;
			e.Graphics.DrawRoundedRectangle(color, rect, 8);
		}

		private void _rdoAskLater_CheckedChanged(object sender, EventArgs e)
		{
			if (_rdoAskLater.Checked && !_updatingDisplay)
				RevertToProblemState();
		}

		private void _rdoUseExisting_CheckedChanged(object sender, EventArgs e)
		{
			if (_rdoUseExisting.Checked && !_updatingDisplay)
				UseExistingClip();
		}

		private void _rdoReRecord_CheckedChanged(object sender, EventArgs e)
		{
			if (_rdoReRecord.Checked && !_updatingDisplay)
				DeleteClip();
		}

		private void _btnPlayClip_Click(object sender, EventArgs e)
		{
			_audioButtonsControl.OnPlay(this, null);
		}

		private void _btnPlayClip_MouseEnter(object sender, EventArgs e)
		{
			_audioButtonsControl.SimulateMouseOverPlayButton();
			_btnPlayClip.ForeColor = AppPalette.HilightColor;
			_pnlPlayClip.Invalidate();
		}

		private void _btnPlayClip_MouseLeave(object sender, EventArgs e)
		{
			_audioButtonsControl.SimulateMouseOverPlayButton(false);
			_btnPlayClip.ForeColor = Color.DarkGray;
			_pnlPlayClip.Invalidate();
		}

		private void _btnShiftClips_MouseEnter(object sender, EventArgs e)
		{
			_btnShiftClips.ForeColor = AppPalette.HilightColor;
		}

		private void _btnShiftClips_MouseLeave(object sender, EventArgs e)
		{
			_btnShiftClips.ForeColor = Color.DarkGray;
		}
	}
}
