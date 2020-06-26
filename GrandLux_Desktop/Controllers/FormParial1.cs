using MetroFramework;
using System;
using GrandLux_Desktop.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

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
            using (GrandLuxEntities db = new GrandLuxEntities())
            {
                GuestsDGV.DataSource = db.Guests.Select(g => new { g.Id, g.First_Name, g.Last_Name, g.Address, g.E_Mail, g.Phone }).ToList();
            }

            GuestFNameTB.Text = GuestLNameTB.Text = GuestAddressTB.Text = "";
            GuestEmailTB.Text = GuestPhoneTB.Text = "";
            guest.Id = 0;
        }

        
        private void GuestsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
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
                ShowGuestData();
            }
            else
            {
                MetroMessageBox.Show(this, "Please click on a row to delete it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuestSearchBtn_Click(object sender, EventArgs e)
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

        private void RoomsBtn_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Rooms";
            HideAllPanels();
            RoomsPanel.Visible = true;
            RoomNoNUD.Maximum = 100000;
            FloorNoNUD.Maximum = 1000;
            ShowRoomData();
        }

        public void ShowRoomData()
        {
            using (GrandLuxEntities db = new GrandLuxEntities())
            {
                RoomsDGV.DataSource = db.Rooms.Select(r => new { r.Room_Number, r.Floor_Number, r.Room_Type.Name, r.Status_Id }).ToList();

                RoomTypeCB.DataSource = db.Room_Type.ToList();
                RoomTypeCB.DisplayMember = "Name";
                RoomTypeCB.ValueMember = "Id";
                RoomStatusCB.DataSource = db.Room_Status.ToList();
                RoomStatusCB.DisplayMember = "Name";
                RoomStatusCB.ValueMember = "Id";
            }

            RoomTypeCB.SelectedItem = RoomStatusCB.SelectedItem = null;
            RoomTypeCB.SelectedText = "      ------- Select a room Type -------    ";
            RoomStatusCB.SelectedText = "      ------- Select a room status -------    ";
            RoomNoNUD.Value = FloorNoNUD.Value = 0;
            room.Room_Number = 0;
        }

        private void RoomsAddBtn_Click(object sender, EventArgs e)
        {
            bool numericInputs = RoomNoNUD.Value != 0 && FloorNoNUD.Value != 0;
            bool roomTypeCBText = RoomTypeCB.SelectedText != "      ------- Select a room Type -------    ";
            bool roomStatusCBText = RoomStatusCB.SelectedText != "      ------- Select a room status -------    ";
            bool inputsValidation = numericInputs && roomTypeCBText && roomStatusCBText;

            room.Room_Number = Convert.ToInt32(RoomNoNUD.Value);
            room.Floor_Number = Convert.ToInt32(FloorNoNUD.Value);
            room.Type_Id = Convert.ToInt32(RoomTypeCB.SelectedValue);
            room.Status_Id = Convert.ToInt32(RoomStatusCB.SelectedValue);

            //MessageBox.Show(room.Type_Id.ToString());
            //MessageBox.Show(room.Status_Id.ToString());

            AddEntity<Room>(room, inputsValidation, room.Room_Number);
        }

        private void AddEntity<T>(T entity, bool inputsValidation, int id) where T : class
        {
            if (inputsValidation)
            {
                try
                {
                    string state = (id == 0) ? "Added" : "Modified";    //To print a message to the user of wether an item is added or modified

                    using (GrandLuxEntities db = new GrandLuxEntities())
                    {
                        db.Set<T>().AddOrUpdate(entity);
                        db.SaveChanges();
                    }

                    MetroMessageBox.Show(this, $"A record has been {state}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData(entity);   //To fill the Data grid view and clear the input values
                }
                catch(Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Please fill the missing data or click on a row to update it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowData<T>(T entity)
        {
            //Using reflection to call the corresponding function to the entity type
            string entityType = typeof(T).ToString().Remove(0, 24);
            string methodName = $"Show{entityType}Data";
            MethodInfo methodInfo = this.GetType().GetMethod(methodName);
            methodInfo.Invoke(this, null);
        }
    }
}
