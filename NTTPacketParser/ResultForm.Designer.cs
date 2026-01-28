namespace NTTPacketParser
{
	partial class ResultForm
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
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			dgvMain = new DataGridView();
			dgvOtherDetails = new DataGridView();
			label1 = new Label();
			label2 = new Label();
			btnClose = new Button();
			((System.ComponentModel.ISupportInitialize)dgvMain).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgvOtherDetails).BeginInit();
			SuspendLayout();
			// 
			// dgvMain
			// 
			dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvMain.Location = new Point(25, 30);
			dgvMain.Name = "dgvMain";
			dgvMain.RowHeadersWidth = 51;
			dgvMain.Size = new Size(819, 271);
			dgvMain.TabIndex = 0;
			// 
			// dgvOtherDetails
			// 
			dgvOtherDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvOtherDetails.Location = new Point(25, 351);
			dgvOtherDetails.Name = "dgvOtherDetails";
			dgvOtherDetails.RowHeadersWidth = 51;
			dgvOtherDetails.Size = new Size(819, 185);
			dgvOtherDetails.TabIndex = 1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(25, 7);
			label1.Name = "label1";
			label1.Size = new Size(98, 20);
			label1.TabIndex = 2;
			label1.Text = "Parsed Values";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(25, 328);
			label2.Name = "label2";
			label2.Size = new Size(117, 20);
			label2.TabIndex = 3;
			label2.Text = "Parsed Data Part";
			// 
			// btnClose
			// 
			btnClose.Location = new Point(736, 555);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(108, 45);
			btnClose.TabIndex = 4;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// ResultForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(868, 613);
			Controls.Add(btnClose);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(dgvOtherDetails);
			Controls.Add(dgvMain);
			Name = "ResultForm";
			Text = "ResultForm";
			Load += ResultForm_Load;
			((System.ComponentModel.ISupportInitialize)dgvMain).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvOtherDetails).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView dgvMain;
		private DataGridView dgvOtherDetails;
		private Label label1;
		private Label label2;
		private Button btnClose;
	}
}