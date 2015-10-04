/*WSU EECS CptS 451*/
/*Instructor: Sakire Arslan Ay*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace parse_yelp
{
    class Parser
    {
        //initialize the input/output data directory. Currently set to execution folder. 
        public static String dataDir = ".//";
        static void Main(string[] args)
        {
            // Uncomment to populate database
            //Brace yourself, it might take a while :-p
            /*
            JSONParser my_parser =  new JSONParser();
            
            //Parse yelp_business.json 
            my_parser.parseJSONFileBusiness(dataDir+"yelp_business.json", dataDir + "business.sql");
            
            //Parse yelp_review.json 
            my_parser.parseJSONFileReview(dataDir+"yelp_review.json",dataDir+"review.sql");

            //Parse yelp_user.json 
            my_parser.parseJSONFileUser(dataDir + "yelp_user.json", dataDir + "user.sql");
            
            //Parse yelp_checkin.json
            my_parser.parseJSONFileCheckin(dataDir + "yelp_checkin.json", dataDir + "checkin.sql");
            
            my_parser.populate();
            */
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConsoleApplication1.Form2());
        }
    }
}
