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

namespace oneline
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public TestOneline t;
        public Oneline line;

        private  TextBlock [,] grid;
        private uint w;
        private uint h;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void GenerateGrid(object sender, RoutedEventArgs e)
        {
            w = Convert.ToUInt16(Width.Text);
            h = Convert.ToUInt16(Height.Text);
            grid = new TextBlock[w,h];
            line=new Oneline(w,h);

            ///
            ///
           // SolidColorBrush brush = new SolidColorBrush(new Color { A = 255, R = Convert.ToByte(R.Value), G = Convert.ToByte(G.Value), B = Convert.ToByte(B.Value) });
           // color.Background = brush;
            int linew = 50 * (int)w;
            int lineh = 50 * (int) h;

            Grid MainG= new Grid();
            MainG.HorizontalAlignment = HorizontalAlignment.Center;
            MainG.Width = linew+w;
            MainG.Height = lineh+h;

            BorderG.Width = linew+w;
            BorderG.Height = lineh+h;

            canvas.Children.Clear();
           
            sphere.Width = linew + w;
            sphere.Height = lineh + h;

            col.Children.Clear();
            row.Children.Clear();

            all.Children.Clear();
           

            for (int i = 0; i < h; i++)
            {
                MainG.RowDefinitions.Add(new RowDefinition());
                if (i == h - 1)
                    col.Children.Add(new Border() { BorderThickness = new Thickness(1, 0, 1, 0), BorderBrush = new SolidColorBrush(new Color { A = 255, R = 0, G = 0, B = 0 }), Height = lineh+h, Width = 51 });
                else
                    col.Children.Add(new Border() { BorderThickness = new Thickness(1, 0, 0, 0), BorderBrush = new SolidColorBrush(new Color { A = 255, R = 0, G = 0, B = 0 }), Height = lineh+h, Width = 51 });
            }
           // col.Children.Add(new Border() { BorderThickness = new Thickness(1, 0, 1, 0), BorderBrush = new SolidColorBrush(new Color { A = 255, R = 0, G = 0, B = 0 }), Height = linew, Width = 50 });

            for (int i = 0; i < w; i++)
            {
                MainG.ColumnDefinitions.Add(new ColumnDefinition());
                if(i==w-1)
                    row.Children.Add(new Border() { BorderThickness = new Thickness(0, 1, 0, 1), BorderBrush = new SolidColorBrush(new Color { A = 255, R = 0, G = 0, B = 0 }), Height = 51, Width = linew+w });
                else
                    row.Children.Add(new Border() { BorderThickness = new Thickness(0, 1, 0, 0), BorderBrush = new SolidColorBrush(new Color { A = 255, R = 0, G = 0, B = 0 }), Height = 51, Width = linew+w });
            }

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    grid[i, j] = new TextBlock { Text = "", Width = 50, Height = 50 };
                    grid[i, j].Background = new SolidColorBrush(Colors.DarkGray);
                    grid[i, j].Name = "T" + (i + j * w).ToString();
                    grid[i, j].MouseDown += blockDown;
                    grid[i, j].Margin = new Thickness(1, 1, 1, 1);
                    grid[i, j].Opacity = 0.5;
                    Grid.SetRow(grid[i, j], j);
                    Grid.SetColumn(grid[i, j], i);
                    MainG.Children.Add(grid[i, j]);
                }
            }

            Grid.SetColumn(MainG, 0);
            all.Children.Add(MainG);
            //row.Children.Add(new Border() { BorderThickness = new Thickness(0, 1, 0, 0), BorderBrush = new SolidColorBrush(new Color { A = 255, R = 0, G = 0, B = 0 }), Height = 50, Width = lineh });

        }
            ///
            
        

        private void blockDown(object sender, MouseButtonEventArgs e)
        {
            var current = sender as TextBlock;
            uint index = Convert.ToUInt16(current.Name.Substring(1));
            uint y = index / line.width;
            uint x = index - y * line.width;

            State s = line.States[x, y];
            if (s == State.Avaliable)
            {
                line.States[x, y] = State.None;
                grid[x, y].Background = new SolidColorBrush(Colors.DarkGray);

            }
            else
            {
                line.States[x, y] = State.Avaliable;
                grid[x, y].Background = new SolidColorBrush(Colors.Aqua);
            }
        }

        private void Run(object sender, RoutedEventArgs e)
        {

            uint x = Convert.ToUInt16(start_x.Text);
            uint y = Convert.ToUInt16(start_y.Text);

            if (x > w || y > h || x < 0 || y < 0)
            {
                MessageBox.Show("起点不合法");
                return;
            }
            line.SetStart(x,y);
            line.InitCaculate();
            line.Caculate();

            if (line.Road.Count == 0)
            {
                MessageBox.Show("oops");
                return;
            }
            for (int i = 0; i < line.Road.Count-1; i++)
            {
                Line l = new Line();
               // l.Width = 10;
                l.Stroke = new SolidColorBrush(Colors.Red);
                l.StrokeThickness = 10;
                l.X1 = line.Road[i].X * 51 + 26;
                l.Y1 = line.Road[i].Y * 51 + 26;

                l.X2 = line.Road[i + 1].X * 51 + 26;
                l.Y2 = line.Road[i + 1].Y * 51 + 26;
               

                canvas.Children.Add(l);
            }

        }

        private void Inv(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    uint index = Convert.ToUInt16(grid[i,j].Name.Substring(1));
                    uint y = index / line.width;
                    uint x = index - y * line.width;

                    State s = line.States[x, y];
                    if (s == State.Avaliable)
                    {
                        line.States[x, y] = State.None;
                        grid[x, y].Background = new SolidColorBrush(Colors.DarkGray);

                    }
                    else
                    {
                        line.States[x, y] = State.Avaliable;
                        grid[x, y].Background = new SolidColorBrush(Colors.Aqua);
                    }
                }
            }

            
           
        }
    }
}
