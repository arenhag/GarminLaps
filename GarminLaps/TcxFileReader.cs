﻿using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using GarminLaps.Dto;

namespace GarminLaps
{
    // Flytta Track/Trackpoint[] från ett Lap till ett annat Lap
    // Ta bort ett Lap (och således flytta alla dess Track/Trackpoint[] till ett annat Lap)
    // Lägga till ett Lap (och således hämta Track/Trackpoint[] från ett annat Lap (definiton?) och lägga till dessa till det nya Lap:et)

    // Vissa parametrar som hör till ett Lap måste beräknas om (t.ex. AverageHeartRateBpm o.s.v.)

    internal class TcxFileReader
    {
        internal int JunkMethodForTest(int a, int b)
        {
            return a + b;
        }

        internal LapData ReadTcxFile(string fileLocation)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(fileLocation);
            
            var nsManager = new XmlNamespaceManager(xmlDocument.NameTable);
            nsManager.AddNamespace("blaha", "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2");

            var laps = xmlDocument.SelectNodes("blaha:TrainingCenterDatabase/blaha:Activities/blaha:Activity/blaha:Lap", nsManager);
            var lapDataToReturn = new LapData();


            foreach (XmlNode lap in laps)
            {
                var lapToReturn = new Lap();

                /*var lapStartTime = DateTime.Parse(lap.Attributes["StartTime"].Value);
                var lapLength = double.Parse(lap.SelectSingleNode("blaha:TotalTimeSeconds", nsManager).InnerXml);
                var lapEndTime = lapStartTime.AddSeconds(lapLength);
                var calories = int.Parse(lap.SelectSingleNode("blaha:Calories", nsManager).InnerXml);*/
                
                var trackPoints = lap.SelectNodes("blaha:Track/blaha:Trackpoint", nsManager);
                foreach (XmlNode trackPoint in trackPoints)
                {
                    var trackPointToReturn = new TrackPoint()
                    {
                        HeartRateBpm = int.Parse(trackPoint.SelectSingleNode("blaha:HeartRateBpm/blaha:Value", nsManager).InnerXml),
                        DateTime = DateTime.Parse(trackPoint.SelectSingleNode("blaha:Time", nsManager).InnerXml)
                    };
                    
                    lapToReturn.TrackPoints.Add(trackPointToReturn);
                }

                lapDataToReturn.Laps.Add(lapToReturn);
            }

            xmlDocument.Save("test.tcx");

            return lapDataToReturn;
        }
    }
}
