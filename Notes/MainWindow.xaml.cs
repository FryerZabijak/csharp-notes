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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Notes
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static readonly string PATH_TO_NOTES = "C:\\Users\\pepam\\source\\repos\\Notes\\Notes\\Notes";
        ObservableCollection<Poznamka> notes = new ObservableCollection<Poznamka>();

        public MainWindow()
        {
            InitializeComponent();

            Listbox_poznamky.ItemsSource = notes;

            string[] files = Directory.GetFiles(PATH_TO_NOTES);
            FilesInListBox(files);
        }

        private void FilesInListBox(string[] files)
        {
            foreach (string file in files)
            {
                string[] splitted_lomeno = file.Split('\\');
                string[] splitted_tecka = splitted_lomeno[splitted_lomeno.Length - 1].Split('.');
                if (splitted_tecka[1].Contains("txt"))
                {
                    Poznamka poznamka = new Poznamka
                    {
                        Nazev = splitted_tecka[0]
                    };
                    notes.Add(poznamka);
                }
            }
        }

        private void Button_odstranit_poznamku_Click(object sender, RoutedEventArgs e)
        {
            if (Listbox_poznamky.SelectedItem == null) return;

            MessageBoxResult res = MessageBox.Show("Jsi si jist, že chceš smazat poznámku?", "Smazání poznámky", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.No) return;

            object selected = Listbox_poznamky.SelectedItem;
            notes.Remove((Poznamka)selected);
            File.Delete(PATH_TO_NOTES+"\\"+selected+".txt");
        }

        private void Listbox_poznamky_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditNote((Poznamka)Listbox_poznamky.SelectedItem);
        }

        private void Button_vytvorit_poznamku_Click(object sender, RoutedEventArgs e)
        {
            Poznamka p = EditNote();
            if(p.Nazev != "" && p.Nazev!=null)
                notes.Add(p);
        }

        private Poznamka EditNote([Optional] Poznamka p)
        {
            if(p == null)
                p = new Poznamka();
            EditNote editNote = new EditNote(p, PATH_TO_NOTES);
            editNote.ShowDialog();
            Listbox_poznamky.Items.Refresh();
            return p;
        }
    }
}
