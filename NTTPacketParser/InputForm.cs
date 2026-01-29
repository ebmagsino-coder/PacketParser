using NTTPacketParser.Helpers;

namespace NTTPacketParser
{
	public partial class InputForm : Form
	{
		public InputForm()
		{
			InitializeComponent();
		}

		private void InputForm_Load(object sender, EventArgs e)
		{
			label3.Text = "© 2026 NTT Data Payment Services — Internal Use Only";

			comboBox1.Items.Add("Response");
			comboBox1.SelectedIndex = 0;

			// Style controls
			foreach (Control ctrl in this.Controls)
			{
				if (ctrl is Button)
				{
					var btn = (Button)ctrl;
					btn.BackColor = Color.FromArgb(0, 120, 215);
					btn.ForeColor = Color.White;
					btn.FlatStyle = FlatStyle.Flat;
					btn.FlatAppearance.BorderSize = 0;
				}
			}
		}

		private void btnParse_Click(object sender, EventArgs e)
		{
			try
			{
				var parser = new PosMessageParser();
				parser.Parse(txtInput.Text);

				var resultForm = new ResultForm(
					parser.Fields,
					parser.OtherDetails,
					this
				);

				resultForm.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error parsing packet: {ex.Message}", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
