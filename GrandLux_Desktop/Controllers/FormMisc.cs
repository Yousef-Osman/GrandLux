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
using System.ComponentModel.DataAnnotations;

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

        public void ShowMiscData()
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

        private void ClearTapsInputs_Click(object sender, EventArgs e)
        {
            ShowMiscData();
        }


        #region Room Type
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

        private void RoomTypeAddBtn_Click(object sender, EventArgs e)
        {
            bool inputsValidation = !string.IsNullOrWhiteSpace(RoomTypeNameTB.Text) && BedsNUD.Value != 0 && MaxCapacityNUD.Value != 0;

            roomType.Name = RoomTypeNameTB.Text;
            roomType.No_of_Beds = Convert.ToInt32(BedsNUD.Value);
            roomType.Max_Capacity = Convert.ToInt32(MaxCapacityNUD.Value);

            AddEntity<Room_Type>(roomType, inputsValidation, roomType.Id);
        }

        private void RoomTypeDeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteEntity<Room_Type>(roomType, roomType.Id);
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
        #endregion


        #region Room Status
        private void RoomStatusAddBtn_Click(object sender, EventArgs e)
        {
            bool inputsValidation = !string.IsNullOrWhiteSpace(RoomStatusNameTB.Text) && !string.IsNullOrWhiteSpace(RoomStatusDescriptionTB.Text);

            roomStatus.Name = RoomStatusNameTB.Text;
            roomStatus.Description = RoomStatusDescriptionTB.Text;

            AddEntity<Room_Status>(roomStatus, inputsValidation, roomStatus.Id);
        }

        private void RoomStatusDeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteEntity<Room_Status>(roomStatus, roomStatus.Id);
        }
        #endregion


        #region Reservation Status
        private void ReservationStatusAddBtn_Click(object sender, EventArgs e)
        {

        }

        private void ReservationStatusDeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteEntity<Reservation_Status>(reservationStatus, reservationStatus.Id);
        }

        private void ReservationStatusDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion
    }
}
