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
        private void GuestsBtn_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Guests";
            HideAllPanels();
            GuestsPanel.Visible = true;
            ShowGuestData();
        }

        private void GuestAddBtn_Click(object sender, EventArgs e)
        {
            bool inputsValidation = !string.IsNullOrWhiteSpace(GuestFNameTB.Text) && !string.IsNullOrWhiteSpace(GuestLNameTB.Text) && !string.IsNullOrWhiteSpace(GuestPhoneTB.Text);

            guest.First_Name = GuestFNameTB.Text;
            guest.Last_Name = GuestLNameTB.Text;
            guest.Address = GuestAddressTB.Text;
            guest.E_Mail = GuestEmailTB.Text;
            guest.Phone = GuestPhoneTB.Text;

            AddEntity<Guest>(guest, inputsValidation, guest.Id);
        }

        public void ShowGuestData()
        {
            try
            {
                using (GrandLuxEntities context = new GrandLuxEntities())
                {
                    GuestsDGV.DataSource = context.Guests.Select(g => new { g.Id, g.First_Name, g.Last_Name, g.Address, g.E_Mail, g.Phone }).ToList();
                }
            }
            catch (Exception)
            {
                MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ClearGuestInputs();
        }

        private void GuestsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GuestsDGV.CurrentRow.Index != -1)
            {
                guest.Id = Convert.ToInt32(GuestsDGV.CurrentRow.Cells["Id"].Value);

                try
                {
                    using (GrandLuxEntities context = new GrandLuxEntities())
                    {
                        guest = context.Guests.Where(g => g.Id == guest.Id).FirstOrDefault();
                    }

                    GuestFNameTB.Text = bookingFNameTB.Text = guest.First_Name;
                    GuestLNameTB.Text = bookingLNameTB.Text = guest.Last_Name;
                    GuestAddressTB.Text = guest.Address;
                    GuestEmailTB.Text = guest.E_Mail;
                    GuestPhoneTB.Text = guest.Phone;
                }
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                BookingConfirmBtn.Enabled = true;
            }
        }

        private void GuestDeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteEntity<Guest>(guest, guest.Id);
        }

        private void GuestSearchBtn_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(GuestFNameTB.Text) && !string.IsNullOrWhiteSpace(GuestLNameTB.Text)) || !string.IsNullOrWhiteSpace(GuestPhoneTB.Text) || !string.IsNullOrWhiteSpace(GuestEmailTB.Text))
            {
                try
                {
                    using (GrandLuxEntities context = new GrandLuxEntities())
                    {
                        GuestsDGV.DataSource = context.Guests.Where(g => (g.First_Name == GuestFNameTB.Text &&
                                                                     g.Last_Name == GuestLNameTB.Text) ||
                                                                     g.E_Mail == GuestEmailTB.Text ||
                                                                     g.Phone == GuestPhoneTB.Text)
                                                        .Select(g => new
                                                        {
                                                            g.Id,
                                                            g.First_Name,
                                                            g.Last_Name,
                                                            g.Address,
                                                            g.E_Mail,
                                                            g.Phone
                                                        }).ToList();
                    }
                }
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ClearGuestInputs();
            }
            else
            {
                MetroMessageBox.Show(this, "Please enter full name, e-mail or Phone number to start searching", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearGuestInputs()
        {
            GuestFNameTB.Text = GuestLNameTB.Text = GuestAddressTB.Text = "";
            GuestEmailTB.Text = GuestPhoneTB.Text = "";
            guest.Id = 0;
        }
    }
}
