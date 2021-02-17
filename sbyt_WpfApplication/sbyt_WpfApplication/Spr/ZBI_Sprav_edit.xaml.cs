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
    /// Логика взаимодействия для ZBI_Sprav_edit.xaml
    /// </summary>
    public partial class ZBI_Sprav_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public ZBI_Sprav_edit()
        {
            InitializeComponent();
            UpdateData();
        }

        //edit
        public ZBI_Sprav_edit(DataRowView _row)
        {
            inputRow = _row;
          
            InitializeComponent();

            UpdateData();
            Fill();
        }


        private void UpdateData()
        {
            string query = String.Format("SELECT [VEHICLE_CODE] ,[KIND],[MODEL] ,[KIND]+' '+[MODEL] AS VEHICLE_NAME  FROM [sbyt].[dbo].[CATALOG_VEHICLE]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox1.DisplayMemberPath = " VEHICLE_NAME ";
            comboBox1.SelectedValuePath = "VEHICLE_CODE";
            comboBox1.ItemsSource = tempTable.DefaultView;

        }


        private void Fill()
        {

            textBox1.Text = inputRow["JBI_NAME"].ToString();
            textBox2.Text = inputRow["UNITS"].ToString();
            textBox3.Text = inputRow["PRICE_PER_UNIT"].ToString();
            textBox4.Text = inputRow["LENGTH"].ToString();
            textBox5.Text = inputRow["WIDTH"].ToString();
            textBox6.Text = inputRow["HEIGHT"].ToString();
            textBox7.Text = inputRow["WEIGHT"].ToString();
            string cbVal = inputRow["VEHICLE_CODE"].ToString();
            textBox9.Text = inputRow["TIME_TO_LOAD"].ToString();
            textBox10.Text = inputRow["TIME_TO_UNLOAD"].ToString();


            //задаем значение для combobox1
             comboBox1.SelectedValue = cbVal;
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
               
                string col1 = textBox1.Text;
                string col2 = textBox2.Text;
                string col3 = textBox3.Text.Replace(',', '.');
                string col4 = textBox4.Text;
                string col5 = textBox5.Text;
                string col6 = textBox6.Text;
                string col7 = textBox7.Text;
                string comBoxValue = comboBox1.SelectedValue.ToString();
                string col9 = textBox9.Text;
                string col10 = textBox10.Text;

                if (string.IsNullOrEmpty(col3))
                    col3 = "NULL";
                if (string.IsNullOrEmpty(col9))
                    col9 = "NULL";
                if (string.IsNullOrEmpty(col10))
                    col10 = "NULL";
                if (string.IsNullOrEmpty(col4))
                    col4 = "NULL";
                if (string.IsNullOrEmpty(col5))
                    col5 = "NULL";
                if (string.IsNullOrEmpty(col6))
                    col6 = "NULL";
               


                string query = String.Format("UPDATE [sbyt].[dbo].[CATALOG_JBI]  SET [JBI_NAME] = '{0}' ,[UNITS] = '{1}' ,[PRICE_PER_UNIT] = {2} ,[LENGTH] = {3} ,[WIDTH] = {4} ,[HEIGHT] = {5} ,[WEIGHT] = {6} ,[VEHICLE_CODE] = {7}, [TIME_TO_LOAD] = {8} ,[TIME_TO_UNLOAD] = {9}  WHERE [JBI_ID]= {10}", col1, col2, col3, col4, col5, col6, col7, comBoxValue, col9, col10, inputRow["JBI_ID"].ToString());  //id
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
                string col3 = textBox3.Text.Replace(',', '.');
                string col4 = textBox4.Text;
                string col5 = textBox5.Text;
                string col6 = textBox6.Text;
                string col7 = textBox7.Text;
                string comBoxValue = comboBox1.SelectedValue.ToString();
                string col9 = textBox9.Text;
                string col10 = textBox10.Text;

                if (string.IsNullOrEmpty(col3))
                    col3 = "NULL";
                if (string.IsNullOrEmpty(col9))
                    col9 = "NULL";
                if (string.IsNullOrEmpty(col10))
                    col10 = "NULL";
                if (string.IsNullOrEmpty(col4))
                    col4 = "NULL";
                if (string.IsNullOrEmpty(col5))
                    col5 = "NULL";
                if (string.IsNullOrEmpty(col6))
                    col6 = "NULL";
                             
                string query = String.Format("INSERT INTO [sbyt].[dbo].[CATALOG_JBI]  ([JBI_NAME] ,[UNITS] ,[PRICE_PER_UNIT] ,[LENGTH],[WIDTH],[HEIGHT] ,[WEIGHT] ,[VEHICLE_CODE] ,[TIME_TO_LOAD] ,[TIME_TO_UNLOAD]) VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})", col1, col2, col3, col4, col5, col6, col7,comBoxValue, col9, col10);
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
            VEHICLE_Sprav_edit Veh_Edit = new VEHICLE_Sprav_edit();
            Veh_Edit.ShowDialog();
            UpdateData();
        
        }


    }
}
