using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Realm2
{
    /// <summary>
    /// Interaction logic for LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window
    {
        Library library;
        public LibraryWindow(Library _library)
        {
            InitializeComponent();
            library = _library;
            bookBox.ItemsSource = library.books;
        }

        private void bookBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book book = bookBox.SelectedItem as Book;
            bookDesc.Text = book.name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Book book = bookBox.SelectedItem as Book;
            if (bookBox.SelectedItem != null)
                library.Interact(book);
        }
    }
}
