using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Shapes;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace TD.PathAlgorithmAnalyser.ViewEntities
{
    public abstract class ViewShape : INotifyPropertyChanged
    {
        public Location.Shapes.Shape DataShape { get; private set; }
        public virtual Shape Shape { get; protected set; }
        
        private double _width;
        public double Width
        {
            get
            {
                return _width;
            }
            protected set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }

        private double _height;
        public double Height
        {
            get
            {
                return _height;
            }
            protected set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private Thickness _margin;
        public Thickness Margin
        {
            get
            {
                return _margin;
            }
            protected set
            {
                _margin = value;
                OnPropertyChanged(nameof(Margin));
            }
        }
        
        private Transform _renderTransform;
        public Transform RenderTransform
        {
            get
            {
                return _renderTransform;
            }
            protected set
            {
                _renderTransform = value;
                OnPropertyChanged(nameof(RenderTransform));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        
        public ViewShape(Location.Shapes.Shape shape)
        {
            DataShape = shape;
        }

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler(this, new PropertyChangedEventArgs(info));
        }

        protected void DefaultProperites()
        {
            Shape.HorizontalAlignment = HorizontalAlignment.Left;
            Shape.VerticalAlignment = VerticalAlignment.Top;
            Shape.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        protected void InitializeBinders()
        {
            Binding binding = new Binding(nameof(Width));
            binding.Source = this;
            Shape.SetBinding(FrameworkElement.WidthProperty, binding);

            binding = new Binding(nameof(Height));
            binding.Source = this;
            Shape.SetBinding(FrameworkElement.HeightProperty, binding);

            binding = new Binding(nameof(Margin));
            binding.Source = this;
            Shape.SetBinding(FrameworkElement.MarginProperty, binding);

            binding = new Binding(nameof(RenderTransform));
            binding.Source = this;
            Shape.SetBinding(UIElement.RenderTransformProperty, binding);
        }
    }
}
