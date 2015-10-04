using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parse_yelp
{
    //class to keep the attributes of user
    public class User
    {
        public string yelping_since { get; set; }
        public Votes votes { get; set; }
        public int review_count { get; set; }
        public string name { get; set; }
        public string user_id { get; set; }
        public string[] friends { get; set; }
        public int fans { get; set; }
        public float average_stars { get; set; }
        public string type { get; set; }
        public Compliments compliments { get; set; }
        public int[] elite { get; set; }
    }

    public class Compliments
    {
        public int profile { get; set; }
        public int funny { get; set; }
        public int plain { get; set; }
        public int writer { get; set; }
        public int list { get; set; }
        public int note { get; set; }
        public int photos { get; set; }
        public int hot { get; set; }
        public int more { get; set; }
        public int cool { get; set; }

        public Compliments()
        {
            //initialize
            profile = 0;
            funny = 0;
            plain = 0;
            writer = 0;
            list = 0;
            note = 0;
            photos = 0;
            hot = 0;
            more = 0;
            cool = 0;
        }
    }
}
