﻿using System;
using System.Xml;
using GarminLaps.Dto;
using GarminLaps.Extensions;

namespace GarminLaps.Readers
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

                // What data do I need to map out new laps and perform the necessary calculations?

                // Needed:
                // * Calories

                // Probably not needed:
                // * TotalTimeSeconds
                // * DistanceMeters
                // * MaximumSpeed (optional)                
                // * AverageHeartBeatBpm (optional)
                // * MaximumHeartBeatBpm (optional)
                // * Intensity
                // * Cadence (optional)
                // * TriggerMethod
                // * Track (optional...!)
                // * Notes (optional)
                // * Extensions (optional)

                /*var lapStartTime = DateTime.Parse(lap.Attributes["StartTime"].Value);
                var lapLength = double.Parse(lap.SelectSingleNode("TCDB:TotalTimeSeconds", nsManager).InnerXml);
                var lapEndTime = lapStartTime.AddSeconds(lapLength);*/

                var calories = UInt16.Parse(lap.SelectSingleNode("TCDB:Calories", nsManager).InnerXml);

                var lapToReturn = new Lap()
                {
                    Calories = calories
                };
                
                var sourceTrackPoints = lap.SelectNodes("TCDB:Track/TCDB:Trackpoint", nsManager);
                foreach (XmlNode sourceTrackPoint in sourceTrackPoints)
                {
                    var heartRateBpm = (sourceTrackPoint.SelectSingleNode("TCDB:HeartRateBpm/TCDB:Value", nsManager)).GetOptionalValue<int>();
                    var latitude = (sourceTrackPoint.SelectSingleNode("TCDB:Position/TCDB:LatitudeDegrees", nsManager)).GetOptionalValue<double>();
                    var longitude = (sourceTrackPoint.SelectSingleNode("TCDB:Position/TCDB:LongitudeDegrees", nsManager)).GetOptionalValue<double>();
                    var altitudeMeters = (sourceTrackPoint.SelectSingleNode("TCDB:AltitudeMeters", nsManager)).GetOptionalValue<double>();
                    var distanceMeters = (sourceTrackPoint.SelectSingleNode("TCDB:DistanceMeters", nsManager)).GetOptionalValue<double>();
                    var cadence = (sourceTrackPoint.SelectSingleNode("TCDB:Cadence", nsManager)).GetOptionalValue<byte>();
                    var sensorState = ((sourceTrackPoint.SelectSingleNode("TCDB:SensorState", nsManager)) == null) ? SensorState.None : (SensorState)Enum.Parse(typeof(SensorState), (sourceTrackPoint.SelectSingleNode("TCDB:SensorState", nsManager)).InnerXml);
                    var extensions = ((sourceTrackPoint.SelectSingleNode("TCDB:Extensions", nsManager)) == null) ? String.Empty : (sourceTrackPoint.SelectSingleNode("TCDB:Extensions", nsManager)).OuterXml;
                    
                    var trackPointToReturn = new TrackPoint()
                    {
                        DateTime = DateTime.Parse(sourceTrackPoint.SelectSingleNode("TCDB:Time", nsManager).InnerXml),
                        Position = (latitude != null && longitude != null) ? new Position(){LatitudeDegrees = (double)latitude, LongitudeDegrees = (double)longitude} : null,
                        AltitudeMeters = altitudeMeters,
                        DistanceMeters = distanceMeters,
                        HeartRateBpm = heartRateBpm,
                        Cadence = cadence,
                        SensorState = sensorState,
                        Extensions = extensions
                    };

                    lapToReturn.TrackPoints.Add(trackPointToReturn);
                }

                lapDataToReturn.Laps.Add(lapToReturn);
            }

            return lapDataToReturn;
        }
    }
}
