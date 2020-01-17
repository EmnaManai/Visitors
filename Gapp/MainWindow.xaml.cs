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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
namespace Gapp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
   
        public MainWindow()
        {
            InitializeComponent();
         
       
        }
      

    


      

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            if (textBoxEmail.Text.Length == 0)
            {
               
                errormessage.Text = "Enter un Login !";
                textBoxEmail.Focus();

            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                if ((email.Equals("admin") && (password.Equals("testadmin"))))
                {
                    AdminView adminView = new AdminView();
                    adminView.Show();
                    this.Close();                }
                else
                {
                    
                    errormessage.Text = "Login ou mot de passe incorrecte ";
                    textBoxEmail.Focus();
                }
            }
        }

        private void PasswordBox1_TouchEnter(object sender, TouchEventArgs e)
        {
           
        }

        private void PasswordBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return && e.Key != Key.Enter)
                return;
            e.Handled = true;
            NewMethod();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
