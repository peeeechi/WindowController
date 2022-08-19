using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Threading.Thread;


namespace WindowController
{
    public class WindowRectRate: IEquatable<WindowRectRate>
    {
        public double LeftRate { get; set; }
        public double TopRate { get; set; }
        public double RightRate { get; set; }
        public double BottomRate { get; set; }

        public static WindowRectRate FromRect(RECT parent, RECT child)
        {
            double parentHeight = Math.Abs(parent.top - parent.bottom);
            double parentWidth = Math.Abs(parent.left - parent.right);

            double childLeft = child.left - parent.left;
            double childRight = child.right - parent.left;
            double childtop = child.top - parent.top;
            double childbottom = child.bottom - parent.top;

            return new WindowRectRate {
                LeftRate = childLeft / parentWidth,
                TopRate = childtop / parentHeight,
                RightRate = childRight / parentWidth,
                BottomRate = childbottom / parentHeight,
            };
        }

        public static bool operator == (WindowRectRate left, WindowRectRate right)
        {
            return (
                    left.LeftRate == right.LeftRate
                &&  left.TopRate == right.TopRate
                &&  left.RightRate == right.RightRate
                &&  left.BottomRate == right.BottomRate
            );
        }

        public static bool operator != (WindowRectRate left, WindowRectRate right)
        {
            return !(left == right);
        }

        public override bool Equals(object other)
        {
            return typeof(WindowRectRate) == other.GetType()? this.Equals((WindowRectRate)other): false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(WindowRectRate other)
        {
            return (this == other);
        }
    }
}
