using System.Data;
using System.Windows.Forms;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui.components
{
    public class ClientsListBox : ListBox
    {
        public ClientsListBox()
        {
            DataTable clients = new DataTable();
            clients.Columns.Add("id", typeof(int));
            clients.Columns.Add("name", typeof(string));

            ClientController.AllClient().ForEach(client => clients.Rows.Add(client.IDCLIENT, client.ToString()));

            ValueMember = "id";
            DisplayMember = "name";
            DataSource = clients;
        }
    }
}
