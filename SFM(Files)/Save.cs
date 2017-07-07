using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SFM_Files_
{
    public partial class Save : Form
    {
        Main pro = new Main();
        Table T = new Table();
        public Save()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML-File | *.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PathTxt.Text = Path.GetFullPath(saveFileDialog.FileName);
            }
        }

        private void Save_Load(object sender, EventArgs e)
        {
            PathTxt.Text = pro.getFilePath();
        }

        private void Sv_Click(object sender, EventArgs e)
        {
            var checkedButton = SaveSortedBy.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            try
            {
                T.saveDataEMPLoyee(PathTxt.Text, checkedButton.Text);
                MessageBox.Show("Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pro.setfilePath(PathTxt.Text);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please check your Path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }

        private void SaveExit_Click(object sender, EventArgs e)
        {
            var checkedButton = SaveSortedBy.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            T.saveDataEMPLoyee(PathTxt.Text, checkedButton.Text);
            System.Windows.Forms.Application.Exit();
        }
    }
}
