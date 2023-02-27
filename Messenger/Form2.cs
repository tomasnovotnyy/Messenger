using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Messenger
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uziv_jmeno = textBox1.Text;
            string heslo = textBox2.Text;
            string message = "Připojeno";

            SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
            consStringBuilder.UserID = "sa";
            consStringBuilder.Password = "student";
            consStringBuilder.InitialCatalog = "messenger";
            consStringBuilder.DataSource = "PC972";
            consStringBuilder.ConnectTimeout = 30;

            try
            {
                using (SqlConnection connection = new SqlConnection(consStringBuilder.ConnectionString))
                {
                    connection.Open();

                    string query = "select * from Uzivatel where uziv_jmeno = @uziv_jmeno and heslo = @heslo";
                    
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;

                    command.Parameters.AddWithValue("@uziv_jmeno", textBox1.Text.Trim());
                    command.Parameters.AddWithValue("@heslo", textBox2.Text.Trim());

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == true)
                    {
                        MessageBox.Show(message);
                        this.Hide();
                        Form3 f3 = new Form3();
                        f3.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Špatné uživatelské jméno nebo heslo");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
