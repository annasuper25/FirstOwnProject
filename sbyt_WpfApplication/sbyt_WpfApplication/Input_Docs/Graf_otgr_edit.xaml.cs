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
    /// Логика взаимодействия для Graf_otgr_edit.xaml
    /// </summary>
    public partial class Graf_otgr_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public Graf_otgr_edit()
        {
            InitializeComponent();
            UpdateData();
            UpdateData2();
            //UpdateData3();
        }

        //edit
        public Graf_otgr_edit(DataRowView _row)
        {
            inputRow = _row;
            InitializeComponent();
            UpdateData();
            UpdateData2();
            //UpdateData3();
            Fill();
        }

        private void UpdateData()
        {
            string query = String.Format(" SELECT distinct [CONTR_ID] from [sbyt].[dbo].[SHIPPING_SCHEDULE]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox1.DisplayMemberPath = "CONTR_ID";
            comboBox1.SelectedValuePath = "CONTR_ID";
            comboBox1.ItemsSource = tempTable.DefaultView;
        }
        private void UpdateData2()
        {
            string query = String.Format("SELECT * FROM [sbyt].[dbo].[CATALOG_JBI]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();

            comboBox2.DisplayMemberPath = "JBI_NAME";
            comboBox2.SelectedValuePath = "JBI_ID";
            comboBox2.ItemsSource = tempTable.DefaultView;
        }
        //private void UpdateData3()
        //{
        //    string query = String.Format("SELECT distinct [SHIPPING_MONTH] FROM [sbyt].[dbo].[SHIPPING_SCHEDULE]");
        //    String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

        //    System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

        //    SqlDataAdapter adapter = new SqlDataAdapter();
        //    adapter.SelectCommand = new SqlCommand(query, connection);

        //    DataTable tempTable = new DataTable();

        //    adapter.Fill(tempTable);

        //    connection.Close();

        //    comboBox3.DisplayMemberPath = "SHIPPING_MONTH";
        //    comboBox3.SelectedValuePath = "SHIPPING_MONTH";
        //    comboBox3.ItemsSource = null;
        //    comboBox3.ItemsSource = tempTable.DefaultView;
        //}

        private void Fill()
        {

            string cbVal = inputRow["CONTR_ID"].ToString();
            string cbVal2 = inputRow["JBI_ID"].ToString();
            textBox3.Text = inputRow["AMOUNT"].ToString();
            string cBVal3 = inputRow["SHIPPING_MONTH"].ToString();
            //string cBVal4 = inputRow["SHIPPING_YEAR"].ToString();

            //задаем значение для combobox1,2
            comboBox1.SelectedValue = cbVal;
            comboBox2.SelectedValue = cbVal2;
            comboBox3.SelectedValue = cBVal3;
            //comboBox4.SelectedValue = cBVal4;
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
                string comBoxValue1 = comboBox1.SelectedValue.ToString();
                string comBoxValue2 = comboBox2.SelectedValue.ToString();
                string comBoxValue3 = comboBox3.SelectionBoxItem.ToString();
                string comBoxValue4 = comboBox4.SelectionBoxItem.ToString();
                string col3 = textBox3.Text;


                string query = String.Format("UPDATE [sbyt].[dbo].[SHIPPING_SCHEDULE]  SET [AMOUNT]={0} ,[SHIPPING_MONTH]={1}, [SHIPPING_YEAR]={2}, [CONTR_ID]={3}, [JBI_ID]={4} WHERE [SHIPPING_SCHEDULE_ID]={5} ", col3, comBoxValue4, comBoxValue3, comBoxValue1, comBoxValue2, inputRow["SHIPPING_SCHEDULE_ID"].ToString());  //id              
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
                string comBoxValue1 = comboBox1.SelectedValue.ToString();
                string comBoxValue2 = comboBox2.SelectedValue.ToString();
                string comBoxValue3 = comboBox3.SelectionBoxItem.ToString();
                string comBoxValue4 = comboBox4.SelectionBoxItem.ToString();
                string col3 = textBox3.Text;

                string query = String.Format("INSERT INTO [sbyt].[dbo].[SHIPPING_SCHEDULE] ([AMOUNT],[SHIPPING_MONTH],[SHIPPING_YEAR],[CONTR_ID] ,[JBI_ID]) VALUES ({0},{1},{2},{3},{4})", col3, comBoxValue4, comBoxValue3, comBoxValue1, comBoxValue2);
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
