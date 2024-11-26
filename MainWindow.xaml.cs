using System.Collections.ObjectModel;
using System.Windows;

namespace _16PR_Kolbazov_RPM
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Note> Notes { get; set; }
        public class Note
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public override string ToString()
            {
                return $"{Title} - {Content}";
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Notes = new ObservableCollection<Note>();
            NotesListBox.ItemsSource = Notes;
        }
        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || string.IsNullOrWhiteSpace(ContentTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните заголовок и текст заметки!");
                return;
            }
            var newNote = new Note { Title = TitleTextBox.Text, Content = ContentTextBox.Text };
            Notes.Add(newNote);
            ClearInputFields();
        }
        private void EditNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedItem is Note selectedNote)
            {
                if (string.IsNullOrWhiteSpace(TitleTextBox.Text) || string.IsNullOrWhiteSpace(ContentTextBox.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните заголовок и текст заметки!");
                    return;
                }
                selectedNote.Title = TitleTextBox.Text;
                selectedNote.Content = ContentTextBox.Text;
            }
        }
        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedItem is Note selectedNote)
            {
                Notes.Remove(selectedNote);
                ClearInputFields();
            }
        }
        private void NotesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (NotesListBox.SelectedItem is Note selectedNote)
            {
                TitleTextBox.Text = selectedNote.Title;
                ContentTextBox.Text = selectedNote.Content;

                EditNoteButton.IsEnabled = true;
                DeleteNoteButton.IsEnabled = true;
            }
            else
            {
                EditNoteButton.IsEnabled = false;
                DeleteNoteButton.IsEnabled = false;
            }
        }
        private void ClearInputFields()
        {
            TitleTextBox.Clear();
            ContentTextBox.Clear();
        }
        private void CleanTitleTextBlock()
        {
            if (NotesListBox.SelectedItem is Note selectedNote)
            {
                Notes.Remove(selectedNote);
                ClearInputFields();
                CleanTitleTextBlock(); // Очищаем заголовок после удаления заметки
            }
        }
    }
}