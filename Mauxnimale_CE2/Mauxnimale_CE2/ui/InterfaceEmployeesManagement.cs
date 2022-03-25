using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceEmployeesManagement : AInterface
    {
        private Header _header;
        private Footer _footer;

        private ComboBox _employeesList;
        private TextBox _lastName, _firstName, _salary;
        private DateTimePicker _internshipStart, _internshipEnd;

        private UIButton _vacationManagementButton, _modifyInfosButton;

        public InterfaceEmployeesManagement(MainWindow window, SALARIE user) : base(window, user) 
        {
            _header = new Header(window);
            _footer = new Footer(window);
            generateComboBox();
            generateVacationButton();
            generateForm();
            generateModifyInfosButton();
        }

        #region Components generation

        private void generateComboBox()
        {
            _employeesList = new ComboBox();
            _employeesList.Name = "EmployeesList";
            _employeesList.Size = new Size(window.Width / 3, 100);
            _employeesList.Location = new Point(50, 200);
            _employeesList.DropDownStyle = ComboBoxStyle.DropDownList;
            _employeesList.Items.Add("-- Veuillez choisir un salarié --");
            _employeesList.Items.AddRange(UserController.getAllEmployees().ToArray());
            _employeesList.SelectedIndex = 0;
            _employeesList.SelectedValueChanged += onEmployeeChosen;
        }

        private void generateVacationButton()
        {
            _vacationManagementButton = new UIButton(UIColor.ORANGE, "Gestion des congés", 200);
            _vacationManagementButton.Name = "VacationManagementButon";
            _vacationManagementButton.Location = new Point(_employeesList.Location.X + _employeesList.Width / 2 - _vacationManagementButton.Width / 2,
                                                           window.Height - 300);
            _vacationManagementButton.Click += new EventHandler(onVacationManagementButtonClick);
        }

        private void generateModifyInfosButton()
        {
            _modifyInfosButton = new UIButton(UIColor.ORANGE, "Modifier les informations", 200);
            _modifyInfosButton.Name = "ModifyInfosButton";
            _modifyInfosButton.Location = new Point(_salary.Location.X + _salary.Width / 2 - _modifyInfosButton.Width / 2, window.Height - 300);
            _modifyInfosButton.Click += new EventHandler(onModifyInfosClick);
            _modifyInfosButton.Enabled = false;
        }

        private void generateForm()
        {
            _lastName = new TextBox();
            _lastName.Size = new Size(window.Width / 5, 0);
            _lastName.Location = new Point(window.Width / 2, 200);
            _lastName.Font = new Font("Poppins", window.Height * 3 / 100);
            _lastName.Enabled = false;

            _firstName = new TextBox();
            _firstName.Size = new Size(window.Width / 5, 0);
            _firstName.Location = new Point(_lastName.Location.X + _lastName.Width + 10, 200);
            _firstName.Font = new Font("Poppins", window.Height * 3 / 100);
            _firstName.Enabled = false;

            _salary = new TextBox();
            _salary.Size = new Size(window.Width / 5 * 2 + 10, 0);
            _salary.Location = new Point(window.Width / 2, 350);
            _salary.Font = new Font("Poppins", window.Height * 3 / 100);

            _internshipStart = new DateTimePicker();
            _internshipStart.Size = new Size(window.Width / 5, window.Height * 3 / 100);
            _internshipStart.Location = new Point(window.Width / 2, 500);

            _internshipEnd = new DateTimePicker();
            _internshipEnd.Size = new Size(window.Width / 5, window.Height * 3 / 100);
            _internshipEnd.Location = new Point(_internshipStart.Location.X + _internshipStart.Width + 10, 500);
        }

        #endregion

        #region Event management

        private void onVacationManagementButtonClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceGestionCongé(window, user));
        }

        private void onEmployeeChosen(object sender, EventArgs e)
        {

        }

        private void onModifyInfosClick(object sender, EventArgs e)
        {
            _salary.Focus();
        }

        #endregion

        public override void load()
        {
            _header.load("Gestion des salariés");
            _footer.load();
            window.Controls.Add(_employeesList);
            window.Controls.Add(_vacationManagementButton);
            window.Controls.Add(_modifyInfosButton);
            window.Controls.Add(_lastName);
            window.Controls.Add(_firstName);
            window.Controls.Add(_salary);
            window.Controls.Add(_internshipStart);
            window.Controls.Add(_internshipEnd);
        }
    }
}
