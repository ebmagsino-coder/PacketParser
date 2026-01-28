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
			comboBox1.Items.Add("Response");
			comboBox1.SelectedIndex = 0;
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
