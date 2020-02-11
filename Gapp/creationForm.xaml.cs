using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows;
using WPF_AutoCompleteComboBox;

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

            BindGrid();
        }
        private void BindGrid()
        {
            DateTime today = DateTime.Now;
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from guestTable";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet dataset = new DataSet();

            da.SelectCommand = cmd;

            List<ComboboxProperties.Combobox>items= new List<ComboboxProperties.Combobox>();
            foreach (DataRow dataRow in dt.Rows)
            {
                string id = dataRow[0].ToString();

                string name = dataRow[1].ToString();
                ComboboxProperties.Combobox a = new ComboboxProperties.Combobox();
                a.Name = name;
                a.Id = id;
                items.Add(a);

            }
            acCbx.ItemsSource = items;




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
            cmd.Parameters.AddWithValue("@lastname", acCbx.SelectedItem);
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

        private void AcCbx_CbSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        { 

              DateTime today = DateTime.Now;
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from guestTable where ID=@id ";
            cmd.Parameters.AddWithValue("@id", acCbx.SelectedValue);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet dataset = new DataSet();

            da.SelectCommand = cmd;

            List<ComboboxProperties.Combobox>items= new List<ComboboxProperties.Combobox>();
            foreach (DataRow dataRow in dt.Rows)
            {
            

                string name = dataRow[1].ToString();
                string company = dataRow[3].ToString();

                txtFirstName.Text = name;
                txtCompany.Text = company;
            }
         



 
        }
    }
}
