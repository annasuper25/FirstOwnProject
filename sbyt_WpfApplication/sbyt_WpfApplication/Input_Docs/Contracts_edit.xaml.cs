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
    /// Логика взаимодействия для Contracts_edit.xaml
    /// </summary>
    public partial class Contracts_edit : Window
    {
        public DataRowView inputRow = null;


        //add
        public Contracts_edit()
        {
            InitializeComponent();
            UpdateData();
            UpdateData2();
        }

         //edit
        public Contracts_edit(DataRowView _row)
        {
            inputRow = _row;
          
            InitializeComponent();
           
            UpdateData();
            UpdateData2();
            Fill();
        }

        private void UpdateData()
        {
            string query = String.Format("SELECT * FROM [sbyt].[dbo].[CUSTOMERS]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox1.DisplayMemberPath = "CUSTOMER_NAME";
            comboBox1.SelectedValuePath = "CUSTOMER_ID";
            comboBox1.ItemsSource = tempTable.DefaultView;
        }

 private void UpdateData2()
        {
            string query1 = String.Format("SELECT * FROM [sbyt].[dbo].[ADDRESS]");
            String connectionString1 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection1 = new System.Data.SqlClient.SqlConnection(connectionString1);

            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = new SqlCommand(query1, connection1);

            DataTable tempTable1 = new DataTable();

            adapter1.Fill(tempTable1); //, "BLOCKS");

            connection1.Close();


            comboBox2.DisplayMemberPath = "NAME";
            comboBox2.SelectedValuePath = "ADDRES_ID";
            comboBox2.ItemsSource = tempTable1.DefaultView;


        }

         private void Fill()
        {
            textBox1.Text = inputRow["DELIVERY"].ToString();
            datePicker1.Text = inputRow["DATE_OPEN"].ToString();
            datePicker2.Text = inputRow["DATE_CLOSE"].ToString();


            string cbVal = inputRow["CUSTOMER_ID"].ToString();
            string cbVal2 = inputRow["DELIVERY_ADDRESS_ID"].ToString();

            //задаем значение для combobox1
         comboBox1.SelectedValue = cbVal;
          if (!string.IsNullOrEmpty(cbVal2))
                comboBox2.SelectedValue = cbVal2;
                   
        }

        private void button1_Click(object sender, RoutedEventArgs e)
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
                string comBoxValue1 = comboBox1.SelectedValue.ToString();
                string col1 = textBox1.Text;
                string comBoxValue2;
                                
                if (col1 == "0")
                    comBoxValue2 = "NULL";
                else
                    if (comboBox2.SelectedValue == null)
                        comBoxValue2 = "NULL";
                    else
                        comBoxValue2 = comboBox2.SelectedValue.ToString();
                //string col2;
                //if (datePicker1.Text == "") 
                //    col2 = "NULL";
                //else col2 = datePicker1.Text;
                //string col3;
                //if (datePicker2.Text == "") 
                //    col3 = "";
                //else col3 = datePicker2.Text;

                string col2 = datePicker1.Text;
                string col3 = datePicker2.Text;

                string query = String.Format("UPDATE [sbyt].[dbo].[CONTRACTS]   SET [CUSTOMER_ID] = {0},[DELIVERY-PICKUP] = {1} ,[DELIVERY_ADDRESS_ID] ={2}, [DATE_OPEN] = '{3}', [DATE_CLOSE] = '{4}' WHERE [CONTRACT_ID]= {5}", comBoxValue1, col1, comBoxValue2, col2, col3, inputRow["CONTRACT_ID"].ToString());  //id
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
                     string col2 = datePicker1.Text;
                     string comBoxValue1 = comboBox1.SelectedValue.ToString();
                     string comBoxValue2 = comboBox2.SelectedValue.ToString();
                     string col3 = datePicker2.Text;


            // SelectedValuePath - не задаем
            //DataRowView adres = comboBox1.SelectedValue as DataRowView;
           
            // SelectedValuePath - задаем
         //   string comBoxValue = comboBox1.SelectedValue.ToString();

                     string query = String.Format("INSERT INTO [sbyt].[dbo].[CONTRACTS] ([CUSTOMER_ID] ,[DELIVERY-PICKUP],[DELIVERY_ADDRESS_ID] ,[DATE_OPEN] ,[DATE_CLOSE]) VALUES ({0}, {1}, {2}, '{3}', '{4}')", comBoxValue1, col1, comBoxValue2, col2, col3);
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

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Spr.Customers_Sprav_edit Customers_s_Edit = new Spr.Customers_Sprav_edit();
            Customers_s_Edit.ShowDialog();

            UpdateData();
            Fill();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {

            Spr.Address_Sprav_edit Address_Edit = new Spr.Address_Sprav_edit();
            Address_Edit.ShowDialog();

            UpdateData2();
            Fill();
        }


    }
}
