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

namespace sbyt_WpfApplication.Output_Docs
{
    /// <summary>
    /// Логика взаимодействия для Otchet_post_sklad_3.xaml
    /// </summary>
    public partial class Otchet_post_sklad_3 : UserControl
    {
        public DataRowView inputRow = null;

        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Otchet_post_sklad_3), new PropertyMetadata(null));

        public Otchet_post_sklad_3()
        {
            InitializeComponent();
            UpdateData();
            UpdateMonth();
        }

        private void UpdateData()
        {

            string query = String.Format(" SELECT distinct YEAR([DATE]) AS YEARS FROM [sbyt].[dbo].[MONTHLY_PLAN_ISSUE]");
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
            string query = String.Format(" SELECT distinct MONTH([DATE]) AS MONTHS FROM [sbyt].[dbo].[MONTHLY_PLAN_ISSUE] WHERE YEAR([DATE]) ={0} ", year);
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
                string query = String.Format("SELECT year([MONTHLY_PLAN_ISSUE].[DATE]) as god,MONTH([MONTHLY_PLAN_ISSUE].[DATE]) as mes, cast([MONTHLY_PLAN_ISSUE].[DATE]as varchar(max)) as [DATE] ,[MONTHLY_PLAN_ISSUE].[MANUFACTORY_ID] ,[MANUFACTORY_NAME] ,[MONTHLY_PLAN_ISSUE].[JBI_ID] ,jbi_name,units,[MONTHLY_PLAN_ISSUE].[AMOUNT] as plan_kol,Waybill_in.[AMOUNT] as fact_kol,Waybill_in.[date],(Waybill_in.[AMOUNT]-[MONTHLY_PLAN_ISSUE].[AMOUNT]) as fact_minus_plan FROM [sbyt].[dbo].[MONTHLY_PLAN_ISSUE] inner join CATALOG_JBI on CATALOG_JBI.[JBI_ID]=[MONTHLY_PLAN_ISSUE].[JBI_ID] inner join MANUFACTORY on MANUFACTORY.[MANUFACTORY_ID]=[MONTHLY_PLAN_ISSUE].[MANUFACTORY_ID] left outer join Waybill_in on [MONTHLY_PLAN_ISSUE].[JBI_ID]=Waybill_in.[JBI_ID] where Waybill_in.[date]=[MONTHLY_PLAN_ISSUE].[DATE] and year([MONTHLY_PLAN_ISSUE].[DATE])={0} and MONTH([MONTHLY_PLAN_ISSUE].[DATE])={1}", year, month);

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

                        // if (row["DELIVERY-PICKUP"].ToString() == 'true')
                        //  bool bc = ;
                        //int f = Convert.ToBoolean(row["DELIVERY-PICKUP"].ToString());

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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // if (comboBox1.SelectedValue == null) return;
                int year = Convert.ToInt32(comboBox1.SelectedValue);
                // UpdateMonth(year);
                //UpdateMonth(year);

                //if (comboBox2.SelectedValue == null) return;
                int month = Convert.ToInt32(comboBox2.SelectedValue);
                NewUpdate(year, month);
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


    }
}
