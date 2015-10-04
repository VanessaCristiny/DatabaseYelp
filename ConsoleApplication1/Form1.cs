using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using parse_yelp;
using MySql.Data.MySqlClient;

namespace ConsoleApplication1
{

    public partial class Form1 : Form
    {
        public List<TempReview> l1;// holds data to dispose on the datagriedview
        public string business_id { get; set; }//holds selected business_id to comparisson
        public string server = "server=localhost;user=root;database=project;password=lugano;";

        public Form1(List<TempReview> l, string bi)
        {
            l1 = l;
            this.business_id = bi;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ste up buttons and datagridview properties
            button1.Text = "Close";
            label1.Text = "Let's look at the checkins:";
            button2.Text = "Check";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DataSource = l1; // data grid view receives the query results
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            MySqlConnection conn = new MySqlConnection(server);
            MySqlDataReader rdr = null;

            conn.Open();
            // fill the checkin days box
            string days = "SELECT distinct (day) FROM daysOfWeek Order by day; ";
            MySqlCommand cmd = new MySqlCommand(days, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                comboBox1.Items.Add(rdr.GetString(0));
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); //close window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult checkinResult;
            MySqlConnection conn = new MySqlConnection(server);
            MySqlDataReader rdr = null;

            conn.Open();
            // select maximum # of checkins on the selected day
            string tablemaxcheckins = "SELECT MAX(numCheckins) FROM has WHERE business_id= '" + business_id + "' AND dayofweek = '" + comboBox1.SelectedItem + "'; ";
            MySqlCommand cmd2 = new MySqlCommand(tablemaxcheckins, conn);
            rdr = cmd2.ExecuteReader();

            rdr.Read();
            if (!rdr.IsDBNull(0)) // if returned set isn't empty
            {
                string results = "Day of Week: " + comboBox1.SelectedItem + ", Maximum Number of Checkins: " + rdr.GetInt32(0) + '\n';
                conn.Close();

                conn.Open();
                // select minumum # of checkins on the selected day
                string tablemincheckins = "SELECT MIN(numCheckins) FROM has WHERE business_id= '" + business_id + "' AND dayofweek = '" + comboBox1.SelectedItem + "'; ";
                MySqlCommand cmd3 = new MySqlCommand(tablemincheckins, conn);
                rdr = cmd3.ExecuteReader();
                rdr.Read();
                results += "Day of Week: " + comboBox1.SelectedItem + ", Minimum Number of Checkins: " + rdr.GetInt32(0) + '\n';
                conn.Close();

                conn.Open();
                // select average # of checkins on the selected day
                string tableavgcheckins = "SELECT AVG(numCheckins) FROM has WHERE business_id= '" + business_id + "' AND dayofweek = '" + comboBox1.SelectedItem + "'; ";
                MySqlCommand cmd4 = new MySqlCommand(tableavgcheckins, conn);
                rdr = cmd4.ExecuteReader();
                rdr.Read();
                results += "Day of Week: " + comboBox1.SelectedItem + ", Average Number of Checkins: " + rdr.GetInt32(0) + '\n';

                //display results on a dialog box
                checkinResult = MessageBox.Show(results, "Checkins");
            }
            else
            {// if no results are returned, display message
                checkinResult = MessageBox.Show("Sorry! No available information on this business");
            }
        }
    }
}
