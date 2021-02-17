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
    /// Логика взаимодействия для Ved_ost_edit.xaml
    /// </summary>
    public partial class Ved_ost_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public Ved_ost_edit()
        {
            InitializeComponent();
            UpdateData();
        }

        //edit
        public Ved_ost_edit(DataRowView _row)
        {
            inputRow = _row;

            InitializeComponent();
            UpdateData();
            Fill();
        }

        private void UpdateData()
        {
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
        }

        private void Fill()
        {
            datePicker1.Text = inputRow["DATE"].ToString();
            string cbVal = inputRow["JBI_ID"].ToString();
            textBox1.Text = inputRow["ACCOUNTING PRICE"].ToString();
            textBox3.Text = inputRow["AMOUNT"].ToString();

            //задаем значение для combobox1
            comboBox1.SelectedValue = cbVal;      
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
                string col2 = textBox1.Text.Replace(',', '.');
                string col3 = textBox3.Text;


                string query = String.Format("UPDATE [sbyt].[dbo].[REGISTER_BALANCES]  SET [DATE] = '{0}',[JBI_ID] ={1} ,[ACCOUNTING PRICE] ={2},[AMOUNT] ={3} WHERE [REGISTER_BALANCES_ID] ={4}", col1, comBoxValue1,col2, col3, inputRow["REGISTER_BALANCES_ID"].ToString());  //id
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
                string col2 = textBox1.Text.Replace(',', '.');
                string col3 = textBox3.Text;

                string query = String.Format("INSERT INTO [sbyt].[dbo].[REGISTER_BALANCES] ([DATE] ,[JBI_ID],[ACCOUNTING PRICE] ,[AMOUNT]) VALUES ('{0}',{1},{2}, {3})", col1, comBoxValue1, col2, col3);
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
