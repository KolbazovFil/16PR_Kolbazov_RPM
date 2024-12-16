using System.Windows;
using System.Windows.Controls;

namespace _16PR_Kolbazov_RPM
{
    public partial class CaesarCipher : Window
    {
        public CaesarCipher()
        {
            InitializeComponent();
        }
        private int shift;
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EncryptText();
        }
        private void ShiftTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(ShiftTextBox.Text, out int newShift))
            {
                shift = newShift % 33; // Учитываем размер русского алфавита
                EncryptText();
            }
            else
            {
                shift = 0; // Вернуться к значению по умолчанию, если ввод некорректен
            }
        }
        private void EncryptText()
        {
            string inputText = InputTextBox.Text;
            string encryptedText = CaesarCipher1(inputText, shift);
            OutputTextBox.Text = encryptedText;
        }
        private string CaesarCipher1(string input, int shift)
        {
            string result = string.Empty;

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'А' : 'а'; // Используем русские буквы
                    char encryptedChar = (char)((((c + shift) - offset) % 33 + 33) % 33 + offset);
                    result += encryptedChar;
                }
                else
                {
                    result += c; // Оставляем символы, которые не являются буквами, без изменений
                }
            }
            return result;
        }
    }
}
