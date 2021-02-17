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
    /// Логика взаимодействия для God_plan_post_1.xaml
    /// </summary>
    public partial class God_plan_post_1 : UserControl
    {
        public DataRowView inputRow = null;

        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(God_plan_post_1), new PropertyMetadata(null));

        public God_plan_post_1()
        {
            InitializeComponent();
            UpdateData();
        }

        private void UpdateData()
        {

            string query = String.Format(" SELECT distinct SHIPPING_YEAR FROM [sbyt].[dbo].[SHIPPING_SCHEDULE]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox1.DisplayMemberPath = "SHIPPING_YEAR";
            comboBox1.SelectedValuePath = "SHIPPING_YEAR";
            comboBox1.ItemsSource = tempTable.DefaultView;

        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
           // comboBox1_SelectionChanged();
            if (comboBox1.SelectedValue == null) return;
            int year = Convert.ToInt32(comboBox1.SelectedValue);
            NewUpdate(year);
        }


        private void NewUpdate(int year = 0 )
        {
            try
            {
                if (year == 0) return;
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = String.Format("SELECT [CONTRACT_ANNEX].[JBI_ID] as jbi_id, [JBI_NAME], UNITS, SHIPPING_MONTH , SHIPPING_YEAR , SUM([AMOUNT]) as summa FROM [sbyt].[dbo].[SHIPPING_SCHEDULE] LEFT OUTER join [CONTRACT_ANNEX] on [SHIPPING_SCHEDULE].[CONTRACT_ANNEX_ID]=[CONTRACT_ANNEX].[CONTRACT_ANNEX_ID] LEFT OUTER join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID]=[CONTRACT_ANNEX].[JBI_ID] where SHIPPING_YEAR = {0}  group by SHIPPING_MONTH,[CONTRACT_ANNEX].[JBI_ID], [JBI_NAME], UNITS, SHIPPING_YEAR order BY SHIPPING_MONTH", year);
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
