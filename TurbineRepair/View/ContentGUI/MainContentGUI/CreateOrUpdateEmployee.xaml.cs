using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TurbineRepair.View.ContentGUI.MainContentGUI
{
    /// <summary>
    /// Логика взаимодействия для CreateOrUpdateEmployee.xaml
    /// </summary>
    public partial class CreateOrUpdateEmployee : UserControl
    {
        public CreateOrUpdateEmployee()
        {
            InitializeComponent();
        }

        public class InputRegex
        {
            Regex regex = new Regex("^[0-9]");

            Regex regexPhone = new Regex("^(\\(?\\+?[0-9]*\\)?)?[0-9_\\- \\(\\)]*$");

            public bool NumberInput(string value)
            {
                return !regex.IsMatch(value);
            }

            public bool LetterInput(string value)
            {
                return regex.IsMatch(value);
            }

            public bool PhoneInput(string value)
            {
                return !regexPhone.IsMatch(value);
            }


        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            InputRegex input = new InputRegex();
            if (input.LetterInput(e.Text))
                e.Handled = true;
        }

        private void TextBox_PreviewTextInputNumber(object sender, TextCompositionEventArgs e)
        {
            InputRegex input = new InputRegex();
            if (input.NumberInput(e.Text))
                e.Handled = true;
        }

        private void TextBox_PreviewTextInputPhone(object sender, TextCompositionEventArgs e)
        {
            InputRegex input = new InputRegex();
            if (input.PhoneInput(e.Text))
                e.Handled = true;
        }
    }
}
