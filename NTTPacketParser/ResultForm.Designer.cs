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
			dgvMain.Size = new Size(819, 242);
			dgvMain.TabIndex = 0;
			// 
			// dgvOtherDetails
			// 
			dgvOtherDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvOtherDetails.Location = new Point(25, 290);
			dgvOtherDetails.Name = "dgvOtherDetails";
			dgvOtherDetails.RowHeadersWidth = 51;
			dgvOtherDetails.Size = new Size(819, 241);
			dgvOtherDetails.TabIndex = 1;
			// 
			// ResultForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(907, 579);
			Controls.Add(dgvOtherDetails);
			Controls.Add(dgvMain);
			Name = "ResultForm";
			Text = "ResultForm";
			Load += ResultForm_Load;
			((System.ComponentModel.ISupportInitialize)dgvMain).EndInit();
			((System.ComponentModel.ISupportInitialize)dgvOtherDetails).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private DataGridView dgvMain;
		private DataGridView dgvOtherDetails;
	}
}