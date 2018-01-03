using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;


namespace KmlTest2
{

    class Program
    {
        static void Main(string[] args)
        {
  //          string importFileName = @"C:\Users\cpc1\OneDrive\Work-Skydrive\battle of normandy.kml";
            string exportFileName = @"C:\Users\cpc1\OneDrive\Work-Skydrive\battle of normandy-test.kml";
    //        ImportPlaceMarks(importFileName);
            ExportKML(exportFileName);
        }

        static void ExportKML(string fileName)
        {
            List<Placemark> placemarks = new List<Placemark>();
            Kml kml = new Kml();
            Document document = new Document();
            using (bonEntities db = new bonEntities())
            {
                foreach (Location location in db.Locations)
                {
                    string locationName = location.Name;
                    string locationDescription = location.Description;
                    float? locationLatitude = location.Latitude;
                    float? locationLongitude = location.Longitude;
                    float? locationAltitude = location.Altitude;
                    SharpKml.Dom.Point point = new SharpKml.Dom.Point();
                    point.Coordinate = new Vector((double)locationLatitude, (double)locationLongitude, (double)locationAltitude);
                    Placemark placemark = new Placemark();
                    placemark.Name = locationName;
                    Description description = new Description();
                    description.Text = locationDescription;
                    placemark.Description = description;
                    placemark.Geometry = point;
                    document.AddFeature(placemark);
                }
            }

            kml.Feature = document;
           
            KmlFile kmlFile = SharpKml.Engine.KmlFile.Create(kml, false);
            using (var stream = System.IO.File.Create(fileName))
            {
                kmlFile.Save(stream);
            }
        }
        static void ImportPlaceMarks(string fileName)
        {
            List<PlaceMark> placeMarks = new List<PlaceMark>();

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\cpc1\OneDrive\Work-Skydrive\battle of normandy.kml");
            XmlNodeList p;
            p = doc.GetElementsByTagName("Placemark");
            foreach (XmlNode sourcePlaceMark in p)
            {
                string thisName = string.Empty;
                string thisDescription = string.Empty;
                float longitude = 0;
                float latitude = 0;
                float altitude = 0;
                XmlNodeList e = sourcePlaceMark.ChildNodes;
                foreach (XmlNode node in e)
                {

                    string nodeName = node.Name;

                    if (nodeName == "name")
                    {
                        thisName = node.FirstChild.Value;
                    }
                    else
                    {
                        if (nodeName == "description")
                        {
                            thisDescription = node.FirstChild.Value;
                        }
                        else
                        {
                            if (nodeName == "Point")
                            {
                                XmlNode coordinatesNode = node.FirstChild.FirstChild;
                                string[] theseCoordinates = coordinatesNode.Value.Trim().Split(',');
                                if (float.TryParse(theseCoordinates[0], out longitude))
                                {

                                }
                                if (float.TryParse(theseCoordinates[1], out latitude))
                                {

                                }
                                if (float.TryParse(theseCoordinates[2], out altitude))
                                {

                                }
                            }
                        }
                    }
                }
                PlaceMark placeMark = new PlaceMark(thisName, thisDescription, longitude, latitude, altitude);
                placeMarks.Add(placeMark);

            }
            foreach (PlaceMark placeMark in placeMarks)
            {
                Console.WriteLine(placeMark.Name);
                placeMark.AddPlaceMark(placeMark);
            }
        }
    }
}