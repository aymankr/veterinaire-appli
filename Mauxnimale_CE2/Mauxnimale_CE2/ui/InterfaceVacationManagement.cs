using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers;

namespace Mauxnimale_CE2.ui
{
    internal class InterfaceVacationManagement : AInterface
    {
        private readonly Header _header;
        private readonly Footer _footer;
        private UIRoundButton _returnBtn, _homeBtn;

        private SALARIE _selectedEmployee;

        // composants de l'interface
        private EmployeesComboBox _employeesList;
        private Label _nbVacationsRemaining;
        private MonthCalendar _calendar;
        private UIButton _addVacationButton, _removeVacationButton;

        public InterfaceVacationManagement(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);
            _selectedEmployee = null;

            generateReturnButtons();
            generateEmployeesList();
            generateVacationsRemaining();
            generateMonthCalendar();
            generateButtons();

            _calendar.Enabled = false;
            _addVacationButton.Enabled = false;
            _removeVacationButton.Enabled = false;
            clearInfos();
        }

        #region Composants de l'interface

        private void generateReturnButtons()
        {
            _returnBtn = new UIRoundButton(window.Width / 20, "<");
            _returnBtn.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            _returnBtn.Click += onReturnButtonClick;

            _homeBtn = new UIRoundButton(window.Width / 20, "«");
            _homeBtn.Location = new Point(window.Width * 8 / 10, window.Height / 10);
            _homeBtn.Click += onHomeButtonClick;
        }

        private void generateEmployeesList()
        {
            _employeesList = new EmployeesComboBox();
            _employeesList.Size = new Size(window.Width / 3, 100);
            _employeesList.Location = new Point(50, 200);

            _employeesList.SelectedValueChanged += onEmployeeChosen;
        }

        private void generateVacationsRemaining()
        {
            Label title = new Label();
            title.Parent = window;
            title.Size = new Size(window.Width / 4 - 20, 50);
            title.Location = new Point((window.Width / 2) + 20, 200 - _employeesList.Height /2);
            title.Font = new Font("Arial", 20, FontStyle.Regular);
            title.Text = "Jours de congés restants :";

            _nbVacationsRemaining = new Label();
            _nbVacationsRemaining.Size = new Size(100, 50);
            _nbVacationsRemaining.Location = new Point(title.Right + 2, 200 - _employeesList.Height / 2);
            _nbVacationsRemaining.Font = new Font("Arial", 20, FontStyle.Regular);
            _nbVacationsRemaining.Text = "30";
        }

        private void generateMonthCalendar()
        {
            _calendar = new MonthCalendar();
            _calendar.Size = new Size(window.Width / 3, window.Height / 3);
            _calendar.Location = new Point((window.Width / 2) + 10, _nbVacationsRemaining.Bottom + 80);

            _calendar.DateSelected += onDateChosen;

            Label title = new Label();
            title.Parent = window;
            title.Size = new Size(window.Width / 4 - 20, 50);
            title.Location = new Point(_calendar.Left + (_calendar.Width / 2) - title.Width / 2, _nbVacationsRemaining.Bottom + 30);
            title.Font = new Font("Arial", 20, FontStyle.Bold);
            title.Text = "Calendrier des congés";
        }

        private void generateButtons()
        {
            _addVacationButton = new UIButton(UIColor.ORANGE, "Add vacation", 250);
            _addVacationButton.Location = new Point(_calendar.Left - 25, window.Height - 300);
            _addVacationButton.Click += onAddVacationClick;

            _removeVacationButton = new UIButton(UIColor.ORANGE, "Remove vacation", 250);
            _removeVacationButton.Location = new Point(_addVacationButton.Right + 20, window.Height - 300);
            _removeVacationButton.Click += onRemoveVacationClick;
        }

        #endregion

        #region Gestion des évenements

        private void onEmployeeChosen(object sender, EventArgs eventArgs)
        {
            if ((int)_employeesList.SelectedValue == -1)
            {
                _selectedEmployee = null;
                clearInfos();
                _calendar.Enabled = false;
                _addVacationButton.Enabled = false;
                _removeVacationButton.Enabled = false;
                return;
            }

            _selectedEmployee = UserController.getEmployeeWithId((int)_employeesList.SelectedValue);
            fillInfos();
            _calendar.Enabled = true;
        }

        private void onDateChosen(object sender, EventArgs eventArgs)
        {
            DateTime chosenDate = _calendar.SelectionStart;

            // Récupération des dates de congés
            List<DateTime> vacationsDates = new List<DateTime>();
            List<JOURNEE_SALARIE> employeeVacations = DayController.getEmployeeVacations(_selectedEmployee);
            employeeVacations.ForEach(day => vacationsDates.Add(day.JOURNEE.DATE));

            // Autorise l'utilisateur a supprimer le jour de congé s'il y en a un pour la date sélectionnée
            if (vacationsDates.Contains(chosenDate))
            {
                _removeVacationButton.Enabled = true;
                _addVacationButton.Enabled = false;
            }
            else // Autorise l'utilisateur a ajouter un jour de congé sur le jour choisi s'il lui en reste assez
            {
                _addVacationButton.Enabled = true;
                _removeVacationButton.Enabled = false;
            }
        }

