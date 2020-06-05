using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.ShapeFile;
using System.IO;

using GeoAPI.Geometries;
using ProjNet.CoordinateSystems;
using NetTopologySuite.IO;
using System.Diagnostics;
using Coordinate = NetTopologySuite.Geometries.Coordinate;
using System.Globalization;

namespace Generalizer
{
    static class Program
    {
        public static string inputFile = string.Empty;
        public static string outputFile = ".\\output";
        public static string outputFileName = ".\\output\\output.shp";
        public static double corridorWidth = 0f;

        public static Feature CreateFeature(Coordinate[] coords)
        {
            var wkt = "LINESTRING (";
            for (int i = 0; i < coords.Count(); i++)
            {
                if (i > 0) { wkt += ", "; }
                wkt += (string)(coords[i].X).ToString().Replace(",", ".").Replace(".", ".")
                    + " "
                    + coords[i].Y.ToString().Replace(",", ".").Replace(".", ".");
            }
            wkt += ")";

            var geomFactory = new GeometryFactory(new PrecisionModel(), 4326);
            var wktReader = new WKTReader(geomFactory);

            var geometry = wktReader.Read(wkt);

            var attributesTable = new AttributesTable();

            return new Feature(geometry, attributesTable);
        }

        public static void CreateShpFile(List<IFeature> features, string path)
        {
            // Create the directory where we will save the shapefile
            var shapeFilePath = path;
            //string[] pathTab = path.Split("\\");
            string[] pathTab = path.Split(new string[] { "\\" }, StringSplitOptions.None);
            var name = pathTab.Last();
            //var shapeFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            if (!Directory.Exists(shapeFilePath))
                Directory.CreateDirectory(shapeFilePath);

            // Construct the shapefile name. Don't add the .shp extension or the ShapefileDataWriter will 
            // produce an unwanted shapefile
            var shapeFileName = Path.Combine(shapeFilePath, name);
            var shapeFilePrjName = Path.Combine(shapeFilePath, $"{name}.prj");

            // Create the shapefile
            var outGeomFactory = GeometryFactory.Default;
            var writer = new ShapefileDataWriter(shapeFileName, outGeomFactory);
            var outDbaseHeader = ShapefileDataWriter.GetHeader(features[0], features.Count);
            writer.Header = outDbaseHeader;
            writer.Write(features);

            // Create the projection file
            using (var streamWriter = new StreamWriter(shapeFilePrjName))
            {
                streamWriter.Write(GeographicCoordinateSystem.WGS84.WKT);
            }

            //var shapeFileReader = new ShapefileDataReader(shapeFileName, GeometryFactory.Default);
            //var read = shapeFileReader.Read();
            //var geom = shapeFileReader.Geometry;
        }


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
                        attrs.Add(header.Fields[i].Name, reader.GetValue(i));
                    }

                    f.Attributes = attrs;

                    collection.Add(f);
                }
            }
            return collection;
        }

        public static Coordinate[] DouglasGeneralize(Geometry geometry, double corridorWidth)
        {
            NetTopologySuite.Geometries.Coordinate[] coordinates = geometry.Coordinates;
            //Test coordinates
            //Coordinate[] coordinates = new Coordinate[] { new Coordinate(0.0, 0.0), new Coordinate(0.50, 0.60), new Coordinate(1.0, 1.10), new Coordinate(2.0, 20.0), new Coordinate(4.0, 4.0), new Coordinate(5.0, 5.0), new Coordinate(10.0, 10.0) }; //test values
            bool[] toDelete = new bool[coordinates.Count()];

            for (int i = 0; i < coordinates.Count(); i++)
            {
                toDelete[i] = false;
            }

            Stack<int> indexesStack = new Stack<int>();
            int leftIndex = 0, rightIndex = coordinates.Length - 1;

            do
            {
                //allInCorridor = true;
                Line line = new Line(coordinates[leftIndex], coordinates[rightIndex]);

                //drbug
                double testY = line.Y(coordinates[leftIndex].X);
                double testYreal = coordinates[leftIndex].Y;
                //end debug

                double maxDistance = 0.0;
                int indexWithMaxDistance = 0;

                for (int i = leftIndex + 1; i < rightIndex; i++) //sprawdz odleglosc kazdego punktu podzbioru od prostej
                {
                    double distance = line.DistanceFromLine(coordinates[i]);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        indexWithMaxDistance = i;
                    }
                }

                if (maxDistance > corridorWidth)
                {
                    // jesli jakis punkt znajduje sie poza korytarzem
                    indexesStack.Push(rightIndex);
                    rightIndex = indexWithMaxDistance;
                }
                else
                {
                    // wszystkie punkty podzbioru znajdują sie w korytarzu
                    for (int i = leftIndex + 1; i < rightIndex; i++)
                    {
                        toDelete[i] = true;
                    }
                    leftIndex = rightIndex;

                    if (indexesStack.Count > 0)
                    {
                        //jeśli na stosie coś pozostało, przypisz do right, jeśli nie, to left zrówna się z right (ostatnim punktem) i petla sie skonczy
                        rightIndex = indexesStack.Pop();
                    }
                }


            }
            while (leftIndex != coordinates.Length - 1); //dopóki nie sprawdzisz wszystkich punktów

            List<NetTopologySuite.Geometries.Coordinate> newCoordinates = new List<NetTopologySuite.Geometries.Coordinate>();
            for (int i = 0; i < toDelete.Count(); i++)
            {
                if (!toDelete[i])
                {
                    newCoordinates.Add(coordinates[i]);
                }
            }

            return newCoordinates.ToArray();
        }

        public static void ReadArgs(string[] args, ref string inputFile, ref string outputFile, ref double corridorWidth)
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

            string corridorString;
            if (w != null)
            {
                corridorString = o;
                //corridorWidth = float.Parse(o);
            }
            else
            {
                Console.WriteLine("CorridorWidth: ");
                corridorString = Console.ReadLine();

            }
            corridorWidth = double.Parse(corridorString.Replace(".", ",").Replace(",", ","));
        }

        public static void GeneralizeFile () 
        {
            FeatureCollection collection = ReadShapeFile(inputFile);
            FeatureCollection newCollection = new FeatureCollection();
            List<IFeature> newFeatures = new List<IFeature>();

            for (int i = 0; i < collection.Count; i++)
            {
                Feature feature = (Feature)collection[i];
                Geometry geometry = collection[i].Geometry;
                Coordinate[] newCoords = DouglasGeneralize(geometry, corridorWidth);
                newFeatures.Add(CreateFeature(newCoords));

            }
            CreateShpFile(newFeatures, outputFile);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /////////////////////////////////////////////////
            ///

            //ReadArgs(args, ref inputFile, ref outputFile, ref corridorWidth);

            /*FeatureCollection collection = ReadShapeFile(inputFile);
            FeatureCollection newCollection = new FeatureCollection();
            List<IFeature> newFeatures = new List<IFeature>();

            for (int i = 0; i < collection.Count; i++)
            {
                Feature feature = (Feature)collection[i];
                Geometry geometry = collection[i].Geometry;
                Coordinate[] newCoords = DouglasGeneralize(geometry, corridorWidth);
                newFeatures.Add(CreateFeature(newCoords));

            }
            CreateShpFile(newFeatures, outputFile);*/

        }
    }
}
