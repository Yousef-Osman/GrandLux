using System;
using GrandLux_Desktop.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GrandLux_Desktop
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private void ReservationsBtn_Click(object sender, EventArgs e)
        {
            TitleLabel.Text = "Reservations";
            HideAllPanels();
            ReservationsPanel.Visible = true;
            ShowReservationData();

            ReservationDeleteBtn.Enabled = false;
        }

        public void ShowReservationData()
        {
            using (GrandLuxEntities db = new GrandLuxEntities())
            {
                ReservationsDGV.DataSource = db.Reservations.Select(r => new
                {
                    r.Id,
                    r.Guest.First_Name,
                    r.Guest.Last_Name,
                    r.Check_In,
                    r.Check_Out,
                    r.Adults,
                    r.Children,
                    Room_No = r.Room.Room_Number,
                    Room_Type = r.Room.Room_Type.Name
                }).ToList();
            }

            GuestFNameTB.Text = GuestLNameTB.Text = GuestAddressTB.Text = "";
            GuestEmailTB.Text = GuestPhoneTB.Text = "";
            guest.Id = 0;
        }

        private void ReservationCheckBtn_Click(object sender, EventArgs e)
        {
            reservation.Check_In = CheckInDTP.Value.Date;
            reservation.Check_Out = CheckOutDTP.Value.Date;
            reservation.Adults = Convert.ToInt32(AdultsNUD.Value);
            reservation.Children = Convert.ToInt32(ChildernNUD.Value);

            var TotalCapacity = Convert.ToInt32(reservation.Adults + reservation.Children);

            using (GrandLuxEntities db = new GrandLuxEntities())
            {
                var roomList = db.Rooms.ToList();
                var roomDisplayList = new List<Room>();

                foreach (var ro in roomList)
                {
                    bool inDate = reservation.Check_In >= ro.Check_In && reservation.Check_In <= ro.Check_Out;
                    bool outDate = reservation.Check_Out >= ro.Check_In && reservation.Check_Out <= ro.Check_Out;
                    bool capacity = ro.Room_Type.Max_Capacity >= TotalCapacity;
                    bool availability = ro.Room_Status.Name == "Available";
                    if (!(inDate || outDate) && capacity && availability)
                    {
                        roomDisplayList.Add(ro);
                    }
                }

                CheckDGV.DataSource = roomDisplayList.Select(r => new
                {
                    r.Id,
                    r.Room_Number,
                    r.Floor_Number,
                    Room_Type = r.Room_Type.Name,
                    r.Room_Type.Max_Capacity
                }).ToList();
            }
        }

        private void CheckDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CheckDGV.CurrentRow.Index != -1)
            {
                room.Id = Convert.ToInt32(CheckDGV.CurrentRow.Cells["Id"].Value);
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    room = db.Rooms.Where(r => r.Id == room.Id).FirstOrDefault();

                }

                RoomNoDisplayOnlyTB.Text = room.Room_Number.ToString();
                room.Check_In = CheckInDTP.Value.Date;
                room.Check_Out = CheckOutDTP.Value.Date;
                BookBtn.Enabled = true;
            }
        }

        private void BookBtn_Click(object sender, EventArgs e)
        {
            reservation.Room_Id = room.Id;
            GuestsBtn_Click(this, null);
            GuestsDisplayPanel.Height = 280;
            GuestsDGV.Height = 240;
            GuestBookingPanal.Visible = true;
        }

        private void BookingConfirmBtn_Click(object sender, EventArgs e)
        {
            bool inputsValidation = true;

            reservation.Guest_Id = guest.Id;
            room.Status_Id = 1;
            AddEntity<Room>(room, inputsValidation, room.Id);
            AddEntity<Reservation>(reservation, inputsValidation, reservation.Id);

            HideAllPanels();
            ReservationsPanel.Visible = true;
        }

        private void ReservationDeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteEntity<Reservation>(reservation, reservation.Id);
            room.Check_In = room.Check_Out = null;
            room.Status_Id = 2;
            AddEntity<Room>(room, true, room.Id);

            ReservationDeleteBtn.Enabled = false;
        }

        private void ReservationsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ReservationsDGV.CurrentRow.Index != -1)
            {
                reservation.Id = Convert.ToInt32(ReservationsDGV.CurrentRow.Cells["Id"].Value);
                using (GrandLuxEntities db = new GrandLuxEntities())
                {
                    reservation = db.Reservations.Where(r => r.Id == reservation.Id).FirstOrDefault();
                    room = db.Rooms.Where(r => r.Id == reservation.Room_Id).FirstOrDefault();
                }

                ReservationDeleteBtn.Enabled = true;
            }
        }
    }
}
