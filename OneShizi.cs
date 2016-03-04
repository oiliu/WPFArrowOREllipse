using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArrowsLinesDll
{
    public class OneShizi : Shape
    {
        #region 控件属性：开始坐标,长度，宽度，旋转的依赖属性，是否旋转
        public static readonly DependencyProperty StartPointPointProperty = DependencyProperty.Register(
            "StartPoint",
            typeof(Point),
            typeof(OneShizi),
            new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));
        public Point StartPoint
        {
            get { return (Point)this.GetValue(StartPointPointProperty); }
            set { this.SetValue(StartPointPointProperty, value); }
        }
        //长度
        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
            "Length",
            typeof(double),
            typeof(OneShizi),
            new FrameworkPropertyMetadata(22.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public double Length
        {
            get { return (double)this.GetValue(LengthProperty); }
            set { this.SetValue(LengthProperty, value); }
        }
        //是否旋转
        public static readonly DependencyProperty IsAngleProperty = DependencyProperty.Register(
            "IsAngle",
            typeof(bool),
            typeof(OneShizi),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public bool IsAngle
        {
            get { return (bool)this.GetValue(IsAngleProperty); }
            set { this.SetValue(IsAngleProperty, value); }
        }
        //旋转的依赖属性
        public static readonly DependencyProperty ArrowAngleProperty = DependencyProperty.Register(
            "ArrowAngle",
            typeof(double),
            typeof(OneShizi),
            new FrameworkPropertyMetadata(45.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public double ArrowAngle
        {
            get { return (double)this.GetValue(ArrowAngleProperty); }
            set { this.SetValue(ArrowAngleProperty, value); }
        }
        #endregion
        protected override Geometry DefiningGeometry
        {
            get
            {
                //画竖线
                Point startPoint = new Point(this.StartPoint.X, this.StartPoint.Y - Length / 2);
                Point endPoint = new Point(this.StartPoint.X, this.StartPoint.Y + Length / 2);
                if (IsAngle)
                {
                    var matx = new Matrix();
                    Vector vect = startPoint - endPoint;
                    //获取单位向量
                    vect.Normalize();
                    vect *= this.Length;
                    matx.Rotate(-this.ArrowAngle / 2);
                    var matx2 = new Matrix();
                    matx2.Rotate(this.ArrowAngle / 2);
                    var s = startPoint + (vect * matx);
                    var e = endPoint + (vect * matx2);
                    Geometry line = new LineGeometry(new Point(s.X, s.Y + Length), new Point(e.X, e.Y + Length - Length / 6));
                    return line;
                }
                else
                {
                    return new LineGeometry(startPoint, endPoint);
                }
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            //画横线
            base.OnRender(dc);
            Point startPoint = new Point(this.StartPoint.X - Length / 2, this.StartPoint.Y);
            Point endPoint = new Point(this.StartPoint.X + Length / 2, this.StartPoint.Y);
            if (IsAngle)
            {
                var matx = new Matrix();
                Vector vect = startPoint - endPoint;
                //获取单位向量
                vect.Normalize();
                vect *= this.Length;
                matx.Rotate(-this.ArrowAngle / 2);
                var matx2 = new Matrix();
                matx2.Rotate(this.ArrowAngle / 2);
                var s = startPoint + (vect * matx);
                var e = endPoint + (vect * matx2);

                Geometry line = new LineGeometry(new Point(s.X + Length, s.Y), new Point(e.X + Length - Length / 8, e.Y));
                GeometryDrawing drawing = new GeometryDrawing(Brushes.Transparent, new Pen(this.Stroke, this.StrokeThickness), line);
                dc.DrawDrawing(drawing);
            }
            else
            {
                Geometry line = new LineGeometry(startPoint, endPoint);
                GeometryDrawing drawing = new GeometryDrawing(Brushes.Transparent, new Pen(this.Stroke, this.StrokeThickness), line);
                dc.DrawDrawing(drawing);
            }
        }
    }
}