using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArrowsLinesDll
{
    public class OneOnlyArrow : Shape
    {
        #region 控件属性：开始坐标,长度，宽度，旋转的依赖属性，是否旋转
        public static readonly DependencyProperty StartPointPointProperty = DependencyProperty.Register(
            "StartPoint",
            typeof(Point),
            typeof(OneOnlyArrow),
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
            typeof(OneOnlyArrow),
            new FrameworkPropertyMetadata(22.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public double Length
        {
            get { return (double)this.GetValue(LengthProperty); }
            set { this.SetValue(LengthProperty, value); }
        }
        #endregion
        protected override Geometry DefiningGeometry
        {
            get
            {
                return new LineGeometry(new Point(0, 0), new Point(0, 0));
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            var endPoint = new Point(this.StartPoint.X, this.StartPoint.Y - 20);
            //画横线
            base.OnRender(dc);
            var matx = new Matrix();
            Vector vect = this.StartPoint - endPoint;
            //获取单位向量
            vect.Normalize();
            vect *= this.Length;
            matx.Rotate(45.0 / 2);
            // 计算上半段箭头的点
            var s = endPoint + (vect * matx);
            dc.DrawLine(new Pen(this.Stroke == null ? Brushes.Green : this.Stroke, this.StrokeThickness)
                , s
                , new Point(endPoint.X, endPoint.Y - this.StrokeThickness / 4));
            var matx2 = new Matrix();
            matx2.Rotate(-45.0 / 2);
            // 计算下半段箭头的点
            var e = endPoint + (vect * matx2);
            dc.DrawLine(new Pen(this.Stroke == null ? Brushes.Green : this.Stroke, this.StrokeThickness)
                , e
                , new Point(endPoint.X, endPoint.Y + this.StrokeThickness / 4));
        }
    }
}