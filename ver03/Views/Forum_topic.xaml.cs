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
using System.ComponentModel;
using System.Windows.Threading;
using ver03.DataBase;
using ver03.FileConstructions;
using System.Threading;

namespace ver03.Views
{
    /// <summary>
    /// Логика взаимодействия для Forum_topic.xaml
    /// </summary>
    public partial class Forum_topic : UserControl
    {
        DBLoad dbl;
        DBFill dbf;
        int id_topic;
        ForumTopic mytopic;
        List<ForumAnswer> answers;
        DispatcherTimer timer = new DispatcherTimer();

        bool is_answer = false;
        ForumAnswer other_answer;

        public Forum_topic()
        {
            InitializeComponent();
            id_topic = ver03.Properties.Settings.Default.TopicSelectId;

            ver03.Properties.Settings.Default.Loading = true;
            Thread tr = new Thread(Load_this_topic);
            tr.Start();

            timer.Tick += new EventHandler(Upload_Answers);
            timer.Interval = new TimeSpan(0, 0, 5);

            timer.Start();
        }

        void Upload_Answers(object sender, EventArgs e)
        {

            Thread tr = new Thread(Load_Answers);
            tr.Start();
            //MessageBox.Show("Tick");
        }


        void Load_this_topic()
        {
           // ver03.Properties.Settings.Default.Loading = true;
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    //Thread.Sleep(TimeSpan.FromSeconds(3)); //SLEEP
                    dbl = new DBLoad();
                    mytopic = dbl.GetTopick(id_topic);
                    tb_header.Text = mytopic.name.Replace(Environment.NewLine, " ");
                    //tb_topic_context.Text = mytopic.context.Replace(Environment.NewLine, " ");
                    tb_topic_context.Text = mytopic.context;
                    Load_Answers();
                    ver03.Properties.Settings.Default.Loading = false;
                });
        }
        void Load_Answers()
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    timer.Stop();
                    dbl = new DBLoad();
                    mytopic = dbl.GetTopick(id_topic);
                    sp_answers.Children.Clear();
                    answers = dbl.GetAnswers(id_topic);

                    foreach (var x in answers)
                    {
                        sp_answers.Children.Add(GetAnswer(x));
                    }
                    timer.Start();
                });
        }
        void Create_Answer()
        {
            if (ver03.Properties.Settings.Default.Autoriz)
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                (ThreadStart)delegate ()
                {
                    dbf = new DBFill();
                    dbf.CreateForumAnswer(ver03.Properties.Settings.Default.UserID, id_topic, GetTextFromRTB(rtb_myanswer));
                    
                    if (is_answer)
                    {
                        dbf.CreateNoteFromForumAnswer(other_answer, ver03.Properties.Settings.Default.UserID, GetTextFromRTB(rtb_myanswer), tb_header.Text);
                        is_answer = false;
                    }
                    rtb_myanswer.Document.Blocks.Clear();
                    Scroll.ScrollToEnd();

                    Load_Answers();
                });
            }
            else MessageBox.Show("Нужна авторизация");
        }
        Grid GetAnswer(ForumAnswer x)
        {
            Grid gr = new Grid();

            TextBlock tb_context = new TextBlock();
            tb_context.FontSize = 18;
            tb_context.Text = x.context.Replace(Environment.NewLine, " ");
            tb_context.TextWrapping = TextWrapping.Wrap;
            //tb_context.Background = Brushes.LightGreen;
            tb_context.Margin = new Thickness(5, 5, 165, 5);

            StackPanel dop_stack = new StackPanel();
            dop_stack.VerticalAlignment = VerticalAlignment.Stretch;

            TextBlock tb_author = new TextBlock();
            tb_author.FontSize = 18;
            tb_author.Text = x.nick_author;
            tb_author.HorizontalAlignment = HorizontalAlignment.Right;
            //tb_author.Background = Brushes.Yellow;
            tb_author.Margin = new Thickness(0, 5, 5, 0);

            TextBlock tb_date = new TextBlock();
            tb_date.FontSize = 18;
            tb_date.Text = x.date_create;
            tb_date.HorizontalAlignment = HorizontalAlignment.Right;
            tb_date.VerticalAlignment = VerticalAlignment.Bottom;
            tb_date.Margin = new Thickness(0,0,5,5);
            //tb_date.Background = Brushes.OrangeRed;

            dop_stack.Children.Add(tb_author);
            dop_stack.Children.Add(tb_date);


            gr.Style = (Style)gr.FindResource("answer_style00");
            gr.Children.Add(tb_context);
            gr.Children.Add(dop_stack);

            gr.Tag = x;
            gr.MouseDown += Answer_CLick;
            return gr;
        }

        private void Answer_CLick(object sender, MouseButtonEventArgs e)
        {
            is_answer = true;
            other_answer = (ForumAnswer)((Grid)sender).Tag;
            rtb_myanswer.AppendText(ver03.Properties.Settings.Default.NickName + ", ");
            Scroll.ScrollToEnd();
        }

        private string GetTextFromRTB(RichTextBox x)
        {
            TextRange z = new TextRange(x.Document.ContentStart, x.Document.ContentEnd);
            return z.Text.Replace(Environment.NewLine, " ");
        }


        private void Create_new_Answer(object sender, RoutedEventArgs e)
        {
            Thread tr = new Thread(Create_Answer);
            tr.Start();
        }
    }
}
