using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TD.Location;
using TD.PathAlgorithmAnalyser.Mappers;

namespace TD.PathAlgorithmAnalyser.ViewEntities
{
    public class ViewRoad
    {
        public Road Road { get; }

        private List<ViewShape> _roadShapes;
        public ViewShape[] RoadShapes
        {
            get
            {
                return _roadShapes.ToArray();
            }
        }

        public ViewRoad(Road road)
        {
            _roadShapes = road.GetFigures().Select(s => s.ToViewShape()).ToList();
            InitializeRoadShapes();
        }

        private void InitializeRoadShapes()
        {
            _roadShapes.ForEach((s) => 
            {
                s.Shape.Stroke = new SolidColorBrush(Constants.Colors.RoadColor);
                s.Shape.Fill = new SolidColorBrush(Constants.Colors.RoadColor);
            });
        }
    }
}
