using System.Windows;
using System.Windows.Controls;

namespace _16PR_Kolbazov_RPM
{
    public partial class CaesarCipher : Window
    {
        public CaesarCipher()   // Выполняет инициализацию компонентов окна, загружая элементы управления, такие как текстовые поля и кнопки
        {
            InitializeComponent();
        }

        private int shift;  // Переменная которая хранит значение смещения (сдвига)
        private void InputTextChange(object sender, TextChangedEventArgs e) // Обрабатывает событие изменения текста в текстовом поле. Каждый раз, когда текст изменяется, он вызывает метод EncryptText()
        {
            EncryptText();
        }
        private void ShiftTextBox_TextChanged(object sender, TextChangedEventArgs e)    // Обрабатывает событие изменения текста в текстовом поле, где пользователь вводит значение сдвига.
        {
            if (int.TryParse(ShiftTextBox.Text, out int newShift))  // Проверяет, возможно ли преобразовать текст в целое число. Если преобразование удачно, оно присваивает новое значение переменной newShift.
            {
                shift = newShift % 33; // Учитываем размер русского алфавита
                EncryptText();  // После установки нового значения сдвига вызывается метод EncryptText() для пересчета зашифрованного текста с новым сдвигом.
            }
            else
            {
                shift = 0; // Вернуться к значению по умолчанию, если ввод некорректен
            }
        }
        private void EncryptText()  // Отвечает непосредственно за процесс шифрования.
        {
            string inputText = InputTextBox.Text;   // Он получает текст из поля ввода (InputTextBox).
            string encryptedText = CaesarCipher1(inputText, shift); // Для выполнения фактического шифрования текста с учетом текущего значения сдвига.
            OutputTextBox.Text = encryptedText; // Зашифрованный текст записывается в выходное текстовое поле (OutputTextBox).
        }
        private string CaesarCipher1(string input, int shift)   // Этот метод выполняет сам алгоритм шифрования
        {
            string result = string.Empty; // Создает пустую строку, которая будет содержать зашифрованный текст.

            foreach (char c in input)   // Проходит по каждому символу входного текста.
            {
                if (char.IsLetter(c))   // Если символ является буквой (char.IsLetter(c)), проверяет регистр.
                {
                    char offset = char.IsUpper(c) ? 'А' : 'а';
                    char encryptedChar = (char)((((c + shift) - offset) % 33 + 33) % 33 + offset);  // Используется остаток от деления для циклического смещения по алфавиту. 
                    result += encryptedChar;    // Зашифрованный символ добавляется к результату.
                }
                else
                {
                    result += c; // Оставляем символы, которые не являются буквами, без изменений
                }
            }
            return result;  //Возвращает зашифрованную строку.
        }
    }
}
