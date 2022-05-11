using System;

namespace TP1LouisPhilippeRousseau
{
    public struct Point
    {
        public int X { get; private init; }

        public int Y { get;  private init; }

        public Point(int x , int y)
        {
            X = x;
            Y = y;
        }
        public static Point operator +(Point point1 , Point  point2) => new Point(point1.X + point2.X, point1.Y + point2.Y);
        public static Point operator -(Point point1) => new Point(-point1.X ,-point1.Y);
        public static bool operator ==(Point point1 , Point point2) => (point1.X == point2.X && point1.Y == point2.Y);
        public static bool operator !=(Point point1, Point point2) => !(point1 == point2);
        public static double Distance(Point point1 , Point point2) => Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));

        
    }
}
