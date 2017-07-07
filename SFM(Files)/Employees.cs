using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Xml.XPath;

namespace SFM_Files_
{
    public class Employee
    {
        public string LName;
        public string SSN;
        public string Job;
        public string Salary;
        public string Age;

        public Employee() { }

        public Employee(string LName, string SSN, string Job, string Salary, string Age)
        {
            this.LName = LName;
            this.SSN = SSN;
            this.Job = Job;
            this.Salary = Salary;
            this.Age = Age;
        }

        public string Get_Key(string attr)
        {
            if (attr == "LName")
            {
                return LName;
            }
            else if (attr == "SSN")
            {
                return SSN;
            }
            else if (attr == "Job")
            {
                return Job;
            }
            else if (attr == "Salary")
            {
                return Salary;
            }
            else if (attr == "Age")
            {
                return Age;
            }
            return null;
        }



    }

    public class Table
    {
        public static List<KeyValuePair<string, Employee>> List = new List<KeyValuePair<string, Employee>>();

        public string uploadEmployeeData(string filePath)
        {
            List.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlElement root = doc.DocumentElement;
            string s;
            try
            {
                 s = root.Attributes["SortedBy"].Value;
            }
            catch
            {
                MessageBox.Show(" Not An Employee Table File ", " Error !! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            XmlNodeList list = doc.GetElementsByTagName("employee");
            for (int i = 0; i < list.Count; i++)
            {
                XmlNodeList childerns = list[i].ChildNodes;
                Employee emp = new Employee(childerns[0].InnerText, childerns[1].InnerText, childerns[2].InnerText, childerns[3].InnerText, childerns[4].InnerText);
                var element = new KeyValuePair<string, Employee>(emp.Get_Key(s), emp);
                List.Add(element);
            }
            return s;
        }

        public List<Employee> searchRecord(string attribute, string type)
        {
            List<Employee> tmp_list = new List<Employee>();

            if (type == "Name")
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (List.ElementAt(i).Value.LName.Contains(attribute))                {
                    tmp_list.Add(List.ElementAt(i).Value);
                    }
            }
            }
            else if (type == "SNN"){

                for (int i = 0; i < List.Count; i++)
                {
                    if (List.ElementAt(i).Value.SSN.Equals(attribute))
                    {
                        tmp_list.Add(List.ElementAt(i).Value);
                    }

                }
            }
            else if(type == "Job"){

                for (int i = 0; i < List.Count; i++)
                {
                    if (List.ElementAt(i).Value.Job.Equals(attribute))
                    {
                        tmp_list.Add(List.ElementAt(i).Value);
                    }

                }
            }
            else if(type == "Salary"){

                for (int i = 0; i < List.Count; i++)
                {
                    if (List.ElementAt(i).Value.Salary.Equals(attribute))
                    {
                        tmp_list.Add(List.ElementAt(i).Value);
                    }

                }

            }
            else{
                for (int i = 0; i < List.Count; i++)
                {
                    if (List.ElementAt(i).Value.Age.Equals(attribute))
                    {
                        tmp_list.Add(List.ElementAt(i).Value);
                    }

                }
            }
           
               
            
            return tmp_list;
        }


     









        public void updateRecord(Employee newE, int index, string key)
        {
            var element = new KeyValuePair<string, Employee>(key, newE);
            List[index] = element;
        }

        public void insertNewRecoord(Employee emp, string key)
        {
            var element = new KeyValuePair<string, Employee>(key, emp);
            List.Add(element);
        }

        public void deleteRecord(int index)
        {
            List.Remove(List[index]);
        }

        public bool Check_SSN(string SSN)
        {
            foreach (KeyValuePair<string, Employee> val in List)
            {
                if (val.Value.SSN.ToString().Equals(SSN))
                {
                    return false;
                }
            }
            return true;
        }

        public void Sort(string sortedBy)
        {
            if (sortedBy == "SSN" || sortedBy == "Age" || sortedBy == "Salary")
            {
                List = List.OrderBy(x => int.Parse(x.Key)).ToList();
            }
            else
            {
                List = List.OrderBy(x => x.Key).ToList();
            }

        }

        public void Refresh(DataGridView Data)
        {
            Data.Rows.Clear();
            Fill(Data);
        }
        public void saveDataEMPLoyee(string filePath , String sortBy)
        {
            //sort list by value attr....
            if (sortBy == "LName")
            {
                List = List.OrderBy(x => x.Value.LName).ToList();
            }
            else if (sortBy == "SSN")
            {
                List = List.OrderBy(x => int.Parse(x.Value.SSN)).ToList();
            }
            else if (sortBy == "Job")
            {
                List = List.OrderBy(x => x.Value.Job).ToList();
            }
            else if (sortBy == "Salary")
            {
                List = List.OrderBy(x => int.Parse(x.Value.Salary)).ToList();
            }
            else if (sortBy == "Age")
            {
                List = List.OrderBy(x => int.Parse(x.Value.Age)).ToList();
            }
            ///////////////////////////////////////////////////////////////////////////
            XmlWriter w = XmlWriter.Create(filePath);
            w.WriteStartDocument();
            w.WriteStartElement("Table");
            w.WriteAttributeString("SortedBy" , sortBy);
            foreach(KeyValuePair<string,Employee> val in List)
            {
                w.WriteStartElement("employee");
                w.WriteStartElement("LName");
                w.WriteString(val.Value.LName);
                w.WriteEndElement();
                w.WriteStartElement("SSN");
                w.WriteString(val.Value.SSN);
                w.WriteEndElement();
                w.WriteStartElement("Job");
                w.WriteString(val.Value.Job);
                w.WriteEndElement();
                w.WriteStartElement("Salary");
                w.WriteString(val.Value.Salary);
                w.WriteEndElement();
                w.WriteStartElement("Age");
                w.WriteString(val.Value.Age);
                w.WriteEndElement();
                w.WriteEndElement();
            }
            w.WriteEndElement();
            w.WriteEndDocument();
            w.Close();
            
        }
        public void Fill(DataGridView Data)
        {
            foreach (KeyValuePair<string, Employee> val in List)
            {
                Data.Rows.Add(val.Value.LName, val.Value.SSN, val.Value.Job, val.Value.Salary, val.Value.Age);
            }
        }
        
    }


}







































