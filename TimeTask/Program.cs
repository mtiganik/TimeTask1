using System;
using TimeTask.Models;

namespace TimeTask 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filename = "visits1.txt";
            Program p = new Program();
            List<(Time,Time)> Input = p.getInputData(filename);

            //List<Hour> Hours = InitializeCompareData();

            List<Hour> HoursResult = p.getResultsOnList(Input);

            foreach (Hour hour in HoursResult)
            {
                Console.WriteLine("/// " + hour.Id + " ");
                foreach (var minute in hour.Minutes)
                {
                    Console.WriteLine(minute.Id + " " + minute.CountVal);
                }
                Console.WriteLine();
            }
            //    foreach (var item in Input)
            //{
            //    Console.WriteLine($"{item.Item1.hour}:{item.Item1.minute} -> {item.Item2.hour}:{item.Item2.minute}");
            //}
            //Console.WriteLine("Hello World!");
        }

        List<Hour> getResultsOnList(List<(Time, Time)> Input)
        {
            var result = InitializeCompareData();
            for(int i = 0; i < Input.Count; i++)
            {
                if(Input[i].Item1.hour == Input[i].Item2.hour) // kui sisenemine ja väljumine toimusid sama tunni sees
                {
                    for(int j = Input[i].Item1.minute; j <= Input[i].Item2.minute; j++)
                    {
                        int count = result[Input[i].Item1.hour].Minutes[j].CountVal;
                        count++;
                        result[Input[i].Item1.hour].Minutes[j].CountVal = count; 
                    }
                }
                else if(Input[i].Item2.hour - Input[i].Item1.hour == 1) // väljumine järgmisel tunnil
                {
                    for(int j = Input[i].Item1.minute; j < 60; j++)
                    {
                        int count = result[Input[i].Item1.hour].Minutes[j].CountVal;
                        count++;
                        result[Input[i].Item1.hour].Minutes[j].CountVal = count;

                    }
                    for(int j = 0; j <= Input[i].Item2.minute; j++)
                    {
                        int count = result[Input[i].Item2.hour].Minutes[j].CountVal;
                        count++;
                        result[Input[i].Item2.hour].Minutes[j].CountVal = count;
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            return result;
        }

        //private List<Hour> IncreaseAMinute(List<Hour> Data, int hour, int minute)
        //{
        //    int count = Data[hour].Minutes[minute].CountVal;
        //    count++;
        //    Data[hour].Minutes[minute].CountVal = count;    
        //    return Data;
        //}

        List<Hour> InitializeCompareData()
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

        public List<(Time,Time)> getInputData(string filename)
        {
            List<(Time, Time)> result = new List<(Time, Time)>();
            if (File.Exists(filename))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    //Tuple<int, int> row;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] linesplit = line.Split(',');
                        string[] entranceString = linesplit[0].Split(':');
                        string[] leaveString = linesplit[1].Split(':');
                        //((int, int), (int, int)) row = ((int.Parse(entrance[0]), int.Parse(entrance[1])), (int.Parse(leave[0]), int.Parse(leave[1])));
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
                        (Time,Time) row =(entrance, leave);
                        result.Add(row);
                    }
                }
            }
            return result;
        }
    }
}
