using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.ShapeFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using GeoAPI.Geometries;
using ProjNet.CoordinateSystems;
using NetTopologySuite.IO;
using System.Diagnostics;
using Coordinate = NetTopologySuite.Geometries.Coordinate;
using System.Globalization;

namespace generalizator
{
    class Program
    {

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
            //// Wkt in 4326 SRID (WGS84)
            ////var wkt = "POLYGON ((-86.7605020509258 41.5101338613656, -86.7604972038273 41.5100611525915, -86.7604971708084 41.5100606308085, -86.7604611720717 41.5094596307695, -86.7604611426546 41.5094591103497, -86.7604291439208 41.5088571103154, -86.760429130715 41.508856853856, -86.7603991319814 41.5082548538241, -86.7603991259966 41.5082547317887, -86.7603701303631 41.5076537960468, -86.7603401446338 41.5070530565908, -86.7603071566895 41.5064532528163, -86.7603071500912 41.506453131098, -86.7602814240795 41.5059715533315, -86.7605549835241 41.5059607024218, -86.7605808466407 41.5064448078787, -86.760613844555 41.5070447469854, -86.7606138651484 41.5070451395365, -86.7606438664126 41.5076461395046, -86.7606438727239 41.5076462680791, -86.7606728710439 41.5082472070294, -86.7607028628788 41.5088490177453, -86.7607348434949 41.5094506292495, -86.7607708135428 41.5100511081057, -86.760776407335 41.5101350123382, -86.7605020509258 41.5101338613656))";

            //var wkt = "LINESTRING (";
            //for(int i=0; i<coords.Count(); i++)
            //{
            //    if (i > 0) wkt += ", ";
            //    wkt += (string)(coords[i].X).ToString().Replace(",", ".").Replace(".", ".") + " " + coords[i].Y.ToString().Replace(",", ".").Replace(".", ".");
            //}
            //wkt += ")";

            //var geomFactory = new GeometryFactory(new PrecisionModel(), 4326);
            //var wktReader = new WKTReader(geomFactory);

            //var geometry = wktReader.Read(wkt);

            ////Debug.WriteLine($"Geometry Type: {geometry.GeometryType}");
            ////Debug.WriteLine($"Shapefile Type: {Shapefile.GetShapeType(geometry)}");

            
            ////IPolygon geomPolygon = geometry as IPolygon;


            //var attributesTable = new AttributesTable();
            //var features = new List<IFeature>
            //{
            //    new Feature(geometry, attributesTable)
            //};

            // Create the directory where we will save the shapefile
            var shapeFilePath =  path;
            string[] pathTab = path.Split("\\");
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

            for(int i =0; i< coordinates.Count(); i++)
            {
                toDelete[i] = false;
            }

            Stack<int> indexesStack = new Stack<int>();
            int leftIndex = 0, rightIndex = coordinates.Length-1;

            do
            {
                //allInCorridor = true;
                Line line = new Line(coordinates[leftIndex], coordinates[rightIndex]);

                //drbug
                double testY = line.Y(coordinates[leftIndex].X);
                double testYreal = coordinates[leftIndex].Y;
                //end debug

                double maxDistance = 0.0;
                int indexWithMaxDistance=0;

                for (int i = leftIndex + 1; i < rightIndex; i++) //sprawdz odleglosc kazdego punktu podzbioru od prostej
                {
                    double distance = line.DistanceFromLine(coordinates[i]);
                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        indexWithMaxDistance = i;
                    }
                }

                if(maxDistance > corridorWidth)
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
            for (int i =0; i<toDelete.Count(); i++)
            {
                if(!toDelete[i])
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
            corridorWidth = double.Parse( corridorString.Replace(".", ",").Replace(",", ","));
        }

        static void Main(string[] args)
        {

            string inputFile = null;
            string outputFile = null;
            double corridorWidth = 0.0f;

            ReadArgs(args, ref inputFile, ref outputFile, ref corridorWidth);

            FeatureCollection collection = ReadShapeFile(inputFile);
            FeatureCollection newCollection = new FeatureCollection();
            List<IFeature> newFeatures = new List<IFeature>();

            for (int i = 0; i< collection.Count; i++)
            {
                Feature feature = (Feature)collection[i];
                Geometry geometry = collection[i].Geometry;
                Coordinate[] newCoords = DouglasGeneralize(geometry, corridorWidth);
                newFeatures.Add(CreateFeature(newCoords));
                
            }
            CreateShpFile(newFeatures, outputFile);
        }
    }
}
