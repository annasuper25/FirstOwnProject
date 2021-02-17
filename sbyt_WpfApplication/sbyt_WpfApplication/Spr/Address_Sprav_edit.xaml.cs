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
    /// Логика взаимодействия для Address_Sprav_edit.xaml
    /// </summary>
    public partial class Address_Sprav_edit : Window
    {
        
        public DataRowView inputRow = null;


        //add
        public Address_Sprav_edit()
        {
           
            InitializeComponent();

            UpdateData();
        }

      


        //edit
        public Address_Sprav_edit( DataRowView _row )
        {
            inputRow = _row;
          
            InitializeComponent();
           
            UpdateData();
            Fill();
        }


        private void UpdateData()
        {
            string query = String.Format("SELECT * FROM [sbyt].[dbo].[BLOCKS]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);
          
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable); //, "BLOCKS");
       
            connection.Close();


            comboBox1.DisplayMemberPath = "BLOCK_NAME";
            comboBox1.SelectedValuePath = "BLOCK_ID";
            comboBox1.ItemsSource = tempTable.DefaultView;

        }

        private void Fill()
        {
           // textBox4.Text = inputRow["ADDRES_ID"].ToString();
            textBox1.Text = inputRow["NAME"].ToString();           
            textBox3.Text = inputRow["DISTANCE_BLOCK"].ToString();


          string cbVal =   inputRow["BLOCK_ID"].ToString();

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
          //  string col4 = textBox4.Text;
            string col1 = textBox1.Text;
            string comBoxValue = comboBox1.SelectedValue.ToString();
            string col3 = textBox3.Text.Replace(',', '.');

            string query = String.Format("UPDATE [sbyt].[dbo].[ADDRESS]   SET [NAME] = '{0}',[BLOCK_ID] = {1} ,[DISTANCE_BLOCK] ={2} WHERE [ADDRES_ID]= {3}", col1, comBoxValue, col3, inputRow["ADDRES_ID"].ToString());  //id
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
            string comBoxValue = comboBox1.SelectedValue.ToString();
            string col3 = textBox3.Text.Replace(',', '.');


            // SelectedValuePath - не задаем
            //DataRowView adres = comboBox1.SelectedValue as DataRowView;
            //if (adres != null)
            //{
            //    string val1 = adres["BLOCK_ID"].ToString();
            //    string val2 = adres["BLOCK_NAME"].ToString();     
            //}


            // SelectedValuePath - задаем
         //   string comBoxValue = comboBox1.SelectedValue.ToString();

            string query = String.Format("INSERT INTO [sbyt].[dbo].[ADDRESS] ([NAME],[BLOCK_ID],[DISTANCE_BLOCK]) VALUES ('{0}', {1}, {2})", col1, comBoxValue, col3);
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
