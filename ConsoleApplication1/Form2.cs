using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using parse_yelp;

namespace ConsoleApplication1
{

    public partial class Form2 : Form
    {
        public string server = "server=localhost;user=root;database=project;password=lugano;";

        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(server);
            MySqlDataReader rdr = null, rdr2 = null, rdr3 = null, rdr4 = null;

            button1.Text = "Search";
            button2.Text = "Quit";
            conn.Open();
            // fill the day's box
            string days = "SELECT distinct (day) FROM daysOfWeek Order by day; ";
            MySqlCommand cmd2 = new MySqlCommand(days, conn);
            rdr2 = cmd2.ExecuteReader();
            while (rdr2.Read())
            {
                comboBox1.Items.Add(rdr2.GetString(0));
            }
            conn.Close();

            conn.Open();
            //fill the open time box
            string timeOpen = "SELECT distinct (openTime) from IsOpen ORDER BY openTime;";
            MySqlCommand cmd3 = new MySqlCommand(timeOpen, conn);
            rdr3 = cmd3.ExecuteReader();
            while (rdr3.Read())
            {
                comboBox2.Items.Add(rdr3.GetString(0));
            }
            conn.Close();

            conn.Open();
            //fill the close time box
            string timeClose = "SELECT distinct (closeTime) from IsOpen ORDER BY closeTime;";
            MySqlCommand cmd4 = new MySqlCommand(timeClose, conn);
            rdr4 = cmd4.ExecuteReader();
            while (rdr4.Read())
            {
                comboBox3.Items.Add(rdr4.GetString(0));
            }
            conn.Close();

            // any of the attributes/ all attributes box
            comboBox4.Items.Add("All Attributes");
            comboBox4.Items.Add("Any of the Attributes");

            conn.Open();
            //gets main categories
            string mainCat = "SELECT distinct (main_cat) FROM SubCategories; ";
            MySqlCommand cmd = new MySqlCommand(mainCat, conn);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                checkedListBox1.Items.Add(rdr.GetString(0));
            }
            conn.Close();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(server);
            MySqlDataReader rdr = null;
            conn.Open();

            //gets subcategories related to the selected main categories
            string subCat = "SELECT distinct (sub_cat) FROM SubCategories WHERE ";
            foreach (var item in checkedListBox1.CheckedItems)
            {
                if (subCat.Contains("main_cat"))
                {
                    subCat += " OR ";
                }
                subCat += "main_cat='" + item.ToString() + "'";
            }
            subCat += "ORDER BY sub_cat;";

            checkedListBox2.Items.Clear();
            checkedListBox3.Items.Clear();
            if (checkedListBox1.CheckedItems.Count > 0) //if user selected one or more main categories
            {
                MySqlCommand cmd = new MySqlCommand(subCat, conn);
                rdr = cmd.ExecuteReader();
                //displays subcategories
                while (rdr.Read())
                {
                    checkedListBox2.Items.Add(rdr.GetString(0));
                }
            }
            conn.Close();
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> attributes = new List<string>();//keeps attributes related to selected categories
            MySqlConnection conn = new MySqlConnection(server);
            MySqlDataReader rdr = null, rdr2 = null;
            conn.Open();

            string subCat = "SELECT distinct (attributeName) FROM AppearsIn WHERE ";
            foreach (var item in checkedListBox2.CheckedItems)
            {
                if (subCat.Contains("categoryName="))
                {
                    subCat += " OR ";
                }
                subCat += "categoryName='" + item.ToString() + "'";
            }
            subCat += ";";

