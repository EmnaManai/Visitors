using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Windows.Threading;

namespace Gapp
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        OleDbConnection con;
        DataTable dt;
        public Home()
        {
            con = new OleDbConnection();

            con.ConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\guestDb.mdb";
            InitializeComponent();
          
        }
        private void WindowActivated(object sender, EventArgs e)
        {
            BindGrid();
        }
        private void BindGrid()
        {
            DateTime today = DateTime.Now.Date;
            OleDbCommand cmd= new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select lastName,firstName,company,arrivalTime, departureTime from guestTable where creationDate = #" + today+"#";
    
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvData.ItemsSource = dt.AsDataView();
            gvData.Columns[0].Width = 100;
            gvData.Columns[0].Header = "Nom";
            gvData.Columns[1].Width = 100;
            gvData.Columns[1].Header = "Prénom";
            gvData.Columns[2].Width = 143;
            gvData.Columns[2].Header = "Societé";
            gvData.Columns[3].Width = 200;
            gvData.Columns[3].Header = "Date d'entrée";
            gvData.Columns[4].Width = 200;
            gvData.Columns[4].Header = "Date de sortie";

            //gvData.Columns[5].Width = 300;



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            creationForm creation = new creationForm();
            creation.Show();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DateTime today = DateTime.Now.Date;
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select lastName,firstName,company,arrivalTime, departureTime from guestTable where lastName like '%" + txtSearch.Text+ "%' and creationDate = #" + today + "#";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvData.ItemsSource = dt.AsDataView();
            gvData.Columns[0].Width = 100;
            gvData.Columns[1].Width = 100;
            gvData.Columns[2].Width = 143;
            gvData.Columns[3].Width = 200;
            gvData.Columns[4].Width = 200;

            //gvData.Columns[5].Width = 300;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try {
            DataRowView row = (DataRowView)gvData.SelectedItems[0];
   
                string rowId = row["lastName"].ToString();
                DateTime departureTime = DateTime.Now;


                OleDbCommand cmd = new OleDbCommand();
                if (con.State != ConnectionState.Open)
                    con.Open();

                cmd.Connection = con; ;
              
                cmd.CommandText = "update guestTable set departureTime=@departureTime where lastName=@lastName";

                cmd.Parameters.AddWithValue("@departureTime", departureTime.ToString("dd/mm/yyyy hh:mm:ss"));
                cmd.Parameters.AddWithValue("@lastName", rowId);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Au revoir :) ");


            }
            catch(Exception ee)
            {
                MessageBox.Show("Vous devez selectionner votre Nom");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            timelbl.Content = DateTime.Now.ToLongTimeString();
            datelbl.Content = DateTime.Now.ToLongDateString();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
