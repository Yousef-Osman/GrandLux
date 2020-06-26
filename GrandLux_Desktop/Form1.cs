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
            DisplayHome();
        }

        private void HideAllPanels()
        {
            HomePanel.Visible = false;
            GuestsPanel.Visible = false;
            RoomsPanel.Visible = false;
            MiscPanel.Visible = false;
        }

        private void LogoPanel_Click(object sender, EventArgs e)
        {
            DisplayHome();
        }

        private void DisplayHome()
        {
            TitleLabel.Text = "Home";
            HideAllPanels();
            HomePanel.Visible = true;
        }

        private void RoomsAddBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
