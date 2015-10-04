using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parse_yelp
{
    // class to keep all the attributes of business 
    public class Business
    {
        public string business_id { get; set; }
        public string full_address { get; set; }
        public Hours hours { get; set; }
        public bool open { get; set; }
        public string[] categories { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int review_count { get; set; }
        public string name { get; set; }
        public string[] neighborhoods { get; set; }
        public float stars { get; set; }
        public Attributes attributes { get; set; }
        public string type { get; set; }
    }

    public class Hours
    {
        public Sunday Sunday { get; set; }
        public Monday Monday { get; set; }
        public Tuesday Tuesday { get; set; }
        public Wednesday Wednesday { get; set; }
        public Thursday Thursday { get; set; }
        public Friday Friday { get; set; }
        public Saturday Saturday { get; set; }

        public Hours()
        {
            //initialize
            Sunday = new Sunday();
            Monday = new Monday();
            Tuesday = new Tuesday();
            Wednesday = new Wednesday();
            Thursday = new Thursday();
            Friday = new Friday();
            Saturday = new Saturday();
        }
    }

    public class Sunday
    {
        public string close { get; set; }
        public string open { get; set; }

        public Sunday()
        {
            this.open = null;
        }
    }

    public class Monday
    {
        public string close { get; set; }
        public string open { get; set; }

        public Monday()
        {
            open = null;
        }
    }

    public class Tuesday
    {
        public string close { get; set; }
        public string open { get; set; }

        public Tuesday()
        {
            open = null;
        }
    }

    public class Wednesday
    {
        public string close { get; set; }
        public string open { get; set; }

        public Wednesday()
        {
            open = null;
        }

    }

    public class Thursday
    {
        public string close { get; set; }
        public string open { get; set; }

        public Thursday()
        {
            open = null;
        }

    }

    public class Friday
    {
        public string close { get; set; }
        public string open { get; set; }

        public Friday()
        {
            open = null;
        }

    }

    public class Saturday
    {
        public string close { get; set; }
        public string open { get; set; }

        public Saturday()
        {
            open = null;
        }

    }

    public class Attributes
    {
        public string ByAppointmentOnly { get; set; }
        public string Takeout { get; set; }
        public GoodFor GoodFor { get; set; }
        public string Caters { get; set; }
        public string NoiseLevel { get; set; }
        public string WiFi { get; set; }
        public string TakesReservations { get; set; }
        public string Delivery { get; set; }
        public string DogsAllowed { get; set; }
        public Ambience Ambience { get; set; }
        public Parking Parking { get; set; }
        public string WheelchairAccessible { get; set; }
        public string HasTV { get; set; }
        public string OutdoorSeating { get; set; }
        public string Attire { get; set; }
        public string Alcohol { get; set; }
        public string WaiterService { get; set; }
        public string AcceptsCreditCards { get; set; }
        public string GoodforKids { get; set; }
        public string GoodForGroups { get; set; }
        public string PriceRange { get; set; }
        public Music Music { get; set; }
        public string GoodForDancing { get; set; }
        public string CoatCheck { get; set; }
        public string Smoking { get; set; }
        public string HappyHour { get; set; }
        public string Corkage { get; set; }
        public string BYOB { get; set; }
        public string OrderatCounter { get; set; }
        public string BYOBCorkage { get; set; }
        public DietaryRestrictions DietaryRestrictions { get; set; }
        public string Open24Hours { get; set; }

        public Attributes()
        {
            NoiseLevel = null;
            WiFi = null;
            Attire = null;
            Alcohol = null;
            PriceRange = null;
            Smoking = null;
            BYOBCorkage = null;
            Ambience = new Ambience();
            GoodFor = new GoodFor();
            Parking = new Parking();
            DietaryRestrictions = new DietaryRestrictions();
            Music = new Music();
            ByAppointmentOnly = null;
            Takeout = null;
            Caters = null;
            TakesReservations = null;
            Delivery = null;
            DogsAllowed = null;
            WheelchairAccessible = null;
            HasTV = null;
            OutdoorSeating = null;
            WaiterService = null;
            AcceptsCreditCards = null;
            GoodforKids = null;
            GoodForGroups = null;
            GoodForDancing = null;
            CoatCheck = null;
            HappyHour = null;
            Corkage = null;
            BYOB = null;
            OrderatCounter = null;
            Open24Hours = null;
        }

    }
    public class GoodFor
    {
        public string dessert { get; set; }
        public string latenight { get; set; }
        public string lunch { get; set; }
        public string dinner { get; set; }
        public string breakfast { get; set; }
        public string brunch { get; set; }

        public GoodFor()
        {
            dessert = null;
            latenight = null;
            lunch = null;
            dinner = null;
            breakfast = null;
            brunch = null;
        }
    }

    public class Ambience
    {
        public string romantic { get; set; }
        public string intimate { get; set; }
        public string touristy { get; set; }
        public string hipster { get; set; }
        public string divey { get; set; }
        public string classy { get; set; }
        public string trendy { get; set; }
        public string upscale { get; set; }
        public string casual { get; set; }

        public Ambience()
        {
            romantic = null;
            intimate = null;
            touristy = null;
            hipster = null;
            divey = null;
            classy = null;
            trendy = null;
            upscale = null;
            casual = null;
        }
    }

    public class Parking
    {
        public string garage { get; set; }
        public string street { get; set; }
        public string validated { get; set; }
        public string lot { get; set; }
        public string valet { get; set; }

        public Parking()
        {
            garage = null;
            street = null;
            validated = null;
            lot = null;
            valet = null;
        }
    }

    public class Music
    {
        public string dj { get; set; }
        public string background_music { get; set; }
        public string jukebox { get; set; }
        public string live { get; set; }
        public string video { get; set; }
        public string karaoke { get; set; }

        public Music()
        {
            dj = null;
            background_music = null;
            jukebox = null;
            live = null;
            video = null;
            karaoke = null;
        }
    }

    public class DietaryRestrictions
    {
        public string dairyfree { get; set; }
        public string glutenfree { get; set; }
        public string vegan { get; set; }
        public string kosher { get; set; }
        public string halal { get; set; }
        public string soyfree { get; set; }
        public string vegetarian { get; set; }

        public DietaryRestrictions()
        {
            dairyfree = null;
            glutenfree = null;
            vegan = null;
            kosher = null;
            halal = null;
            soyfree = null;
            vegetarian = null;
        }
    }

    // class created to dispose the results on the datagridview
    public class Bus
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public float Stars { get; set; }
        public string Bus_id { get; set; }
        public string Full_Address { get; set; }

        public Bus(string name, string ci, string st, float s, string bi, string fa)
        {
            this.Name = name;
            this.City = ci;
            this.State = st;
            this.Stars = s;
            this.Bus_id = bi;
            this.Full_Address = fa;
        }
    }
}