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
            try
            {
                using (GrandLuxEntities context = new GrandLuxEntities())
                {
                    RoomsDGV.DataSource = context.Rooms.Select(r => new {
                        r.Id,
                        r.Room_Number,
                        r.Floor_Number,
                        Room_Type = r.Room_Type.Name,
                        Room_Status = r.Room_Status.Name
                    }).ToList();

                    RoomTypeCB.DataSource = context.Room_Type.ToList();
                    RoomTypeCB.DisplayMember = "Name";
                    RoomTypeCB.ValueMember = "Id";

                    RoomStatusCB.DataSource = context.Room_Status.ToList();
                    RoomStatusCB.DisplayMember = "Name";
                    RoomStatusCB.ValueMember = "Id";
                }
            }
            catch (Exception)
            {
                MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetRoomInputs();
        }

        private void RoomsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (RoomsDGV.CurrentRow.Index != -1)
            {
                room.Id = Convert.ToInt32(RoomsDGV.CurrentRow.Cells["Id"].Value);

                try
                {
                    using (GrandLuxEntities context = new GrandLuxEntities())
                    {
                        room = context.Rooms.Where(r => r.Id == room.Id).FirstOrDefault();
                    }

                    RoomNoNUD.Value = room.Room_Number;
                    FloorNoNUD.Value = room.Floor_Number;
                    RoomTypeCB.SelectedValue = room.Type_Id;
                    RoomStatusCB.SelectedValue = room.Status_Id;
                }
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

            AddEntity<Room>(room, inputsValidation, room.Id);
        }

        private void RoomsDeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteEntity<Room>(room, room.Id);
        }

        private void RoomsSearchBtn_Click(object sender, EventArgs e)
        {
            bool numericInputs = RoomNoNUD.Value != 0 && FloorNoNUD.Value != 0;
            bool roomTypeCBText = RoomTypeCB.SelectedText != "      ------- Select a room Type -------    ";
            bool roomStatusCBText = RoomStatusCB.SelectedText != "      ------- Select a room status -------    ";
            bool inputsValidation = numericInputs || roomTypeCBText || roomStatusCBText;

            if (inputsValidation)
            {
                var roomTypeId = Convert.ToInt32(RoomTypeCB.SelectedValue);
                var roomStatusId = Convert.ToInt32(RoomStatusCB.SelectedValue);

                try
                {
                    using (GrandLuxEntities context = new GrandLuxEntities())
                    {
                        RoomsDGV.DataSource = context.Rooms.Where(r => r.Room_Number == RoomNoNUD.Value ||
                                                     r.Floor_Number == FloorNoNUD.Value ||
                                                     r.Type_Id == roomTypeId ||
                                                     r.Status_Id == roomStatusId)
                                         .Select(r => new {
                                             r.Room_Number,
                                             r.Floor_Number,
                                             Room_Type = r.Room_Type.Name,
                                             Room_Status = r.Room_Status.Name
                                         }).ToList();
                    }
                }
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ResetRoomInputs();
            }
            else
            {
                MetroMessageBox.Show(this, "Please insert valid data to start searching", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetRoomInputs()
        {
            RoomTypeCB.SelectedItem = RoomStatusCB.SelectedItem = null;
            RoomTypeCB.SelectedText = "      ------- Select a room Type -------    ";
            RoomStatusCB.SelectedText = "      ------- Select a room status -------    ";
            RoomNoNUD.Value = FloorNoNUD.Value = 0;
            room.Id = 0;
        }
    }
}
