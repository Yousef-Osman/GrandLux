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
            ShowEmployeeData();
        }

        private void EmployeesDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EmployeesDGV.CurrentRow.Index != -1)
            {
                try
                {
                    employee.Id = Convert.ToInt32(EmployeesDGV.CurrentRow.Cells["Id"].Value);
                    using (GrandLuxEntities context = new GrandLuxEntities())
                    {
                        employee = context.Employees.Where(a => a.Id == employee.Id).FirstOrDefault();
                    }

                    EmployeeFNameTB.Text = bookingFNameTB.Text = employee.First_Name;
                    EmployeeLNameTB.Text = bookingLNameTB.Text = employee.Last_Name;
                    EmployeeAddressTB.Text = employee.Address;
                    EmployeeEmailTB.Text = employee.E_Mail;
                    EmployeePhoneTB.Text = employee.Phone;
                    EmployeeJobTB.Text = employee.Job_Title;
                }
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                try
                {
                    using (GrandLuxEntities context = new GrandLuxEntities())
                    {
                        EmployeesDGV.DataSource = context.Employees.Where(a => (a.First_Name == EmployeeFNameTB.Text &&
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
                }
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                using (GrandLuxEntities context = new GrandLuxEntities())
                {
                    EmployeesDGV.DataSource = context.Employees.Select(a => new { a.Id, a.First_Name, a.Last_Name, a.Address, a.E_Mail, a.Phone, a.Job_Title }).ToList();

                }
            }
            catch (Exception)
            {
                MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ClearEmployeeInputs();
        }

        private void ClearEmployeeInputs()
        {
            EmployeeFNameTB.Text = EmployeeLNameTB.Text = EmployeeAddressTB.Text = "";
            EmployeeEmailTB.Text = EmployeePhoneTB.Text = EmployeeJobTB.Text = "";
            employee.Id = 0;
        }
    }
}