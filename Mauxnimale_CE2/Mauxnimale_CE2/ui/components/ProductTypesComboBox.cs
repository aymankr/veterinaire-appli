using System.Data;
using System.Windows.Forms;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui.components
{
    internal class ProductTypesComboBox : ComboBox
    {
        /// <summary>
        /// Créer une combo box permettant de sélectionner des salariés présents dans la base.
        /// </summary>
        public ProductTypesComboBox()
        {
            // Style
            Name = "ProductTypesList";
            DropDownStyle = ComboBoxStyle.DropDownList;

            // Values
            DataTable types = new DataTable();
            types.Columns.Add("id", typeof(int));
            types.Columns.Add("name", typeof(string));

            ProductController.getTypes().ForEach(type => types.Rows.Add(type.IDTYPE, type.NOMTYPE));
            
            DataRow emptyRow = types.NewRow();
            emptyRow["id"] = -1;
            emptyRow["name"] = "-- Tout --";
            types.Rows.InsertAt(emptyRow, 0);

            ValueMember = "id";
            DisplayMember = "name";
            DataSource = types;
        }
    }
}
