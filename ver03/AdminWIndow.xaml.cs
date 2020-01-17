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
using ver03.FileConstructions.Administartion;
using ver03.DataBase.Administration;



namespace ver03
{
    public partial class AdminWIndow : Window
    {
        DBLoadAdmin dbl = new DBLoadAdmin();
        DBFillAdmin dbf = new DBFillAdmin();

        List<User> users = new List<User>();
        List<User> usersToEdit = new List<User>();

        List<UserInfo> usersInfo = new List<UserInfo>();
        List<UserInfo> usersInfoToEdit = new List<UserInfo>();

        List<UserLog> usersLogs = new List<UserLog>();
        List<UserLog> usersLogsToEdit = new List<UserLog>();

        List<Auction> auctions = new List<Auction>();
        List<Auction> auctionsToEdit = new List<Auction>();

        List<Car> cars = new List<Car>();
        List<Car> carsToEdit = new List<Car>();

        List<ForumSection> forumSections = new List<ForumSection>();
        List<ForumSection> forumSectionsToEdit = new List<ForumSection>();

        List<ForumTopic> forumTopics = new List<ForumTopic>();
        List<ForumTopic> forumTopicsToEdit = new List<ForumTopic>();

        List<ForumAnswer> forumAnswers = new List<ForumAnswer>();
        List<ForumAnswer> forumAnswersToEdit = new List<ForumAnswer>();

        public AdminWIndow()
        {
            InitializeComponent();

            LoadAll();
            UpAll();
        }

        void LoadAll()
        {
            LoadUsersTableCollection();
            LoadUsersInfoTableCollection();
            LoadUsersLogsTableCollection();
            LoadAuctionsTableCollection();
            LoadCarsTableCollection();
            LoadForumSectionsTableCollection();
            LoadForumTopicsTableCollection();
            LoadForumAnswersTableCollection();

            void LoadUsersTableCollection()
            {
                users = dbl.GetUsersTable();
            }
            void LoadUsersInfoTableCollection()
            {
                usersInfo = dbl.GetUsersInfoTable();
            }
            void LoadUsersLogsTableCollection()
            {
                usersLogs = dbl.GetUsersLogsTable();
            }
            void LoadAuctionsTableCollection()
            {
                auctions = dbl.GetAuctionsTable();
            }
            void LoadCarsTableCollection()
            {
                cars = dbl.GetCarsTable();
            }
            void LoadForumSectionsTableCollection()
            {
                forumSections = dbl.GetForumSectionsTable();
            }
            void LoadForumTopicsTableCollection()
            {
                forumTopics = dbl.GetForumTopicsTable();
            }
            void LoadForumAnswersTableCollection()
            {
                forumAnswers = dbl.GetForumAnswersTable();
            }
        }
        void UpAll()
        {
            UpUsersTable();
            UpUsersInfoTable();
            UpUsersLogsTable();
            UpAuctionsTable();
            UpCarsTable();
            UpForumSectionsTable();
            UpForumTopicsTable();
            UpForumAnswersTable();

            void UpUsersTable()
            {
                dg_users.ItemsSource = users;
                ((ComboBox)FindName("cb_users")).ItemsSource = users;
            }
            void UpUsersInfoTable()
            {
                dg_users_info.ItemsSource = usersInfo;
            }
            void UpUsersLogsTable()
            {
                dg_users_log.ItemsSource = usersLogs;
            }
            void UpAuctionsTable()
            {
                ((DataGrid)FindName("dg_auctions")).ItemsSource = auctions;
                ((ComboBox)FindName("cb_auctions")).ItemsSource = auctions;
            }
            void UpCarsTable()
            {
                ((DataGrid)FindName("dg_cars")).ItemsSource = cars;
                ((ComboBox)FindName("cb_cars")).ItemsSource = cars;
            }
            void UpForumSectionsTable()
            {
                ((DataGrid)FindName("dg_forum_section")).ItemsSource = forumSections;
                //((ComboBox)FindName("cb_cars")).ItemsSource = cars;
            }
            void UpForumTopicsTable()
            {
                ((DataGrid)FindName("dg_forum_topics")).ItemsSource = forumTopics;
                ((ComboBox)FindName("cb_forum_topics")).ItemsSource = forumTopics;
                ((ComboBox)FindName("cb_user_topic")).ItemsSource = forumTopics;
            }
            void UpForumAnswersTable()
            {
                ((DataGrid)FindName("dg_forum_answers")).ItemsSource = forumAnswers;
               // ((ComboBox)FindName("cb_forum_topics")).ItemsSource = forumTopics;
            }
        }

