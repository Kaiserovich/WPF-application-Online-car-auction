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
using ver03.FileConstructions;
using System.Threading;
using ver03.DataBase;
using System.ComponentModel;
using ver03.ViewModels;


namespace ver03.Views
{
    /// <summary>
    /// Логика взаимодействия для Forum_main.xaml
    /// </summary>
    public partial class Forum_main : UserControl
    {
        DBLoad db;
        DBFill dbf;
        ContentPresenter viewer;
        int selected_section;

        public Forum_main()
        {
            InitializeComponent();
            viewer = OutputView;
            db = new DBLoad();
            dbf = new DBFill();

            ver03.Properties.Settings.Default.Loading = true;
            Thread newThread = new Thread(ShowForum);
            newThread.Start();

        }


        List<ForumTopic> topicks;
        List<ForumSection> sections;

        private void LoadSections()
        {
            sections = db.GetTopicksSections();
        }
        private void LoadTopicks()
        {
            topicks = db.GetAllTopicks();
        }
        private void ShowForum()
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    //Thread.Sleep(TimeSpan.FromSeconds(3)); //SLEEP
                    LoadSections();
                    LoadTopicks();
                    StackPanel vistack = new StackPanel();
                    Expander myexp;

                    if (sections == null)
                    {
                        MessageBox.Show("Ошибка получения секций для форума");
                        ver03.Properties.Settings.Default.Loading = false;
                        return;
                    }

