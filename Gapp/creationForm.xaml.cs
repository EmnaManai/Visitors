using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace Gapp
{
    /// <summary>
    /// Logique d'interaction pour creationForm.xaml
    /// </summary>
    public partial class creationForm : Window
    {
        OleDbConnection con;
        public creationForm()
        {
            InitializeComponent();
            con = new OleDbConnection();

            con.ConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\guestDb.mdb";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DateTime arrivalTime = DateTime.Now;
         

            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
   
            cmd.Connection = con; ;
          
            cmd.CommandText = "insert into guestTable(lastName,firstName,company,arrivalTime,departureTime,creationDate) Values(@firstname,@lastname,@company,@arrivalTime,@departureTime,@creationDate)";
            cmd.Parameters.AddWithValue("@firstname", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@lastname", txtlastName.Text);
            cmd.Parameters.AddWithValue("@company", txtCompany.Text);
            cmd.Parameters.AddWithValue("@arrivalTime", OleDbType.Date).Value = arrivalTime.ToOADate();

            cmd.Parameters.AddWithValue("@departureTime", "");
            cmd.Parameters.Add("@creationDate", OleDbType.Date).Value = arrivalTime.ToShortDateString();
            cmd.ExecuteNonQuery();
            System.Windows.MessageBox.Show("Enregistré");
            this.Close();
          
           
        }

        private void txtlastName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