        /// <summary>
        /// Ajoute un jour de congé sur la date sélectionné s'il en reste assez ou si l'utilisateur force l'ajout.
        /// </summary>
        private void onAddVacationClick(object sender, EventArgs eventArgs)
        {
            string message, caption;
            if (int.Parse(_nbVacationsRemaining.Text) > 0) // Signifie que le salarié a des jours de congés restants
            {
                message = "Confirmer l'ajout d'un jour de congé pour " + _selectedEmployee.NOM + " " + _selectedEmployee.PRENOM + " le " 
                           + _calendar.SelectionStart.ToShortDateString() + " ?";
                caption = "Demande de confirmation";
            }
            else
            {
                message = _selectedEmployee.NOM + " " + _selectedEmployee.PRENOM + " a dépassé les 30 jours de congés annuel normalement authorisés.\n" +
                                 "Souhaitez-vous en ajouter un nouveau pour le " + _calendar.SelectionStart.Date.ToString() + " malgré tout ?";
                caption = "Quota de congés atteint";

            }

            DialogResult confirm = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                setVacationOnDate(_selectedEmployee, _calendar.SelectionStart.Date, true);
                fillInfos();
                _addVacationButton.Enabled = false;
                _removeVacationButton.Enabled = true;
            }
        }


        /// <summary>
        /// Supprime un congé sur le jour sélectionnée. Suppose qu'il existe un congé sur le jour sélectionné.
        /// </summary>
        private void onRemoveVacationClick(object sender, EventArgs eventArgs)
        {
            string message = "Confirmer la suppression du jour de congé pour " + _selectedEmployee.NOM + " " + _selectedEmployee.PRENOM + " le "
                           + _calendar.SelectionStart.ToShortDateString() + " ?";
            DialogResult confirm = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                setVacationOnDate(_selectedEmployee, _calendar.SelectionStart.Date, false);
                fillInfos();
                _addVacationButton.Enabled = false;
                _removeVacationButton.Enabled = true;
            }
        }

        private void onReturnButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceEmployeesManagement(window, user));
        }

        private void onHomeButtonClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        #endregion

        #region Infos management

        private void clearInfos()
        {
            _nbVacationsRemaining.Text = "0";
            _calendar.RemoveAllBoldedDates();
            _calendar.UpdateBoldedDates();
        }

        private void fillInfos()
        {
            // Ajout du nombre de jours de congés restants
            List<JOURNEE_SALARIE> employeeVacations = DayController.getEmployeeVacations(_selectedEmployee);
            _nbVacationsRemaining.Text = (30 - employeeVacations.Count).ToString();

            // Visualisation de ces jours dans le calendrier
            _calendar.RemoveAllBoldedDates();
            employeeVacations.ForEach(day => _calendar.AddBoldedDate(day.JOURNEE.DATE));
            _calendar.UpdateBoldedDates();

            DateTime chosenDate = _calendar.SelectionStart;

            // Récupération des dates de congés
            List<DateTime> vacationsDates = new List<DateTime>();
            employeeVacations.ForEach(day => vacationsDates.Add(day.JOURNEE.DATE));

            // Autorise l'utilisateur a supprimer le jour de congé s'il y en a un pour la date sélectionnée
            if (vacationsDates.Contains(chosenDate))
            {
                _removeVacationButton.Enabled = true;
                _addVacationButton.Enabled = false;
            }
            else // Autorise l'utilisateur a ajouter un jour de congé sur le jour choisi s'il lui en reste assez
            {
                _addVacationButton.Enabled = true;
                _removeVacationButton.Enabled = false;
            }
        }

        #endregion

        #region Vacations management

        /// <summary>
        /// Pose ou supprime une jour de congé pour l'employé donné sur la date donnée. Ajoute la date si elle n'existe pas dans la bd.
        /// </summary>
        /// <param name="employee">L'employé concerné</param>
        /// <param name="date">La date concernée</param>
        /// <param name="vacation">true pour ajouter un congé, false pour supprimer.</param>
        private void setVacationOnDate(SALARIE employee, DateTime date, bool vacation)
        {
            // Récupère la journée liée dans la base de données
            JOURNEE dayToAdd = DayController.getDay(date);
            if (dayToAdd == null)   // La créer si elle n'existe pas encore
                DayController.addDay(date);
            dayToAdd = DayController.getDay(date);

            // Récupère la journée liée à l'employée dans la base de données
            JOURNEE_SALARIE employeeDayToAdd = DayController.getDayEmployee(dayToAdd, employee);
            if (employeeDayToAdd == null)   // La créer si elle n'existe pas encore
                DayController.addDayEmployee(_selectedEmployee, dayToAdd, "08:00", "18:00");
            employeeDayToAdd = DayController.getDayEmployee(dayToAdd, employee);

            // Ajouter le congé sur cette journée
            DayController.setVacation(employeeDayToAdd, vacation);
        }

        #endregion

        public override void load()
        {
            _header.load("Mauxnimale - Gestion Congé");
            _footer.load();

            window.Controls.Add(_homeBtn);
            window.Controls.Add(_returnBtn);
            window.Controls.Add(_employeesList);
            window.Controls.Add(_nbVacationsRemaining);
            window.Controls.Add(_calendar);
            window.Controls.Add(_addVacationButton);
            window.Controls.Add(_removeVacationButton);
        }
    }
}