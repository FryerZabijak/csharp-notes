using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.IO;

namespace Notes
{
    /// <summary>
    /// Interakční logika pro EditNote.xaml
    /// </summary>
    public partial class EditNote : Window
    {
        Poznamka p;
        string PATH_TO_NOTES = "C:\\Users\\pepam\\source\\repos\\Notes\\Notes\\Notes";
        string old_name;
        public EditNote([Optional]Poznamka p, string PATH_TO_NOTES = "C:\\Users\\pepam\\source\\repos\\Notes\\Notes\\Notes")
        {
            InitializeComponent();
            
            this.p = p;
            this.PATH_TO_NOTES = PATH_TO_NOTES;
            Textbox_poznamka_nazev.Text = p.Nazev;
            old_name = p.Nazev;
            if(p.Nazev != null)
            {
                StreamReader load_note = new StreamReader(PATH_TO_NOTES + "\\" + p.Nazev + ".txt");
                p.Text = load_note.ReadToEnd();
                load_note.Close();
            }
            Textbox_poznamka_text.Text = p.Text;
        }

        private void Button_ulozit_Click(object sender, RoutedEventArgs e)
        {
            if(Textbox_poznamka_nazev.Text.Length<=0)
            {
                MessageBox.Show("Vyplňte, prosím, název poznámky");
                return;
            }
            p.Nazev = Textbox_poznamka_nazev.Text;
            p.Text = Textbox_poznamka_text.Text;
            if (p.Nazev != old_name) File.Delete(PATH_TO_NOTES + "\\" + old_name + ".txt");
            StreamWriter save_note = new StreamWriter(PATH_TO_NOTES+"\\"+p.Nazev+".txt");
            save_note.Write(p.Text);
            save_note.Close();
            this.Close();
        }

        private void Button_zrusit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
