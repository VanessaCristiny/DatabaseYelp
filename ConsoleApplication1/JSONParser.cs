using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using MySql.Data.MySqlClient;
using ConsoleApplication1;


namespace parse_yelp
{

    class JSONParser
    {
        // keep all files, businesses, reviews, users and checkins
        public List<Business> lBusiness { get; set; }
        public List<Review> lReview { get; set; }
        public List<User> lUser { get; set; }
        public List<Checkin> lCheckin { get; set; }

        internal void parseJSONFileBusiness(string p1, string p2)
        {
            StreamReader reader = new StreamReader(p1);//get the path
            lBusiness = new List<Business>();//initialize the list

            while (!reader.EndOfStream)//read until the end of the file
            {
                string json = reader.ReadLine();// read the line
                Business temp = JsonConvert.DeserializeObject<Business>(json);//temp receives the information properly 

                string l = json.Replace("\"", "");
                l = l.Replace(":", "");
                l = l.Replace(",", "");
                l = l.Replace("[", "");
                l = l.Replace("]", "");
                l = l.Replace("}", "");
                l = l.Replace("{", "");
                l = l.Trim();
                string[] words = l.Split(' ');
                //manually set attribute values (JsonConvert wasn't working)
                for (int i = 0; i < words.Count(); i++)
                {
                    if (words[i].ToLower() == "only" && words[i - 1].ToLower() == "appointment" && words[i - 2].ToLower() == "by")
                    {
                        temp.attributes.ByAppointmentOnly = words[i + 1];
                    }
                    else if (words[i].ToLower() == "take-out")
                    {
                        temp.attributes.Takeout = words[i + 1];
                    }
                    else if (words[i] == "caters")
                    {
                        temp.attributes.Caters = words[i + 1];
                    }
                    else if (words[i].ToLower() == "reservations" && words[i - 1].ToLower() == "takes")
                    {
                        temp.attributes.TakesReservations = words[i + 1];
                    }
                    else if (words[i] == "delivery")
                    {
                        temp.attributes.Delivery = words[i + 1];
                    }
                    else if (words[i].ToLower() == "allowed" && words[i - 1].ToLower() == "dogs")
                    {
                        temp.attributes.DogsAllowed = words[i + 1];
                    }
                    else if (words[i].ToLower() == "acessible" && words[i - 1].ToLower() == "chair" && words[i - 2].ToLower() == "wheel")
                    {
                        temp.attributes.WheelchairAccessible = words[i + 1];
                    }
                    else if (words[i].ToLower() == "tv" && words[i - 1].ToLower() == "has")
                    {
                        temp.attributes.HasTV = words[i + 1];
                    }
                    else if (words[i].ToLower() == "seating" && words[i - 1].ToLower() == "outdoor")
                    {
                        temp.attributes.OutdoorSeating = words[i + 1];
                    }
                    else if (words[i].ToLower() == "service" && words[i - 1].ToLower() == "waiter")
                    {
                        temp.attributes.WaiterService = words[i + 1];
                    }
                    else if (words[i].ToLower() == "cards" && words[i - 1].ToLower() == "credit" && words[i - 2].ToLower() == "accepts")
                    {
                        temp.attributes.AcceptsCreditCards = words[i + 1];
                    }
                    else if (words[i].ToLower() == "kids" && words[i - 1].ToLower() == "for" && words[i - 2].ToLower() == "good")
                    {
                        temp.attributes.GoodforKids = words[i + 1];
                    }
                    else if (words[i].ToLower() == "groups" && words[i - 1].ToLower() == "for" && words[i - 2].ToLower() == "good")
                    {
                        temp.attributes.GoodForGroups = words[i + 1];
                    }
                    else if (words[i].ToLower() == "dancing" && words[i - 1].ToLower() == "for" && words[i - 2].ToLower() == "good")
                    {
                        temp.attributes.GoodForDancing = words[i + 1];
                    }
                    else if (words[i].ToLower() == "check" && words[i - 1].ToLower() == "coat")
                    {
                        temp.attributes.CoatCheck = words[i + 1];
                    }
                    else if (words[i].ToLower() == "hour" && words[i - 1].ToLower() == "happy")
                    {
                        temp.attributes.HappyHour = words[i + 1];
                    }
                    else if (words[i].ToLower() == "corkage")
                    {
                        temp.attributes.Corkage = words[i + 1];
                    }
                    else if (words[i].ToLower() == "byob")
                    {
                        temp.attributes.BYOB = words[i + 1];
                    }
                    else if (words[i].ToLower() == "counter" && words[i - 1].ToLower() == "at" && words[i - 2].ToLower() == "order")
                    {
                        temp.attributes.OrderatCounter = words[i + 1];
                    }
                    else if (words[i].ToLower() == "hours" && words[i - 1].ToLower() == "24" && words[i - 2].ToLower() == "open")
                    {
                        temp.attributes.Open24Hours = words[i + 1];
                    }
                    else if (words[i] == "dessert")
                    {
                        temp.attributes.GoodFor.dessert = words[i + 1];
                    }
                    else if (words[i] == "latenight")
                    {
                        temp.attributes.GoodFor.latenight = words[i + 1];
                    }
                    else if (words[i] == "lunch")
                    {
                        temp.attributes.GoodFor.lunch = words[i + 1];
                    }
                    else if (words[i] == "dinner")
                    {
                        temp.attributes.GoodFor.dinner = words[i + 1];
                    }
                    else if (words[i] == "breakfast")
                    {
                        temp.attributes.GoodFor.breakfast = words[i + 1];
                    }
                    else if (words[i] == "brunch")
                    {
                        temp.attributes.GoodFor.brunch = words[i + 1];
                    }
                    else if (words[i] == "romantic")
                    {
                        temp.attributes.Ambience.romantic = words[i + 1];
                    }
                    else if (words[i] == "intimate")
                    {
                        temp.attributes.Ambience.intimate = words[i + 1];
                    }
                    else if (words[i] == "touristy")
                    {
                        temp.attributes.Ambience.touristy = words[i + 1];
                    }
                    else if (words[i] == "hipster")
                    {
                        temp.attributes.Ambience.hipster = words[i + 1];
                    }
                    else if (words[i] == "divey")
                    {
                        temp.attributes.Ambience.divey = words[i + 1];
                    }
                    else if (words[i] == "classy")
                    {
                        temp.attributes.Ambience.classy = words[i + 1];
                    }
                    else if (words[i] == "trendy")
                    {
                        temp.attributes.Ambience.trendy = words[i + 1];
                    }
                    else if (words[i] == "upscale")
                    {
                        temp.attributes.Ambience.upscale = words[i + 1];
                    }
                    else if (words[i] == "casual" && words[i - 1] != "Attire")
                    {
                        temp.attributes.Ambience.casual = words[i + 1];
                    }
                    else if (words[i] == "garage")
                    {
                        temp.attributes.Parking.garage = words[i + 1];
                    }
                    else if (words[i] == "street")
                    {
                        temp.attributes.Parking.street = words[i + 1];
                    }
                    else if (words[i] == "validated")
                    {
                        temp.attributes.Parking.validated = words[i + 1];
                    }
                    else if (words[i] == "lot")
                    {
                        temp.attributes.Parking.lot = words[i + 1];
                    }
                    else if (words[i] == "valet")
                    {
                        temp.attributes.Parking.valet = words[i + 1];
                    }
                    else if (words[i] == "dj")
                    {
                        temp.attributes.Music.dj = words[i + 1];
                    }
                    else if (words[i] == "background_music")
                    {
                        temp.attributes.Music.background_music = words[i + 1];
                    }
                    else if (words[i] == "jukebox")
                    {
                        temp.attributes.Music.jukebox = words[i + 1];
                    }
                    else if (words[i] == "live")
                    {
                        temp.attributes.Music.live = words[i + 1];
                    }
                    else if (words[i] == "video")
                    {
                        temp.attributes.Music.video = words[i + 1];
                    }
                    else if (words[i] == "karaoke")
                    {
                        temp.attributes.Music.karaoke = words[i + 1];
                    }
                    else if (words[i].ToLower() == "dairy-free")
                    {
                        temp.attributes.DietaryRestrictions.dairyfree = words[i + 1];
                    }
                    else if (words[i] == "gluten-free")
                    {
                        temp.attributes.DietaryRestrictions.glutenfree = words[i + 1];
                    }
                    else if (words[i] == "vegan")
                    {
                        temp.attributes.DietaryRestrictions.vegan = words[i + 1];
                    }
                    else if (words[i] == "kosher")
                    {
                        temp.attributes.DietaryRestrictions.kosher = words[i + 1];
                    }
                    else if (words[i] == "halal")
                    {
                        temp.attributes.DietaryRestrictions.halal = words[i + 1];
                    }
                    else if (words[i].ToLower() == "soy-free")
                    {
                        temp.attributes.DietaryRestrictions.soyfree = words[i + 1];
                    }
                    else if (words[i] == "vegetarian")
                    {
                        temp.attributes.DietaryRestrictions.vegetarian = words[i + 1];
                    }
                    else if (words[i].ToLower() == "range" && words[i - 1].ToLower() == "price")
                    {
                        temp.attributes.PriceRange = words[i + 1];
                    }
                }
                lBusiness.Add(temp); // add in the list
            }
            reader.Close();// close the file
        }

