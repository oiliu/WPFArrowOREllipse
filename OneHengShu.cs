using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ArrowsLinesDll
{
    public class OneHengShu : Shape
    {
        #region 控件属性：开始坐标,长度，宽度，是横或竖线
        public static readonly DependencyProperty StartPointPointProperty = DependencyProperty.Register(
            "StartPoint",
            typeof(Point),
            typeof(OneEllipseGeometry),
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
            typeof(OneEllipseGeometry),
            new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public double Length
        {
            get { return (double)this.GetValue(LengthProperty); }
            set { this.SetValue(LengthProperty, value); }
        }
        //是否显示横行
        public static readonly DependencyProperty ShowHengProperty = DependencyProperty.Register(
            "ShowHeng",
            typeof(bool),
            typeof(OneEllipseGeometry),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public bool ShowHeng
        {
            get { return (bool)this.GetValue(ShowHengProperty); }
            set { this.SetValue(ShowHengProperty, value); }
        }

        #endregion
        protected override Geometry DefiningGeometry
        {
            get
            {

                if (ShowHeng)
                {
                    return new LineGeometry(new Point(
                this.StartPoint.X - Length / 2, this.StartPoint.Y)
                , new Point(this.StartPoint.X + Length / 2, this.StartPoint.Y));
                }
                else
                {
                    return new LineGeometry(
                        new Point(this.StartPoint.X, this.StartPoint.Y - Length / 2)
                        , new Point(this.StartPoint.X, this.StartPoint.Y + Length / 2));
                }
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);   //}
        }
    }
}