            checkedListBox3.Items.Clear();
            if (checkedListBox2.CheckedItems.Count > 0)
            {
                MySqlCommand cmd = new MySqlCommand(subCat, conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    attributes.Add(rdr.GetString(0));
                }
                conn.Close();

                foreach (var item in attributes)
                {
                    conn.Open();
                    //selects distinct non- false values related with selected attributes
                    string selectValue = "SELECT distinct (value) FROM relatedTo WHERE attributeName = '" + item + "' AND value != 'false' AND value != ' ';";
                    MySqlCommand cmd2 = new MySqlCommand(selectValue, conn);
                    rdr2 = cmd2.ExecuteReader();
                    while (rdr2.Read())
                    {
                        string value = item;
                        if (rdr2.GetString(0).ToLower() != "true")
                        {
                            value += "_" + rdr2.GetString(0); //concats if value is different of true (there is no need to concat true values)
                        }
                        checkedListBox3.Items.Add(value);
                    }
                    conn.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(server);
            MySqlDataReader rdr = null;
            conn.Open();
            string attrib = "SELECT distinct business.name, business.city, business.state, business.stars, business.business_id, business.fulladdress FROM Business, RelatedTo, IsOpen, SubCategories WHERE subcategories.business_id = business.business_id AND business.business_id = RelatedTo.business_id AND IsOpen.business_id = business.business_id AND (";

            if (comboBox4.SelectedItem == "All Attributes")
            {
                foreach (var item in checkedListBox3.CheckedItems)
                {
                    string temp = item.ToString();
                    if (item.ToString().Contains('_'))
                    {
                        string[] words = item.ToString().Split('_');
                        if (words[0].ToLower() == "musicbackground")
                        {
                            temp = "MusicBackground_Music";
                        }
                        else
                            temp = words[0];//gets attribute name 
                    }
                    if (attrib.Contains("attributeName="))
                    {
                        attrib += " AND ";//all
                    }
                    attrib += "attributeName='" + temp + "'";
                }
                //sets day and time comparissons
                attrib += ") AND isOpen.day = '" + comboBox1.SelectedItem + "' AND isOpen.openTime <= '" + comboBox2.SelectedItem + "' AND (isOpen.closeTime >= '" + comboBox3.SelectedItem + "' OR isOpen.closeTime <= isOpen.openTime) AND (";

                foreach (var item in checkedListBox1.CheckedItems)
                { // sets main categories comparissons
                    if (attrib.Contains("subcategories.main_cat="))
                    {
                        attrib += " OR ";
                    }
                    attrib += "subcategories.main_cat='" + item + "'";
                }
                attrib += ") AND (";
                foreach (var item in checkedListBox2.CheckedItems)
                {// sets subcategories comparissons
                    if (attrib.Contains("subcategories.sub_cat="))
                    {
                        attrib += " OR ";
                    }
                    attrib += "subcategories.sub_cat='" + item + "'";
                }
                attrib += ") ORDER BY business.state, business.city, business.name;";
            }
            //any of the attributes
            else if (comboBox4.SelectedItem == "Any of the Attributes")
            {
                foreach (var item in checkedListBox3.CheckedItems)
                {
                    string temp = item.ToString();
                    if (item.ToString().Contains('_'))
                    {
                        string[] words = item.ToString().Split('_');
                        if (words[0].ToLower() == "musicbackground")
                        {
                            temp = "MusicBackground_Music";
                        }
                        else
                            temp = words[0];//gets attribute name 
                    }
                    if (attrib.Contains("attributeName="))
                    {
                        attrib += " OR ";//any
                    }
                    attrib += "attributeName='" + temp + "'";
                }
                //sets day and time comparissons
                attrib += ") AND isOpen.day = '" + comboBox1.SelectedItem + "' AND isOpen.openTime <= '" + comboBox2.SelectedItem + "' AND (isOpen.closeTime >= '" + comboBox3.SelectedItem + "' OR isOpen.closeTime <= isOpen.openTime) AND (";
                foreach (var item in checkedListBox1.CheckedItems)
                {// sets main categories comparissons
                    if (attrib.Contains("subcategories.main_cat="))
                    {
                        attrib += " OR ";
                    }
                    attrib += "subcategories.main_cat='" + item + "'";
                }
                attrib += ") AND (";
                foreach (var item in checkedListBox2.CheckedItems)
                {// sets subcategories comparissons
                    if (attrib.Contains("subcategories.sub_cat="))
                    {
                        attrib += " OR ";
                    }
                    attrib += "subcategories.sub_cat='" + item + "'";
                }
                attrib += ") ORDER BY business.state, business.city, business.name;";
            }

            MySqlCommand cmd = new MySqlCommand(attrib, conn);
            rdr = cmd.ExecuteReader();
            //set datagridview properties
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            List<Bus> businesses = new List<Bus>();//list of bus to set the datagridview content
            while (rdr.Read())
            {
                Bus tempBusiness = new Bus(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetFloat(3), rdr.GetString(4), rdr.GetString(5));
                businesses.Add(tempBusiness);
            }
            if (businesses.Count == 0)// if no matches
            {
                string caption = "Warning";
                string message = "No Matches";
                DialogResult result;
                result = MessageBox.Show(message, caption);
            }

            dataGridView1.DataSource = businesses;
            dataGridView1.Columns[4].Visible = false; //business_id column isn't visible (no need)

            //width and height
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit(); //quit button
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(server);
            MySqlDataReader rdr = null;
            conn.Open();
            string reviews = "SELECT review.date, review.stars, review.text, User.name, review.votes_useful FROM Review, User, BelongTo WHERE review.review_id = belongTo.review_id AND user.user_id = belongTo.user_id AND belongto.business_id = '" + dataGridView1.SelectedRows[0].Cells["Bus_id"].Value + "';";

            MySqlCommand cmd = new MySqlCommand(reviews, conn);
            rdr = cmd.ExecuteReader();

            List<TempReview> l1 = new List<TempReview>();//list of tempreview to set the datagridview content
            while (rdr.Read())
            {
                TempReview tempBusiness = new TempReview(rdr.GetString(0), rdr.GetFloat(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(4));
                l1.Add(tempBusiness);
            }
            conn.Close();
            //send information to the other form constructor (new window)
            Form1 displayReviews = new Form1(l1, dataGridView1.SelectedRows[0].Cells["Bus_id"].Value.ToString());
            displayReviews.Show();
        }
    }
}