                    foreach (ForumSection x in sections)
                    {
                        StackPanel expanderPanel = new StackPanel();
                        expanderPanel.Margin = new Thickness(-5,0,0,0);
                        myexp = DrawSectionBlock(x);
                        expanderPanel.Children.Add(ButtonColorChanger(GetSectionButton(x.id), GetSectionBrush(x.color)));

                        foreach (ForumTopic z in topicks)
                        {
                            if (z.id_section == x.id)
                                expanderPanel.Children.Add(DrawButton(z));
                        }
                        myexp.Content = expanderPanel;
                        vistack.Children.Add(myexp);
                    }
                    viewer.Content = vistack;
                    ver03.Properties.Settings.Default.Loading = false;
                });
        }

        private Button DrawButton(ForumTopic x)
        {
            TextBlock tb_topic_name = new TextBlock();
            tb_topic_name.Text = x.name;
            tb_topic_name.FontSize = 22;
            tb_topic_name.HorizontalAlignment = HorizontalAlignment.Center;
            tb_topic_name.VerticalAlignment = VerticalAlignment.Center;
            tb_topic_name.Margin = new Thickness(10, -5, 10, 0);

            TextBlock tb_topic_context = new TextBlock();
            if (x.context.Length >= 10)
            tb_topic_context.Text = x.context.Substring(0, 10) + "...";
            else
                tb_topic_context.Text = x.context;
            tb_topic_context.FontSize = 18;
            tb_topic_context.HorizontalAlignment = HorizontalAlignment.Center;
            tb_topic_context.VerticalAlignment = VerticalAlignment.Bottom;
            tb_topic_context.Margin = new Thickness(10, 10, 10, 5);

            TextBlock tb_topic_author = new TextBlock();
            tb_topic_author.Text = "Author:" + x.nick;
            tb_topic_author.FontSize = 16;
            tb_topic_author.HorizontalAlignment = HorizontalAlignment.Right;
            tb_topic_author.Margin = new Thickness(0,5,5,5);

            TextBlock tb_topic_date = new TextBlock();
            tb_topic_date.Text = x.date;
            tb_topic_date.FontSize = 16;
            tb_topic_date.HorizontalAlignment = HorizontalAlignment.Right;
            tb_topic_date.VerticalAlignment = VerticalAlignment.Bottom;
            tb_topic_date.Margin = new Thickness(0, 5, 5, 5);


            Grid g1 = new Grid();
            g1.Height = 60;
            g1.Children.Add(tb_topic_name);
            g1.Children.Add(tb_topic_context);
            g1.Children.Add(tb_topic_author);
            g1.Children.Add(tb_topic_date);

            Button but = new Button();
            but.Tag = x.id;
            but.Background = GetTopicBrush(1);
            but.BorderThickness = new Thickness(0, 0, 0, 0);
            but.Content = g1;
            but.HorizontalContentAlignment = HorizontalAlignment.Stretch;

            but.Click += OpenTopic;
            
            Binding binding = new Binding();


            Binding binding2 = BindingOperations.GetBinding(xBut, Button.CommandProperty); //костыль
            but.SetBinding(Button.CommandProperty, binding2);

            return but;
        }

        private void OpenTopic(object sender, RoutedEventArgs e)
        {
            SetSelectedTopicId(Convert.ToInt32(((Button)sender).Tag));
        }

        private Expander DrawSectionBlock(ForumSection x)
        {           
            TextBlock myblock = new TextBlock();
            myblock.FontSize = 25;
            myblock.Padding = new Thickness(5, 5, 0, 5);
            myblock.Margin = new Thickness(5, 0,0,0);
            myblock.Text = x.name;

            Expander myexp = new Expander();
            myexp.Header = x.name;
            myexp.Padding = new Thickness(5, 0, 0, 0);// Lock at this....
            myexp.FontSize = 25;
            myexp.Background = GetSectionBrush(x.color);
           
            myexp.Header = myblock;    
            return myexp;
        }
        private Button GetSectionButton(int id_section)
        {
            Button but = GetNewButton_NewSection();
            but.Background = GetSectionBrush(1);
            but.Tag = id_section;
            but.Margin = new Thickness(0, -45, 10, 0); // don't look at me)
            but.HorizontalAlignment = HorizontalAlignment.Right;
            but.BorderThickness = new Thickness(0,0,0,0);
            return but;
        }
        private Brush GetSectionBrush(int id)
        {
            switch (id)
            {
                case 0: return Brushes.Red;
                case 1: return Brushes.LightGray;
                case 2: return Brushes.Green;
                case 3: return Brushes.Purple;
                case 4: return Brushes.Orange;
                case 5: return Brushes.White;
                default: return Brushes.Black;
            }
        }
        private Brush GetTopicBrush(int id)
        {
            switch (id)
            {
                case 0: return Brushes.LightPink;
                case 1: return Brushes.White;
                case 2: return Brushes.LightGreen;
                case 3: return Brushes.LightSeaGreen;
                case 4: return Brushes.LightSteelBlue;
                case 5: return Brushes.Linen;
                default: return Brushes.Black;
            }
        }

        private Button GetNewButton_NewSection()
        {
            Button but = new Button();
            but.Content = "+";
            but.Height = 40;
            but.Width = 40;
            but.FontSize = 20;

            but.Template = (ControlTemplate) FindResource("RoundButtonTemplate2");

            but.Click += show_form_create_topic;
            but.Tag = 0;
            return but;
        }
        private Button GetNewButton_NewSection(int section_id)
        {
            Button but = GetNewButton_NewSection();
            but.Tag = section_id;
            return but;
        }
        private void show_form_create_topic(object sender, RoutedEventArgs e)
        {
            addTopicForm.Visibility = Visibility.Visible;
            selected_section = Convert.ToInt32(((Button)sender).Tag);
            ScrollViewDown();
        }
        private void hide_form_create_topic()
        {
            addTopicForm.Visibility = Visibility.Collapsed;
        }

        
        private void But_create_forum_topick_Click(object sender, RoutedEventArgs e)
        {
            if (ver03.Properties.Settings.Default.Autoriz)
            {
                if (dbf.CreateForumTopic(ver03.Properties.Settings.Default.UserID, selected_section, GetTextFromRTB(rtb_topicName), GetTextFromRTB(rtb_topicContext)))
                {
                    ShowForum();
                    hide_form_create_topic();
                }
                else
                {
                    MessageBox.Show("Error 401");
                }
            }
            else
                MessageBox.Show("Для создания темы, нужна авторизация");
        }
        private string GetTextFromRTB(RichTextBox x)
        {
            TextRange z = new TextRange(x.Document.ContentStart, x.Document.ContentEnd);
            //return z.Text.Replace(Environment.NewLine, " ");
            return z.Text;
        }
        private void ScrollViewDown()
        {
            scroll_Forum.ScrollToBottom();
        }

        private Button ButtonColorChanger(Button but, Brush brush)
        {
            Button but1 = but;
            but1.Background = brush;
            return but1;
        }
        private void SetSelectedTopicId(int id)
        {
            Properties.Settings.Default.TopicSelectId = id;
        }
    }
}
