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

namespace sbyt_WpfApplication.Input_Docs
{
    /// <summary>
    /// Логика взаимодействия для SF_ACTS_edit.xaml
    /// </summary>
    public partial class SF_ACTS_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public SF_ACTS_edit()
        {
            InitializeComponent();
            UpdateData();
            
        }

        //edit
        public SF_ACTS_edit(DataRowView _row)
        {
            inputRow = _row;
            InitializeComponent();
            UpdateData();
           
            Fill();
        }

        private void UpdateData()
        {
            string query = String.Format("SELECT * FROM [sbyt].[dbo].[INVOICE]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();

            comboBox1.DisplayMemberPath = "INVOICE_ID";
            comboBox1.SelectedValuePath = "INVOICE_ID";
            comboBox1.ItemsSource = tempTable.DefaultView;
        }

       
        private void Fill()
        {
            datePicker1.Text = inputRow["DATE"].ToString();
            string cbVal1 = inputRow["INVOICE_ID"].ToString();
            
            textBox2.Text = inputRow["PAYMENT_SUM"].ToString();
            //задаем значение для combobox1

            comboBox1.SelectedValue = cbVal1;
           

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
                string col1 = datePicker1.Text;
                string comBoxValue1 = comboBox1.SelectedValue.ToString();
                string col2 = textBox2.Text.Replace(',', '.');


                string query = String.Format("UPDATE [sbyt].[dbo].[INVOICES_PAYMENT_ACT]  SET [INVOICE_ID] = {0} ,[PAYMENT_SUM] = {1} ,[DATE] = '{2}' WHERE [INVOICES_PAYMENT_ACT_ID]= {3} ", comBoxValue1, col2, col1, inputRow["INVOICES_PAYMENT_ACT_ID"].ToString());  //id
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
                string col1 = datePicker1.Text;
                string comBoxValue1 = comboBox1.SelectedValue.ToString();
                string col2 = textBox2.Text.Replace(',', '.');

                string query = String.Format("INSERT INTO [sbyt].[dbo].[INVOICES_PAYMENT_ACT]  ([INVOICE_ID],[PAYMENT_SUM],[DATE])VALUES ({0},{1},'{2}')", comBoxValue1, col2, col1);
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

    }
}
