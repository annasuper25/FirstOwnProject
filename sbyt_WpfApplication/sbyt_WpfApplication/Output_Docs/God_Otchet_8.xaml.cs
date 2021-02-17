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
    /// Логика взаимодействия для God_Otchet_8.xaml
    /// </summary>
    public partial class God_Otchet_8 : UserControl
    {
        public DataRowView inputRow = null;

        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(God_Otchet_8), new PropertyMetadata(null));

        public God_Otchet_8()
        {
            InitializeComponent();

            UpdateData();
           // this.Loaded += new RoutedEventHandler(God_Otchet_8_Loaded);
        }

        private void UpdateData()
        {

            string query = String.Format(" SELECT distinct YEAR([DATE]) AS YEARS FROM [sbyt].[dbo].[INVOICE]");
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

     //   void God_Otchet_8_Loaded(object sender, RoutedEventArgs e)
     //   {
     //       NewUpdate();
     //   }

        private void NewUpdate(int year = 0)
        {
            try
            {
                if (year == 0) return;
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = String.Format("SELECT CUSTOMER_NAME, SUM(SCHET) as AllSchets, SUM(PAYMENT_SUM) as ALLPayments, SUM(raznica) as  AllRaznica FROM (SELECT CUSTOMERS.CUSTOMER_NAME, [PRICE_PER_UNIT_FACT]*INVOICE.AMOUNT as SCHET,ISNULL(INVOICES_PAYMENT_ACT.PAYMENT_SUM,0) as PAYMENT_SUM,ISNULL(PAYMENT_SUM,0)-[PRICE_PER_UNIT_FACT]*INVOICE.AMOUNT  as raznica  FROM [sbyt].[dbo].[CONTRACT-JBI] right outer join INVOICE on([CONTRACT-JBI].CONTRACT_ID=INVOICE.CONTRACT_ID and [CONTRACT-JBI].JBI_ID=INVOICE.JBI_ID) left outer join INVOICES_PAYMENT_ACT on INVOICE.INVOICE_ID=INVOICES_PAYMENT_ACT.INVOICE_ID INNER join CONTRACTS ON (CONTRACTS.CONTRACT_ID=INVOICE.CONTRACT_ID) inner join CUSTOMERS on (CUSTOMERS.CUSTOMER_ID=CONTRACTS.CUSTOMER_ID) where year(INVOICE.[DATE])= {0})as Table2 group by CUSTOMER_NAME", year);

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = new SqlCommand(query, connection);

                DataTable tempTable = new DataTable();

                adapter.Fill(tempTable);

                connection.Close();

                connection = null;

                _ListView.ItemsSource = tempTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.SelectedValue == null) return;
            int year = Convert.ToInt32(comboBox1.SelectedValue);
            NewUpdate(year);
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue == null) return;
                int year = Convert.ToInt32(comboBox1.SelectedValue);
                //  NewUpdate(year);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
