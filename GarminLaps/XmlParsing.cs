using System;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace GarminLaps
{
    // Flytta Track/Trackpoint[] från ett Lap till ett annat Lap
    // Ta bort ett Lap (och således flytta alla dess Track/Trackpoint[] till ett annat Lap)
    // Lägga till ett Lap (och således hämta Track/Trackpoint[] från ett annat Lap (definiton?) och lägga till dessa till det nya Lap:et)

    // Vissa parametrar som hör till ett Lap måste beräknas om (t.ex. AverageHeartRateBpm o.s.v.)

    internal class XmlParsing
    {
        internal void Main()
        {
            ReadTcxFile();
        }

        internal int JunkMethodForTest(int a, int b)
        {
            return a + b;
        }

        internal void ReadTcxFile()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load("2155649148.tcx");
            
            var nsManager = new XmlNamespaceManager(xmlDocument.NameTable);
            nsManager.AddNamespace("blaha", "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2");

            var laps = xmlDocument.SelectNodes("blaha:TrainingCenterDatabase/blaha:Activities/blaha:Activity/blaha:Lap", nsManager);

            var lapIndex = 1;

            foreach (XmlNode lap in laps)
            {
                var lapStartTime = DateTime.Parse(lap.Attributes["StartTime"].Value);
                var lapLength = double.Parse(lap.SelectSingleNode("blaha:TotalTimeSeconds", nsManager).InnerXml);
                var lapEndTime = lapStartTime.AddSeconds(lapLength);

                Debug.WriteLine("Lap #{0} Start time:\t{1}", lapIndex, lapStartTime);
                Debug.WriteLine("Lap #{0} End time:\t{1}", lapIndex, lapEndTime);

                var calories = int.Parse(lap.SelectSingleNode("blaha:Calories", nsManager).InnerXml);
                var accumulatedHeartRates = 0;
                var trackPoints = lap.SelectNodes("blaha:Track/blaha:Trackpoint", nsManager);

                foreach (XmlNode trackPoint in trackPoints)
                {
                    var heartRate = int.Parse(trackPoint.SelectSingleNode("blaha:HeartRateBpm/blaha:Value", nsManager).InnerXml);
                    accumulatedHeartRates += heartRate;
                }

                lapIndex++;
            }

            xmlDocument.Save("test.tcx");
        }
    }
}
