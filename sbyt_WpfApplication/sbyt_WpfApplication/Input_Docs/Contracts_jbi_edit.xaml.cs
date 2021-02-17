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
    /// Логика взаимодействия для Contracts_jbi_edit.xaml
    /// </summary>
    public partial class Contracts_jbi_edit : Window
    {
         public DataRowView inputRow = null;
                   

         //add
        public Contracts_jbi_edit()
        {
            InitializeComponent();

            UpdateData2();
        }

         
         //edit
        public Contracts_jbi_edit(DataRowView _row)
        {
            inputRow = _row;
          
            InitializeComponent();
           
            UpdateData2();
            Fill();
        }

        public void UpdateData2()
        {
           // string query = String.Format("SELECT * FROM [sbyt].[dbo].[CATALOG_JBI] where [CONTRACT_ID]={0} and [JBI_ID] not in  (SELECT [JBI_ID]  FROM [sbyt].[dbo].[CONTRACT-JBI]  where [CONTRACT_ID]={1} )", contrID, contrID);
            string query = String.Format("SELECT * FROM [sbyt].[dbo].[CATALOG_JBI]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable); 

            connection.Close();


            comboBox1.DisplayMemberPath = "JBI_NAME";
            comboBox1.SelectedValuePath = "JBI_ID";
            comboBox1.ItemsSource = tempTable.DefaultView;


            string query1 = String.Format("SELECT * FROM [sbyt].[dbo].[CONTRACTS]");
            String connectionString1 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection1 = new System.Data.SqlClient.SqlConnection(connectionString1);

            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = new SqlCommand(query1, connection1);

            DataTable tempTable1 = new DataTable();

            adapter1.Fill(tempTable1);

            connection1.Close();


            comboBox2.DisplayMemberPath = "CONTRACT_ID";
            comboBox2.SelectedValuePath = "CONTRACT_ID";
            comboBox2.ItemsSource = tempTable1.DefaultView;
        }

        private void Fill()
        {
           // textBox1.Text = inputRow["CONTRACT_ID"].ToString();
            string cbVal2 = inputRow["CONTRACT_ID"].ToString();
            string cbVal = inputRow["JBI_ID"].ToString();
            textBox2.Text = inputRow["AMOUNT"].ToString();
            textBox3.Text = inputRow["PRICE_PER_UNIT_FACT"].ToString();
            
            //задаем значение для combobox1
            comboBox1.SelectedValue = cbVal;
            comboBox2.SelectedValue = cbVal2;
        }

        private void button1_Click_Ok(object sender, RoutedEventArgs e)
        {
            if (inputRow == null)           
                Add();                    
            else
                Editing();              
            //   Contract_in_doc.NewUpdatePril();          
        }

        private void Editing()
        {
            try
            {
              //  string col1 = textBox1.Text;
                string comBoxValue2 = comboBox2.SelectedValue.ToString();
                string col2 = textBox2.Text;
                string comBoxValue = comboBox1.SelectedValue.ToString();
                string col3 = textBox3.Text.Replace(',','.');

                if (string.IsNullOrEmpty(col3))
                    col3 = "NULL";

                if (string.IsNullOrEmpty(col2))
                    col2 = "NULL";

                string query = String.Format("UPDATE [sbyt].[dbo].[CONTRACT-JBI] SET [JBI_ID] = {0},[CONTRACT_ID] = {1} ,[AMOUNT] ={2}, [PRICE_PER_UNIT_FACT] = {3} WHERE [CONTRACT_ID]= {4} and JBI_ID = {5}", comBoxValue, comBoxValue2, col2, col3, inputRow["CONTRACT_ID"].ToString(), inputRow["JBI_ID"].ToString());  //id
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
               // string col1 = textBox1.Text;
                string comBoxValue2 = comboBox2.SelectedValue.ToString();
                string col2 = textBox2.Text;
                string comBoxValue = comboBox1.SelectedValue.ToString();
                string col3 = textBox3.Text.Replace(',', '.');

                if (string.IsNullOrEmpty(col3))
                    col3 = "NULL";

                if (string.IsNullOrEmpty(col2))
                    col2 = "NULL";

                string query = String.Format("INSERT INTO [sbyt].[dbo].[CONTRACT-JBI] ([JBI_ID],[CONTRACT_ID],[AMOUNT],[PRICE_PER_UNIT_FACT]) VALUES ({0}, {1}, {2}, {3})", comBoxValue, comBoxValue2, col2, col3);
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
