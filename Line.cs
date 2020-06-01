using DotSpatial.Topology;
using DotSpatial.Topology.Index.Strtree;
using System;
using System.Collections.Generic;
using System.Text;

namespace generalizator
{
    class Line
    {
        private double a, b, c;

        public Line()
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
            return a * x + b;            
        }

        public double X(double y)
        {
            return (y - b) / a;
        }

        public void fromPoints(Coordinate pointA, Coordinate pointB)
        {
            double A=0, B=0, C=0;
            if (pointA.X - pointB.X != 0)
            {
                if(pointA.Y - pointB.Y != 0) //obliczenie wspolczynnikow
                {
                    A = -(pointA.Y - pointB.Y) / (pointA.X - pointB.X);
                    B = 1;
                    C = -(pointA.X * pointB.Y - pointA.Y * pointB.X) / (pointA.X - pointB.X);
                }
                else //pozioma linia
                {
                    this.a = 0;
                    this.b = 1;
                    this.c = - pointA.Y;
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
                    //invalid line
                }
            }
        }

        public double DistanceFromLine(Coordinate point)
        {
            return Math.Abs(a * point.X + b * point.Y + C) / Math.Sqrt(a * a + b * b);
        }
    }

}
