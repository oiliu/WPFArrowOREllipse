using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrowsLinesDll
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// 两点之间带箭头的直线
    /// </summary>
    public class ArrowLine : ArrowBase
    {
        #region Fields

        /// <summary>
        /// 结束点
        /// </summary>
        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register(
            "EndPoint",
            typeof(Point),
            typeof(ArrowLine),
            new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 线段
        /// </summary>
        private readonly LineSegment lineSegment = new LineSegment();

        #endregion Fields

        #region Properties

        /// <summary>
        /// 结束点
        /// </summary>
        public Point EndPoint
        {
            get { return (Point)this.GetValue(EndPointProperty); }
            set { this.SetValue(EndPointProperty, value); }
        }

        #endregion Properties

        #region Protected Methods

        /// <summary>
        /// 填充Figure
        /// </summary>
        /// <returns>PathSegment集合</returns>
        protected override PathSegmentCollection FillFigure()
        {
            this.lineSegment.Point = this.EndPoint;
            return new PathSegmentCollection
            {
                this.lineSegment
            };
        }

        /// <summary>
        /// 获取开始箭头处的结束点
        /// </summary>
        /// <returns>开始箭头处的结束点</returns>
        protected override Point GetStartArrowEndPoint()
        {
            return this.EndPoint;
        }

        /// <summary>
        /// 获取结束箭头处的开始点
        /// </summary>
        /// <returns>结束箭头处的开始点</returns>
        protected override Point GetEndArrowStartPoint()
        {
            return this.StartPoint;
        }

        /// <summary>
        /// 获取结束箭头处的结束点
        /// </summary>
        /// <returns>结束箭头处的结束点</returns>
        protected override Point GetEndArrowEndPoint()
        {
            return this.EndPoint;
        }
        #region 是否显示起点圆圈
        public bool ShowStartCircle
        {
            get { return (bool)this.GetValue(ShowStartCircleProperty); }
            set { this.SetValue(ShowStartCircleProperty, value); }
        }
        public static readonly DependencyProperty ShowStartCircleProperty = DependencyProperty.Register(
            "ShowStartCircle",
            typeof(bool),
            typeof(AdjustableArrowBezierCurve),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #region 是否显示末点双箭头
        public bool ShowEndArrow
        {
            get { return (bool)this.GetValue(ShowEndArrowProperty); }
            set { this.SetValue(ShowEndArrowProperty, value); }
        }
        public static readonly DependencyProperty ShowEndArrowProperty = DependencyProperty.Register(
            "ShowEndArrow",
            typeof(bool),
            typeof(AdjustableArrowBezierCurve),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion
        #region 是否显示末点双箭头
        public int EndRadius
        {
            get { return (int)this.GetValue(EndRadiusProperty); }
            set { this.SetValue(EndRadiusProperty, value); }
        }
        public static readonly DependencyProperty EndRadiusProperty = DependencyProperty.Register(
            "EndRadius",
            typeof(int),
            typeof(AdjustableArrowBezierCurve),
            new FrameworkPropertyMetadata(8, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var radius= this.EndRadius;
            if (this.ShowStartCircle)
            {
                var matx = new Matrix();
                Vector vect = this.StartPoint - this.EndPoint;
                //获取单位向量
                vect.Normalize();
                vect *= this.ArrowLength;
                matx.Rotate(this.ArrowAngle);
                var ellipseCenterPoint = this.StartPoint + (vect * matx);
                dc.DrawEllipse(this.Stroke
                    , new Pen(this.Stroke, this.StrokeThickness)
                    , this.StartPoint
                    //new Point(ellipseCenterPoint.X + this.StrokeThickness / 2, ellipseCenterPoint.Y + (raduis / 2) + this.StrokeThickness)
                    , radius
                    , radius);
            }
            //是否显示叠加箭头
            if (this.ShowEndArrow)
            {
                var matx = new Matrix();
                Vector vect = this.StartPoint - this.EndPoint;
                //获取单位向量
                vect.Normalize();
                vect *= this.ArrowLength;
                matx.Rotate(-this.ArrowAngle);
                // 计算上半段箭头的点
                var startPoint = this.EndPoint + (vect * matx);
                dc.DrawLine(new Pen(this.Stroke, this.StrokeThickness), startPoint, this.EndPoint);
                var matx2 = new Matrix();
                matx2.Rotate(this.ArrowAngle);
                // 计算下半段箭头的点
                var endPoint = this.EndPoint + (vect * matx2);
                dc.DrawLine(new Pen(this.Stroke, this.StrokeThickness), endPoint, this.EndPoint);
            }
        }

        #endregion  Protected Methods
    }
}