        private void dg_users_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            usersToEdit.Add((User)(dg_users.SelectedItem));
        }
        private void dg_users_info_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            usersInfoToEdit.Add((UserInfo)(dg_users_info.SelectedItem));
        }
        private void dg_users_log_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            usersLogsToEdit.Add((UserLog)(dg_users_log.SelectedItem));
        }
        private void dg_auctions_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            auctionsToEdit.Add((Auction)(((DataGrid)sender).SelectedItem));
        }
        private void dg_cars_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            carsToEdit.Add((Car)(((DataGrid)sender).SelectedItem));
        }
        private void dg_forum_section_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            forumSectionsToEdit.Add((ForumSection)(((DataGrid)sender).SelectedItem));
        }
        private void dg_forum_topics_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            forumTopicsToEdit.Add((ForumTopic)(((DataGrid)sender).SelectedItem));
        }
        private void dg_forum_answers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            forumAnswersToEdit.Add((ForumAnswer)(((DataGrid)sender).SelectedItem));
        }

        private void But_Load(object sender, RoutedEventArgs e)
        {
            LoadAll();
            UpAll();
        }
        private void But_UpLoad(object sender, RoutedEventArgs e)
        {
            if (usersToEdit.Count > 0 || usersInfoToEdit.Count > 0 || usersLogsToEdit.Count > 0 || auctionsToEdit.Count > 0 ||
                carsToEdit.Count > 0 || forumSectionsToEdit.Count > 0 || forumTopicsToEdit.Count > 0 || forumAnswersToEdit.Count > 0)
            {
                if (usersToEdit.Count > 0)
                {
                    dbf.UpdateUsers(usersToEdit);
                    usersToEdit = new List<User>();
                }

                if (usersInfoToEdit.Count > 0)
                {
                    dbf.UpdateUsersInfo(usersInfoToEdit);
                    usersInfoToEdit = new List<UserInfo>();
                }
                if (usersLogsToEdit.Count > 0)
                {
                    MessageBox.Show("Изменения логов временно не доступно.");                   
                    //dbf.UpdateUsersLogs(usersLogsToEdit);
                }
                if (auctionsToEdit.Count > 0)
                {
                    dbf.UpdateAuctions(auctionsToEdit);
                    auctionsToEdit = new List<Auction>();
                }
                if (carsToEdit.Count > 0)
                {
                    dbf.UpdateCars(carsToEdit);
                    carsToEdit = new List<Car>();
                    MessageBox.Show("Upload cars");
                }
                if (forumSectionsToEdit.Count > 0)
                {
                    dbf.UpdateForumSections(forumSectionsToEdit);
                    forumSectionsToEdit = new List<ForumSection>();
                    MessageBox.Show("Upload forum_sections");
                }
                if (forumTopicsToEdit.Count > 0)
                {
                    dbf.UpdateForumTopics(forumTopicsToEdit);
                    forumTopicsToEdit = new List<ForumTopic>();
                    MessageBox.Show("Upload table forum_topics");
                }
                if (forumAnswersToEdit.Count > 0)
                {
                    dbf.UpdateForumAnswers(forumAnswersToEdit);
                    forumAnswersToEdit = new List<ForumAnswer>();
                    MessageBox.Show("Upload table forum_answers");
                }

                LoadAll();
                UpAll();
            }
            else
                MessageBox.Show("No data changed");
            
        }

        private void But_del_Auction(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)FindName("cb_auctions")).SelectedItem != null)
            {
                dbf.DeleteAuction(Convert.ToInt32(((ComboBox)FindName("cb_auctions")).SelectedItem.ToString().Split(' ')[0]));
                LoadAll();
            }
        }
        private void But_del_car(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)FindName("cb_cars")).SelectedItem != null)
            {
                dbf.DeleteCar(Convert.ToInt32(((ComboBox)FindName("cb_cars")).SelectedItem.ToString().Split(' ')[0]));
                LoadAll();
            }
        }
        private void But_del_topic(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)FindName("cb_forum_topics")).SelectedItem != null)
            {
                dbf.DeleteForumTopic(Convert.ToInt32(((ComboBox)FindName("cb_forum_topics")).SelectedItem.ToString().Split(' ')[0]));
                LoadAll();
            }
        }

        private void But_del_user_answers(object sender, RoutedEventArgs e)
        {
            if (((ComboBox)FindName("cb_users")).SelectedItem != null && ((ComboBox)FindName("cb_user_topic")).SelectedItem != null)
            {
                dbf.DeleteUserAnswersFromTopic(Convert.ToInt32(((ComboBox)FindName("cb_users")).SelectedItem.ToString().Split(' ')[0]),
                    Convert.ToInt32(((ComboBox)FindName("cb_user_topic")).SelectedItem.ToString().Split(' ')[0]));
                LoadAll();
            }
        }
    }
}
