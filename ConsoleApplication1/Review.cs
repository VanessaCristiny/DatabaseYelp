using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parse_yelp
{
    // class to keep all the attributes of review
    public class Review
    {
        public Votes votes { get; set; }
        public string user_id { get; set; }
        public string review_id { get; set; }
        public int stars { get; set; }
        public string date { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public string business_id { get; set; }
    }

    public class Votes
    {
        // it's used by class user too
        public int funny { get; set; }
        public int useful { get; set; }
        public int cool { get; set; }
    }
    // class created to dispose the results on the datagridview
    public class TempReview
    {
        public string Date { get; set; }
        public float Stars { get; set; }
        public string Text { get; set; }
        public string User_Name { get; set; }
        public int Votes { get; set; }

        public TempReview(string d, float s, string t, string n, int v)
        {
            this.Date = d;
            this.Stars = s;
            this.Text = t;
            this.User_Name = n;
            this.Votes = v;
        }
    }
}
