using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using static TD.PathAlgorithmAnalyser.ViewEntities.TransformManager;
using System.Windows.Data;
using System.Threading;

namespace TD.PathAlgorithmAnalyser.ViewEntities
{
    public class ViewCircle : ViewShape
    {
        private Ellipse viewCircle;
        public override Shape Shape
        {
            get
            {
                return viewCircle;
            }

            protected set
            {
                viewCircle = (Ellipse)value;
            }
        }

        public ViewCircle(Location.Shapes.Circle circle):base(circle)
        {
            viewCircle = new Ellipse();
            Circle_Changed(null, null);
            DefaultProperites();
            InitializeBinders();

            circle.Changed += Circle_Changed;
        }

        private void Circle_Changed(object sender, EventArgs e)
        {
            TaskScheduler scheduler = WindowContainer.MainWindow.GetScheduler();
            Task.Factory.StartNew(() =>
            {
                Location.Shapes.Circle circle = (Location.Shapes.Circle)DataShape;
                Width = TransformWidth(circle.Radius * 2);
                Height = TransformHeight(circle.Radius * 2);
                Margin = new Thickness(
                    TransformWidth(circle.Center.X - circle.Radius),
                    TransformHeight(circle.Center.Y - circle.Radius),
                    0,
                    0);
            },
            CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);
        }
    }
}
