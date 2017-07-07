using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;


namespace SFM_Files_
{
    public partial class Search : Form
    {
        Main pro =new Main();
        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }
        

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                dgvSearch.Rows.Clear();
                var ls = pro.T.searchRecord(txtSearch.Text, cmbSearch.SelectedItem.ToString());
                foreach (Employee v in ls)
                {
                    dgvSearch.Rows.Add(v.LName, v.SSN, v.Job, v.Salary, v.Age);
                }
                if (dgvSearch.Rows.Count == 1)
                    MessageBox.Show("No result found!,,, Check search keywords.", "Search ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch
            {

                MessageBox.Show("Please select which can search by!!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
           
        }
    }
}
