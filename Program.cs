using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using System;
using System.Collections.Generic;

namespace generalizator
{
    class Program
    {
        public static FeatureCollection ReadShapeFile(string localShapeFile)
        {
            var collection = new FeatureCollection();
            var factory = new GeometryFactory();
            using (var reader = new ShapefileDataReader(localShapeFile, factory))
            {
                var header = reader.DbaseHeader;
                while (reader.Read())
                {
                    var f = new Feature { Geometry = reader.Geometry };

                    var attrs = new AttributesTable();
                    for (var i = 0; i < header.NumFields; i++)
                    {
                        //attrs.a
                        attrs.Add(header.Fields[i].Name, reader.GetValue(i));
                    }

                    f.Attributes = attrs;

                    collection.Add(f);
                }
            }
            return collection;
        }

        public static Geometry DouglasGeneralize(Geometry geometry, float corridorWidth)
        {
            Coordinate[] coordinates = geometry.Coordinates;
            List<Coordinate> coordsList = new List<Coordinate>(coordinates);
            //TODO
            bool allInCorridor = false;

            while(!allInCorridor)
            {

            }

            return geometry;
        }

        public static void ReadArgs(string[] args, ref string inputFile, ref string outputFile, ref float corridorWidth)
        {
            string f = null;
            string o = null;
            string w = null;
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] == "-f") { f = args[i + 1]; }
                else if (args[i] == "-o") { o = args[i + 1]; }
                else if (args[i] == "-w") { w = args[i + 1]; }
            }

            if (f != null)
            {
                inputFile = f;
            }
            else
            {
                Console.WriteLine("Path to input file: ");
                inputFile = Console.ReadLine();
            }

            if (o != null)
            {

                outputFile = o;
            }
            else
            {
                Console.WriteLine("Path to output file: ");
                outputFile = Console.ReadLine();
            }

            if (w != null)
            {

                corridorWidth = float.Parse(o);
            }
            else
            {
                Console.WriteLine("CorridorWidth: ");
                corridorWidth = float.Parse(Console.ReadLine());
            }

        }

        static void Main(string[] args)
        {

            string inputFile = null;
            string outputFile = null;
            float corridorWidth = 0.0f;

            ReadArgs(args, ref inputFile, ref outputFile, ref corridorWidth);

            FeatureCollection collection = ReadShapeFile(inputFile);

            for(int i = 0; i< collection.Count; i++)
            {
                Geometry geometry = collection[i].Geometry;
                //Coordinate[] coordinates = collection[i].Geometry.Coordinates;
                DouglasGeneralize(geometry, corridorWidth);
            }

            //Shapefile shp = new Shapefile();

            //String filename;
            //GeometryFactory geometryFactory = new GeometryFactory(new PrecisionModel());
            //ShapefileDataReader inputFileReader = Shapefile.CreateDataReader(inputFile, geometryFactory);
            //Geometry geometry = inputFileReader.Geometry;
        }
    }
}
