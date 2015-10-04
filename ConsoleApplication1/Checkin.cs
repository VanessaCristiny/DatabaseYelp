using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApplication1
{

    public class Checkin
    {
        public List<Checkin_Info> checkin_info = new List<Checkin_Info>();//holds the checkin_info
        public string business_id { get; set; }
    }

    public class Checkin_Info
    {
        public string dayTime { get; set; } // holds the day and the time of the checkin 
        public int checkin { get; set; } // holds the number of checkins
    }  
}

    
