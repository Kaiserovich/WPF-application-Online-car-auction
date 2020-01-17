using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver03.DataBase.FTP;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace ver03.ViewConstructions
{
    class AuctCarPhotos
    {
        public string[] photos;
        int car_id;
        Image img = new Image();
        FileManager menej = new FileManager();
        TextBlock index;

        int nowPhoto = 0;

        public AuctCarPhotos(int car_id, Image img, TextBlock index)
        {
            this.img = img;
            this.car_id = car_id;
            this.index = index;
            menej.DeleteCarBufferFolder();
            menej.CreateCarBuffeFolder();
            menej.LoadCarPhotosInBuffer(car_id);
            photos = menej.GetBufferPhotos();
            UpPhoto();
            SetIndex();
        }

        void UpPhoto()
        {
            try
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(photos[nowPhoto]);
                logo.EndInit();

                img.Source = logo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SetNextPhoto()
        {
            if (nowPhoto < photos.Length - 1)
                nowPhoto += 1;
            else
                nowPhoto = 0;
            UpPhoto();
            SetIndex();
        }
        public void SetLastPhoto()
        {
            if (nowPhoto > 0)
                nowPhoto -= 1;
            else
                nowPhoto = photos.Length - 1;
            UpPhoto();
            SetIndex();
        }
        void SetIndex()
        {
            if (photos.Length > 0)
                index.Text = (nowPhoto + 1).ToString() + @" \ " + photos.Length.ToString();
            else index.Text = @"0 \ 0";
        }
    }
}
