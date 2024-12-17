using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace _16PR_Kolbazov_RPM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  // Инициализирует компоненты окна
            Notes = new ObservableCollection<Note>();   // Создает новый экземпляр ObservableCollection<Note>, который затем связывается с элементом управления NotesListBox.
            NotesListBox.ItemsSource = Notes;
        }
        public ObservableCollection<Note> Notes { get; set; }   // Rоллекция, которая хранит объекты типа Note. Использование ObservableCollection позволяет автоматически обновлять интерфейс.
        public class Note   // Представляет отдельную заметку с двумя свойствами: Title - Заголовок заметки и Content - Содержимое заметки.
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public override string ToString()
            {
                return $"{Title} - {Content}";
            }
        }
        private void AddNoteButton_Click(object sender, RoutedEventArgs e)  // Обрабатывает событие клика по кнопке добавления заметки.
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || string.IsNullOrWhiteSpace(ContentTextBox.Text)) // Проверяет, заполнены ли заголовок и текст заметки.
            {
                MessageBox.Show("Пожалуйста, заполните заголовок и текст заметки!");    // Если нет, выводит сообщение об ошибке.
                return;
            }
            var newNote = new Note { Title = TitleTextBox.Text, Content = ContentTextBox.Text };    // Создает новую заметку (newNote) с заголовком и содержимым из соответствующих текстовых полей.
            Notes.Add(newNote); // Добавляет новую заметку в коллекцию.
            ClearInputFields(); // Вызывает метод ClearInputFields(), чтобы очистить текстовые поля после добавления заметки.
        }
        private void EditNoteButton_Click(object sender, RoutedEventArgs e) // Обрабатывает событие клика по кнопке редактирования заметки.
        {
            if (NotesListBox.SelectedItem is Note selectedNote) // Проверяет, выбрана ли какая-либо заметка в списке.
            {
                if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || string.IsNullOrWhiteSpace(ContentTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните заголовок и текст заметки!");
                    return;
                }
                selectedNote.Title = TitleTextBox.Text; // Обновляет заголовок и содержимое выбранной заметки на значения из соответствующих текстовых полей.
                selectedNote.Content = ContentTextBox.Text;
            }
        }
        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)   // Обрабатывает событие клика по кнопке удаления заметки.
        {
            if (NotesListBox.SelectedItem is Note selectedNote) // Проверяет, выбрана ли какая-либо заметка.
            {
                Notes.Remove(selectedNote); // Удаляет из коллекции Notes.
                ClearInputFields(); // Вызывает метод ClearInputFields(), чтобы очистить текстовые поля после удаления.
            }
        }
        private void NotesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)  // Обрабатывает события изменения выбора в ListBox.
        {
            if (NotesListBox.SelectedItem is Note selectedNote) // Если выбрана заметка, устанавливает текстовые поля для редактирования значениями заголовка и содержимого этой заметки.
            {
                TitleTextBox.Text = selectedNote.Title;
                ContentTextBox.Text = selectedNote.Content;

                EditNoteButton.IsEnabled = true;    // Активирует кнопки редактирования и удаления.
                DeleteNoteButton.IsEnabled = true;
            }
            else
            {
                EditNoteButton.IsEnabled = false;   // Если ничего не выбрано, кнопки отключаются.
                DeleteNoteButton.IsEnabled = false;
            }
        }
        private void CleanTitleTextBlock()  // Отвечает за удаление выбранной заметки из списка.
        {
            if (NotesListBox.SelectedItem is Note selectedNote) // Если в списке NotesListBox выбрана заметка, она будет удалена из коллекции Notes.
            {
                Notes.Remove(selectedNote);
                ClearInputFields(); // Вызывается метод ClearInputFields() для очистки текстовых полей.
                CleanTitleTextBlock(); // Очищаем заголовок после удаления заметки.
            }
        }
        private void TitleTextBox_GotFocus(object sender, RoutedEventArgs e)    // Обрабатывает событие получения фокуса полем ввода заголовка.
        {
            TitlePlaceHolder.Visibility = Visibility.Collapsed; // Скрывает плейсхолдер при фокусе
        }
        private void TitleTextBox_LostFocus(object sender, RoutedEventArgs e)   // Обрабатывает событие потери фокуса полем ввода заголовка.
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))   // Если поле пустое.
            {
                TitlePlaceHolder.Visibility = Visibility.Visible;   // Показывает плейсхолдерб.
            }
        }
        private void TitleTextBox_TextChanged(object sender, TextChangedEventArgs e)    // Обрабатывает событие изменения текста в поле ввода заголовка.
        {
            if (!string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                TitlePlaceHolder.Visibility = Visibility.Collapsed; // Если в поле есть текст, плейсхолдер скрывается.
            }
            else
            {
                TitlePlaceHolder.Visibility = Visibility.Visible;   // Если текстовое поле пустое, плейсхолдер отображается вновь.
            }
        }
        private void ClearInputFields() // Используется для очистки текстовых полей ввода
        {
            TitleTextBox.Clear();   //  Вызывает метод Clear() для обоих полей, чтобы сбросить их значение.
            ContentTextBox.Clear();
        }
        private void ContentTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ContentPlaceHolder.Visibility = Visibility.Collapsed;
        }
        private void ContentTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContentTextBox.Text))
            {
                ContentPlaceHolder.Visibility = Visibility.Visible;   // Показывает плейсхолдерб если текст пуст
            }
        }
        private void ContentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ContentTextBox.Text))
            {
                ContentPlaceHolder.Visibility = Visibility.Collapsed; // Скрывает плейсхолдер, если текст введен
            }
            else
            {
                ContentPlaceHolder.Visibility = Visibility.Visible;    // Показывает плейсхолдер, если текст пуст
            }
        }
    }
}