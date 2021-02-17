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
    /// Логика взаимодействия для Mes_plan_otgr_in_doc.xaml
    /// </summary>
    public partial class Mes_plan_otgr_2 : UserControl
    {
        public DataRowView inputRow = null;

        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Mes_plan_otgr_2), new PropertyMetadata(null));

        public Mes_plan_otgr_2()
        {
            InitializeComponent();
            UpdateData();
            UpdateMonth();
        }

        private void UpdateData()
        {

            string query = String.Format(" SELECT distinct YEAR([DATE]) AS YEARS FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING]");
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
            string query = String.Format(" SELECT distinct MONTH([DATE]) AS MONTHS FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING] WHERE YEAR([DATE]) ={0} ",year);
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

        private void NewUpdate(int year =0, int month = 0)
        {
            try
            {
                if (year == 0) return;
                if (month == 0) return;
             
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = String.Format("SELECT cast([DATE] as varchar(max)) as [DATE],[CUSTOMER_NAME], cast([DELIVERY-PICKUP] AS INT) [DELIVERY-PICKUP],[ADDRESS].[NAME],[JBI_NAME] ,[UNITS],SUM ([AMOUNT]) as AMOUNT FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING] INNER JOIN CONTRACTS ON [REQUEST_FOR_SHIPPING].CONTRACT_ID=CONTRACTS.CONTRACT_ID INNER JOIN CUSTOMERS ON CONTRACTS.[CUSTOMER_ID]=CUSTOMERS.[CUSTOMER_ID] INNER JOIN CATALOG_JBI ON [REQUEST_FOR_SHIPPING].[JBI_ID]=CATALOG_JBI.[JBI_ID] LEFT OUTER JOIN [ADDRESS] ON CONTRACTS.[DELIVERY_ADDRESS_ID]=[ADDRESS].[ADDRES_ID] WHERE MONTH(DATE)={0} AND YEAR(DATE)={1} GROUP BY [DATE],[CUSTOMER_NAME],[DELIVERY-PICKUP],[ADDRESS].[NAME],[JBI_NAME] ,[UNITS] ORDER BY DATE", month, year);

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
