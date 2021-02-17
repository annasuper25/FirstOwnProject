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
    /// Логика взаимодействия для Zayavka_edit.xaml
    /// </summary>
    public partial class Zayavka_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public Zayavka_edit()
        {
            InitializeComponent();
            UpdateData();
        }

        //edit
        public Zayavka_edit(DataRowView _row)
        {
            inputRow = _row;

            InitializeComponent();

            UpdateData();
            Fill();
        }

        private void UpdateData()
        {
            string query = String.Format("SELECT * FROM [sbyt].[dbo].[CONTRACTS]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();

            comboBox1.DisplayMemberPath = "CONTRACT_ID";
            comboBox1.SelectedValuePath = "CONTRACT_ID";
            comboBox1.ItemsSource = tempTable.DefaultView;


            string query1 = String.Format("SELECT * FROM [sbyt].[dbo].[CUSTOMERS]");
            String connectionString1 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection1 = new System.Data.SqlClient.SqlConnection(connectionString1);

            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = new SqlCommand(query1, connection1);

            DataTable tempTable1 = new DataTable();

            adapter1.Fill(tempTable1);

            connection1.Close();

            comboBox2.DisplayMemberPath = "CUSTOMER_NAME";
            comboBox2.SelectedValuePath = "CUSTOMER_ID";
            comboBox2.ItemsSource = tempTable1.DefaultView;

            string query2 = String.Format("SELECT * FROM [sbyt].[dbo].[CATALOG_JBI]");
            String connectionString2 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection2 = new System.Data.SqlClient.SqlConnection(connectionString2);

            SqlDataAdapter adapter2 = new SqlDataAdapter();
            adapter2.SelectCommand = new SqlCommand(query2, connection2);

            DataTable tempTable2 = new DataTable();

            adapter2.Fill(tempTable2);

            connection2.Close();

            comboBox3.DisplayMemberPath = "JBI_NAME";
            comboBox3.SelectedValuePath = "JBI_ID";
            comboBox3.ItemsSource = tempTable2.DefaultView;
        }

        private void Fill()
        {
            textBox1.Text = inputRow["AMOUNT"].ToString();
            string cbVal = inputRow["CUSTOMER_ID"].ToString();
            string cbVal2 = inputRow["CONTRACT_ID"].ToString();
            string cbVal3 = inputRow["JBI_ID"].ToString();
            datePicker1.Text = inputRow["DATE"].ToString();
           
            //задаем значение для combobox1
            //     comboBox1.SelectedValue = cbVal;
            comboBox1.SelectedValue = cbVal;
            comboBox2.SelectedValue = cbVal2;
            comboBox3.SelectedValue = cbVal3;

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
                string comBoxValue2 = comboBox2.SelectedValue.ToString();
                string comBoxValue3 = comboBox3.SelectedValue.ToString();
                string col3 = textBox1.Text;


                string query = String.Format("UPDATE [sbyt].[dbo].[MONTHLY_PLAN_ISSUE]   SET [DATE] ='{0}' ,[MANUFACTORY_ID] = {1} ,[JBI_ID]= {2} , [AMOUNT] = {3} WHERE [DATE] ='{4}' ,[MANUFACTORY_ID] = {5} ,[JBI_ID]= {6} ", col1, comBoxValue1, comBoxValue2, col3, inputRow["DATE"].ToString(), inputRow["MANUFACTORY_ID"].ToString(), inputRow["JBI_ID"].ToString());  //id
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
                string comBoxValue2 = comboBox2.SelectedValue.ToString();
                string comBoxValue3 = comboBox3.SelectedValue.ToString();
                string col3 = textBox1.Text;

                string query = String.Format("INSERT INTO [sbyt].[dbo].[MONTHLY_PLAN_ISSUE] ([DATE],[MANUFACTORY_ID] ,[JBI_ID],[AMOUNT]) VALUES ('{0}',{1},{2}, {3} )", col1, comBoxValue1, comBoxValue2, col3);
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