        internal void parseJSONFileReview(string p1, string p2)
        {
            StreamReader reader = new StreamReader(p1);
            lReview = new List<Review>();
            //read all reviews
            while (!reader.EndOfStream)
            {
                string json = reader.ReadLine();
                Review temp = JsonConvert.DeserializeObject<Review>(json);
                lReview.Add(temp); // add to list
            }
            reader.Close();
        }

        internal void parseJSONFileUser(string p1, string p2)
        {
            StreamReader reader = new StreamReader(p1);
            lUser = new List<User>();
            //read all users
            while (!reader.EndOfStream)
            {
                string json = reader.ReadLine();
                User temp = JsonConvert.DeserializeObject<User>(json);
                lUser.Add(temp); // add to list
            }
            reader.Close();
        }

        internal void parseJSONFileCheckin(string p1, string p2)
        {
            StreamReader reader = new StreamReader(p1);
            lCheckin = new List<Checkin>();

            while (!reader.EndOfStream)
            {
                string json = reader.ReadLine();
                string l = json.Replace("{\"checkin_info\": {", "");
                l = l.Replace(", \"type\": \"checkin", "");
                l = l.Replace("\"", "");
                l = l.Replace(":", "");
                l = l.Replace(",", "");
                l = l.Replace("}", "");
                l = l.Trim();

                string[] words = l.Split(' ');
                string day = null;

                Checkin check = new Checkin();
                //read days, time and #checkins to the list
                foreach (var w in words)
                {
                    Checkin_Info temp = new Checkin_Info();
                    if (w == "business_id")
                    {
                        break;
                    }
                    else if (w.Contains('-'))
                    {
                        day = w;
                    }
                    else if (!w.Equals(null) && !w.Contains('-'))
                    {
                        int count = Convert.ToInt32(w);
                        temp.dayTime = day;
                        temp.checkin = count;
                        check.checkin_info.Add(temp);//add to the list of checkin_info
                    }
                }
                check.business_id = words[(words.Length) - 1];//set business_id
                lCheckin.Add(check);// add to list
            }
            reader.Close();
        }

