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
    /// Логика взаимодействия для Otchet_samovyvoz_7.xaml
    /// </summary>
    public partial class Otchet_samovyvoz_7 : UserControl
    {
        public DataRowView inputRow = null;

        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Otchet_samovyvoz_7), new PropertyMetadata(null));

        public Otchet_samovyvoz_7()
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
            string query = String.Format(" SELECT distinct MONTH([DATE]) AS MONTHS FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING]  INNER JOIN WAYBILL_OUT ON WAYBILL_OUT.JBI_ID=[REQUEST_FOR_SHIPPING].JBI_ID WHERE YEAR([DATE]) ={0} AND MONTH([DATE])=MONTH([FACT_DATE_OF_RELEASE])", year);
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
           
                //string cbVal = inputRow["YEARS"].ToString();

                //задаем значение для combobox1
                //  comboBox2.SelectedValue = cbVal;

                //  string comBoxValue = comboBox2.SelectedValue.ToString();

                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = String.Format("SELECT [REQUEST_FOR_SHIPPING].[REQUEST_FOR_SHIPPING_ID] as id_pl ,[REQUEST_FOR_SHIPPING].[CONTRACT_ID],WAYBILL_OUT.[CONTRACT_ID] as Contract_fact ,[REQUEST_FOR_SHIPPING].[JBI_ID] ,[WAYBILL_OUT].[JBI_ID] as jbi_po_faktu ,cast([DATE] as varchar(max)) as Data_po_planu ,cast([FACT_DATE_OF_RELEASE] as varchar(max)) as [FACT_DATE_OF_RELEASE],ISNULL([REQUEST_FOR_SHIPPING].[AMOUNT],0) AS PLANOVOE ,ISNULL(WAYBILL_OUT.AMOUNT,0) AS FAKTOVOE,ISNULL(WAYBILL_OUT.AMOUNT,0)-ISNULL([REQUEST_FOR_SHIPPING].[AMOUNT],0) as Raznica ,CUSTOMERS.CUSTOMER_NAME ,[JBI_NAME],units FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING] right outer JOIN WAYBILL_OUT ON [REQUEST_FOR_SHIPPING].[REQUEST_FOR_SHIPPING_ID]=WAYBILL_OUT.[REQUEST_FOR_SHIPPING_ID] left outer JOIN CONTRACTS ON WAYBILL_OUT.CONTRACT_ID=CONTRACTS.CONTRACT_ID left outer JOIN CUSTOMERS ON CONTRACTS.[CUSTOMER_ID]=CUSTOMERS.[CUSTOMER_ID] INNER JOIN CATALOG_JBI ON WAYBILL_OUT.[JBI_ID]=CATALOG_JBI.[JBI_ID] WHERE MONTH([FACT_DATE_OF_RELEASE])={0} and YEAR([FACT_DATE_OF_RELEASE])={1} and [DELIVERY-PICKUP]=0 UNION  SELECT [REQUEST_FOR_SHIPPING].[REQUEST_FOR_SHIPPING_ID] ,[REQUEST_FOR_SHIPPING].[CONTRACT_ID] ,WAYBILL_OUT.[CONTRACT_ID] as Contract_fact ,[REQUEST_FOR_SHIPPING].[JBI_ID],[WAYBILL_OUT].[JBI_ID] as jbi_po_faktu ,cast([DATE] as varchar(max)) as Data_po_planu ,cast([FACT_DATE_OF_RELEASE] as varchar(max)) as [FACT_DATE_OF_RELEASE],ISNULL([REQUEST_FOR_SHIPPING].[AMOUNT],0) AS PLANOVOE,ISNULL(WAYBILL_OUT.AMOUNT,0) AS FAKTOVOE,ISNULL(WAYBILL_OUT.AMOUNT,0)-ISNULL([REQUEST_FOR_SHIPPING].[AMOUNT],0) as Raznica ,CUSTOMERS.CUSTOMER_NAME,[JBI_NAME],units FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING] LEFT outer JOIN WAYBILL_OUT ON [REQUEST_FOR_SHIPPING].[REQUEST_FOR_SHIPPING_ID]=WAYBILL_OUT.[REQUEST_FOR_SHIPPING_ID] left outer JOIN CONTRACTS ON [REQUEST_FOR_SHIPPING].CONTRACT_ID=CONTRACTS.CONTRACT_ID left outer JOIN CUSTOMERS ON CONTRACTS.[CUSTOMER_ID]=CUSTOMERS.[CUSTOMER_ID] INNER JOIN CATALOG_JBI ON [REQUEST_FOR_SHIPPING].[JBI_ID]=CATALOG_JBI.[JBI_ID] WHERE MONTH(DATE)={0} AND YEAR(DATE)={1}  and [DELIVERY-PICKUP]=0", month, year);

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

                        DateTime tempTime = Convert.ToDateTime(row["Data_po_planu"].ToString());

                        row["Data_po_planu"] = tempTime.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                       // MessageBox.Show(ex.Message);
                    }

                }

                foreach (DataRow row in tempTable.Rows)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(row["FACT_DATE_OF_RELEASE"].ToString()))
                        {
                            DateTime tempTime = Convert.ToDateTime(row["FACT_DATE_OF_RELEASE"].ToString());
                            row["FACT_DATE_OF_RELEASE"] = tempTime.ToShortDateString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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
