namespace AudioToSerial
{
	partial class AudioApp
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			serialsCombo = new ComboBox();
			refreshSerialButton = new Button();
			bindButton = new Button();
			label1 = new Label();
			waveViewer = new NAudio.Gui.WaveViewer();
			lowTextBox = new TextBox();
			midTextBox = new TextBox();
			highTextBox = new TextBox();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			SuspendLayout();
			// 
			// serialsCombo
			// 
			serialsCombo.FormattingEnabled = true;
			serialsCombo.Location = new Point(12, 27);
			serialsCombo.Margin = new Padding(3, 3, 18, 3);
			serialsCombo.Name = "serialsCombo";
			serialsCombo.Size = new Size(224, 23);
			serialsCombo.TabIndex = 1;
			// 
			// refreshSerialButton
			// 
			refreshSerialButton.Location = new Point(12, 56);
			refreshSerialButton.Margin = new Padding(3, 3, 18, 3);
			refreshSerialButton.Name = "refreshSerialButton";
			refreshSerialButton.Size = new Size(224, 23);
			refreshSerialButton.TabIndex = 2;
			refreshSerialButton.Text = "Refresh";
			refreshSerialButton.UseVisualStyleBackColor = true;
			refreshSerialButton.Click += refreshSerialButton_Click;
			// 
			// bindButton
			// 
			bindButton.Location = new Point(12, 85);
			bindButton.Margin = new Padding(3, 3, 18, 3);
			bindButton.Name = "bindButton";
			bindButton.Size = new Size(224, 23);
			bindButton.TabIndex = 3;
			bindButton.Text = "Bind";
			bindButton.UseVisualStyleBackColor = true;
			bindButton.Click += bindButton_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 9);
			label1.Name = "label1";
			label1.Size = new Size(60, 15);
			label1.TabIndex = 6;
			label1.Text = "Serial Port";
			// 
			// waveViewer
			// 
			waveViewer.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			waveViewer.BackColor = SystemColors.ActiveCaption;
			waveViewer.BorderStyle = BorderStyle.FixedSingle;
			waveViewer.Location = new Point(12, 138);
			waveViewer.Name = "waveViewer";
			waveViewer.SamplesPerPixel = 128;
			waveViewer.Size = new Size(542, 188);
			waveViewer.StartPosition = 0L;
			waveViewer.TabIndex = 9;
			waveViewer.WaveStream = null;
			// 
			// lowTextBox
			// 
			lowTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lowTextBox.Location = new Point(323, 27);
			lowTextBox.Name = "lowTextBox";
			lowTextBox.ReadOnly = true;
			lowTextBox.Size = new Size(231, 23);
			lowTextBox.TabIndex = 10;
			// 
			// midTextBox
			// 
			midTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			midTextBox.Location = new Point(323, 56);
			midTextBox.Name = "midTextBox";
			midTextBox.ReadOnly = true;
			midTextBox.Size = new Size(231, 23);
			midTextBox.TabIndex = 11;
			// 
			// highTextBox
			// 
			highTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			highTextBox.Location = new Point(323, 85);
			highTextBox.Name = "highTextBox";
			highTextBox.ReadOnly = true;
			highTextBox.Size = new Size(231, 23);
			highTextBox.TabIndex = 12;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(279, 30);
			label2.Name = "label2";
			label2.Size = new Size(29, 15);
			label2.TabIndex = 13;
			label2.Text = "Low";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new Point(279, 59);
			label3.Name = "label3";
			label3.Size = new Size(28, 15);
			label3.TabIndex = 14;
			label3.Text = "Mid";
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new Point(279, 89);
			label4.Name = "label4";
			label4.Size = new Size(33, 15);
			label4.TabIndex = 15;
			label4.Text = "High";
			// 
			// AudioApp
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(566, 338);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(highTextBox);
			Controls.Add(midTextBox);
			Controls.Add(lowTextBox);
			Controls.Add(waveViewer);
			Controls.Add(label1);
			Controls.Add(bindButton);
			Controls.Add(refreshSerialButton);
			Controls.Add(serialsCombo);
			Name = "AudioApp";
			Text = "Audio2Serial";
			FormClosing += AudioApp_FormClosing;
			Load += AudioApp_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox serialsCombo;
		private Button refreshSerialButton;
		private Button bindButton;
		private Label label1;
		private NAudio.Gui.WaveViewer waveViewer;
		private TextBox lowTextBox;
		private TextBox midTextBox;
		private TextBox highTextBox;
		private Label label2;
		private Label label3;
		private Label label4;
	}
}
