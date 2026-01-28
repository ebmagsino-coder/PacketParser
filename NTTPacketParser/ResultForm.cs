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
		public ResultForm(
		List<ParsedField> mainFields,
		List<TlvField> otherDetails)
		{
			InitializeComponent();

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
		}
	}
}
