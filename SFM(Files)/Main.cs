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

    public partial class Main : Form
    {
        public static Employee ProObj;
        public static string filePath;
        public static string sortedby;
        public Table T = new Table();
        public static int indx;
        public Main()
        {
            InitializeComponent();
        }
        /***************************** Static Getters ********************************/
        public int Get_index(string attr)
        {
            if (attr == "LName")
            {
                return 0;
            }
            else if (attr == "SSN")
            {
                return 1;
            }
            else if (attr == "Job")
            {
                return 2;
            }
            else if (attr == "Salary")
            {
                return 3;
            }
            else if (attr == "Age")
            {
                return 4;
            }
            return -1;
        }
        public Employee getObj()
        {
            return ProObj;
        }

        public int getIndex()
        {
            return indx;
        }

        public string getFilePath()
        {
            return filePath;
        }

        public string getSortedBy()
        {
            return sortedby;
        }

        public void setfilePath(string S)
        {
            filePath = S;
        }

        /*****************************************************************************/
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form n = new Add_Employee();
            n.ShowDialog();
            T.Sort(sortedby);
            T.Refresh(DGV);
            n.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ProObj = new Employee(DGV.SelectedRows[0].Cells[0].Value.ToString(), DGV.SelectedRows[0].Cells[1].Value.ToString(), DGV.SelectedRows[0].Cells[2].Value.ToString(), DGV.SelectedRows[0].Cells[3].Value.ToString(), DGV.SelectedRows[0].Cells[4].Value.ToString());
                indx = DGV.SelectedRows[0].Index;
                Form E = new Edit_Employee();
                E.ShowDialog();
                T.Sort(sortedby);
                T.Refresh(DGV);
                E.Dispose();
            }
            catch
            {
                MessageBox.Show("invaild Data to edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form E =new Search();
            E.ShowDialog();
            E.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = @"C:\";
            ofd.Title = "Browse For Xml File";

            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            ofd.DefaultExt = "XML";
            ofd.Filter = "Xml File (*.xml)|*.xml";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Data Grid View cleared
                DGV.Rows.Clear();
                // load the path 
                filePath = Path.GetFullPath(ofd.FileName);
                //load the file into the Dictonary
                sortedby = T.uploadEmployeeData(filePath);
                int index = Get_index(sortedby);
               if (sortedby != null)
                {
                    //Enable every thing
                    AddBtn.Enabled = true;
                    EditBtn.Enabled = true;
                    DelBtn.Enabled = true;
                    SearchBtn.Enabled = true;
                    Savebtn.Enabled = true;
                    label2.Visible = false;
                    //load the dictionary into the Datagrid view
                    T.Fill(DGV);
                }
            }
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            Form s = new Save();
            s.ShowDialog();
            s.Dispose();
            try
            {
                sortedby = T.uploadEmployeeData(filePath);
                T.Refresh(DGV);
            }
            catch
            {
                this.Close();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            AddBtn.Enabled = false;
            EditBtn.Enabled = false;
            DelBtn.Enabled = false;
            SearchBtn.Enabled = false;
            Savebtn.Enabled = false;
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                indx = DGV.SelectedRows[0].Index;
                DialogResult dialogResult = MessageBox.Show("Are You Sure that You Want to Remove " + DGV.SelectedRows[0].Cells[0].Value.ToString() , "Are you sure", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DGV.Rows.RemoveAt(indx);
                    T.deleteRecord(indx);
                }
            }
            catch
            {
                MessageBox.Show("invaild Data to Remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                ProObj = new Employee(DGV.SelectedRows[0].Cells[0].Value.ToString(), DGV.SelectedRows[0].Cells[1].Value.ToString(), DGV.SelectedRows[0].Cells[2].Value.ToString(), DGV.SelectedRows[0].Cells[3].Value.ToString(), DGV.SelectedRows[0].Cells[4].Value.ToString());
                indx = DGV.SelectedRows[0].Index;
                Form E = new Edit_Employee();
                E.ShowDialog();
                T.Sort(sortedby);
                T.Refresh(DGV);
                E.Dispose();
            }
            catch
            {
                MessageBox.Show("invaild Data to edit", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                indx = DGV.SelectedRows[0].Index;
                DialogResult dialogResult = MessageBox.Show("Are You Sure that You Want to Remove " + DGV.SelectedRows[0].Cells[0].Value.ToString(), "Are you sure", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DGV.Rows.RemoveAt(indx);
                    T.deleteRecord(indx);
                }
            }
            catch
            {
                MessageBox.Show("invaild Data to Remove", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }










    




















}
