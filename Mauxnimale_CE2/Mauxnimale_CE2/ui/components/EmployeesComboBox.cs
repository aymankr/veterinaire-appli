using System;
using System.Data;
using System.Windows.Forms;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui.components
{
    internal class EmployeesComboBox : ComboBox
    {
        /// <summary>
        /// Créer une combo box permettant de sélectionner des salariés présents dans la base.
        /// </summary>
        public EmployeesComboBox()
        {
            // Style
            Name = "EmployeesList";
            DropDownStyle = ComboBoxStyle.DropDownList;

            // Values
            DataTable employees = new DataTable();
            employees.Columns.Add("id", typeof(int));
            employees.Columns.Add("name", typeof(string));
            employees.Columns.Add("salary", typeof(decimal));
            employees.Columns.Add("internshipStart", typeof(DateTime));
            employees.Columns.Add("internshipEnd", typeof(DateTime));

            foreach (SALARIE employee in UserController.getAllEmployees())
            {
                if (employee.PREMIERECONNEXION)
                    employees.Rows.Add(employee.IDCOMPTE, employee.ToString(), employee.SALAIRE, employee.DATEDEBUTSTAGE, employee.DATEFINSTAGE);
            }

            DataRow emptyRow = employees.NewRow();
            emptyRow["id"] = -1;
            emptyRow["name"] = "-- Veuillez choisir un salarié--";
            employees.Rows.InsertAt(emptyRow, 0);

            ValueMember = "id";
            DisplayMember = "name";
            DataSource = employees;
        }
    }
}
