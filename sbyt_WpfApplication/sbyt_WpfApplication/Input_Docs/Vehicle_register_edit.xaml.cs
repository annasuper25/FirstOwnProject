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
    /// Логика взаимодействия для Vehicle_register_edit.xaml
    /// </summary>
    public partial class Vehicle_register_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public Vehicle_register_edit()
        {
            InitializeComponent();
            UpdateData();
           
        }

        //edit
        public Vehicle_register_edit(DataRowView _row)
        {
            inputRow = _row;

            InitializeComponent();
            UpdateData();
           
            Fill();
        }

        private void UpdateData()
        {
            string query = String.Format("SELECT VEHICLE_CODE FROM [sbyt].[dbo].[CATALOG_VEHICLE]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();

            comboBox1.DisplayMemberPath = "VEHICLE_CODE";
            comboBox1.SelectedValuePath = "VEHICLE_CODE";
            comboBox1.ItemsSource = tempTable.DefaultView;
        }

        private void Fill()
        {
            datePicker1.Text = inputRow["VEHICLE_DATE"].ToString();
            string cbVal1 = inputRow["VEHICLE_CODE"].ToString();
            textBox1.Text = inputRow["TECHNICAL_CONDITION"].ToString();
            //задаем значение для combobox1

            comboBox1.SelectedValue = cbVal1;
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
                string col3 = textBox1.Text;

                string query = String.Format("UPDATE [sbyt].[dbo].[VEHICLE_REGISTER]  SET [VEHICLE_CODE] = {0},[TECHNICAL_CONDITION] ={1} ,[VEHICLE_DATE]= '{2}' WHERE [VEHICLE_ID]= {3} ", comBoxValue1, col3, col1, inputRow["VEHICLE_ID"].ToString());  //id
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
                string col3 = textBox1.Text;

                string query = String.Format("INSERT INTO [sbyt].[dbo].[VEHICLE_REGISTER]  ([VEHICLE_CODE],[TECHNICAL_CONDITION],[VEHICLE_DATE]) VALUES ({0},{1},'{2}')", comBoxValue1, col3, col1);
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
