using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ver03.FileConstructions;
using System.Windows.Media;
using System.Windows;
using ver03.DataBase.FTP;
using System.Windows.Media.Imaging;

namespace ver03.ViewConstructions
{
    public class AuctionBlock
    {
        public CarAutopark myCar;
        public Auction auction;
        public FileManager menej = new FileManager();
        string[] myCarPhotos;
        object lastPhotoButTem;
        object nextPhotoButTem;

        public AuctionBlock(CarAutopark myCar, object butL, object butR, Auction auction)
        {
            this.auction = auction;
            lastPhotoButTem = butL;
            nextPhotoButTem = butR;
            this.myCar = myCar;
            myCarPhotos = menej.GetPhotosByCar(myCar.id);
        }

        public Grid GetBlock()
        {
            Image nowImage = new Image();
            TextBlock selPhotoIndicator = new TextBlock();
            int nowCarPhotoID = 0;
            Brush backBrush = Brushes.Gainsboro;

            Grid mainGrid = new Grid();
            mainGrid.Height = 300;
            mainGrid.Background = backBrush;

            ColumnDefinition def1 = new ColumnDefinition();
            def1.Width = new GridLength(300, GridUnitType.Pixel);
            ColumnDefinition def2 = new ColumnDefinition();
            def2.Width = new GridLength(1, GridUnitType.Star);

            mainGrid.ColumnDefinitions.Add(def1);
            mainGrid.ColumnDefinitions.Add(def2);

            mainGrid.Children.Add(LeftMainGridColumn());
            mainGrid.Children.Add(RightMainGridColumn());

            return mainGrid;

            Image GetMainCarPhoto()
            {
                try
                {
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.UriSource = new Uri(myCarPhotos[nowCarPhotoID]);
                    logo.EndInit();

                    nowImage.Margin = new Thickness(10, 10, 10, 90);
                    nowImage.Source = logo;

                    return nowImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return new Image();
                }
            }
            Grid LeftMainGridColumn()
            {
                Grid lgr = new Grid();
                lgr.Children.Add(GetMainCarPhoto());
                lgr.Children.Add(GetBackPhotoButton());
                lgr.Children.Add(GetNextPhotoButton());
                lgr.Children.Add(GetSelectPhotoIndicator());
                return lgr;

                Button GetBackPhotoButton()
                {
                    Button but = new Button();
                    but.Background = backBrush;
                    but.Margin = new Thickness(10, 230, 230, 10);
                    but.BorderThickness = new Thickness(0, 0, 0, 0);
                    but.FontSize = 30;
                    but.Template = (ControlTemplate)lastPhotoButTem;
                    but.Click += But_Last_Click;
                    return but;


                    void But_Last_Click(object sender, RoutedEventArgs e)
                    {
                        if (nowCarPhotoID > 0)
                        {
                            nowCarPhotoID -= 1;
                            GetMainCarPhoto();
                            UpSelectPhotoIndicator();
                        }
                    }
                }
                Button GetNextPhotoButton()
                {
                    Button but = new Button();
                    but.Margin = new Thickness(230, 230, 10, 10);
                    but.Background = backBrush;
                    but.BorderThickness = new Thickness(0, 0, 0, 0);
                    but.FontSize = 30;
                    but.Template = (ControlTemplate)nextPhotoButTem;
                    but.Click += But_Next_Click;
                    return but;

                    void But_Next_Click(object sender, RoutedEventArgs e)
                    {
                        if (nowCarPhotoID < myCarPhotos.Length - 1)
                        {
                            nowCarPhotoID += 1;
                            GetMainCarPhoto();
                            UpSelectPhotoIndicator();
                        }
                    }
                }
                TextBlock GetSelectPhotoIndicator()
                {
                    selPhotoIndicator.HorizontalAlignment = HorizontalAlignment.Center;
                    selPhotoIndicator.VerticalAlignment = VerticalAlignment.Bottom;
                    selPhotoIndicator.Margin = new Thickness(25, 25, 25, 25);
                    selPhotoIndicator.FontSize = 30;
                    UpSelectPhotoIndicator();
                    return selPhotoIndicator;
                }
                void UpSelectPhotoIndicator()
                {
                    if (myCarPhotos != null)
                        selPhotoIndicator.Text = (nowCarPhotoID + 1).ToString() + "/" + myCarPhotos.Length.ToString();
                    else
                        selPhotoIndicator.Text = "0|0";
                }
            }
            Grid RightMainGridColumn()
            {
                Grid rGr = new Grid();
                Grid.SetColumn(rGr, 1);
                rGr.Margin = new Thickness(10, 0, 0, 10);

                ColumnDefinition colDef1 = new ColumnDefinition();
                ColumnDefinition colDef2 = new ColumnDefinition();
                colDef1.Width = new GridLength(150, GridUnitType.Pixel);
                rGr.ColumnDefinitions.Add(colDef1);
                rGr.ColumnDefinitions.Add(colDef2);

                RowDefinition rowDef1 = new RowDefinition();
                RowDefinition rowDef2 = new RowDefinition();
                RowDefinition rowDef3 = new RowDefinition();
                RowDefinition rowDef4 = new RowDefinition();
                RowDefinition rowDef5 = new RowDefinition();
                RowDefinition rowDef6 = new RowDefinition();
                rowDef6.Height = new GridLength(60, GridUnitType.Pixel);
                rGr.RowDefinitions.Add(rowDef1);
                rGr.RowDefinitions.Add(rowDef2);
                rGr.RowDefinitions.Add(rowDef3);
                rGr.RowDefinitions.Add(rowDef4);
                rGr.RowDefinitions.Add(rowDef5);
                rGr.RowDefinitions.Add(rowDef6);

                rGr.Children.Add(GetHeader());
                rGr.Children.Add(GetLeftLabel("Год", 1));
                rGr.Children.Add(GetRightLabel(myCar.year.ToString(), 1));
                rGr.Children.Add(GetLeftLabel("Пробег", 2));
                rGr.Children.Add(GetRightLabel(GetMilleageToString(myCar.mileage.ToString()), 2));
                rGr.Children.Add(GetLeftLabel("Дата начала", 3));
                rGr.Children.Add(GetRightLabel(auction.dt_start, 3));
                rGr.Children.Add(GetLeftLabel("Стартовая цена", 4));
                rGr.Children.Add(GetRightLabel(ConvPrice(auction.start_price), 4));
                rGr.Children.Add(GetFutter());

                string ConvPrice(int price)
                {
                    int len = price.ToString().Length;
                    if (len <= 3)
                        return price.ToString() + "$";
                    else if (len <= 6)
                        return price.ToString().Insert(len - 3, " ") + "$";
                    else if (len <= 9)
                        return price.ToString().Insert(len - 6, " ").Insert(len - 2, " ") + "$";
                    return price.ToString() + "$";
                }

                return rGr;

                string GetMilleageToString(string milleage)
                {
                    int len = milleage.ToString().Length;
                    if (len <= 3)
                        return milleage.ToString() + " км";
                    else if (len <= 6)
                        return milleage.ToString().Insert(len - 3, " ") + " км";
                    else if (len <= 9)
                        return milleage.ToString().Insert(len - 6, " ").Insert(len - 2, " ") + " км";
                    else
                        return milleage.ToString() + " км";
                }

                TextBlock GetHeader()
                {
                    TextBlock tb = new TextBlock();
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.Text = myCar.brend + " " + myCar.model;
                    tb.FontSize = 30;
                    Grid.SetColumnSpan(tb, 2);
                    return tb;
                }
                TextBlock GetLeftLabel(string str, int row)
                {
                    TextBlock tb = new TextBlock();
                    tb.HorizontalAlignment = HorizontalAlignment.Right;
                    tb.Text = str + ":";
                    tb.FontSize = 20;
                    Grid.SetColumn(tb, 0);
                    Grid.SetRow(tb, row);
                    return tb;
                }
                TextBlock GetRightLabel(string str, int row)
                {
                    TextBlock tb = new TextBlock();
                    tb.HorizontalAlignment = HorizontalAlignment.Center;
                    tb.Text = str;
                    tb.FontSize = 20;
                    Grid.SetColumn(tb, 1);
                    Grid.SetRow(tb, row);
                    return tb;
                }
                Grid GetFutter()
                {
                    Grid gr = new Grid();
                    Grid.SetColumnSpan(gr, 2);
                    Grid.SetRow(gr, 5);

                    //gr.Background = Brushes.Orange;
                    gr.Margin = new Thickness(0, 0, 10, 0);

                    ColumnDefinition col1 = new ColumnDefinition();
                    ColumnDefinition col2 = new ColumnDefinition();

                    col1.Width = new GridLength(1, GridUnitType.Star);
                    col2.Width = new GridLength(90, GridUnitType.Pixel);

                    gr.ColumnDefinitions.Add(col1);
                    gr.ColumnDefinitions.Add(col2);

                    TextBlock tb = new TextBlock();
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.HorizontalAlignment = HorizontalAlignment.Left;
                    tb.Text = "Описание: " + (myCar.context.Replace(Environment.NewLine, " "));
                    tb.FontSize = 20;
                    Grid.SetColumn(tb, 0);

                    TextBlock tb_but = new TextBlock();
                    tb_but.HorizontalAlignment = HorizontalAlignment.Stretch;
                    tb_but.VerticalAlignment = VerticalAlignment.Bottom;
                    tb_but.Text = "Подробнее...";
                    tb_but.Foreground = Brushes.Blue;
                    tb_but.Margin = new Thickness(0, 0, 0, 9);
                    tb_but.FontSize = 15;
                    tb_but.MouseDown += Tb_but_MouseDown;
                    tb_but.MouseEnter += Tb_but_MouseEnter;
                    tb_but.MouseLeave += Tb_but_MouseLeave; ;
                    Grid.SetColumn(tb_but, 1);


                    gr.Children.Add(tb_but);
                    gr.Children.Add(tb);

                    return gr;

                    void Tb_but_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
                    {
                        MessageBox.Show(myCar.context);
                    }
                    void Tb_but_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
                    {
                        ((TextBlock)sender).Foreground = Brushes.Green;
                    }
                    void Tb_but_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
                    {
                        ((TextBlock)sender).Foreground = Brushes.Blue;
                    }
                }

            }

        }
    }
}
