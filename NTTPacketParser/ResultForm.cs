using NTTPacketParser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTTPacketParser
{
	public partial class ResultForm : Form
	{
		private readonly InputForm _parentForm;

		public ResultForm(
		List<ParsedField> mainFields,
		List<TlvField> otherDetails,
		InputForm parentForm)
		{
			InitializeComponent();
			_parentForm = parentForm;

			dgvMain.DataSource = mainFields;
			dgvOtherDetails.DataSource = otherDetails;
		}


		public ResultForm()
		{
			InitializeComponent();
		}

		private void ResultForm_Load(object sender, EventArgs e)
		{
			dgvMain.AutoGenerateColumns = true;
			dgvMain.ReadOnly = true;
			dgvOtherDetails.AutoGenerateColumns = true;
			dgvOtherDetails.ReadOnly = true;
			_parentForm.Enabled = false;

			// Configure main grid
			dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvMain.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

			// Configure TLV grid
			dgvOtherDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvOtherDetails.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			dgvOtherDetails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			_parentForm.Enabled = true;
			base.OnFormClosed(e);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
