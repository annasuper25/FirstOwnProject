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
    /// Логика взаимодействия для SF_in_doc_edit.xaml
    /// </summary>
    public partial class SF_in_doc_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public SF_in_doc_edit()
        {
            InitializeComponent();
            UpdateData();
            UpdateData2();
        }

        //edit
        public SF_in_doc_edit(DataRowView _row)
        {
            inputRow = _row;
            InitializeComponent();
            UpdateData();
            int contr = Convert.ToInt32(_row["CONTRACT_ID"]);
            UpdateData2(contr);
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

            //int contrID = Convert.ToInt32(_row["CONTRACT_ID"]);


            //string query2 = String.Format("SELECT * FROM [sbyt].[dbo].[CONTRACT-JBI]");
            //String connectionString2 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            //System.Data.SqlClient.SqlConnection connection2 = new System.Data.SqlClient.SqlConnection(connectionString2);

            //SqlDataAdapter adapter2 = new SqlDataAdapter();
            //adapter2.SelectCommand = new SqlCommand(query2, connection2);

            //DataTable tempTable2 = new DataTable();

            //adapter2.Fill(tempTable2);

            //connection2.Close();

            //comboBox3.DisplayMemberPath = "JBI_NAME";
            //comboBox3.SelectedValuePath = "JBI_ID";
            //comboBox3.ItemsSource = tempTable2.DefaultView;
        }

        private void UpdateData2(int contr = 0)
        {
            if (contr == 0) return;
            string query2 = String.Format("SELECT [CONTRACT-JBI].[JBI_ID],[JBI_NAME] FROM [sbyt].[dbo].[CONTRACT-JBI] inner join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID] = [CONTRACT-JBI].[JBI_ID] WHERE [CONTRACT_ID]={0} ", contr);
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
            datePicker1.Text = inputRow["DATE"].ToString();          
            string cbVal1 = inputRow["CONTRACT_ID"].ToString();
            string cbVal3 = inputRow["JBI_ID"].ToString();
             textBox1.Text = inputRow["AMOUNT"].ToString();
            //задаем значение для combobox1
                   
            comboBox1.SelectedValue = cbVal1;
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
                string comBoxValue3 = comboBox3.SelectedValue.ToString();
                string col3 = textBox1.Text;

                string query = String.Format("UPDATE [sbyt].[dbo].[INVOICE]  SET [CONTRACT_ID] ={0} ,[JBI_ID]= {1} , [DATE] ='{2}', [AMOUNT] = {3} WHERE [INVOICE_ID]= {4} ",  comBoxValue1,comBoxValue3, col1, col3, inputRow["INVOICE_ID"].ToString());  //id
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
                
                string comBoxValue3 = comboBox3.SelectedValue.ToString();
                string col3 = textBox1.Text;

                string query = String.Format("INSERT INTO [sbyt].[dbo].[INVOICE]  ([CONTRACT_ID] ,[JBI_ID] ,[DATE],[AMOUNT]) VALUES ({0},{1},'{2}', {3} )", comBoxValue1,comBoxValue3, col1, col3);
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

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue == null) return;
                int contr = Convert.ToInt32(comboBox1.SelectedValue);
                UpdateData2(contr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
