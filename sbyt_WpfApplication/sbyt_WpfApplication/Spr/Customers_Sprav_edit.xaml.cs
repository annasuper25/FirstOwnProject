using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace sbyt_WpfApplication.Spr
{
    /// <summary>
    /// Логика взаимодействия для Customers_Sprav_edit.xaml
    /// </summary>
    public partial class Customers_Sprav_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public Customers_Sprav_edit()
        {
            InitializeComponent();
        }

         //edit
        public Customers_Sprav_edit(DataRowView _row)
        {
            inputRow = _row;
          
            InitializeComponent();
            Fill();
        }

         private void Fill()
        {
            textBox3.Text = inputRow["TEL_FAX"].ToString();
            textBox2.Text = inputRow["CUSTOMER_NAME"].ToString();
            textBox1.Text = inputRow["ACCOUNT_NUMBER"].ToString();
        }

        private void button_OK_Click(object sender, RoutedEventArgs e)
        {
            if (inputRow == null)
                Add();
            else
                Editing();
        }


        private void Editing()
        {
            try
            {
                string col1 = textBox1.Text;
                string col2 = textBox2.Text;
                string col3 = textBox3.Text;


                string query = String.Format("UPDATE [sbyt].[dbo].[CUSTOMERS]   SET [CUSTOMER_NAME] ='{0}' ,[TEL_FAX] = '{2}' ,[ACCOUNT_NUMBER] = '{1}' WHERE [CUSTOMER_ID]= {3} ", col2, col1, col3, inputRow["CUSTOMER_ID"].ToString());  //id
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, connection);

                DataTable tempTable = new DataTable();
                adapter.Fill(tempTable);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Add()
        {
            try
            {
                string col1 = textBox1.Text;
                string col2 = textBox2.Text;
                string col3 = textBox3.Text;

                string query = String.Format("INSERT INTO [sbyt].[dbo].[CUSTOMERS] ([CUSTOMER_NAME] ,[ACCOUNT_NUMBER],[TEL_FAX]) VALUES ('{0}','{1}','{2}' )", col2, col1, col3);
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, connection);

                DataTable tempTable = new DataTable();
                adapter.Fill(tempTable);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


    }
}
