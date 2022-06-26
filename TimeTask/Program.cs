using System;
using TimeTask.Models;

namespace TimeTask 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename = "visits.txt";

            List<(Time,Time)> Input = getInputData(filename);

            List<Hour> HoursResult = getResultsOnList(Input);

            DisplayData(HoursResult);

            //// Debug code
            //foreach (Hour hour in HoursResult)
            //{
            //    Console.WriteLine("/// " + hour.Id + " ");
            //    foreach (var minute in hour.Minutes)
            //    {
            //        Console.WriteLine(minute.Id + " " + minute.CountVal);
            //    }
            //    Console.WriteLine();
            //}
            Console.ReadLine();
        }

        static public List<(Time, Time)> getInputData(string filename)
        {
            List<(Time, Time)> result = new List<(Time, Time)>();
            if (File.Exists(filename))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] linesplit = line.Split(',');
                        string[] entranceString = linesplit[0].Split(':');
                        string[] leaveString = linesplit[1].Split(':');
                        Time entrance = new Time()
                        {
                            hour = int.Parse(entranceString[0]),
                            minute = int.Parse(entranceString[1]),
                        };
                        Time leave = new Time()
                        {
                            hour = int.Parse(leaveString[0]),
                            minute = int.Parse(leaveString[1]),
                        };
                        (Time, Time) row = (entrance, leave);
                        result.Add(row);
                    }
                }
            }
            else
            {
                string directory = Directory.GetCurrentDirectory();
                throw new FileNotFoundException($"File {filename} was not found. Please place it to {directory} folder");
            }
            return result;
        }

        static List<Hour> getResultsOnList(List<(Time, Time)> Input)
        {
            var result = InitializeCompareData();
            for (int i = 0; i < Input.Count; i++)
            {
                if (Input[i].Item1.hour == Input[i].Item2.hour) // kui sisenemine ja väljumine toimusid sama tunni sees
                {
                    for (int j = Input[i].Item1.minute; j <= Input[i].Item2.minute; j++)
                    {
                        IncreaseStartHourMinute(Input, result, i, j);
                    }
                }
                else if (Input[i].Item2.hour - Input[i].Item1.hour == 1) // väljumine järgmisel tunnil
                {
                    for (int j = Input[i].Item1.minute; j < 60; j++)
                    {
                        IncreaseStartHourMinute(Input, result, i, j);

                    }
                    for (int j = 0; j <= Input[i].Item2.minute; j++)
                    {
                        IncreaseEndHourMinute(Input, result, i, j);
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            return result;
        }

        static List<Hour> InitializeCompareData()
        {
            List<Hour> hourList = new List<Hour>();

            for (int i = 0; i < 24; i++)
            {
                List<Minute> minuteList = new List<Minute>();
                for (int j = 0; j < 60; j++)
                {
                    minuteList.Add(new Minute()
                    {
                        CountVal = 0,
                        Id = j,
                    });
                }
                hourList.Add(new Hour()
                {
                    Id = i,
                    Minutes = minuteList
                });
            }
            return hourList;
        }




        static void DisplayData(List<Hour> Input)
        {
            // find max visitors count at any time
            int count = 0;
            foreach (Hour hour in Input)
            {
                foreach(Minute minute in hour.Minutes)
                {
                    if(minute.CountVal > count) count = minute.CountVal;
                }
            }
            Console.WriteLine($"Maximum visitors ({count}) where at the museum on these times:");
            foreach (Hour hour in Input)
            {
                foreach (Minute minute in hour.Minutes)
                {
                    if (minute.CountVal == count) Console.WriteLine(hour.Id + ":" + minute.Id);

                }
            }
        }


        private static void IncreaseStartHourMinute(List<(Time, Time)> Input, List<Hour> result, int i, int j)
        {
            int count = result[Input[i].Item1.hour].Minutes[j].CountVal;
            count++;
            result[Input[i].Item1.hour].Minutes[j].CountVal = count;
        }
        private static void IncreaseEndHourMinute(List<(Time, Time)> Input, List<Hour> result, int i, int j)
        {
            int count = result[Input[i].Item2.hour].Minutes[j].CountVal;
            count++;
            result[Input[i].Item2.hour].Minutes[j].CountVal = count;
        }

    }
}
