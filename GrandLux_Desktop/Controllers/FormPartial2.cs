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

namespace GrandLux_Desktop
{
    public partial class Form1
    {
        private void MiscBtn_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Miscellaneous";
            HideAllPanels();
            MiscPanel.Visible = true;
            ShowMiscData();
        }

        private void ShowMiscData()
        {
            using (GrandLuxEntities db = new GrandLuxEntities())
            {
                RoomTypeDGV.DataSource = db.Room_Type.Select(r => new { Type_Id = r.Id, Type_Name = r.Name, No_Of_Beds = r.No_of_Beds, Maximum_Capacity = r.Max_Capacity }).ToList();
                RoomStatusDGV.DataSource = db.Room_Status.Select(r => new { Status_Id = r.Id, Status_Name = r.Name, r.Description }).ToList();
            }
            RoomTypeNameTB.Text = "";
            BedsNUD.Value = MaxCapacityNUD.Value = 0;
            RoomStatusNameTB.Text = RoomStatusDescriptionTB.Text = "";
            roomType.Id = roomStatus.Id = 0;
        }

        private void RoomTypeAddBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RoomTypeNameTB.Text) && BedsNUD.Value != 0 && MaxCapacityNUD.Value != 0)
            {
                string state;    //used to print a message to the user after an item is added or modified

                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    roomType.Name = RoomTypeNameTB.Text;
                    roomType.No_of_Beds = Convert.ToInt32(BedsNUD.Value);
                    roomType.Max_Capacity = Convert.ToInt32(MaxCapacityNUD.Value);

                    state = roomType.Id == 0 ? "Added" : "Modified";
                    db.Room_Type.AddOrUpdate(roomType);
                    db.SaveChanges();
                }

                MetroMessageBox.Show(this, $"A Room Type has been {state}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowMiscData();
            }
        }

        private void RoomTypeDeleteBtn_Click(object sender, EventArgs e)
        {
            if (roomType.Id != 0)
            {
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    //db.Room_Type.Remove(roomType);
                    db.Entry(roomType).State = EntityState.Deleted;
                    db.SaveChanges();
                }

                MetroMessageBox.Show(this, $"A Room Type has been removed", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowMiscData();
            }
            else
            {
                MetroMessageBox.Show(this, "Please click on a row to delete it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RoomTypeDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RoomTypeDGV.CurrentRow.Index != -1)
            {
                roomType.Id = Convert.ToInt32(RoomTypeDGV.CurrentRow.Cells["Type_Id"].Value);
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    roomType = db.Room_Type.Where(t => t.Id == roomType.Id).FirstOrDefault();

                    RoomTypeNameTB.Text = roomType.Name;
                    BedsNUD.Value = roomType.No_of_Beds;
                    MaxCapacityNUD.Value = roomType.Max_Capacity;
                }
            }
        }

        private void RoomStatusAddBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RoomStatusNameTB.Text) && !string.IsNullOrWhiteSpace(RoomStatusDescriptionTB.Text))
            {
                string state;    //used to print a message to the user after an item is added or modified

                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    roomStatus.Name = RoomStatusNameTB.Text;
                    roomStatus.Description = RoomStatusDescriptionTB.Text;

                    state = roomType.Id == 0 ? "Added" : "Modified";
                    db.Room_Status.AddOrUpdate(roomStatus);
                    db.SaveChanges();
                }

                MetroMessageBox.Show(this, $"A Room Status has been {state}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowMiscData();
            }
        }

        private void RoomStatusDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RoomStatusDGV.CurrentRow.Index != -1)
            {
                roomStatus.Id = Convert.ToInt32(RoomStatusDGV.CurrentRow.Cells["Status_Id"].Value);
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    roomStatus = db.Room_Status.Where(s => s.Id == roomStatus.Id).FirstOrDefault();

                    RoomStatusNameTB.Text = roomStatus.Name;
                    RoomStatusDescriptionTB.Text = roomStatus.Description;
                }
            }
        }

        private void RoomStatusDeleteBtn_Click(object sender, EventArgs e)
        {
            if (roomStatus.Id != 0)
            {
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    db.Entry(roomStatus).State = EntityState.Deleted;
                    db.SaveChanges();
                }

                MetroMessageBox.Show(this, $"A Room Status has been removed", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowMiscData();
            }
            else
            {
                MetroMessageBox.Show(this, "Please click on a row to delete it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ReservationStatusAddBtn_Click(object sender, EventArgs e)
        {

        }

        private void ReservationStatusDeleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void ReservationStatusDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ClearTapsInputs_Click(object sender, EventArgs e)
        {
            ShowMiscData();
        }
    }
}
