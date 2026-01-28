namespace NTTPacketParser
{
    partial class InputForm
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
			label1 = new Label();
			txtInput = new TextBox();
			label2 = new Label();
			comboBox1 = new ComboBox();
			btnParse = new Button();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(26, 25);
			label1.Name = "label1";
			label1.Size = new Size(92, 20);
			label1.TabIndex = 0;
			label1.Text = "Input Packet:";
			// 
			// txtInput
			// 
			txtInput.Location = new Point(26, 48);
			txtInput.Multiline = true;
			txtInput.Name = "txtInput";
			txtInput.Size = new Size(680, 158);
			txtInput.TabIndex = 1;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(26, 237);
			label2.Name = "label2";
			label2.Size = new Size(105, 20);
			label2.TabIndex = 2;
			label2.Text = "Message Type:";
			// 
			// comboBox1
			// 
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new Point(137, 234);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(210, 28);
			comboBox1.TabIndex = 3;
			// 
			// btnParse
			// 
			btnParse.Location = new Point(590, 228);
			btnParse.Name = "btnParse";
			btnParse.Size = new Size(116, 38);
			btnParse.TabIndex = 4;
			btnParse.Text = "Parse";
			btnParse.UseVisualStyleBackColor = true;
			btnParse.Click += btnParse_Click;
			// 
			// InputForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(735, 308);
			Controls.Add(btnParse);
			Controls.Add(comboBox1);
			Controls.Add(label2);
			Controls.Add(txtInput);
			Controls.Add(label1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Name = "InputForm";
			Text = "Packet Parser";
			Load += InputForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private TextBox txtInput;
		private Label label2;
		private ComboBox comboBox1;
		private Button btnParse;
	}
}
