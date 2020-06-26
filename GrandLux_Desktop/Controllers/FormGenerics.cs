using MetroFramework;
using System;
using GrandLux_Desktop.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Windows.Forms;
using System.Reflection;

namespace GrandLux_Desktop
{
    public partial class Form1
    {
        /// <summary>
        /// Called when the add button is pressed
        /// it addes or updates room, reservation guest, room type, room status, etc.
        /// </summary>
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
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Please fill the missing data or click on a row to update it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Called when the delete button is pressed
        /// it deletes room, reservation guest, room type, room status, etc.
        /// </summary>
        private void DeleteEntity<T>(T entity, int id) where T : class
        {
            if (id != 0)
            {
                try
                {
                    using (GrandLuxEntities db = new GrandLuxEntities())
                    {
                        db.Entry(entity).State = EntityState.Deleted;
                        db.SaveChanges();
                    }

                    MetroMessageBox.Show(this, $"A record has been removed", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData(entity);
                }
                catch (Exception)
                {
                    MetroMessageBox.Show(this, "Something went Wrong, try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Please click on a row to delete it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Using reflection to call the corresponding function to the entity type
        /// the invoked function fills datag= grid view with data and clears input data
        /// </summary>
        private void ShowData<T>(T entity)
        {
            string entityType = typeof(T).ToString().Remove(0, 24);
            string methodName = $"Show{entityType}Data";
            MethodInfo methodInfo = this.GetType().GetMethod(methodName);
            methodInfo.Invoke(this, null);
        }
    }
}
