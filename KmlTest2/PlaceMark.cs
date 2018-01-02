using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmlTest2
{
    public class PlaceMark
    {
        public string Name;
        public string Description;
        public Point Point;

        public PlaceMark(string name, string description,float longitude, float latitude, float altitude)
        {
            Name = name;
            Description = description;
            Point = new Point(longitude, latitude, altitude);
        }

        public int AddPlaceMark(PlaceMark placeMark)
        {
            int result = 0;

            bonEntities db = new bonEntities();
            Location location = new Location();
            location.Name = @placeMark.Name;
            location.@Description = placeMark.Description;
            location.Latitude = placeMark.Point.Coordinates.Latitude;
            location.Longitude = placeMark.Point.Coordinates.Longitude;
            location.Altitude = placeMark.Point.Coordinates.Altitude;
            db.Locations.Add(location);
            db.SaveChanges();
            result = location.id;
            return result;
        }
    }

    public class Coordinates
    {
        public float Longitude;
        public float Latitude;
        public float Altitude;

        public Coordinates(float longitude, float latitude, float altitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Altitude = altitude;
        }
    }

    public class Point
    {
        public Coordinates Coordinates;

        public Point (float longitude, float latitude, float altitude)
        {
            Coordinates = new Coordinates(longitude, latitude, altitude);
        }
    }
}

