using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TD.PathAlgorithmAnalyser.Mappers;
using TD.Units;

namespace TD.PathAlgorithmAnalyser.ViewEntities
{
    public class ViewUnit
    {
        private Unit _unit;

        public ViewShape Shape { get; private set; }
        
        public ViewUnit(Unit unit)
        {
            _unit = unit;
            unit.OnValidStateChanged += Unit_OnValidStateChanged;
            Shape = unit.Shape.ToViewShape();
            InitializeUnitShape();
        }

        private void Unit_OnValidStateChanged(object sender, EventArgs e)
        {
            if (_unit.IsValid)
            {
                Shape.Shape.Fill = new SolidColorBrush(Constants.Colors.DefaultUnitColor);
                return;
            }
            Shape.Shape.Fill = new SolidColorBrush(Constants.Colors.InvalidTowerPosition);
        }

        private void InitializeUnitShape()
        {
            Shape.Shape.Stroke = new SolidColorBrush(Constants.Colors.DefaultUnitBorder);
            Shape.Shape.Fill = new SolidColorBrush(Constants.Colors.DefaultUnitColor);

            Shape.Shape.Focusable = true;
            Shape.Shape.KeyDown += Shape_KeyDown;
        }

        //! for  test
        private void Shape_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Shape.Shape.Focus();
            if (e.Key == System.Windows.Input.Key.Up
                || e.Key == System.Windows.Input.Key.W)
            {
                Shape.DataShape.Center = new Location.Point()
                {
                    X = Shape.DataShape.Center.X,
                    Y = Shape.DataShape.Center.Y - 25
                };
                return;
            }
            if (e.Key == System.Windows.Input.Key.Left
                || e.Key == System.Windows.Input.Key.A)
            {
                Shape.DataShape.Center = new Location.Point()
                {
                    X = Shape.DataShape.Center.X - 25,
                    Y = Shape.DataShape.Center.Y
                };
                return;
            }
            if (e.Key == System.Windows.Input.Key.Down
                || e.Key == System.Windows.Input.Key.S)
            {
                Shape.DataShape.Center = new Location.Point()
                {
                    X = Shape.DataShape.Center.X,
                    Y = Shape.DataShape.Center.Y + 25
                };
                return;
            }
            if (e.Key == System.Windows.Input.Key.Right
                || e.Key == System.Windows.Input.Key.D)
            {
                Shape.DataShape.Center = new Location.Point()
                {
                    X = Shape.DataShape.Center.X + 25,
                    Y = Shape.DataShape.Center.Y
                };
            }
        }
    }
}
