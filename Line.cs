using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Generalizer
{
    class Line
    {
        private double a, b, c;

        public Line(Coordinate coordinate)
        {
            this.a = 0;
            this.b = 0;
        }

        public double A
        {
            get { return a; }
            set { a = value; }
        }

        public double B
        {
            get { return b; }
            set { b = value; }
        }

        public double C
        {
            get { return c; }
            set { c = value; }
        }

        public Line(Coordinate pointA, Coordinate pointB)
        {
            fromPoints(pointA, pointB);
        }

        public double Y(double x)
        {
            return (a * x + c) / b;
        }

        public double X(double y)
        {
            return (y - b) / a;
        }

        public void fromPoints(Coordinate pointA, Coordinate pointB)
        {
            if (pointA.X - pointB.X != 0)
            {
                if (pointA.Y - pointB.Y != 0) //obliczenie wspolczynnikow
                {
                    this.a = (pointA.Y - pointB.Y) / (pointA.X - pointB.X);
                    this.b = -1;
                    this.c = (pointA.X * pointB.Y - pointA.Y * pointB.X) / (pointA.X - pointB.X);
                }
                else //pozioma linia
                {
                    this.a = 0;
                    this.b = 1;
                    this.c = -pointA.Y;
                }
            }
            else
            {
                if (pointA.Y - pointB.Y != 0) //pionowa linia
                {
                    this.a = 1;
                    this.b = 0;
                    this.c = -pointA.X;
                }
                else //punkty są rowne
                {
                    this.a = 0;
                    this.b = 0;
                    this.c = 0;
                    //invalid line
                }
            }
        }

        public double DistanceFromLine(Coordinate point)
        {
            return Math.Abs(a * point.X + b * point.Y + c) / Math.Sqrt(a * a + b * b);
        }
    }
}
