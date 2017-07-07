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
    public partial class Add_Employee : Form
    {
        Table T = new Table();
        Main pro = new Main();
        bool image = false;
        public Add_Employee()
        {
            InitializeComponent();
        }

        private void SSNTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void SSNTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

         String path;
        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = @"C:\";
            ofd.Title = "Browse For Profile Photo";

            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            ofd.DefaultExt = "JPG";
            ofd.Filter = "Image files (jpg , png , jpeg)|*.png|*.jpeg|*.jpg";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image file = Image.FromFile(ofd.FileName);
                PB.BackgroundImage = file;
                path = Path.GetFullPath(ofd.FileName);
                image = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NameTxt.Text == "" || SSNTxt.Text == "" || JobTxt.Text == "" || AgeTxt.Text == "" || SalleryTxt.Text == "")
            {
                MessageBox.Show(" One or More Field is Empty Please Fill Them to Submit ", " Error !! " , MessageBoxButtons.OK ,MessageBoxIcon.Error);
            }
            else if (T.Check_SSN(SSNTxt.Text.ToString()))
            {
                if (image)
                {
                    // Add Photo 
                    string pname = ".\\imgs\\" + NameTxt.Text.ToString() + "-" + SSNTxt.Text.ToString() + ".JPG";
                    FileInfo file = new FileInfo(path);
                    file.CopyTo(pname,true);
                }
                // Make object then append to the list
                Employee tmp = new Employee(NameTxt.Text, SSNTxt.Text, JobTxt.Text, SalleryTxt.Text, AgeTxt.Text);
                T.insertNewRecoord(tmp , tmp.Get_Key(pro.getSortedBy()));
            }
            else
            {
                MessageBox.Show(" SSN Already Exist ! .. Please Type  your Real SSN ", " Error !! ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            this.Close();
        }

        private void AgeTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SalleryTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
