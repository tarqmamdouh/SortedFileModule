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
    public partial class Edit_Employee : Form
    {
        Main pro = new Main();
        String path;
        bool image = false;
        string old_ssn;
        public Edit_Employee()
        {
            InitializeComponent();
        }

        public static Image GetImage(string path)
        {
            Image img;
            try
            {
                using (Image temp = Image.FromFile(path))
                {
                    img = new Bitmap(temp);
                }
                return img;
            }
            catch
            {

            }
            return null;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
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

            ofd.ReadOnlyChecked = true;
            ofd.ShowReadOnly = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image file = Image.FromFile(ofd.FileName);
                PB.BackgroundImage = file;
                path = Path.GetFullPath(ofd.FileName);
                image = true;
            }
        }

        private void SSNTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Edit_Employee_Load(object sender, EventArgs e)
        {
            Employee emp = pro.getObj();
            NameTxt.Text = emp.LName;
            SSNTxt.Text = emp.SSN;
            old_ssn = emp.SSN;
            JobText.Text = emp.Job;
            AgeTxt.Text = emp.Age;
            SalleryTxt.Text = emp.Salary;
            PB.BackgroundImage = GetImage(".\\imgs\\" + emp.LName + "-" + emp.SSN + ".JPG");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NameTxt.Text == "" || SSNTxt.Text == "" || JobText.Text == "" || AgeTxt.Text == "" || SalleryTxt.Text == "")
            {
                MessageBox.Show(" One or More Field is Empty Please Fill Them to Submit ", " Error !! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (old_ssn != SSNTxt.Text)
            {
                if (!pro.T.Check_SSN(SSNTxt.Text))
                {
                    MessageBox.Show(" SSN Already Exist ! .. Please Type  your Real SSN ", " Error !! ", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                }
                else
                {
                    if (image)
                    {
                        // Add Photo 
                        string pname = ".\\imgs\\" + NameTxt.Text.ToString() + "-" + SSNTxt.Text.ToString() + ".JPG";
                        FileInfo file = new FileInfo(path);
                        file.CopyTo(pname, true);
                    }
                    //update the object
                    Employee tmp = new Employee(NameTxt.Text, SSNTxt.Text, JobText.Text, SalleryTxt.Text, AgeTxt.Text);
                    pro.T.updateRecord(tmp, pro.getIndex(), tmp.Get_Key(pro.getSortedBy()));
                    pro.T.Sort(pro.getSortedBy());
                    this.Close();
                }
            }
            else
            {
                if (image)
                {
                    // Add Photo 
                    string pname = ".\\imgs\\" + NameTxt.Text.ToString() + "-" + SSNTxt.Text.ToString() + ".JPG";
                    File.Copy(path,pname, true);
                    
                    }
                //update the object
                Employee tmp = new Employee(NameTxt.Text, SSNTxt.Text, JobText.Text, SalleryTxt.Text, AgeTxt.Text);
                pro.T.updateRecord(tmp, pro.getIndex(), tmp.Get_Key(pro.getSortedBy()));
                pro.T.Sort(pro.getSortedBy());
                this.Close();
            }


        }
    }
}
