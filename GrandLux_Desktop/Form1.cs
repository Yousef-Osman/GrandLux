using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrandLux_Desktop
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private Guest guest;
        private Employee employee;
        private Room room;
        private Reservation reservation;
        private Reservation_Status reservationStatus;
        private Room_Status roomStatus;
        private Room_Type roomType;

        public Form1()
        {
            InitializeComponent();
            guest = new Guest();
            employee = new Employee();
            room = new Room();
            reservation = new Reservation();
            reservationStatus = new Reservation_Status();
            roomStatus = new Room_Status();
            roomType = new Room_Type();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideAllPanels();
            //HomePanel.Visible = true;
        }

        private void GuestsBtn_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Guests";
            HideAllPanels();
            GuestsPanel.Visible = true;
            ShowGuestsData();
        }

        private void HideAllPanels()
        {
            GuestsPanel.Visible = false;

            guest.Id = 0;
        }

        private void GuestAddBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GuestFNameTB.Text) && !string.IsNullOrWhiteSpace(GuestLNameTB.Text) && !string.IsNullOrWhiteSpace(GuestPhoneTB.Text))
            {
                string state;    //used to print a message to the user after an item is added or modified

                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    guest.First_Name = GuestFNameTB.Text;
                    guest.Last_Name = GuestLNameTB.Text;
                    guest.Address = GuestAddressTB.Text;
                    guest.E_Mail = GuestEmailTB.Text;
                    guest.Phone = GuestPhoneTB.Text;

                    state = guest.Id == 0 ? "Added" : "Modified";
                    db.Entry(guest).State = guest.Id == 0 ? EntityState.Added : EntityState.Modified;
                    db.SaveChanges();
                }

                MetroMessageBox.Show(this, $"A Guest has been {state}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowGuestsData();
                guest.Id = 0;
            }
            else
            {
                MetroMessageBox.Show(this, "Please fill the missing data or click on a row to update it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowGuestsData()
        {
            using (GrandLuxEntities db = new GrandLuxEntities())
            {
                GuestsDGV.DataSource = db.Guests.Select(g => new { g.Id, g.First_Name, g.Last_Name, g.Address, g.E_Mail, g.Phone }).ToList();
            }

            GuestFNameTB.Text = GuestLNameTB.Text = GuestAddressTB.Text = "";
            GuestEmailTB.Text = GuestPhoneTB.Text = "";
            guest.Id = 0;
        }

        private void GuestsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GuestsDGV.CurrentRow.Index != -1)
            {
                guest.Id = Convert.ToInt32(GuestsDGV.CurrentRow.Cells["Id"].Value);
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    guest = db.Guests.Where(g => g.Id == guest.Id).FirstOrDefault();

                    GuestFNameTB.Text = guest.First_Name;
                    GuestLNameTB.Text = guest.Last_Name;
                    GuestAddressTB.Text = guest.Address;
                    GuestEmailTB.Text = guest.E_Mail;
                    GuestPhoneTB.Text = guest.Phone;
                }
            }
        }

        private void GuestDeleteBtn_Click(object sender, EventArgs e)
        {
            if (guest.Id != 0)
            {
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    db.Entry(guest).State = EntityState.Deleted;
                    db.SaveChanges();
                }

                MetroMessageBox.Show(this, $"A Guest has been removed", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowGuestsData();
            }
            else
            {
                MetroMessageBox.Show(this, "Please click on a row to delete it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PermissionSearchBtn_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(GuestFNameTB.Text) && !string.IsNullOrWhiteSpace(GuestLNameTB.Text)) || !string.IsNullOrWhiteSpace(GuestPhoneTB.Text) || !string.IsNullOrWhiteSpace(GuestEmailTB.Text))
            {
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    var bb = db.Guests.Where(g => (g.First_Name == GuestFNameTB.Text && g.Last_Name == GuestLNameTB.Text) || g.E_Mail == GuestEmailTB.Text || g.Phone == GuestPhoneTB.Text)
                                                    .Select(g => new { g.Id, g.First_Name, g.Last_Name, g.Address, g.E_Mail, g.Phone });
                    GuestsDGV.DataSource = bb.ToList();
                }
                GuestFNameTB.Text = GuestLNameTB.Text = GuestAddressTB.Text = "";
                GuestEmailTB.Text = GuestPhoneTB.Text = "";
                guest.Id = 0;
            }
            else
            {
                MetroMessageBox.Show(this, "Please enter full name, e-mail or Phone number to start searching", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
