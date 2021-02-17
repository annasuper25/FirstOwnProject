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
    /// Логика взаимодействия для Mes_plan_vip_edit.xaml
    /// </summary>
    public partial class Mes_plan_vip_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public Mes_plan_vip_edit()
        {
            InitializeComponent();
            UpdateData();
        }

        //edit
        public Mes_plan_vip_edit(DataRowView _row)
        {
            inputRow = _row;

            InitializeComponent();
            UpdateData();
            Fill();
        }

        private void UpdateData()
        {
            string query = String.Format("SELECT * FROM [sbyt].[dbo].[MANUFACTORY]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable); 

            connection.Close();

            comboBox1.DisplayMemberPath = "MANUFACTORY_NAME";
            comboBox1.SelectedValuePath = "MANUFACTORY_ID";
            comboBox1.ItemsSource = tempTable.DefaultView;


            string query1 = String.Format("SELECT * FROM [sbyt].[dbo].[CATALOG_JBI]");
            String connectionString1 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection1 = new System.Data.SqlClient.SqlConnection(connectionString1);

            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = new SqlCommand(query1, connection1);

            DataTable tempTable1 = new DataTable();

            adapter1.Fill(tempTable1); //, "BLOCKS");

            connection1.Close();

            comboBox2.DisplayMemberPath = "JBI_NAME";
            comboBox2.SelectedValuePath = "JBI_ID";
            comboBox2.ItemsSource = tempTable1.DefaultView;
        }

        private void Fill()
        {
            datePicker1.Text = inputRow["DATE"].ToString();
            string cbVal = inputRow["MANUFACTORY_ID"].ToString();
            string cbVal2 = inputRow["JBI_ID"].ToString();
            textBox3.Text = inputRow["AMOUNT"].ToString();
            
            //задаем значение для combobox1
            comboBox1.SelectedValue = cbVal;
            comboBox2.SelectedValue = cbVal2;
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
                string col3 = textBox3.Text;


                 string query = String.Format("UPDATE [sbyt].[dbo].[MONTHLY_PLAN_ISSUE]  SET [DATE] = '{0}' ,[MANUFACTORY_ID] = {1} ,[JBI_ID] = {2} ,[AMOUNT]  ={3} WHERE [DATE] ='{4}' and [MANUFACTORY_ID] = {5}  and [JBI_ID]= {6} ", col1, comBoxValue1, comBoxValue2, col3, inputRow["DATE"].ToString(), inputRow["MANUFACTORY_ID"].ToString(), inputRow["JBI_ID"].ToString());  //id
                //string query = String.Format("UPDATE [sbyt].[dbo].[MONTHLY_PLAN_ISSUE]  SET [AMOUNT]  ={0} WHERE [DATE] ='{1}' ,[MANUFACTORY_ID] = {2} ,[JBI_ID]= {3} ", col3, inputRow["DATE"].ToString(), inputRow["MANUFACTORY_ID"].ToString(), inputRow["JBI_ID"].ToString());  //id
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
                string col3 = textBox3.Text;

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
