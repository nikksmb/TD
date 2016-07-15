using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using static TD.PathAlgorithmAnalyser.ViewEntities.TransformManager;
using System.Windows.Media;
using System.Threading;

namespace TD.PathAlgorithmAnalyser.ViewEntities
{
    public class ViewRectangle : ViewShape
    {
        private Rectangle viewRectangle;
        public override Shape Shape
        {
            get
            {
                return viewRectangle;
            }

            protected set
            {
                viewRectangle = (Rectangle)value;
            }
        }

#if (DEBUG)
        private Ellipse A;
        private Ellipse B;
        private Ellipse C;
        private Ellipse D;
#endif

        public ViewRectangle(Location.Shapes.Rectangle rectangle):base(rectangle)
        {
            Shape = new Rectangle();

            Rectangle_Changed(null, null);

            DefaultProperites();
            InitializeBinders();

#if (DEBUG)

                A = new Ellipse();
                A.Width = 10;
                A.Height = 10;

                B = new Ellipse();
                B.Width = 10;
                B.Height = 10;

                C = new Ellipse();
                C.Width = 10;
                C.Height = 10;

                D = new Ellipse();
                D.Width = 10;
                D.Height = 10;

                A.HorizontalAlignment = HorizontalAlignment.Left;
                A.VerticalAlignment = VerticalAlignment.Top;
                A.RenderTransformOrigin = new Point(0.5, 0.5);

                B.HorizontalAlignment = HorizontalAlignment.Left;
                B.VerticalAlignment = VerticalAlignment.Top;
                B.RenderTransformOrigin = new Point(0.5, 0.5);

                C.HorizontalAlignment = HorizontalAlignment.Left;
                C.VerticalAlignment = VerticalAlignment.Top;
                C.RenderTransformOrigin = new Point(0.5, 0.5);

                D.HorizontalAlignment = HorizontalAlignment.Left;
                D.VerticalAlignment = VerticalAlignment.Top;
                D.RenderTransformOrigin = new Point(0.5, 0.5);

                A.Margin = new Thickness(
                        TransformWidth(rectangle.A.X - 100),
                        TransformHeight(rectangle.A.Y - 100),
                        0,
                        0);

                B.Margin = new Thickness(
                    TransformWidth(rectangle.B.X - 100),
                    TransformHeight(rectangle.B.Y - 100),
                    0,
                    0);

                C.Margin = new Thickness(
                    TransformWidth(rectangle.C.X - 100),
                    TransformHeight(rectangle.C.Y - 100),
                    0,
                    0);

                D.Margin = new Thickness(
                    TransformWidth(rectangle.D.X - 100),
                    TransformHeight(rectangle.D.Y - 100),
                    0,
                    0);

                WindowContainer.MainWindow.fieldGrid.Children.Add(A);
                WindowContainer.MainWindow.fieldGrid.Children.Add(B);
                WindowContainer.MainWindow.fieldGrid.Children.Add(C);
                WindowContainer.MainWindow.fieldGrid.Children.Add(D);
#endif
            rectangle.Changed += Rectangle_Changed;
        }

        private void Rectangle_Changed(object sender, EventArgs e)
        {
            TaskScheduler scheduler = WindowContainer.MainWindow.GetScheduler();
            Task.Factory.StartNew(() =>
            {
                Location.Shapes.Rectangle rectangle = (Location.Shapes.Rectangle)DataShape;

                Width = TransformWidth(rectangle.Width);
                Height = TransformHeight(rectangle.Height);

                double angle = rectangle.Angle * 180 / Math.PI;
                RenderTransform = new RotateTransform(angle);

#if (DEBUG)

                if (!WindowContainer.MainWindow.fieldGrid.Children.Contains(A))
                    WindowContainer.MainWindow.fieldGrid.Children.Add(A);
                if (!WindowContainer.MainWindow.fieldGrid.Children.Contains(B))
                    WindowContainer.MainWindow.fieldGrid.Children.Add(B);
                if (!WindowContainer.MainWindow.fieldGrid.Children.Contains(C))
                    WindowContainer.MainWindow.fieldGrid.Children.Add(C);
                if (!WindowContainer.MainWindow.fieldGrid.Children.Contains(D))
                    WindowContainer.MainWindow.fieldGrid.Children.Add(D);

                A.Margin = new Thickness(
                    TransformWidth(rectangle.A.X - 100),
                    TransformHeight(rectangle.A.Y - 100),
                    0,
                    0);

                B.Margin = new Thickness(
                    TransformWidth(rectangle.B.X - 100),
                    TransformHeight(rectangle.B.Y - 100),
                    0,
                    0);

                C.Margin = new Thickness(
                    TransformWidth(rectangle.C.X - 100),
                    TransformHeight(rectangle.C.Y - 100),
                    0,
                    0);

                D.Margin = new Thickness(
                    TransformWidth(rectangle.D.X - 100),
                    TransformHeight(rectangle.D.Y - 100),
                    0,
                    0);

                A.Stroke = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));
                B.Stroke = new SolidColorBrush(Color.FromArgb(200, 0, 255, 0));
                C.Stroke = new SolidColorBrush(Color.FromArgb(200, 0, 0, 255));
                D.Stroke = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
                A.Fill = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));
                B.Fill = new SolidColorBrush(Color.FromArgb(200, 0, 255, 0));
                C.Fill = new SolidColorBrush(Color.FromArgb(200, 0, 0, 255));
                D.Fill = new SolidColorBrush(Color.FromArgb(200, 0, 0, 0));
#endif
                Margin = new Thickness(
                    TransformWidth(rectangle.Center.X - rectangle.Width / 2),
                    TransformHeight(rectangle.Center.Y - rectangle.Height / 2),
                    0,
                    0);
            },
            CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);
        }
    }
}
