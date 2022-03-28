using System;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceEmployeesManagement : AInterface
    {
        private readonly Header _header;
        private readonly Footer _footer;
        private UIRoundButton _homeBtn;

        private SALARIE _selectedEmployee;

        private ComboBox _employeesList;
        private TextBox _lastName, _firstName, _salary;
        private DateTimePicker _internshipStart, _internshipEnd;

        private UIButton _vacationManagementButton, _modifyInfosButton;

        public InterfaceEmployeesManagement(MainWindow window, SALARIE user) : base(window, user) 
        {
            _header = new Header(window);
            _footer = new Footer(window, user);
            _selectedEmployee = null;

            generateHomeButton();
            generateComboBox();
            generateVacationButton();
            generateForm();
            generateModifyInfosButton();
            setFormEnabled(false);
        }

        #region Components generation

        private void generateHomeButton()
        {
            _homeBtn = new UIRoundButton(window.Width / 20, "<");
            _homeBtn.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            _homeBtn.Click += onHomeButtonClick;
        }


        private void generateComboBox()
        {
            _employeesList = new EmployeesComboBox();
            _employeesList.Size = new Size(window.Width / 3, 100);
            _employeesList.Location = new Point(50, 200);

            // Events
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
        private void onHomeButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        private void onVacationManagementButtonClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceVacationManagement(window, user));
        }

        private void onEmployeeChosen(object sender, EventArgs e)
        {
            int employeeId = (int)_employeesList.SelectedValue;
            if (employeeId == -1)   // Case "veuillez choisir un salarié selectionnée"
            {
                clearForm();
                setFormEnabled(false);
                return;
            }

            _selectedEmployee = UserController.getEmployeeWithId(employeeId);
            setFormEnabled(true);
            fillForm();
        }

        /// <summary>
        /// Modifie les informations changées concernant l'employé s'il y en a.
        /// Suppose qu'un employé est actuellement sélectionné.
        /// </summary>
        private void onModifyInfosClick(object sender, EventArgs e)
        {
            // Vérification des entrées
            if (!anyInfoChanged())
            {
                MessageBox.Show("Aucune information n'a été modifiée.", "Echec de l'opération", MessageBoxButtons.OK);
                return;
            }
            if (!isSalaryValid())
            {
                MessageBox.Show("Le salaire entré n'est pas valide. Pensez à utiliser un point à la place de la virgule", "Entrée non valide.", MessageBoxButtons.OK);
                return;
            }
            if (!areInternshipDatesValid())
            {
                MessageBox.Show("Les dates de stage entrées ne sont pas valides. La date de début de stage doit être antérieur à la date de fin de stage.", 
                                "Entrée non valide.", MessageBoxButtons.OK);
                return;
            }

            // Demander confirmation
            DialogResult confirmed = MessageBox.Show("Confirmer les modifications ?", "Demande de confirmation", MessageBoxButtons.YesNo);
            if (confirmed == DialogResult.Yes)
            {
                // Mise à jour des informations modifiées
                if (!_salary.Text.Equals(_selectedEmployee.SALAIRE.ToString()))
                    UserController.updateSalary(_selectedEmployee, decimal.Parse(_salary.Text));

                if (_internshipStart.Value != _internshipStart.MinDate &&
                    (!_internshipStart.Value.Equals(_selectedEmployee.DATEDEBUTSTAGE) ||
                    !_internshipEnd.Value.Equals(_selectedEmployee.DATEFINSTAGE)))
                    UserController.updateInternshipDates(_selectedEmployee, _internshipStart.Value, _internshipEnd.Value);
            }
        }

        #endregion

        #region Form management

        /// <summary>
        /// Autorise ou interdit l'utilisateur du logiciel à modifier les champs du formulaires (excépté nom et prénom).
        /// </summary>
        /// <param name="enabled">true pour autoriser, false pour interdire.</param>
        private void setFormEnabled(bool enabled)
        {
            _salary.Enabled = enabled;
            _internshipStart.Enabled = enabled;
            _internshipEnd.Enabled = enabled;
            _modifyInfosButton.Enabled = enabled;
        }

        /// <summary>
        /// Rempli les informations du formulaire avec ceux de l'employé actuellement sélectionné.
        /// Suppose qu'un employé est sélectionné.
        /// </summary>
        private void fillForm()
        {
            _firstName.Text = _selectedEmployee.PRENOM;
            _lastName.Text = _selectedEmployee.NOM;
            _salary.Text = _selectedEmployee.SALAIRE.ToString();

            if (_selectedEmployee.DATEDEBUTSTAGE != null && _selectedEmployee.DATEFINSTAGE != null)
            {
                _internshipStart.Value = (DateTime)_selectedEmployee.DATEDEBUTSTAGE;
                _internshipEnd.Value = (DateTime)_selectedEmployee.DATEFINSTAGE;
            }
            else
            {
                _internshipStart.Value = _internshipStart.MinDate;
                _internshipEnd.Value = _internshipEnd.MinDate;
            }
        }

        /// <summary>
        /// Efface tous les champs du formulaire (met les dates à aujourd'hui).
        /// </summary>
        private void clearForm()
        {
            _firstName.Text = "";
            _lastName.Text = "";
            _salary.Text = "";
            _internshipStart.Value = _internshipStart.MinDate;
            _internshipEnd.Value = _internshipEnd.MinDate;
        }

        #endregion

        #region Form input verification

        /// <summary>
        /// Détermine si au moins une information  a été modifiée dans le formulaire.
        /// </summary>
        /// <returns>true si au moins une information a été modifiée, false sinon.</returns>
        private bool anyInfoChanged()
        {
            if (_selectedEmployee == null)   // Si aucun employé n'a été sélectionné, pas besoin de vérifier
                return false;

            if (!_salary.Text.Equals(_selectedEmployee.SALAIRE.ToString()))
                return true;
            if (_internshipStart.Value != _internshipStart.MinDate &&
                !_internshipStart.Value.Equals(_selectedEmployee.DATEDEBUTSTAGE))
                return true;
            if (_internshipEnd.Value != _internshipStart.MinDate &&
                !_internshipEnd.Value.Equals(_selectedEmployee.DATEFINSTAGE))
                return true;

            return false;
        }

        /// <summary>
        /// Détermine si la valeur rentrée pour le salaire est valide.
        /// </summary>
        /// <returns>true si la valeur rentrée pour le salaire est valide, false sinon.</returns>
        private bool isSalaryValid()
        {
            decimal enteredSalary;
            if (decimal.TryParse(_salary.Text, out enteredSalary))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Détermine si les dates de stages entrées sont valides. 
        /// Début de stage < fin de stage
        /// </summary>
        /// <returns>true si les dates de stages entrés sont valides.</returns>
        private bool areInternshipDatesValid()
        {
            if (_internshipStart.Value == _internshipStart.MinDate ||
                _internshipStart.Value.CompareTo(_internshipEnd.Value) < 0)
                return true;

            return false;
        }

        #endregion

        public override void load()
        {
            _header.load("Gestion des salariés");
            _footer.load();

            window.Controls.Add(_homeBtn);
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
