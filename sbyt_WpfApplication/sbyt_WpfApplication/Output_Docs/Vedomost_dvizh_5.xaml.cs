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
    /// Логика взаимодействия для Vedomost_dvizh_5.xaml
    /// </summary>
    public partial class Vedomost_dvizh_5 : UserControl
    {
        public DataRowView inputRow = null;

        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Vedomost_dvizh_5), new PropertyMetadata(null));

        public Vedomost_dvizh_5()
        {
            InitializeComponent();
            UpdateData();
            UpdateMonth();
        }

        private void UpdateData()
        {

            string query = String.Format(" SELECT distinct YEAR([DATE]) AS YEARS FROM [sbyt].[dbo].[REGISTER_BALANCES]");
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
            string query = String.Format("SELECT distinct MONTH([DATE]) AS MONTHS FROM [sbyt].[dbo].[REGISTER_BALANCES]  WHERE YEAR([DATE]) ={0}", year);
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

        private void NewUpdate(int year = 0, int month = 0)
        {
            try
            {
                if (year == 0) return;
                if (month == 0) return;

                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = String.Format("SELECT [REGISTER_BALANCES_ID],cast([DATE] as varchar(max)) as [DATE],[REGISTER_BALANCES].[JBI_ID] ,[CATALOG_JBI].[JBI_NAME] as Name_jbi,[UNITS] ,[AMOUNT] FROM [sbyt].[dbo].[REGISTER_BALANCES]  inner join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID]=[REGISTER_BALANCES].[JBI_ID] WHERE MONTH(DATE)={0} AND YEAR(DATE)={1} and DAY(DATE)=01 ", month, year);

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
                        if (!string.IsNullOrEmpty(row["DATE"].ToString()))
                        {
                            DateTime tempTime = Convert.ToDateTime(row["DATE"].ToString());

                            row["DATE"] = tempTime.ToShortDateString();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }


                String connectionString1 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query1 = String.Format("SELECT [WAYBILL_IN_ID] as ID_NAKL,cast([DATE] as varchar(max)) as [DATE],[WAYBILL_IN].[JBI_ID] ,[CATALOG_JBI].[JBI_NAME] ,[UNITS],[AMOUNT] FROM [sbyt].[dbo].[WAYBILL_IN] inner join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID]=[WAYBILL_IN].[JBI_ID] where MONTH(date)={0} and YEAR([DATE])={1} UNION SELECT [WAYBILL_OUT_ID] as ID_NAKL,cast([FACT_DATE_OF_RELEASE] as varchar(max)) as [DATE],[WAYBILL_OUT].[JBI_ID],[CATALOG_JBI].[JBI_NAME] ,[UNITS] ,-[AMOUNT] FROM [sbyt].[dbo].[WAYBILL_OUT] inner join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID]=[WAYBILL_OUT].[JBI_ID] where MONTH([FACT_DATE_OF_RELEASE])={0} and YEAR([FACT_DATE_OF_RELEASE])={1} ORDER BY DATE,[JBI_NAME]", month, year);

                System.Data.SqlClient.SqlConnection connection1 = new System.Data.SqlClient.SqlConnection(connectionString1);

                connection1.Open();

                SqlDataAdapter adapter1 = new SqlDataAdapter();

                adapter1.SelectCommand = new SqlCommand(query1, connection1);

                DataTable tempTable1 = new DataTable();

                adapter1.Fill(tempTable1);

                connection1.Close();

                connection1 = null;

                foreach (DataRow row in tempTable1.Rows)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(row["DATE"].ToString()))
                        {
                            DateTime tempTime = Convert.ToDateTime(row["DATE"].ToString());

                            row["DATE"] = tempTime.ToShortDateString();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

                String connectionString2 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query2 = String.Format("SELECT JBI_ID, JBI_NAME, UNITS, SUM(AMOUNT) AS Ostatok FROM (SELECT dbo.REGISTER_BALANCES.REGISTER_BALANCES_ID, dbo.REGISTER_BALANCES.DATE, dbo.REGISTER_BALANCES.JBI_ID, dbo.CATALOG_JBI.JBI_NAME, dbo.CATALOG_JBI.UNITS, dbo.REGISTER_BALANCES.AMOUNT FROM dbo.REGISTER_BALANCES INNER JOIN dbo.CATALOG_JBI ON dbo.CATALOG_JBI.JBI_ID = dbo.REGISTER_BALANCES.JBI_ID  WHERE  (MONTH(dbo.REGISTER_BALANCES.DATE)={0} and YEAR(dbo.REGISTER_BALANCES.DATE)={1} and DAY(dbo.REGISTER_BALANCES.DATE) = 1) UNION SELECT dbo.WAYBILL_IN.WAYBILL_IN_ID, dbo.WAYBILL_IN.DATE, dbo.WAYBILL_IN.JBI_ID, CATALOG_JBI_2.JBI_NAME, CATALOG_JBI_2.UNITS, dbo.WAYBILL_IN.AMOUNT FROM dbo.WAYBILL_IN INNER JOIN dbo.CATALOG_JBI AS CATALOG_JBI_2 ON CATALOG_JBI_2.JBI_ID = dbo.WAYBILL_IN.JBI_ID WHERE (MONTH(dbo.WAYBILL_IN.DATE) = {0} and YEAR(dbo.WAYBILL_IN.DATE)={1} ) UNION SELECT dbo.WAYBILL_OUT.WAYBILL_OUT_ID, dbo.WAYBILL_OUT.FACT_DATE_OF_RELEASE, dbo.WAYBILL_OUT.JBI_ID, CATALOG_JBI_1.JBI_NAME, CATALOG_JBI_1.UNITS, - dbo.WAYBILL_OUT.AMOUNT AS Expr1 FROM  dbo.WAYBILL_OUT INNER JOIN  dbo.CATALOG_JBI AS CATALOG_JBI_1 ON CATALOG_JBI_1.JBI_ID = dbo.WAYBILL_OUT.JBI_ID   WHERE     (MONTH(dbo.WAYBILL_OUT.FACT_DATE_OF_RELEASE) = {0} and YEAR (dbo.WAYBILL_OUT.FACT_DATE_OF_RELEASE) = {1})) AS tb1 GROUP BY JBI_ID, JBI_NAME, UNITS", month, year);

                System.Data.SqlClient.SqlConnection connection2 = new System.Data.SqlClient.SqlConnection(connectionString2);

                connection2.Open();

                SqlDataAdapter adapter2 = new SqlDataAdapter();

                adapter2.SelectCommand = new SqlCommand(query2, connection2);

                DataTable tempTable2 = new DataTable();

                adapter2.Fill(tempTable2);

                connection2.Close();

                connection2 = null;
                
                _ListView.ItemsSource = tempTable.DefaultView;
                _ListView1.ItemsSource = tempTable1.DefaultView;
                _ListView2.ItemsSource = tempTable2.DefaultView;

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
