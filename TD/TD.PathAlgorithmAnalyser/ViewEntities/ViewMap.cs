using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using TD.Location;

namespace TD.PathAlgorithmAnalyser.ViewEntities
{
    public class ViewMap
    {
        public ViewRoad Road { get; }
        public List<ViewUnit> Units { get; }
        
        public Map Map { get; }
        public Grid Grid { get; }
        
        public ViewMap(Map map, Grid grid)
        {
            Map = map;
            Map.OnObjectAdded += Map_OnObjectAdded;

            Road = new ViewRoad(map.Road);

            Units = new List<ViewUnit>();

            Grid = grid;
            Grid.Children.Clear();
            foreach(ViewShape s in Road.RoadShapes)
            {
                Grid.Children.Add(s.Shape);
            }

        }

        private void Map_OnObjectAdded(object sender, EventArgs e)
        {
            if (sender is ILocatable)
            {
                if (sender is Units.Unit)
                {
                    ViewUnit viewUnit = new ViewUnit((Units.Unit)sender);
                    Grid.Children.Add(viewUnit.Shape.Shape);
                    Units.Add(viewUnit);
                }
                if (sender is Towers.Tower)
                {

                }
                return;
            }
            throw new InvalidOperationException();
        }
    }
}
