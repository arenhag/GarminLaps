using System;
using System.Xml;
using GarminLaps.Dto;
using GarminLaps.Extensions;

namespace GarminLaps
{
    // Flytta Track/Trackpoint[] från ett Lap till ett annat Lap
    // Ta bort ett Lap (och således flytta alla dess Track/Trackpoint[] till ett annat Lap)
    // Lägga till ett Lap (och således hämta Track/Trackpoint[] från ett annat Lap (definiton?) och lägga till dessa till det nya Lap:et)

    // Vissa parametrar som hör till ett Lap måste beräknas om (t.ex. AverageHeartRateBpm o.s.v.)

    internal class TcxFileReader
    {
        internal LapData ReadTcxFile(string fileLocation)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(fileLocation);
            
            var nsManager = new XmlNamespaceManager(xmlDocument.NameTable);
            nsManager.AddNamespace("TCDB", "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2");

            var laps = xmlDocument.SelectNodes("TCDB:TrainingCenterDatabase/TCDB:Activities/TCDB:Activity/TCDB:Lap", nsManager);
            var lapDataToReturn = new LapData();

            foreach (XmlNode lap in laps)
            {
                var lapToReturn = new Lap();

                /*var lapStartTime = DateTime.Parse(lap.Attributes["StartTime"].Value);
                var lapLength = double.Parse(lap.SelectSingleNode("TCDB:TotalTimeSeconds", nsManager).InnerXml);
                var lapEndTime = lapStartTime.AddSeconds(lapLength);
                var calories = int.Parse(lap.SelectSingleNode("TCDB:Calories", nsManager).InnerXml);*/
                
                var sourceTrackPoints = lap.SelectNodes("TCDB:Track/TCDB:Trackpoint", nsManager);
                foreach (XmlNode sourceTrackPoint in sourceTrackPoints)
                {
                    var heartRateBpm = (sourceTrackPoint.SelectSingleNode("TCDB:HeartRateBpm/TCDB:Value", nsManager)).GetOptionalValue<int>();
                    var latitude = (sourceTrackPoint.SelectSingleNode("TCDB:Position/TCDB:LatitudeDegrees", nsManager)).GetOptionalValue<double>();
                    var longitude = (sourceTrackPoint.SelectSingleNode("TCDB:Position/TCDB:LongitudeDegrees", nsManager)).GetOptionalValue<double>();
                    var altitudeMeters = (sourceTrackPoint.SelectSingleNode("TCDB:AltitudeMeters", nsManager)).GetOptionalValue<double>();
                    var distanceMeters = (sourceTrackPoint.SelectSingleNode("TCDB:DistanceMeters", nsManager)).GetOptionalValue<double>();
                    
                    var trackPointToReturn = new TrackPoint()
                    {
                        DateTime = DateTime.Parse(sourceTrackPoint.SelectSingleNode("TCDB:Time", nsManager).InnerXml), // non-optional value
                        Position = (latitude != null && longitude != null) ? new Position(){LatitudeDegrees = (double)latitude, LongitudeDegrees = (double)longitude} : null,
                        AltitudeMeters = altitudeMeters,
                        DistanceMeters = distanceMeters,
                        HeartRateBpm = heartRateBpm
                        // Cadence
                        // SensorState
                        // Extensions -- perhaps just parse this as an anonymous XML blob?
                    };

                    lapToReturn.TrackPoints.Add(trackPointToReturn);
                }

                lapDataToReturn.Laps.Add(lapToReturn);
            }

            return lapDataToReturn;
        }
    }
}
