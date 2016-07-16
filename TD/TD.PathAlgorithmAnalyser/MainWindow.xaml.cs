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
using TD.Location;
using TD.PathAlgorithmAnalyser.ViewEntities;
using TD.PathAlgorithmAnalyser.Mappers;
using System.Threading;

namespace TD.PathAlgorithmAnalyser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowContainer.MainWindow = this;
            InitializeComponent();

            _uiTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            fieldGrid.Width = Constants.FieldWidth;
            fieldGrid.Height = Constants.FieldHeight;
            pointArr = new Location.Point[points.Length/2];
            for (int i = pointArr.Length - 1; i>=0; i--)
            {
                pointArr[pointArr.Length - i - 1] = new Location.Point() {X = points[2 * i], Y = points[2 * i + 1] };
            }
        }

        private TaskScheduler _uiTaskScheduler;
        private Location.Point[] pointArr;

        private int[] points = new int[] {
            1000, 1000,
            1000, 8000,
            4000, 8000,
            4000, 1000,
            6000, 1000,
            6500, 8000,
            9000, 1000
            
            /*1000,1000,
            8000,1000,
            1500,2000,
            2000,3000,
            9000,5000,
            9000,6000,
            6500,6000,
            8000,4000,
            1000,6500,
            1000,1000,
            3000,9000*/
            
        };

        public TaskScheduler GetScheduler()
        {
            return _uiTaskScheduler;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Map map = new Map();
            map.Road = new Road(pointArr);
            viewMap = new ViewMap(map, fieldGrid);

            rectangle = (Location.Shapes.Rectangle)map.Road.GetFigures()[1];
        }

        private Location.Shapes.Rectangle rectangle;
        private ViewMap viewMap;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            rectangle.Width = Convert.ToInt32(widthBox.Text);
        }

        private void heightBtn_Click(object sender, RoutedEventArgs e)
        {
            rectangle.Height = Convert.ToInt32(heightBox.Text);
        }

        private void angleBtn_Click(object sender, RoutedEventArgs e)
        {
            rectangle.Angle = Convert.ToInt32(angleBox.Text) * Math.PI / 180;
        }

        private void centerBtn_Click(object sender, RoutedEventArgs e)
        {
            rectangle.Center = new Location.Point()
            {
                X = Convert.ToInt32(centerXBox.Text),
                Y = Convert.ToInt32(centerYBox.Text)
            };
        }

        private Task ChangeRectangle()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    Thread.Sleep(1000);
                    rectangle.Height = i * 100;
                }
            });
        }

        private async void button1_Click_1(object sender, RoutedEventArgs e)
        {
            await ChangeRectangle();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            viewMap.Map.StartWave();
        }
    }
}
