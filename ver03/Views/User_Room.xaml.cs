using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using ver03.DataBase;
using ver03.FileConstructions;
using ver03.ViewConstructions;

namespace ver03.Views
{
    /// <summary>
    /// Логика взаимодействия для User_Room.xaml
    /// </summary>
    public partial class User_Room : UserControl
    {
        DBLoad dbl = new DBLoad();
        DBFill dbf = new DBFill();
        List<Note> notes = new List<Note>();
        int user_id = ver03.Properties.Settings.Default.UserID;

        ver03.DataBase.Administration.DBLoadAdmin dblA = new DataBase.Administration.DBLoadAdmin();

        public ver03.FileConstructions.Administartion.UserInfo user_inf;

        public User_Room()
        {
            InitializeComponent();
            LoadAndUpNotes();
            LoadAndUpUserInfo();
        }

        public void LoadAndUpNotes()
        {
            Task task = new Task(LoadNotesCollection);
            task.Start();

            void LoadNotesCollection()
            {
                notes = dbl.GetUserNotes(user_id);
                UpNotes();
            }
            void UpNotes()
            {
                foreach (var note in notes)
                {
                    NoteBlock block = new NoteBlock(note);
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                        (ThreadStart)delegate ()
                        {
                            sp_notes.Children.Add(block.GetNoteBlock());
                        });
                }
            }
        }
        public void LoadAndUpUserInfo()
        {
            Task task = new Task(LoadUser);
            task.Start();

            void LoadUser()
            {
                user_inf = dblA.GetUsersInfoById(user_id);
                UpUserInfo();
            }
            void UpUserInfo()
            {
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle,
                    (ThreadStart)delegate ()
                    {
                        tb_login.Text = ver03.Properties.Settings.Default.NickName;
                        tb_name.Text = user_inf.name;
                        tb_telephone.Text = user_inf.telephone;
                        tb_email.Text = user_inf.email;
                    });
            }
        }
    }
}
