using System;
using TimeTask.Models;

namespace TimeTask 
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filename = "visits.txt";

            List<(Time, Time)> Input = getInputData(filename);

            List<Hour> HoursResult = getResultsOnList(Input);

            DisplayData(HoursResult);

            // Debug(HoursResult);

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
                int StartHour = Input[i].Item1.hour;
                int EndHour = Input[i].Item2.hour;
                int HourTicker = StartHour;
                int startIndex, endIndex = 0;
                while(HourTicker <= EndHour)
                {
                    if (HourTicker == StartHour) startIndex = Input[i].Item1.minute;
                    else startIndex = 0;
                    if (HourTicker == EndHour) endIndex = Input[i].Item2.minute + 1;
                    else endIndex = 60;

                    for(int j = startIndex; j < endIndex; j++)
                    {
                        int count = result[HourTicker].Minutes[j].CountVal;
                        count++;
                        result[HourTicker].Minutes[j].CountVal = count;

                    }
                    HourTicker++;
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
            List<string> result = new List<string>();
            for(int i = 0; i < 24; i++)
            {
                for(int j = 0; j < 60; j++)
                {
                    if(Input[i].Minutes[j].CountVal == count)
                    {
                        string res = Input[i].ToString(j);
                        bool isSingleMinute = true;

                        IncreaseCounter(ref i, ref j);
                        while (Input[i].Minutes[j].CountVal == count)
                        {
                            isSingleMinute = false;
                            IncreaseCounter(ref i, ref j);
                        }
                        if (!isSingleMinute)
                        {
                            DecreaseCounter(ref i, ref j);
                            res += " - " + Input[i].ToString(j);
                            IncreaseCounter(ref i, ref j);
                        }
                        result.Add(res);
                    }
                }
            }
            
            Console.WriteLine($"Maximum visitors ({count}) where at the museum on these times:");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }

        private static void IncreaseCounter(ref int i, ref int j)
        {
            j++;
            if (j == 60)
            {
                i++;
                j = 0;
            }
        }

        private static void DecreaseCounter(ref int i, ref int j)
        {
            j--;
            if (j == -1)
            {
                i--;
                j = 60;
            }
        }
        private static void Debug(List<Hour> HoursResult)
        {
            foreach (Hour hour in HoursResult)
            {
                Console.WriteLine("/// " + hour.Id + " ");
                foreach (var minute in hour.Minutes)
                {
                    Console.WriteLine(hour.ToString(minute.Id) + " " + minute.CountVal);
                }
                Console.WriteLine();
            }
        }

    }
}

