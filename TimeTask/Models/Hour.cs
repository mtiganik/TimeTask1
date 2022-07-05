using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTask.Models
{
    public class Hour
    {
        public int Id { get; set; }
        public List<Minute> Minutes { get; set; }

        public string ToString(int minute)
        {
            return Id.ToString("00") + ":" + Minutes[minute].Id.ToString("00");
        }
    }
}
