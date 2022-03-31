using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using Mauxnimale_CE2.ui.components;
using Mauxnimale_CE2.ui.components.componentsTools;
using Mauxnimale_CE2.api.controllers;
using Mauxnimale_CE2.api.entities;

namespace Mauxnimale_CE2.ui.stocks
{
    public class InterfaceProductsTypeManagement : AInterface
    {
        private Header _header;
        private Footer _footer;

        private Label _typeListTitle;
        private ListBox _typesList;
        private TYPE_PRODUIT _selectedType;
        private TextBox _typeNameFilter;

        private UIRoundButton _backButton, _homeButton;
        private UIButton _newTypeButton, _updateButton, _removeButton;

        private TextBox _name;

        public InterfaceProductsTypeManagement(MainWindow window, SALARIE user) : base(window, user)
        {
            _header = new Header(window);
            _footer = new Footer(window, user);
            _selectedType = null;
        }

        #region Génération des composants de l'interface

        private void generateTypesList()
        {
            // Taille & position
            _typesList = new ListBox();
            _typesList.Size = new Size(window.Width / 4, window.Height / 2);
            _typesList.Location = new Point(window.Width / 20, window.Height / 3);

            // Données
            updateTypeList(ProductController.getTypes());

            // Evenements
            _typesList.SelectedValueChanged += onTypeSelected;

            window.Controls.Add(_typesList);
        }

        private void generateTypeNameFilter()
        {
            // Taille & position
            _typeNameFilter = new TextBox();
            _typeNameFilter.Size = new Size(_typesList.Width, _typesList.Width / 10);
            _typeNameFilter.Location = new Point(_typesList.Left, _typesList.Top - _typeNameFilter.Height - 10);

            // Evenements
            _typeNameFilter.TextChanged += onTypeNameChanged;

            window.Controls.Add(_typeNameFilter);
        }

        private void generateNameBox()
        {
            _name = new TextBox();
            _name.Size = new Size(window.Width / 3, window.Height * 5 / 100);
            _name.Location = new Point(_typesList.Right + 100, _typesList.Top);
            _name.Font = new Font("Poppins", window.Height * 3 / 100);
            _name.ForeColor = Color.Gray;
            _name.Text = "";
            _name.Enabled = false;
            _name.TextChanged += onNameChanged;
            window.Controls.Add(_name);
        }

        private void generateButtons()
        {
            _backButton = new UIRoundButton(window.Width / 20, "<");
            _backButton.Location = new Point(window.Width * 9 / 10, window.Height / 10);
            _backButton.Click += backClick;
            window.Controls.Add(_backButton);

            _homeButton = new UIRoundButton(window.Width / 20, "«");
            _homeButton.Location = new Point(window.Width * 8 / 10, window.Height / 10);
            _homeButton.Click += homeClick;
            window.Controls.Add(_homeButton);

            _updateButton = new UIButton(UIColor.ORANGE, "Modifier le nom", window.Width / 4);
            _updateButton.Height = _name.Height;
            _updateButton.Location = new Point(_name.Right + 15, _name.Top);
            _updateButton.Enabled = false;
            _updateButton.Click += onUpdateClick;
            window.Controls.Add(_updateButton);

            _removeButton = new UIButton(UIColor.ORANGE, "Supprimer", window.Width / 4);
            _removeButton.Height = _name.Height;
            _removeButton.Location = new Point(_name.Right + 15, _updateButton.Bottom + 15);
            _removeButton.Enabled = false;
            _removeButton.Click += onRemoveClick;
            window.Controls.Add(_removeButton);

            _newTypeButton = new UIButton(UIColor.ORANGE, "Ajouter un nouveau type", window.Width / 4);
            _newTypeButton.Height = _name.Height;
            _newTypeButton.Location = new Point(_typesList.Right + 100, _typesList.Bottom - _newTypeButton.Height);
            _newTypeButton.Click += onNewClick;
            window.Controls.Add(_newTypeButton);
        }

