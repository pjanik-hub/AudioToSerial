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
			components = new System.ComponentModel.Container();
			serialsCombo = new ComboBox();
			refreshSerialButton = new Button();
			bindButton = new Button();
			label1 = new Label();
			fftPlot = new ScottPlot.WinForms.FormsPlot();
			timer1 = new System.Windows.Forms.Timer(components);
			bindResult = new Label();
			powerCheckBox = new CheckBox();
			lowTxtBx = new TextBox();
			midTxtBx = new TextBox();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			highTxtBx = new TextBox();
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
			// fftPlot
			// 
			fftPlot.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			fftPlot.DisplayScale = 1F;
			fftPlot.Location = new Point(12, 162);
			fftPlot.Margin = new Padding(4);
			fftPlot.Name = "fftPlot";
			fftPlot.Size = new Size(498, 186);
			fftPlot.TabIndex = 28;
			// 
			// timer1
			// 
			timer1.Tick += timer1_Tick;
			// 
			// bindResult
			// 
			bindResult.AutoSize = true;
			bindResult.Location = new Point(12, 111);
			bindResult.Name = "bindResult";
			bindResult.Size = new Size(0, 15);
			bindResult.TabIndex = 29;
			// 
			// powerCheckBox
			// 
			powerCheckBox.AutoSize = true;
			powerCheckBox.Location = new Point(257, 29);
			powerCheckBox.Name = "powerCheckBox";
			powerCheckBox.Size = new Size(106, 19);
			powerCheckBox.TabIndex = 30;
			powerCheckBox.Text = "Use Power (dB)";
			powerCheckBox.UseVisualStyleBackColor = true;
			powerCheckBox.CheckedChanged += powerCheckBox_CheckedChanged;
			// 
			// lowTxtBx
			// 
			lowTxtBx.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lowTxtBx.Location = new Point(403, 27);
			lowTxtBx.Name = "lowTxtBx";
			lowTxtBx.ReadOnly = true;
			lowTxtBx.Size = new Size(107, 23);
			lowTxtBx.TabIndex = 31;
			lowTxtBx.TextAlign = HorizontalAlignment.Right;
			// 
			// midTxtBx
			// 
			midTxtBx.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			midTxtBx.Location = new Point(403, 71);
			midTxtBx.Name = "midTxtBx";
			midTxtBx.ReadOnly = true;
			midTxtBx.Size = new Size(107, 23);
			midTxtBx.TabIndex = 32;
			midTxtBx.TextAlign = HorizontalAlignment.Right;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new Point(480, 9);
			label2.Name = "label2";
			label2.Size = new Size(29, 15);
			label2.TabIndex = 33;
			label2.Text = "Low";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new Point(482, 53);
			label3.Name = "label3";
			label3.Size = new Size(28, 15);
			label3.TabIndex = 34;
			label3.Text = "Mid";
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new Point(477, 97);
			label4.Name = "label4";
			label4.Size = new Size(33, 15);
			label4.TabIndex = 36;
			label4.Text = "High";
			// 
			// highTxtBx
			// 
			highTxtBx.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			highTxtBx.Location = new Point(403, 115);
			highTxtBx.Name = "highTxtBx";
			highTxtBx.ReadOnly = true;
			highTxtBx.Size = new Size(107, 23);
			highTxtBx.TabIndex = 35;
			highTxtBx.TextAlign = HorizontalAlignment.Right;
			// 
			// AudioApp
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(522, 361);
			Controls.Add(label4);
			Controls.Add(highTxtBx);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(midTxtBx);
			Controls.Add(lowTxtBx);
			Controls.Add(powerCheckBox);
			Controls.Add(bindResult);
			Controls.Add(fftPlot);
			Controls.Add(label1);
			Controls.Add(bindButton);
			Controls.Add(refreshSerialButton);
			Controls.Add(serialsCombo);
			MinimumSize = new Size(538, 400);
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
		private ScottPlot.WinForms.FormsPlot fftPlot;
		private System.Windows.Forms.Timer timer1;
		private Label bindResult;
		private CheckBox powerCheckBox;
		private TextBox lowTxtBx;
		private TextBox midTxtBx;
		private Label label2;
		private Label label3;
		private Label label4;
		private TextBox highTxtBx;
	}
}
