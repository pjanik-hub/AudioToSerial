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
			minLowTxtBox = new TextBox();
			label5 = new Label();
			label6 = new Label();
			maxLowTxtBox = new TextBox();
			label7 = new Label();
			maxMidTxtBox = new TextBox();
			label8 = new Label();
			minMidTxtBox = new TextBox();
			label9 = new Label();
			maxHighTxtBox = new TextBox();
			label10 = new Label();
			minHighTxtBox = new TextBox();
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
			waveViewer.Location = new Point(12, 307);
			waveViewer.Name = "waveViewer";
			waveViewer.SamplesPerPixel = 128;
			waveViewer.Size = new Size(381, 188);
			waveViewer.StartPosition = 0L;
			waveViewer.TabIndex = 9;
			waveViewer.WaveStream = null;
			// 
			// lowTextBox
			// 
			lowTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lowTextBox.Location = new Point(259, 27);
			lowTextBox.Name = "lowTextBox";
			lowTextBox.ReadOnly = true;
			lowTextBox.Size = new Size(134, 23);
			lowTextBox.TabIndex = 10;
			// 
			// midTextBox
			// 
			midTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			midTextBox.Location = new Point(259, 115);
			midTextBox.Name = "midTextBox";
			midTextBox.ReadOnly = true;
			midTextBox.Size = new Size(134, 23);
			midTextBox.TabIndex = 11;
			// 
			// highTextBox
			// 
			highTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			highTextBox.Location = new Point(259, 203);
			highTextBox.Name = "highTextBox";
			highTextBox.ReadOnly = true;
			highTextBox.Size = new Size(134, 23);
			highTextBox.TabIndex = 12;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(259, 9);
			label2.Name = "label2";
			label2.Size = new Size(29, 15);
			label2.TabIndex = 13;
			label2.Text = "Low";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new Point(259, 97);
			label3.Name = "label3";
			label3.Size = new Size(28, 15);
			label3.TabIndex = 14;
			label3.Text = "Mid";
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new Point(259, 185);
			label4.Name = "label4";
			label4.Size = new Size(33, 15);
			label4.TabIndex = 15;
			label4.Text = "High";
			// 
			// minLowTxtBox
			// 
			minLowTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			minLowTxtBox.Location = new Point(259, 71);
			minLowTxtBox.Margin = new Padding(3, 3, 3, 18);
			minLowTxtBox.Name = "minLowTxtBox";
			minLowTxtBox.ReadOnly = true;
			minLowTxtBox.Size = new Size(64, 23);
			minLowTxtBox.TabIndex = 16;
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Location = new Point(259, 53);
			label5.Name = "label5";
			label5.Size = new Size(28, 15);
			label5.TabIndex = 17;
			label5.Text = "Min";
			// 
			// label6
			// 
			label6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label6.AutoSize = true;
			label6.Location = new Point(329, 53);
			label6.Name = "label6";
			label6.Size = new Size(30, 15);
			label6.TabIndex = 19;
			label6.Text = "Max";
			// 
			// maxLowTxtBox
			// 
			maxLowTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			maxLowTxtBox.Location = new Point(329, 71);
			maxLowTxtBox.Margin = new Padding(3, 3, 3, 18);
			maxLowTxtBox.Name = "maxLowTxtBox";
			maxLowTxtBox.ReadOnly = true;
			maxLowTxtBox.Size = new Size(64, 23);
			maxLowTxtBox.TabIndex = 18;
			// 
			// label7
			// 
			label7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label7.AutoSize = true;
			label7.Location = new Point(329, 141);
			label7.Name = "label7";
			label7.Size = new Size(30, 15);
			label7.TabIndex = 23;
			label7.Text = "Max";
			// 
			// maxMidTxtBox
			// 
			maxMidTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			maxMidTxtBox.Location = new Point(329, 159);
			maxMidTxtBox.Margin = new Padding(3, 3, 3, 18);
			maxMidTxtBox.Name = "maxMidTxtBox";
			maxMidTxtBox.ReadOnly = true;
			maxMidTxtBox.Size = new Size(64, 23);
			maxMidTxtBox.TabIndex = 22;
			// 
			// label8
			// 
			label8.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label8.AutoSize = true;
			label8.Location = new Point(259, 141);
			label8.Name = "label8";
			label8.Size = new Size(28, 15);
			label8.TabIndex = 21;
			label8.Text = "Min";
			// 
			// minMidTxtBox
			// 
			minMidTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			minMidTxtBox.Location = new Point(259, 159);
			minMidTxtBox.Margin = new Padding(3, 3, 3, 18);
			minMidTxtBox.Name = "minMidTxtBox";
			minMidTxtBox.ReadOnly = true;
			minMidTxtBox.Size = new Size(64, 23);
			minMidTxtBox.TabIndex = 20;
			// 
			// label9
			// 
			label9.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label9.AutoSize = true;
			label9.Location = new Point(329, 229);
			label9.Name = "label9";
			label9.Size = new Size(30, 15);
			label9.TabIndex = 27;
			label9.Text = "Max";
			// 
			// maxHighTxtBox
			// 
			maxHighTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			maxHighTxtBox.Location = new Point(329, 247);
			maxHighTxtBox.Margin = new Padding(3, 3, 3, 18);
			maxHighTxtBox.Name = "maxHighTxtBox";
			maxHighTxtBox.ReadOnly = true;
			maxHighTxtBox.Size = new Size(64, 23);
			maxHighTxtBox.TabIndex = 26;
			// 
			// label10
			// 
			label10.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label10.AutoSize = true;
			label10.Location = new Point(259, 229);
			label10.Name = "label10";
			label10.Size = new Size(28, 15);
			label10.TabIndex = 25;
			label10.Text = "Min";
			// 
			// minHighTxtBox
			// 
			minHighTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			minHighTxtBox.Location = new Point(259, 247);
			minHighTxtBox.Margin = new Padding(3, 3, 3, 18);
			minHighTxtBox.Name = "minHighTxtBox";
			minHighTxtBox.ReadOnly = true;
			minHighTxtBox.Size = new Size(64, 23);
			minHighTxtBox.TabIndex = 24;
			// 
			// AudioApp
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(405, 507);
			Controls.Add(label9);
			Controls.Add(maxHighTxtBox);
			Controls.Add(label10);
			Controls.Add(minHighTxtBox);
			Controls.Add(label7);
			Controls.Add(maxMidTxtBox);
			Controls.Add(label8);
			Controls.Add(minMidTxtBox);
			Controls.Add(label6);
			Controls.Add(maxLowTxtBox);
			Controls.Add(label5);
			Controls.Add(minLowTxtBox);
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
		private TextBox minLowTxtBox;
		private Label label5;
		private Label label6;
		private TextBox maxLowTxtBox;
		private Label label7;
		private TextBox maxMidTxtBox;
		private Label label8;
		private TextBox minMidTxtBox;
		private Label label9;
		private TextBox maxHighTxtBox;
		private Label label10;
		private TextBox minHighTxtBox;
	}
}
