namespace HearThis.Publishing
{
    partial class PublishDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
				components?.Dispose();
			base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label4;
            this._cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this._openFolderLink = new System.Windows.Forms.LinkLabel();
            this._publishButton = new System.Windows.Forms.Button();
            this._destinationLabel = new System.Windows.Forms.Label();
            this._changeDestinationLink = new System.Windows.Forms.LinkLabel();
            this._logBox = new SIL.Windows.Forms.Progress.LogBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._scrAppBuilderRadio = new System.Windows.Forms.RadioButton();
            this._flacRadio = new System.Windows.Forms.RadioButton();
            this._mp3Radio = new System.Windows.Forms.RadioButton();
            this._saberRadio = new System.Windows.Forms.RadioButton();
            this._megaVoiceRadio = new System.Windows.Forms.RadioButton();
            this._audiBibleRadio = new System.Windows.Forms.RadioButton();
            this._kulumiRadio = new System.Windows.Forms.RadioButton();
            this._audacityLabelFile = new System.Windows.Forms.RadioButton();
            this._oggOpusRadio = new System.Windows.Forms.RadioButton();
            this._oggVorbisRadio = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanelAudioFormat = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this._tableLayoutRight = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelVerseIndexFormat = new System.Windows.Forms.TableLayoutPanel();
            this._none = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this._cueSheet = new System.Windows.Forms.RadioButton();
            this._includePhraseLevelLabels = new System.Windows.Forms.CheckBox();
            this._tableLayoutPanelBooksToPublish = new System.Windows.Forms.TableLayoutPanel();
            this._rdoCurrentBook = new System.Windows.Forms.RadioButton();
            this._lblBooksToPublish = new System.Windows.Forms.Label();
            this._rdoAllBooks = new System.Windows.Forms.RadioButton();
            this.l10NSharpExtender1 = new L10NSharp.UI.L10NSharpExtender(this.components);
            label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanelAudioFormat.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this._tableLayoutRight.SuspendLayout();
            this.tableLayoutPanelVerseIndexFormat.SuspendLayout();
            this._tableLayoutPanelBooksToPublish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.l10NSharpExtender1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.l10NSharpExtender1.SetLocalizableToolTip(label4, null);
            this.l10NSharpExtender1.SetLocalizationComment(label4, null);
            this.l10NSharpExtender1.SetLocalizingId(label4, "PublishDialog.PublishDialog.label4");
            label4.Location = new System.Drawing.Point(181, 270);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(180, 2);
            label4.TabIndex = 17;
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.l10NSharpExtender1.SetLocalizableToolTip(this._cancelButton, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._cancelButton, null);
            this.l10NSharpExtender1.SetLocalizingId(this._cancelButton, "PublishDialog.PublishDialog._cancelButton");
            this._cancelButton.Location = new System.Drawing.Point(584, 288);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 33);
            this._cancelButton.TabIndex = 10;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this.label2, null);
            this.l10NSharpExtender1.SetLocalizationComment(this.label2, null);
            this.l10NSharpExtender1.SetLocalizingId(this.label2, "PublishDialog.PublishDialog.label2");
            this.label2.Location = new System.Drawing.Point(26, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Destination";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // _openFolderLink
            // 
            this._openFolderLink.AutoEllipsis = true;
            this._openFolderLink.AutoSize = true;
            this._openFolderLink.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._openFolderLink, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._openFolderLink, null);
            this.l10NSharpExtender1.SetLocalizingId(this._openFolderLink, "PublishDialog.PublishDialog._openFolderLink");
            this._openFolderLink.Location = new System.Drawing.Point(27, 304);
            this._openFolderLink.MaximumSize = new System.Drawing.Size(386, 17);
            this._openFolderLink.Name = "_openFolderLink";
            this._openFolderLink.Size = new System.Drawing.Size(189, 17);
            this._openFolderLink.TabIndex = 8;
            this._openFolderLink.TabStop = true;
            this._openFolderLink.Text = "Open folder of exported audio";
            this._openFolderLink.Visible = false;
            this._openFolderLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._openFolderLink_LinkClicked);
            // 
            // _publishButton
            // 
            this._publishButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._publishButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._publishButton, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._publishButton, null);
            this.l10NSharpExtender1.SetLocalizingId(this._publishButton, "PublishDialog.PublishDialog._publishButton");
            this._publishButton.Location = new System.Drawing.Point(499, 288);
            this._publishButton.Name = "_publishButton";
            this._publishButton.Size = new System.Drawing.Size(80, 33);
            this._publishButton.TabIndex = 9;
            this._publishButton.Text = "&Export";
            this._publishButton.UseVisualStyleBackColor = true;
            this._publishButton.Click += new System.EventHandler(this._publishButton_Click);
            // 
            // _destinationLabel
            // 
            this._destinationLabel.AutoEllipsis = true;
            this._destinationLabel.AutoSize = true;
            this._destinationLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._destinationLabel, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._destinationLabel, null);
            this.l10NSharpExtender1.SetLocalizingId(this._destinationLabel, "PublishDialog.PublishDialog._destinationLabel");
            this._destinationLabel.Location = new System.Drawing.Point(27, 304);
            this._destinationLabel.MaximumSize = new System.Drawing.Size(386, 17);
            this._destinationLabel.Name = "_destinationLabel";
            this._destinationLabel.Size = new System.Drawing.Size(64, 17);
            this._destinationLabel.TabIndex = 8;
            this._destinationLabel.Text = "C:\\foobar";
            // 
            // _changeDestinationLink
            // 
            this._changeDestinationLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._changeDestinationLink.AutoSize = true;
            this.l10NSharpExtender1.SetLocalizableToolTip(this._changeDestinationLink, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._changeDestinationLink, null);
            this.l10NSharpExtender1.SetLocalizingId(this._changeDestinationLink, "PublishDialog.PublishDialog._changeDestinationLink");
            this._changeDestinationLink.Location = new System.Drawing.Point(251, 288);
            this._changeDestinationLink.Name = "_changeDestinationLink";
            this._changeDestinationLink.Size = new System.Drawing.Size(109, 13);
            this._changeDestinationLink.TabIndex = 7;
            this._changeDestinationLink.TabStop = true;
            this._changeDestinationLink.Text = "Change Destination...";
            this._changeDestinationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._changeDestinationLink_LinkClicked);
            // 
            // _logBox
            // 
            this._logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._logBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._logBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(173)))), ((int)(((byte)(179)))));
            this._logBox.CancelRequested = false;
            this._logBox.ErrorEncountered = false;
            this._logBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._logBox.GetDiagnosticsMethod = null;
            this.l10NSharpExtender1.SetLocalizableToolTip(this._logBox, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._logBox, null);
            this.l10NSharpExtender1.SetLocalizingId(this._logBox, "PublishDialog.PublishDialog.LogBox");
            this._logBox.Location = new System.Drawing.Point(30, 328);
            this._logBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._logBox.MaxLength = 715827882;
            this._logBox.MaxLengthErrorMessage = "Maximum length exceeded!";
            this._logBox.Name = "_logBox";
            this._logBox.ProgressIndicator = null;
            this._logBox.ShowCopyToClipboardMenuItem = false;
            this._logBox.ShowDetailsMenuItem = false;
            this._logBox.ShowDiagnosticsMenuItem = false;
            this._logBox.ShowFontMenuItem = false;
            this._logBox.ShowMenu = true;
            this._logBox.Size = new System.Drawing.Size(640, 189);
            this._logBox.TabIndex = 11;
            // 
            // _scrAppBuilderRadio
            // 
            this._scrAppBuilderRadio.AutoSize = true;
            this._scrAppBuilderRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.l10NSharpExtender1.SetLocalizableToolTip(this._scrAppBuilderRadio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._scrAppBuilderRadio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._scrAppBuilderRadio, "PublishDialog.PublishDialog._scrAppBuilderRadio");
            this._scrAppBuilderRadio.Location = new System.Drawing.Point(3, 104);
            this._scrAppBuilderRadio.Name = "_scrAppBuilderRadio";
            this._scrAppBuilderRadio.Size = new System.Drawing.Size(150, 21);
            this._scrAppBuilderRadio.TabIndex = 16;
            this._scrAppBuilderRadio.Text = "Scripture App Builder";
            this.toolTip1.SetToolTip(this._scrAppBuilderRadio, "https://software.sil.org/scriptureappbuilder/");
            this._scrAppBuilderRadio.UseVisualStyleBackColor = true;
            this._scrAppBuilderRadio.CheckedChanged += new System.EventHandler(this._scrAppBuilderRadio_CheckedChanged);
            // 
            // _flacRadio
            // 
            this._flacRadio.AutoSize = true;
            this._flacRadio.Checked = true;
            this._flacRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._flacRadio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._flacRadio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._flacRadio, "PublishDialog.PublishDialog._flacRadio");
            this._flacRadio.Location = new System.Drawing.Point(3, 239);
            this._flacRadio.Name = "_flacRadio";
            this._flacRadio.Size = new System.Drawing.Size(117, 19);
            this._flacRadio.TabIndex = 5;
            this._flacRadio.TabStop = true;
            this._flacRadio.Text = "Folder of FLACs";
            this.toolTip1.SetToolTip(this._flacRadio, "https://xiph.org/flac/");
            this._flacRadio.UseVisualStyleBackColor = true;
            // 
            // _mp3Radio
            // 
            this._mp3Radio.AutoSize = true;
            this._mp3Radio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._mp3Radio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._mp3Radio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._mp3Radio, "PublishDialog.PublishDialog._mp3Radio");
            this._mp3Radio.Location = new System.Drawing.Point(3, 158);
            this._mp3Radio.Name = "_mp3Radio";
            this._mp3Radio.Size = new System.Drawing.Size(115, 21);
            this._mp3Radio.TabIndex = 3;
            this._mp3Radio.Text = "Folder of MP3s";
            this.toolTip1.SetToolTip(this._mp3Radio, "https://en.wikipedia.org/wiki/MP3");
            this._mp3Radio.UseVisualStyleBackColor = true;
            // 
            // _saberRadio
            // 
            this._saberRadio.AutoSize = true;
            this._saberRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._saberRadio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._saberRadio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._saberRadio, "PublishDialog.PublishDialog._saberRadio");
            this._saberRadio.Location = new System.Drawing.Point(3, 131);
            this._saberRadio.Name = "_saberRadio";
            this._saberRadio.Size = new System.Drawing.Size(60, 21);
            this._saberRadio.TabIndex = 2;
            this._saberRadio.Text = "Saber";
            this.toolTip1.SetToolTip(this._saberRadio, "https://globalrecordings.net/en/saber");
            this._saberRadio.UseVisualStyleBackColor = true;
            // 
            // _megaVoiceRadio
            // 
            this._megaVoiceRadio.AutoSize = true;
            this._megaVoiceRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._megaVoiceRadio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._megaVoiceRadio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._megaVoiceRadio, "PublishDialog.PublishDialog._megaVoiceRadio");
            this._megaVoiceRadio.Location = new System.Drawing.Point(3, 50);
            this._megaVoiceRadio.Name = "_megaVoiceRadio";
            this._megaVoiceRadio.Size = new System.Drawing.Size(91, 21);
            this._megaVoiceRadio.TabIndex = 1;
            this._megaVoiceRadio.Text = "MegaVoice";
            this.toolTip1.SetToolTip(this._megaVoiceRadio, "https://www.megavoice.com/");
            this._megaVoiceRadio.UseVisualStyleBackColor = true;
            // 
            // _audiBibleRadio
            // 
            this._audiBibleRadio.AutoSize = true;
            this._audiBibleRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._audiBibleRadio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._audiBibleRadio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._audiBibleRadio, "PublishDialog.PublishDialog._audiBibleRadio");
            this._audiBibleRadio.Location = new System.Drawing.Point(3, 23);
            this._audiBibleRadio.Name = "_audiBibleRadio";
            this._audiBibleRadio.Size = new System.Drawing.Size(80, 21);
            this._audiBibleRadio.TabIndex = 0;
            this._audiBibleRadio.Text = "AudiBible";
            this.toolTip1.SetToolTip(this._audiBibleRadio, "https://www.davarpartners.com/audibible/");
            this._audiBibleRadio.UseVisualStyleBackColor = true;
            // 
            // _kulumiRadio
            // 
            this._kulumiRadio.AutoSize = true;
            this._kulumiRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._kulumiRadio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._kulumiRadio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._kulumiRadio, "PublishDialog.PublishDialog.kulumiRadio");
            this._kulumiRadio.Location = new System.Drawing.Point(3, 77);
            this._kulumiRadio.Name = "_kulumiRadio";
            this._kulumiRadio.Size = new System.Drawing.Size(65, 21);
            this._kulumiRadio.TabIndex = 17;
            this._kulumiRadio.Text = "Kulumi";
            this.toolTip1.SetToolTip(this._kulumiRadio, "https://xiph.org/flac/");
            this._kulumiRadio.UseVisualStyleBackColor = true;
            // 
            // _audacityLabelFile
            // 
            this._audacityLabelFile.AutoSize = true;
            this._audacityLabelFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._audacityLabelFile, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._audacityLabelFile, "Param 0: \"Scripture App Builder\" (product name); Param 1: \"Audacity\" (product nam" +
        "e)");
            this.l10NSharpExtender1.SetLocalizingId(this._audacityLabelFile, "PublishDialog._audacityLabelFile");
            this._audacityLabelFile.Location = new System.Drawing.Point(18, 50);
            this._audacityLabelFile.Name = "_audacityLabelFile";
            this._audacityLabelFile.Size = new System.Drawing.Size(126, 21);
            this._audacityLabelFile.TabIndex = 3;
            this._audacityLabelFile.Text = "{1} Label File ({0})";
            this.toolTip1.SetToolTip(this._audacityLabelFile, "https://manual.audacityteam.org/man/label_tracks.html");
            this._audacityLabelFile.UseVisualStyleBackColor = true;
            this._audacityLabelFile.CheckedChanged += new System.EventHandler(this._audacityLabelFile_CheckedChanged);
            // 
            // _oggOpusRadio
            // 
            this._oggOpusRadio.AutoSize = true;
            this._oggOpusRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._oggOpusRadio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._oggOpusRadio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._oggOpusRadio, "PublishDialog.PublishDialog._flacRadio");
            this._oggOpusRadio.Location = new System.Drawing.Point(3, 185);
            this._oggOpusRadio.Name = "_oggOpusRadio";
            this._oggOpusRadio.Size = new System.Drawing.Size(146, 21);
            this._oggOpusRadio.TabIndex = 18;
            this._oggOpusRadio.Text = "Folder of OGG Opus";
            this.toolTip1.SetToolTip(this._oggOpusRadio, "https://xiph.org/flac/");
            this._oggOpusRadio.UseVisualStyleBackColor = true;
            this._oggOpusRadio.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // _oggVorbisRadio
            // 
            this._oggVorbisRadio.AutoSize = true;
            this._oggVorbisRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._oggVorbisRadio, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._oggVorbisRadio, null);
            this.l10NSharpExtender1.SetLocalizingId(this._oggVorbisRadio, "PublishDialog.PublishDialog._flacRadio");
            this._oggVorbisRadio.Location = new System.Drawing.Point(3, 212);
            this._oggVorbisRadio.Name = "_oggVorbisRadio";
            this._oggVorbisRadio.Size = new System.Drawing.Size(166, 21);
            this._oggVorbisRadio.TabIndex = 19;
            this._oggVorbisRadio.Text = "Folder of OGGs (Vorbis)";
            this.toolTip1.SetToolTip(this._oggVorbisRadio, "https://xiph.org/flac/");
            this._oggVorbisRadio.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelAudioFormat
            // 
            this.tableLayoutPanelAudioFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelAudioFormat.AutoSize = true;
            this.tableLayoutPanelAudioFormat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelAudioFormat.ColumnCount = 1;
            this.tableLayoutPanelAudioFormat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelAudioFormat.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._audiBibleRadio, 0, 1);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._megaVoiceRadio, 0, 2);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._scrAppBuilderRadio, 0, 4);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._kulumiRadio, 0, 3);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._saberRadio, 0, 5);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._mp3Radio, 0, 6);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._oggOpusRadio, 0, 7);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._flacRadio, 0, 9);
            this.tableLayoutPanelAudioFormat.Controls.Add(this._oggVorbisRadio, 0, 8);
            this.tableLayoutPanelAudioFormat.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelAudioFormat.Name = "tableLayoutPanelAudioFormat";
            this.tableLayoutPanelAudioFormat.RowCount = 11;
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanelAudioFormat.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanelAudioFormat.Size = new System.Drawing.Size(172, 264);
            this.tableLayoutPanelAudioFormat.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this.label3, null);
            this.l10NSharpExtender1.SetLocalizationComment(this.label3, null);
            this.l10NSharpExtender1.SetLocalizingId(this.label3, "PublishDialog.PublishDialog.label3");
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Audio Format";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(label4, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this._tableLayoutRight, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelAudioFormat, 0, 0);
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(34, 8);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(629, 274);
            this.tableLayoutPanelMain.TabIndex = 16;
            // 
            // _tableLayoutRight
            // 
            this._tableLayoutRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tableLayoutRight.AutoSize = true;
            this._tableLayoutRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tableLayoutRight.ColumnCount = 1;
            this._tableLayoutRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutRight.Controls.Add(this.tableLayoutPanelVerseIndexFormat, 0, 0);
            this._tableLayoutRight.Controls.Add(this._tableLayoutPanelBooksToPublish, 0, 2);
            this._tableLayoutRight.Location = new System.Drawing.Point(367, 3);
            this._tableLayoutRight.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this._tableLayoutRight.Name = "_tableLayoutRight";
            this._tableLayoutRight.RowCount = 3;
            this._tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutRight.Size = new System.Drawing.Size(259, 267);
            this._tableLayoutRight.TabIndex = 2;
            // 
            // tableLayoutPanelVerseIndexFormat
            // 
            this.tableLayoutPanelVerseIndexFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelVerseIndexFormat.AutoSize = true;
            this.tableLayoutPanelVerseIndexFormat.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelVerseIndexFormat.ColumnCount = 1;
            this.tableLayoutPanelVerseIndexFormat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelVerseIndexFormat.Controls.Add(this._none, 0, 1);
            this.tableLayoutPanelVerseIndexFormat.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelVerseIndexFormat.Controls.Add(this._audacityLabelFile, 0, 2);
            this.tableLayoutPanelVerseIndexFormat.Controls.Add(this._cueSheet, 0, 4);
            this.tableLayoutPanelVerseIndexFormat.Controls.Add(this._includePhraseLevelLabels, 0, 3);
            this.tableLayoutPanelVerseIndexFormat.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelVerseIndexFormat.Name = "tableLayoutPanelVerseIndexFormat";
            this.tableLayoutPanelVerseIndexFormat.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.tableLayoutPanelVerseIndexFormat.RowCount = 5;
            this.tableLayoutPanelVerseIndexFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelVerseIndexFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelVerseIndexFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelVerseIndexFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelVerseIndexFormat.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelVerseIndexFormat.Size = new System.Drawing.Size(253, 128);
            this.tableLayoutPanelVerseIndexFormat.TabIndex = 1;
            // 
            // _none
            // 
            this._none.AutoSize = true;
            this._none.Checked = true;
            this._none.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._none, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._none, "");
            this.l10NSharpExtender1.SetLocalizingId(this._none, "PublishDialog._none");
            this._none.Location = new System.Drawing.Point(18, 23);
            this._none.Name = "_none";
            this._none.Size = new System.Drawing.Size(58, 21);
            this._none.TabIndex = 1;
            this._none.TabStop = true;
            this._none.Text = "None";
            this._none.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this.label1, null);
            this.l10NSharpExtender1.SetLocalizationComment(this.label1, null);
            this.l10NSharpExtender1.SetLocalizingId(this.label1, "PublishDialog.VerseIndexFormat");
            this.label1.Location = new System.Drawing.Point(18, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label1.Size = new System.Drawing.Size(232, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Verse Index Format";
            // 
            // _cueSheet
            // 
            this._cueSheet.AutoSize = true;
            this._cueSheet.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._cueSheet, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._cueSheet, "");
            this.l10NSharpExtender1.SetLocalizationPriority(this._cueSheet, L10NSharp.LocalizationPriority.NotLocalizable);
            this.l10NSharpExtender1.SetLocalizingId(this._cueSheet, "PublishDialog._cueSheet");
            this._cueSheet.Location = new System.Drawing.Point(18, 104);
            this._cueSheet.Name = "_cueSheet";
            this._cueSheet.Size = new System.Drawing.Size(84, 21);
            this._cueSheet.TabIndex = 2;
            this._cueSheet.Text = "Cue Sheet";
            this._cueSheet.UseVisualStyleBackColor = true;
            this._cueSheet.Visible = false;
            // 
            // _includePhraseLevelLabels
            // 
            this._includePhraseLevelLabels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._includePhraseLevelLabels.AutoSize = true;
            this._includePhraseLevelLabels.Enabled = false;
            this._includePhraseLevelLabels.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.l10NSharpExtender1.SetLocalizableToolTip(this._includePhraseLevelLabels, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._includePhraseLevelLabels, null);
            this.l10NSharpExtender1.SetLocalizingId(this._includePhraseLevelLabels, "PublishDialog._chkPhraseLevelLabels");
            this._includePhraseLevelLabels.Location = new System.Drawing.Point(18, 77);
            this._includePhraseLevelLabels.Name = "_includePhraseLevelLabels";
            this._includePhraseLevelLabels.Size = new System.Drawing.Size(232, 21);
            this._includePhraseLevelLabels.TabIndex = 18;
            this._includePhraseLevelLabels.Text = "Include labels for phrase-level clips";
            this._includePhraseLevelLabels.UseVisualStyleBackColor = true;
            this._includePhraseLevelLabels.CheckedChanged += new System.EventHandler(this._includePhraseLevelLabels_CheckedChanged);
            // 
            // _tableLayoutPanelBooksToPublish
            // 
            this._tableLayoutPanelBooksToPublish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tableLayoutPanelBooksToPublish.AutoSize = true;
            this._tableLayoutPanelBooksToPublish.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._tableLayoutPanelBooksToPublish.ColumnCount = 1;
            this._tableLayoutPanelBooksToPublish.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanelBooksToPublish.Controls.Add(this._rdoCurrentBook, 0, 2);
            this._tableLayoutPanelBooksToPublish.Controls.Add(this._lblBooksToPublish, 0, 0);
            this._tableLayoutPanelBooksToPublish.Controls.Add(this._rdoAllBooks, 0, 1);
            this._tableLayoutPanelBooksToPublish.Location = new System.Drawing.Point(3, 180);
            this._tableLayoutPanelBooksToPublish.Name = "_tableLayoutPanelBooksToPublish";
            this._tableLayoutPanelBooksToPublish.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this._tableLayoutPanelBooksToPublish.RowCount = 3;
            this._tableLayoutPanelBooksToPublish.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanelBooksToPublish.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanelBooksToPublish.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._tableLayoutPanelBooksToPublish.Size = new System.Drawing.Size(253, 84);
            this._tableLayoutPanelBooksToPublish.TabIndex = 2;
            // 
            // _rdoCurrentBook
            // 
            this._rdoCurrentBook.AutoSize = true;
            this._rdoCurrentBook.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.l10NSharpExtender1.SetLocalizableToolTip(this._rdoCurrentBook, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._rdoCurrentBook, null);
            this.l10NSharpExtender1.SetLocalizingId(this._rdoCurrentBook, "PublishDialog._rdoCurrentBook");
            this._rdoCurrentBook.Location = new System.Drawing.Point(18, 60);
            this._rdoCurrentBook.Name = "_rdoCurrentBook";
            this._rdoCurrentBook.Size = new System.Drawing.Size(124, 21);
            this._rdoCurrentBook.TabIndex = 21;
            this._rdoCurrentBook.TabStop = true;
            this._rdoCurrentBook.Text = "Current Book: {0}";
            this._rdoCurrentBook.UseVisualStyleBackColor = true;
            // 
            // _lblBooksToPublish
            // 
            this._lblBooksToPublish.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblBooksToPublish.AutoSize = true;
            this._lblBooksToPublish.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l10NSharpExtender1.SetLocalizableToolTip(this._lblBooksToPublish, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._lblBooksToPublish, null);
            this.l10NSharpExtender1.SetLocalizingId(this._lblBooksToPublish, "PublishDialog._lblBooksToPublish");
            this._lblBooksToPublish.Location = new System.Drawing.Point(18, 0);
            this._lblBooksToPublish.Name = "_lblBooksToPublish";
            this._lblBooksToPublish.Padding = new System.Windows.Forms.Padding(0, 10, 0, 3);
            this._lblBooksToPublish.Size = new System.Drawing.Size(232, 30);
            this._lblBooksToPublish.TabIndex = 19;
            this._lblBooksToPublish.Text = "Books to Export";
            // 
            // _rdoAllBooks
            // 
            this._rdoAllBooks.AutoSize = true;
            this._rdoAllBooks.Checked = true;
            this._rdoAllBooks.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.l10NSharpExtender1.SetLocalizableToolTip(this._rdoAllBooks, null);
            this.l10NSharpExtender1.SetLocalizationComment(this._rdoAllBooks, null);
            this.l10NSharpExtender1.SetLocalizingId(this._rdoAllBooks, "PublishDialog._rdoAllBooks");
            this._rdoAllBooks.Location = new System.Drawing.Point(18, 33);
            this._rdoAllBooks.Name = "_rdoAllBooks";
            this._rdoAllBooks.Size = new System.Drawing.Size(139, 21);
            this._rdoAllBooks.TabIndex = 20;
            this._rdoAllBooks.TabStop = true;
            this._rdoAllBooks.Text = "All books in project";
            this._rdoAllBooks.UseVisualStyleBackColor = true;
            // 
            // l10NSharpExtender1
            // 
            this.l10NSharpExtender1.LocalizationManagerId = "HearThis";
            this.l10NSharpExtender1.PrefixForNewItems = "PublishDialog";
            // 
            // PublishDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(700, 539);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this._changeDestinationLink);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._openFolderLink);
            this.Controls.Add(this._logBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._destinationLabel);
            this.Controls.Add(this._publishButton);
            this.l10NSharpExtender1.SetLocalizableToolTip(this, null);
            this.l10NSharpExtender1.SetLocalizationComment(this, null);
            this.l10NSharpExtender1.SetLocalizingId(this, "PublishDialog.WindowTitle");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(548, 492);
            this.Name = "PublishDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Export Sound Files";
            this.tableLayoutPanelAudioFormat.ResumeLayout(false);
            this.tableLayoutPanelAudioFormat.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this._tableLayoutRight.ResumeLayout(false);
            this._tableLayoutRight.PerformLayout();
            this.tableLayoutPanelVerseIndexFormat.ResumeLayout(false);
            this.tableLayoutPanelVerseIndexFormat.PerformLayout();
            this._tableLayoutPanelBooksToPublish.ResumeLayout(false);
            this._tableLayoutPanelBooksToPublish.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.l10NSharpExtender1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.Button _cancelButton;
		private L10NSharp.UI.L10NSharpExtender l10NSharpExtender1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel _openFolderLink;
		private System.Windows.Forms.Button _publishButton;
		private System.Windows.Forms.Label _destinationLabel;
		private System.Windows.Forms.LinkLabel _changeDestinationLink;
		private SIL.Windows.Forms.Progress.LogBox _logBox;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAudioFormat;
		private System.Windows.Forms.RadioButton _kulumiRadio;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton _audiBibleRadio;
		private System.Windows.Forms.RadioButton _megaVoiceRadio;
		private System.Windows.Forms.RadioButton _saberRadio;
		private System.Windows.Forms.RadioButton _mp3Radio;
		private System.Windows.Forms.RadioButton _flacRadio;
		private System.Windows.Forms.RadioButton _scrAppBuilderRadio;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
		private System.Windows.Forms.TableLayoutPanel _tableLayoutRight;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelVerseIndexFormat;
		private System.Windows.Forms.RadioButton _none;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton _audacityLabelFile;
		private System.Windows.Forms.RadioButton _cueSheet;
		private System.Windows.Forms.CheckBox _includePhraseLevelLabels;
		private System.Windows.Forms.TableLayoutPanel _tableLayoutPanelBooksToPublish;
		private System.Windows.Forms.RadioButton _rdoCurrentBook;
		private System.Windows.Forms.Label _lblBooksToPublish;
		private System.Windows.Forms.RadioButton _rdoAllBooks;
		private System.Windows.Forms.RadioButton _oggVorbisRadio;
		private System.Windows.Forms.RadioButton _oggOpusRadio;
	}
}
