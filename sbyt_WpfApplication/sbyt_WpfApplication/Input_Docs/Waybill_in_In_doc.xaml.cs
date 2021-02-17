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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace sbyt_WpfApplication.Input_Docs
{
    /// <summary>
    /// Логика взаимодействия для Waybill_in_In_doc.xaml
    /// </summary>
    public partial class Waybill_in_In_doc : UserControl
    {
        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Waybill_in_In_doc), new PropertyMetadata(null));


        public Waybill_in_In_doc()
        {
            InitializeComponent();
            UpdateData();
            UpdateMonth();
        }

        private void UpdateData()
        {

            string query = String.Format(" SELECT distinct YEAR([DATE]) AS YEARS FROM [sbyt].[dbo].[WAYBILL_IN]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox1.DisplayMemberPath = "YEARS";
            comboBox1.SelectedValuePath = "YEARS";
            comboBox1.ItemsSource = tempTable.DefaultView;

        }

        private void UpdateMonth(int year = 0)
        {
            if (year == 0) return;
            string query = String.Format(" SELECT distinct MONTH([DATE]) AS MONTHS FROM [sbyt].[dbo].[WAYBILL_IN] WHERE YEAR([DATE]) ={0} ", year);
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox2.DisplayMemberPath = "MONTHS";
            comboBox2.SelectedValuePath = "MONTHS";
            comboBox2.ItemsSource = tempTable.DefaultView;
        }

        private void NewUpdate(int year = 0, int month = 0)
        {
            try
            {
                if (year == 0) return;
                if (month == 0) return;

                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = String.Format("SELECT [WAYBILL_IN_ID],[WAYBILL_IN].[MANUFACTORY_ID] ,[MANUFACTORY_NAME],cast([DATE] as varchar(max)) as [DATE],[WAYBILL_IN].[JBI_ID],[JBI_NAME] ,[UNITS],[AMOUNT], SUMMAAMOUNT FROM [sbyt].[dbo].[WAYBILL_IN]  inner join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID]= [WAYBILL_IN].[JBI_ID]  inner join [MANUFACTORY] on [MANUFACTORY].[MANUFACTORY_ID] = [WAYBILL_IN].[MANUFACTORY_ID] left outer join ( select [WAYBILL_IN].[JBI_ID], SUM(AMOUNT) AS SUMMAAMOUNT from [WAYBILL_IN] WHERE MONTH(DATE)={0} AND YEAR(DATE)={1} group by [WAYBILL_IN].[JBI_ID] ) as table1 on table1.[JBI_ID]=[WAYBILL_IN].[JBI_ID] WHERE MONTH(DATE)={0} AND YEAR(DATE)={1} ", month, year);

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = new SqlCommand(query, connection);

                DataTable tempTable = new DataTable();

                adapter.Fill(tempTable);

                connection.Close();

                connection = null;

                foreach (DataRow row in tempTable.Rows)
                {
                    try
                    {

                        DateTime tempTime = Convert.ToDateTime(row["DATE"].ToString());
                        row["DATE"] = tempTime.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                    }

                }


                _ListView.ItemsSource = tempTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue == null) return;
                int year = Convert.ToInt32(comboBox1.SelectedValue);
                UpdateMonth(year);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBox2.SelectedValue == null) return;
                int month = Convert.ToInt32(comboBox2.SelectedValue);
                //   UpdateMonth(year);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int year = Convert.ToInt32(comboBox1.SelectedValue);
                int month = Convert.ToInt32(comboBox2.SelectedValue);
                NewUpdate(year, month);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
