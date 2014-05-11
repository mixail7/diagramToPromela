using System.Windows;
using System.Windows.Forms;

namespace Modeler
{
    /// <summary>
    /// Логика взаимодействия для RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : Window
    {
        public RenameWindow()
        {
            InitializeComponent();
        }
        
        public RenameWindow(string oldName, string old_type)
        {
            InitializeComponent();
            NewNameTextBox.Text = oldName;
            _type_model = old_type;
            switch (_type_model)
            {
                case ".modelcf":
                    // Поток управления
                    TypeModel.SelectedIndex = 0;
                    break;
                case ".modeld":
                    // Данные
                    TypeModel.SelectedIndex = 1;
                    break;
                case ".modelr":
                    // Ресурсы
                    TypeModel.SelectedIndex = 2;
                    break;
                case ".modele":
                    // Исключения
                    TypeModel.SelectedIndex = 3;
                    break;
            }
        }

        public string _newName = null;
        public string _type_model = null;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _newName = NewNameTextBox.Text;
            switch (TypeModel.SelectedIndex)
            {
                case 0:
                    // Поток управления
                    _type_model = ".modelcf";
                    break;
                case 1:
                    // Данные
                    _type_model = ".modeld";
                    break;
                case 2:
                    // Ресурсы
                    _type_model = ".modelr";
                    break;
                case 3:
                    // Исключения
                    _type_model = ".modele";
                    break;
            }
            this.DialogResult = true;
            Close();
        }
    }
}
