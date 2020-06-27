using MetroFramework;
using System;
using GrandLux_Desktop.Models;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GrandLux_Desktop
{
    public partial class Form1
    {
        private void EmployeesBtn_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Employees";
            HideAllPanels();
            EmployeesPanel.Visible = true;
        }

        private void EmployeesDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EmployeesDGV.CurrentRow.Index != -1)
            {
                employee.Id = Convert.ToInt32(EmployeesDGV.CurrentRow.Cells["Id"].Value);
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    employee = db.Employees.Where(a => a.Id == employee.Id).FirstOrDefault();
                }

                EmployeeFNameTB.Text = bookingFNameTB.Text = employee.First_Name;
                EmployeeLNameTB.Text = bookingLNameTB.Text = employee.Last_Name;
                EmployeeAddressTB.Text = employee.Address;
                EmployeeEmailTB.Text = employee.E_Mail;
                EmployeePhoneTB.Text = employee.Phone;
                EmployeeJobTB.Text = employee.Job_Title;
            }
        }

        private void EmployeesAddBtn_Click(object sender, EventArgs e)
        {
            bool inputsValidation = !string.IsNullOrWhiteSpace(EmployeeFNameTB.Text) && !string.IsNullOrWhiteSpace(EmployeeLNameTB.Text) && !string.IsNullOrWhiteSpace(EmployeePhoneTB.Text);

            employee.First_Name = EmployeeFNameTB.Text;
            employee.Last_Name = EmployeeLNameTB.Text;
            employee.Address = EmployeeAddressTB.Text;
            employee.E_Mail = EmployeeEmailTB.Text;
            employee.Phone = EmployeePhoneTB.Text;
            employee.Job_Title = EmployeeJobTB.Text;

            AddEntity<Employee>(employee, inputsValidation, employee.Id);
        }

        private void EmployeesDeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteEntity<Employee>(employee, employee.Id);
        }

        private void EmployeesSearchBtn_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(EmployeeFNameTB.Text) && !string.IsNullOrWhiteSpace(EmployeeLNameTB.Text)) || !string.IsNullOrWhiteSpace(EmployeePhoneTB.Text) || !string.IsNullOrWhiteSpace(EmployeeEmailTB.Text))
            {
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    EmployeesDGV.DataSource = db.Employees.Where(a => (a.First_Name == EmployeeFNameTB.Text &&
                                                                 a.Last_Name == EmployeeLNameTB.Text) ||
                                                                 a.E_Mail == EmployeeEmailTB.Text ||
                                                                 a.Phone == EmployeePhoneTB.Text ||
                                                                 a.Job_Title == EmployeeJobTB.Text)
                                                    .Select(a => new {
                                                        a.Id,
                                                        a.First_Name,
                                                        a.Last_Name,
                                                        a.Address,
                                                        a.E_Mail,
                                                        a.Phone,
                                                        a.Job_Title
                                                    }).ToList();
                }

                ClearEmployeeInputs();
            }
            else
            {
                MetroMessageBox.Show(this, "Please enter full name, e-mail Phone number or job title to start searching", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowEmployeeData()
        {
            using (GrandLuxEntities db = new GrandLuxEntities())
            {
                EmployeesDGV.DataSource = db.Employees.Select(a => new { a.Id, a.First_Name, a.Last_Name, a.Address, a.E_Mail, a.Phone, a.Job_Title }).ToList();
            }

            ClearEmployeeInputs();
        }

        private void ClearEmployeeInputs()
        {
            EmployeeFNameTB.Text = EmployeeLNameTB.Text = EmployeeAddressTB.Text = "";
            EmployeeEmailTB.Text = EmployeePhoneTB.Text = "";
            employee.Id = 0;
        }
    }
}
