using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArrowsLinesDll
{
    public class OneEllipseGeometry : Shape
    {
        #region 控件属性：圆点坐标，横向半径，纵向半径，

        public static readonly DependencyProperty CenterPointProperty = DependencyProperty.Register(
            "CenterPoint",
            typeof(Point),
            typeof(OneEllipseGeometry),
            new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));
        public Point CenterPoint
        {
            get { return (Point)this.GetValue(CenterPointProperty); }
            set { this.SetValue(CenterPointProperty, value); }
        }
        //横向半径
        public static readonly DependencyProperty RadiusXProperty = DependencyProperty.Register(
            "RadiusX",
            typeof(double),
            typeof(OneEllipseGeometry),
            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public double RadiusX
        {
            get { return (double)this.GetValue(RadiusXProperty); }
            set { this.SetValue(RadiusXProperty, value); }
        }
        //纵向半径
        public static readonly DependencyProperty RadiusYProperty = DependencyProperty.Register(
            "RadiusY",
            typeof(double),
            typeof(OneEllipseGeometry),
            new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public double RadiusY
        {
            get { return (double)this.GetValue(RadiusYProperty); }
            set { this.SetValue(RadiusYProperty, value); }
        }

        #endregion

        protected override Geometry DefiningGeometry
        {
            get
            {
                //定义形状
                //throw new NotFiniteNumberException();
                return new EllipseGeometry(this.CenterPoint, RadiusX, RadiusY);
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            Geometry ellipse = new EllipseGeometry(this.CenterPoint, RadiusX, RadiusY);
            GeometryDrawing drawing = new GeometryDrawing(Brushes.Transparent, new Pen(this.Stroke, this.StrokeThickness), ellipse);
            dc.DrawDrawing(drawing);
        }
    }
}

