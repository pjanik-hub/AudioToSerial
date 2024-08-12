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
			// AudioApp
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(566, 338);
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
	}
}