        internal void populate()
        {
            string server = "server=localhost;user=root;database=project;password=lugano;";
            MySqlConnection conn = new MySqlConnection(server);
            conn.Open();

            // attributes
            string tableAttributes = "INSERT IGNORE INTO Attribute VALUES ('AcceptsCreditCards'); INSERT IGNORE INTO Attribute VALUES ('Alcohol'); INSERT IGNORE INTO Attribute VALUES ('AmbienceCasual'); ";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('AmbienceClassy'); INSERT IGNORE INTO Attribute VALUES ('AmbienceDivey'); INSERT IGNORE INTO Attribute VALUES ('AmbienceHipster');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('AmbienceIntimate'); INSERT IGNORE INTO Attribute VALUES ('AmbienceRomantic'); INSERT IGNORE INTO Attribute VALUES ('AmbienceTouristy');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('AmbienceTrendy'); INSERT IGNORE INTO Attribute VALUES ('AmbienceUpscale');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('Attire'); INSERT IGNORE INTO Attribute VALUES ('ByAppointmentOnly'); INSERT IGNORE INTO Attribute VALUES ('BYOB'); ";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('BYOBCorkage'); INSERT IGNORE INTO Attribute VALUES ('Caters'); INSERT IGNORE INTO Attribute VALUES ('CoatCheck');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('Corkage'); INSERT IGNORE INTO Attribute VALUES ('Delivery'); INSERT IGNORE INTO Attribute VALUES ('DietaryRestrictionsdairyfree'); ";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('DietaryRestrictionsglutenfree'); INSERT IGNORE INTO Attribute VALUES ('DietaryRestrictionshalal'); INSERT IGNORE INTO Attribute VALUES ('DietaryRestrictionskosher'); ";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('DietaryRestrictionssoyfree'); INSERT IGNORE INTO Attribute VALUES ('DietaryRestrictionsvegan'); INSERT IGNORE INTO Attribute VALUES ('DietaryRestrictionsvegetarian');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('DogsAllowed'); INSERT IGNORE INTO Attribute VALUES ('GoodForBreakfast'); INSERT IGNORE INTO Attribute VALUES ('GoodForBrunch');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('GoodForDessert'); INSERT IGNORE INTO Attribute VALUES ('GoodForDinner'); INSERT IGNORE INTO Attribute VALUES ('GoodForLateNight');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('GoodForLunch'); INSERT IGNORE INTO Attribute VALUES ('GoodForDancing'); INSERT IGNORE INTO Attribute VALUES ('GoodForGroups');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('GoodForKids'); INSERT IGNORE INTO Attribute VALUES ('HappyHour'); INSERT IGNORE INTO Attribute VALUES ('HasTV');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('MusicBackground_Music'); INSERT IGNORE INTO Attribute VALUES ('MusicDj'); INSERT IGNORE INTO Attribute VALUES ('MusicJukebox');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('MusicKaraoke'); INSERT IGNORE INTO Attribute VALUES ('MusicLive'); INSERT IGNORE INTO Attribute VALUES ('MusicVideo');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('NoiseLevel'); INSERT IGNORE INTO Attribute VALUES ('Open24Hours'); INSERT IGNORE INTO Attribute VALUES ('OrderatCounter');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('OutdoorSeating'); INSERT IGNORE INTO Attribute VALUES ('ParkingGarage'); INSERT IGNORE INTO Attribute VALUES ('ParkingLot');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('ParkingStreet'); INSERT IGNORE INTO Attribute VALUES ('ParkingValet'); INSERT IGNORE INTO Attribute VALUES ('ParkingValidated');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('PriceRange'); INSERT IGNORE INTO Attribute VALUES ('Smoking'); INSERT IGNORE INTO Attribute VALUES ('Takeout'); INSERT IGNORE INTO Attribute VALUES ('TakesReservations');";
            tableAttributes += "INSERT IGNORE INTO Attribute VALUES ('WaiterService'); INSERT IGNORE INTO Attribute VALUES ('WheelChairAcessible'); INSERT IGNORE INTO Attribute VALUES ('Wifi');";

            MySqlCommand command6 = new MySqlCommand(tableAttributes, conn);
            command6.ExecuteNonQuery();
            //finishes attributes

            //table daysofweek
            string tableDaysofWeek = "INSERT IGNORE INTO DaysOfWeek VALUES ('Sunday'); INSERT IGNORE INTO DaysOfWeek VALUES ('Monday'); INSERT IGNORE INTO DaysOfWeek VALUES ('Tuesday');";
            tableDaysofWeek += "INSERT IGNORE INTO DaysOfWeek VALUES ('Wednesday'); INSERT IGNORE INTO DaysOfWeek VALUES ('Thursday'); INSERT IGNORE INTO DaysOfWeek VALUES ('Friday');";
            tableDaysofWeek += "INSERT IGNORE INTO DaysOfWeek VALUES ('Saturday');";

            MySqlCommand command12 = new MySqlCommand(tableDaysofWeek, conn);
            command12.ExecuteNonQuery();
            //finishes daysofweek

            //compliments
            string tableCompliments = "INSERT IGNORE INTO Compliment VALUES ('Cool'); INSERT IGNORE INTO Compliment VALUES ('Funny'); INSERT IGNORE INTO Compliment VALUES ('More');";
            tableCompliments += "INSERT IGNORE INTO Compliment VALUES ('Hot'); INSERT IGNORE INTO Compliment VALUES ('List'); INSERT IGNORE INTO Compliment VALUES ('Note');";
            tableCompliments += "INSERT IGNORE INTO Compliment VALUES ('Photos'); INSERT IGNORE INTO Compliment VALUES ('Plain'); INSERT IGNORE INTO Compliment VALUES ('Profile');";
            tableCompliments += "INSERT IGNORE INTO Compliment VALUES ('Writer');";

            MySqlCommand command7 = new MySqlCommand(tableCompliments, conn);
            command7.ExecuteNonQuery();
            //finishes compliments

            foreach (var item in lBusiness)
            {
                //table business
                item.full_address = (item.full_address).Replace('\n', ' ');
                item.full_address = (item.full_address).Replace("'", "''");
                item.name = (item.name).Replace("'", "''");
                string tableCategory = null;
                string tableBusiness = "INSERT IGNORE INTO Business VALUES ('" + item.business_id + "','" + item.name + "','" + item.state + "','" + item.city + "','" + item.latitude + "','" + item.longitude + "','" + item.review_count + "','" + item.full_address + "','" + item.stars + "');";
                //table category
                foreach (var item2 in item.categories)
                {
                    var item5 = item2.Replace("'", "''");
                    if (item2.ToLower() == "active life" || item2.ToLower() == "arts & entertainment" || item2.ToLower() == "automotive" || item2.ToLower() == "car rental" || item2.ToLower() == "cafes" || item2.ToLower() == "beauty & spas" || item2.ToLower() == "convenience stores" || item2.ToLower() == "dentists" || item2.ToLower() == "doctors" || item2.ToLower() == "drugstores" || item2.ToLower() == "department stores" || item2.ToLower() == "education" || item2.ToLower() == "event planning & services" || item2.ToLower() == "flowers & gifts" || item2.ToLower() == "food" || item2.ToLower() == "health & medical" || item2.ToLower() == "home services" || item2.ToLower() == "home & garden" || item2.ToLower() == "hospitals" || item2.ToLower() == "hotels & travel" || item2.ToLower() == "hardware stores" || item2.ToLower() == "grocery" || item2.ToLower() == "medical centers" || item2.ToLower() == "nurseries & gardening" || item2.ToLower() == "nightlife" || item2.ToLower() == "restaurants" || item2.ToLower() == "shopping" || item2.ToLower() == "transportation")
                    {
                        tableCategory = "INSERT IGNORE INTO Category VALUES ('" + item5 + "','" + "Main" + "');";
                    }
                    else
                        tableCategory = "INSERT IGNORE INTO Category VALUES ('" + item5 + "','" + "Sub" + "');";
                    MySqlCommand command3 = new MySqlCommand(tableCategory, conn);
                    command3.ExecuteNonQuery();
                    // finishes tablecategory
                }
                MySqlCommand command = new MySqlCommand(tableBusiness, conn);
                command.ExecuteNonQuery();
                // finishes business
            }

            foreach (var item in lBusiness)
            {
                foreach (var item2 in item.categories)
                {
                    if (item2.ToLower() == "active life" || item2.ToLower() == "arts & entertainment" || item2.ToLower() == "automotive" || item2.ToLower() == "car rental" || item2.ToLower() == "cafes" || item2.ToLower() == "beauty & spas" || item2.ToLower() == "convenience stores" || item2.ToLower() == "dentists" || item2.ToLower() == "doctors" || item2.ToLower() == "drugstores" || item2.ToLower() == "department stores" || item2.ToLower() == "education" || item2.ToLower() == "event planning & services" || item2.ToLower() == "flowers & gifts" || item2.ToLower() == "food" || item2.ToLower() == "health & medical" || item2.ToLower() == "home services" || item2.ToLower() == "home & garden" || item2.ToLower() == "hospitals" || item2.ToLower() == "hotels & travel" || item2.ToLower() == "hardware stores" || item2.ToLower() == "grocery" || item2.ToLower() == "medical centers" || item2.ToLower() == "nurseries & gardening" || item2.ToLower() == "nightlife" || item2.ToLower() == "restaurants" || item2.ToLower() == "shopping" || item2.ToLower() == "transportation")
                    {   //table subcategories
                        foreach (var cat in item.categories)
                        {
                            if (item2 != cat)
                            {
                                if (cat.ToLower() != "active life" || cat.ToLower() != "arts & entertainment" && cat.ToLower() != "automotive" && cat.ToLower() != "car rental" && cat.ToLower() != "cafes" && cat.ToLower() != "beauty & spas" && cat.ToLower() != "convenience stores" && cat.ToLower() != "dentists" && cat.ToLower() != "doctors" && cat.ToLower() != "drugstores" && cat.ToLower() != "department stores" && cat.ToLower() != "education" && cat.ToLower() != "event planning & services" && cat.ToLower() != "flowers & gifts" && cat.ToLower() != "food" && cat.ToLower() != "health & medical" && cat.ToLower() != "home services" && cat.ToLower() != "home & garden" && cat.ToLower() != "hospitals" && cat.ToLower() != "hotels & travel" && cat.ToLower() != "hardware stores" && cat.ToLower() != "grocery" && cat.ToLower() != "medical centers" && cat.ToLower() != "nurseries & gardening" && cat.ToLower() != "nightlife" && cat.ToLower() != "restaurants" && cat.ToLower() != "shopping" && cat.ToLower() != "transportation")
                                {
                                    var item4 = item2.Replace("'", "''");
                                    var cat2 = cat.Replace("'", "''");
                                    string tableSubCategories = "INSERT IGNORE INTO subCategories VALUES ('" + item.business_id + "','" + item4 + "','" + cat2 + "');";
                                    MySqlCommand commandTBC = new MySqlCommand(tableSubCategories, conn);
                                    commandTBC.ExecuteNonQuery();
                                    //finishes table subcategories
                                }
                            }
                        }
                    }
                    //table apearsIn
                    var item3 = item2.Replace("'", "''");
                    if (item.attributes.AcceptsCreditCards != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AcceptsCreditCards" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Alcohol != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Alcohol" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.casual != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceCasual" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.classy != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceClassy" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.divey != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceDivey" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.hipster != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceHipster" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.intimate != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceIntimate" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.romantic != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceRomantic" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.touristy != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceTouristy" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.trendy != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceTrendy" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Ambience.upscale != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "AmbienceUpscale" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Attire != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Attire" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.ByAppointmentOnly != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "ByAppointmentOnly" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.BYOB != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "BYOB" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.BYOBCorkage != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "BYOBCorkage" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Caters != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Caters" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.CoatCheck != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "CoatCheck" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Corkage != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Corkage" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Delivery != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Delivery" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.DietaryRestrictions.dairyfree != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "DietaryRestrictionsDairyFree" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.DietaryRestrictions.glutenfree != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "DietaryRestrictionsGlutenFree" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.DietaryRestrictions.halal != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "DietaryRestrictionsHalal" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.DietaryRestrictions.kosher != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "DietaryRestrictionsKosher" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.DietaryRestrictions.soyfree != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "DietaryRestrictionsSoyFree" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.DietaryRestrictions.vegan != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "DietaryRestrictionsVegan" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.DietaryRestrictions.vegetarian != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "DietaryRestrictionsVegetarian" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.DogsAllowed != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "DogsAllowed" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodFor.breakfast != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForBreakFast" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodFor.brunch != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForBrunch" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodFor.dessert != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForDessert" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodFor.dinner != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForDinner" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodFor.latenight != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForLateNight" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodFor.lunch != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForLunch" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodForDancing != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForDancing" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodForGroups != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForGroups" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.GoodforKids != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "GoodForKids" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.HappyHour != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "HappyHour" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.HasTV != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "HasTV" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Music.background_music != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "MusicBackground_Music" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Music.dj != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "MusicDj" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Music.jukebox != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "MusicJukebox" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Music.karaoke != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "MusicKaraoke" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Music.live != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "MusicLive" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Music.video != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "MusicVideo" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.NoiseLevel != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "NoiseLevel" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Open24Hours != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Open24Hours" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.OrderatCounter != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "OrderatCounter" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.OutdoorSeating != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "OutdoorSeating" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Parking.garage != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "ParkingGarage" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Parking.lot != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "ParkingLot" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Parking.street != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "ParkingStreet" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Parking.valet != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "ParkingValet" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Parking.validated != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "ParkingValidated" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.PriceRange != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "PriceRange" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Smoking != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Smoking" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.Takeout != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Takeout" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.TakesReservations != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "TakesReservations" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.WaiterService != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "WaiterService" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.WheelchairAccessible != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "WheelChairAcessible" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    if (item.attributes.WiFi != null)
                    {
                        string tableAppearsIn = "INSERT IGNORE INTO AppearsIn VALUES ('" + item3 + "','" + "Wifi" + "');";
                        MySqlCommand command17 = new MySqlCommand(tableAppearsIn, conn);
                        command17.ExecuteNonQuery();
                    }
                    //finishes appearsIn  
                }
                //tableisopen
                if (!String.IsNullOrEmpty(item.hours.Sunday.open))
                {
                    string tableIsOpen = "INSERT IGNORE INTO IsOpen VALUES ('" + "Sunday" + "','" + item.business_id + "','" + item.hours.Sunday.open + "','" + item.hours.Sunday.close + "')";
                    MySqlCommand command13 = new MySqlCommand(tableIsOpen, conn);
                    command13.ExecuteNonQuery();
                }
                if (item.hours.Monday.open != null)
                {
                    string tableIsOpen = "INSERT IGNORE INTO IsOpen VALUES ('" + "Monday" + "','" + item.business_id + "','" + item.hours.Monday.open + "','" + item.hours.Monday.close + "')";
                    MySqlCommand command13 = new MySqlCommand(tableIsOpen, conn);
                    command13.ExecuteNonQuery();
                }
                if (item.hours.Tuesday.open != null)
                {
                    string tableIsOpen = "INSERT IGNORE INTO IsOpen VALUES ('" + "Tuesday" + "','" + item.business_id + "','" + item.hours.Tuesday.open + "','" + item.hours.Tuesday.close + "')";
                    MySqlCommand command13 = new MySqlCommand(tableIsOpen, conn);
                    command13.ExecuteNonQuery();
                }
                if (item.hours.Wednesday.open != null)
                {
                    string tableIsOpen = "INSERT IGNORE INTO IsOpen VALUES ('" + "Wednesday" + "','" + item.business_id + "','" + item.hours.Wednesday.open + "','" + item.hours.Wednesday.close + "')";
                    MySqlCommand command13 = new MySqlCommand(tableIsOpen, conn);
                    command13.ExecuteNonQuery();
                }
                if (item.hours.Thursday.open != null)
                {
                    string tableIsOpen = "INSERT IGNORE INTO IsOpen VALUES ('" + "Thursday" + "','" + item.business_id + "','" + item.hours.Thursday.open + "','" + item.hours.Thursday.close + "')";
                    MySqlCommand command13 = new MySqlCommand(tableIsOpen, conn);
                    command13.ExecuteNonQuery();
                }
                if (item.hours.Friday.open != null)
                {
                    string tableIsOpen = "INSERT IGNORE INTO IsOpen VALUES ('" + "Friday" + "','" + item.business_id + "','" + item.hours.Friday.open + "','" + item.hours.Friday.close + "')";
                    MySqlCommand command13 = new MySqlCommand(tableIsOpen, conn);
                    command13.ExecuteNonQuery();
                }
                if (item.hours.Saturday.open != null)
                {
                    string tableIsOpen = "INSERT IGNORE INTO IsOpen VALUES ('" + "Saturday" + "','" + item.business_id + "','" + item.hours.Saturday.open + "','" + item.hours.Saturday.close + "')";
                    MySqlCommand command13 = new MySqlCommand(tableIsOpen, conn);
                    command13.ExecuteNonQuery();
                }
                //finishes isOpen

                //table relatedto
                if (item.attributes.AcceptsCreditCards != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AcceptsCreditCards" + "','" + item.attributes.AcceptsCreditCards + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.casual != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceCasual" + "','" + item.attributes.Ambience.casual + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.classy != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceClassy" + "','" + item.attributes.Ambience.classy + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.divey != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceDivey" + "','" + item.attributes.Ambience.divey + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.hipster != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceHipster" + "','" + item.attributes.Ambience.hipster + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.intimate != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceIntimate" + "','" + item.attributes.Ambience.intimate + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.romantic != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceRomantic" + "','" + item.attributes.Ambience.romantic + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.touristy != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceTouristy" + "','" + item.attributes.Ambience.touristy + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.trendy != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceTrendy" + "','" + item.attributes.Ambience.trendy + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Ambience.upscale != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "AmbienceUpscale" + "','" + item.attributes.Ambience.upscale + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.ByAppointmentOnly != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "ByAppointmentOnly" + "','" + item.attributes.ByAppointmentOnly + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.BYOB != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "BYOB" + "','" + item.attributes.BYOB + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Caters != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Caters" + "','" + item.attributes.Caters + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.CoatCheck != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "CoatCheck" + "','" + item.attributes.CoatCheck + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Corkage != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Corkage" + "','" + item.attributes.Corkage + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Delivery != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Delivery" + "','" + item.attributes.Delivery + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.DietaryRestrictions.dairyfree != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "DietaryRestrictionsDairyFree" + "','" + item.attributes.DietaryRestrictions.dairyfree + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.DietaryRestrictions.glutenfree != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "DietaryRestrictionsGlutenFree" + "','" + item.attributes.DietaryRestrictions.glutenfree + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.DietaryRestrictions.halal != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "DietaryRestrictionsHalal" + "','" + item.attributes.DietaryRestrictions.halal + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.DietaryRestrictions.kosher != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "DietaryRestrictionsKosher" + "','" + item.attributes.DietaryRestrictions.kosher + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.DietaryRestrictions.soyfree != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "DietaryRestrictionsSoyFree" + "','" + item.attributes.DietaryRestrictions.soyfree + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.DietaryRestrictions.vegan != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "DietaryRestrictionsVegan" + "','" + item.attributes.DietaryRestrictions.vegan + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.DietaryRestrictions.vegetarian != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "DietaryRestrictionsVegetarian" + "','" + item.attributes.DietaryRestrictions.vegetarian + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.DogsAllowed != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "DogsAllowed" + "','" + item.attributes.DogsAllowed + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodFor.breakfast != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForBreakFast" + "','" + item.attributes.GoodFor.breakfast + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodFor.brunch != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForBrunch" + "','" + item.attributes.GoodFor.brunch + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodFor.dessert != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForDessert" + "','" + item.attributes.GoodFor.dessert + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodFor.dinner != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForDinner" + "','" + item.attributes.GoodFor.dinner + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodFor.latenight != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForLateNight" + "','" + item.attributes.GoodFor.latenight + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodFor.lunch != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForLunch" + "','" + item.attributes.GoodFor.lunch + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodForDancing != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForDancing" + "','" + item.attributes.GoodForDancing + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodForGroups != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForGroups" + "','" + item.attributes.GoodForGroups + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.GoodforKids != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "GoodForKids" + "','" + item.attributes.GoodforKids + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.HappyHour != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "HappyHour" + "','" + item.attributes.HappyHour + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.HasTV != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "HasTV" + "','" + item.attributes.HasTV + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Music.background_music != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "MusicBackground_Music" + "','" + item.attributes.Music.background_music + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Music.dj != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "MusicDj" + "','" + item.attributes.Music.dj + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Music.jukebox != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "MusicJukebox" + "','" + item.attributes.Music.jukebox + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Music.karaoke != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "MusicKaraoke" + "','" + item.attributes.Music.karaoke + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Music.live != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "MusicLive" + "','" + item.attributes.Music.live + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Music.video != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "MusicVideo" + "','" + item.attributes.Music.video + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Open24Hours != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Open24Hours" + "','" + item.attributes.Open24Hours + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.OrderatCounter != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "OrderatCounter" + "','" + item.attributes.OrderatCounter + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.OutdoorSeating != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "OutdoorSeating" + "','" + item.attributes.OutdoorSeating + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Parking.garage != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "ParkingGarage" + "','" + item.attributes.Parking.garage + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Parking.lot != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "ParkingLot" + "','" + item.attributes.Parking.lot + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Parking.street != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "ParkingStreet" + "','" + item.attributes.Parking.street + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Parking.valet != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "ParkingValet" + "','" + item.attributes.Parking.valet + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Parking.validated != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "ParkingValidated" + "','" + item.attributes.Parking.validated + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Takeout != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Takeout" + "','" + item.attributes.Takeout + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.TakesReservations != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "TakesReservations" + "','" + item.attributes.TakesReservations + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.WaiterService != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "WaiterService" + "','" + item.attributes.WaiterService + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.WheelchairAccessible != null)
                {
                    string tableRelatedTo = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "WheelChairAcessible" + "','" + item.attributes.WheelchairAccessible + "');";
                    MySqlCommand command14 = new MySqlCommand(tableRelatedTo, conn);
                    command14.ExecuteNonQuery();
                }
                if (item.attributes.Alcohol != null)
                {
                    string tableRelatedTo2 = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Alcohol" + "','" + item.attributes.Alcohol + "');";
                    MySqlCommand command11 = new MySqlCommand(tableRelatedTo2, conn);
                    command11.ExecuteNonQuery();
                }
                if (item.attributes.Attire != null)
                {
                    string tableRelatedTo3 = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Attire" + "','" + item.attributes.Attire + "');";
                    MySqlCommand command11 = new MySqlCommand(tableRelatedTo3, conn);
                    command11.ExecuteNonQuery();
                }
                if (item.attributes.BYOBCorkage != null)
                {
                    string tableRelatedTo4 = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "BYOBCorkage" + "','" + item.attributes.BYOBCorkage + "');";
                    MySqlCommand command11 = new MySqlCommand(tableRelatedTo4, conn);
                    command11.ExecuteNonQuery();
                }
                if (item.attributes.NoiseLevel != null)
                {
                    string tableRelatedTo5 = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "NoiseLevel" + "','" + item.attributes.NoiseLevel + "');";
                    MySqlCommand command11 = new MySqlCommand(tableRelatedTo5, conn);
                    command11.ExecuteNonQuery();
                }
                if (item.attributes.PriceRange != null)
                {
                    string tableRelatedTo6 = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "PriceRange" + "','" + item.attributes.PriceRange + "');";
                    MySqlCommand command11 = new MySqlCommand(tableRelatedTo6, conn);
                    command11.ExecuteNonQuery();
                }
                if (item.attributes.Smoking != null)
                {
                    string tableRelatedTo7 = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Smoking" + "','" + item.attributes.Smoking + "');";
                    MySqlCommand command11 = new MySqlCommand(tableRelatedTo7, conn);
                    command11.ExecuteNonQuery();
                }
                if (item.attributes.WiFi != null)
                {
                    string tableRelatedTo8 = "INSERT IGNORE INTO RelatedTo VALUES ('" + item.business_id + "','" + "Wifi" + "','" + item.attributes.WiFi + "');";
                    MySqlCommand command11 = new MySqlCommand(tableRelatedTo8, conn);
                    command11.ExecuteNonQuery();
                }
                //finishes relatedto
            }
            //table user
            foreach (var item in lUser)
            {
                string temp = (item.name).Replace("'", "''");
                string tableUser = "INSERT IGNORE INTO User VALUES ('" + item.user_id + "','" + item.review_count + "','" + temp + "','" + item.fans + "','" + item.average_stars + "','" + item.yelping_since + "','" + item.votes.funny + "','" + item.votes.useful + "','" + item.votes.cool + "');";
                MySqlCommand command4 = new MySqlCommand(tableUser, conn);
                command4.ExecuteNonQuery();
                //finishes table user
            }
            //table elite
            foreach (var item in lUser)
            {
                foreach (var item2 in item.elite)
                {
                    string tableElite = "INSERT IGNORE INTO elite VALUES ('" + item2 + "');";
                    MySqlCommand command5 = new MySqlCommand(tableElite, conn);
                    command5.ExecuteNonQuery();
                }
            }
            //finishes table elite

            foreach (var item in lUser)
            {
                //table isSelected
                foreach (var year in item.elite)
                {
                    string tableIsSelected = "INSERT IGNORE INTO IsSelected VALUES ('" + item.user_id + "','" + year + "');";
                    MySqlCommand command9 = new MySqlCommand(tableIsSelected, conn);
                    command9.ExecuteNonQuery();
                }
                //finishes table isselected
                //table arefriendswith
                foreach (var friend in item.friends)
                {
                    string tableAreFriendsWith = "INSERT IGNORE INTO AreFriendsWith VALUES ('" + item.user_id + "','" + friend + "');";
                    MySqlCommand command8 = new MySqlCommand(tableAreFriendsWith, conn);
                    command8.ExecuteNonQuery();
                }
                //finishes table arefriendswith
                //hasComplimented
                if (item.compliments.cool != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "Cool" + "','" + item.compliments.cool + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.funny != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "Funny" + "','" + item.compliments.funny + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.hot != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "Hot" + "','" + item.compliments.hot + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.list != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "List" + "','" + item.compliments.list + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.more != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "More" + "','" + item.compliments.more + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.note != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "Note" + "','" + item.compliments.note + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.photos != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "Photos" + "','" + item.compliments.photos + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.plain != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "Plain" + "','" + item.compliments.plain + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.profile != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "Profile" + "','" + item.compliments.profile + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                if (item.compliments.writer != 0)
                {
                    string tableHasComplimented = "INSERT IGNORE INTO HasComplimented VALUES ('" + item.user_id + "','" + "Writer" + "','" + item.compliments.writer + "');";
                    MySqlCommand command9 = new MySqlCommand(tableHasComplimented, conn);
                    command9.ExecuteNonQuery();
                }
                //finishes hascomplimented
            }
            foreach (var item in lReview)
            {
                //table review
                item.text = (item.text).Replace("'", "''");
                item.text = (item.text).Replace("\\", "/");
                string tableReview = "INSERT IGNORE INTO Review VALUES ('" + item.review_id + "','" + item.text + "','" + item.stars + "','" + item.votes.funny + "','" + item.votes.useful + "','" + item.votes.cool + "','" + item.date + "');";
                MySqlCommand command5 = new MySqlCommand(tableReview, conn);
                command5.ExecuteNonQuery();

                //table belongto
                string tableBelongTo = "INSERT IGNORE INTO BelongTo VALUES ('" + item.review_id + "','" + item.business_id + "','" + item.user_id + "');";
                MySqlCommand command10 = new MySqlCommand(tableBelongTo, conn);
                command10.ExecuteNonQuery();
            }
            //finishes belongto
            //finishes table review

            //table checkin
            //table has
            foreach (var item in lCheckin)
            {
                foreach (var data in item.checkin_info)
                {
                    string tableCheckin = null, tableHas = null;
                    string[] temp = data.dayTime.Split('-');
                    if (temp[1] == "0")
                    {
                        tableCheckin = "INSERT IGNORE INTO Checkin VALUES ('" + "Sunday" + "','" + temp[0] + "');";
                        tableHas = "INSERT IGNORE INTO Has VALUES ('" + item.business_id + "','" + "Sunday" + "','" + temp[0] + "','" + data.checkin + "');";
                    }
                    else if (temp[1] == "1")
                    {
                        tableCheckin = "INSERT IGNORE INTO Checkin VALUES ('" + "Monday" + "','" + temp[0] + "');";
                        tableHas = "INSERT IGNORE INTO Has VALUES ('" + item.business_id + "','" + "Monday" + "','" + temp[0] + "','" + data.checkin + "');";
                    }
                    else if (temp[1] == "2")
                    {
                        tableCheckin = "INSERT IGNORE INTO Checkin VALUES ('" + "Tuesday" + "','" + temp[0] + "');";
                        tableHas = "INSERT IGNORE INTO Has VALUES ('" + item.business_id + "','" + "Tuesday" + "','" + temp[0] + "','" + data.checkin + "');";
                    }
                    else if (temp[1] == "3")
                    {
                        tableCheckin = "INSERT IGNORE INTO Checkin VALUES ('" + "Wednesday" + "','" + temp[0] + "');";
                        tableHas = "INSERT IGNORE INTO Has VALUES ('" + item.business_id + "','" + "Wednesday" + "','" + temp[0] + "','" + data.checkin + "');";
                    }
                    else if (temp[1] == "4")
                    {
                        tableCheckin = "INSERT IGNORE INTO Checkin VALUES ('" + "Thursday" + "','" + temp[0] + "');";
                        tableHas = "INSERT IGNORE INTO Has VALUES ('" + item.business_id + "','" + "Thursday" + "','" + temp[0] + "','" + data.checkin + "');";
                    }
                    else if (temp[1] == "5")
                    {
                        tableCheckin = "INSERT IGNORE INTO Checkin VALUES ('" + "Friday" + "','" + temp[0] + "');";
                        tableHas = "INSERT IGNORE INTO Has VALUES ('" + item.business_id + "','" + "Friday" + "','" + temp[0] + "','" + data.checkin + "');";
                    }
                    else if (temp[1] == "6")
                    {
                        tableCheckin = "INSERT IGNORE INTO Checkin VALUES ('" + "Saturday" + "','" + temp[0] + "');";
                        tableHas = "INSERT IGNORE INTO Has VALUES ('" + item.business_id + "','" + "Saturday" + "','" + temp[0] + "','" + data.checkin + "');";
                    }
                    MySqlCommand command19 = new MySqlCommand(tableCheckin, conn);
                    command19.ExecuteNonQuery();
                    MySqlCommand command20 = new MySqlCommand(tableHas, conn);
                    command20.ExecuteNonQuery();
                }
            }
            //finishes table checkin
            //finishes table has 
            conn.Close();
        }
    }
}