        private void generateLabels()
        {
            _typeListTitle = new Label();
            _typeListTitle.Size = new Size(_typesList.Width, _typesList.Height / 7);
            _typeListTitle.Location = new Point(_typeNameFilter.Left, _typeNameFilter.Top - _typeListTitle.Height - 10);
            _typeListTitle.Text = "Types de produit";
            _typeListTitle.Font = new Font("Poppins", window.Height * 2 / 100);
            window.Controls.Add(_typeListTitle);
        }

        #endregion

        #region Gestion des événements

        /// <summary>
        /// Remplace l'interface actuelle par celle de l'interface de gestion du stock.
        /// </summary>
        private void backClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceStockManagement(window, user));
        }

        /// <summary>
        /// Retourne à l'écran d'acceuil.
        /// </summary>
        private void homeClick(object sender, EventArgs e)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceHome(window, user));
        }

        private void onTypeSelected(object sender, EventArgs eventArgs)
        {
            _selectedType = ProductController.getTypeById((int)_typesList.SelectedValue);

            _name.Enabled = true;
            _name.Text = _selectedType.NOMTYPE;

            _removeButton.Enabled = true;
        }

        private void onTypeNameChanged(object sender, EventArgs eventArgs)
        {
            if (_typeNameFilter.Text.Length == 0)
                updateTypeList(ProductController.getTypes());
            else
                updateTypeList(ProductController.getTypesByName(_typeNameFilter.Text));
        }

        private void onNameChanged(object sender, EventArgs eventArgs)
        {
            if ((_name.Text.Equals(_selectedType.NOMTYPE) || _name.Text.Length == 0) && _updateButton.Enabled)
                _updateButton.Enabled = false;
            else if (!_name.Text.Equals(_selectedType.NOMTYPE) && _name.Text.Length > 0 && !_updateButton.Enabled)
                _updateButton.Enabled = true;
        }

        private void onUpdateClick(object sender, EventArgs eventArgs)
        {
            string message = "Confirmez-vous le changement de nom : \"" + _selectedType.NOMTYPE + "\" en \"" + _name.Text + "\" ?";
            DialogResult confirmed = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmed == DialogResult.Yes)
            {
                ProductController.updateType(_selectedType, _name.Text);

                // Mise à jour de la vue
                if (_typeNameFilter.Text.Length == 0)
                    updateTypeList(ProductController.getTypes());
                else
                    updateTypeList(ProductController.getTypesByName(_typeNameFilter.Text));

                MessageBox.Show("Le nom du type a été modifié avec succès.", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void onRemoveClick(object sender, EventArgs eventArgs)
        {
            string message = "Confirmez-vous la suppression du type : \"" + _selectedType.NOMTYPE + "et de tous ses produits associés ?";
            DialogResult confirmed = MessageBox.Show(message, "Demande de confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmed == DialogResult.Yes)
            {
                ProductController.removeType(_selectedType);

                // Mise à jour de la vue
                if (_typeNameFilter.Text.Length == 0)
                    updateTypeList(ProductController.getTypes());
                else
                    updateTypeList(ProductController.getTypesByName(_typeNameFilter.Text));

                MessageBox.Show("Le type et tous les produits associés ont été supprimés avec succès.", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void onNewClick(object sender, EventArgs eventArgs)
        {
            window.Controls.Clear();
            window.switchInterface(new InterfaceNewProductType(window, user));
        }

        #endregion

        #region Gestion de la liste des types

        /// <summary>
        /// Met à jour les données de la liste avec la liste donnée.
        /// </summary>
        /// <param name="list">La liste à ajouter</param>
        private void updateTypeList(List<TYPE_PRODUIT> list)
        {
            DataTable types = new DataTable();
            types.Columns.Add("id", typeof(int));
            types.Columns.Add("name", typeof(string));

            list.ForEach(type => types.Rows.Add(type.IDTYPE, type.NOMTYPE));

            _typesList.ValueMember = "id";
            _typesList.DisplayMember = "name";
            _typesList.DataSource = types;
        }

        #endregion

        public override void load()
        {
            _header.load("Mauxnimale - Gestion des types de produit");
            _footer.load();

            generateTypesList();
            generateTypeNameFilter();
            generateNameBox();
            generateButtons();
            generateLabels();
        }
    }
}
