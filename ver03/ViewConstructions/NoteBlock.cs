using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ver03.FileConstructions;
using System.Windows;

namespace ver03.ViewConstructions
{
    public class NoteBlock
    {
        public Note note;

        public NoteBlock(Note note)
        {
            this.note = note;
        }

        public Grid GetNoteBlock()
        {
            Grid gr_main = new Grid();
            
            TextBlock tb = new TextBlock();
            tb.FontSize = 18;
            tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            tb.Text = note.id_author + " " + note.tittle;

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();

            col1.Width = new GridLength(1, GridUnitType.Star);
            col2.Width = new GridLength(100, GridUnitType.Pixel);

            gr_main.ColumnDefinitions.Add(col1);
            gr_main.ColumnDefinitions.Add(col2);
            

            Button but_context = new Button();
            but_context.FontSize = 18;
            but_context.Content = "Описание";
            but_context.Click += But_context_Click;

            Grid.SetColumn(but_context, 2);

            gr_main.Children.Add(tb);
            gr_main.Children.Add(but_context);

            return gr_main;

            void But_context_Click(object sender, System.Windows.RoutedEventArgs e)
            {
                System.Windows.MessageBox.Show(note.context);
            }
        }

        
    }
}
