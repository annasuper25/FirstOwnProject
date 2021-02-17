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
    /// Логика взаимодействия для Zayavka_otgr.xaml
    /// </summary>
    public partial class Zayavka_otgr : UserControl
    {
        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Zayavka_otgr), new PropertyMetadata(null));

        public Zayavka_otgr()
        {
            InitializeComponent();
            NewUpdate();
        }

        private void NewUpdate()
        {
            try
            {
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = "SELECT [REQUEST_FOR_SHIPPING_ID],[REQUEST_FOR_SHIPPING].[CONTRACT_ID] ,[CONTRACTS].[CUSTOMER_ID],[CUSTOMER_NAME],[REQUEST_FOR_SHIPPING].[JBI_ID], [JBI_NAME], units ,[AMOUNT] , cast([DATE] as varchar(max)) as [DATE] FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING] inner join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID]=[REQUEST_FOR_SHIPPING].[JBI_ID]  inner join [CONTRACTS] on [CONTRACTS].[CONTRACT_ID]=[REQUEST_FOR_SHIPPING].[CONTRACT_ID]  inner join [CUSTOMERS] on [CUSTOMERS].[CUSTOMER_ID]=[CONTRACTS].[CUSTOMER_ID]";

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Zayavka_edit Zayavka_Edit = new Zayavka_edit();
            Zayavka_Edit.ShowDialog();

            NewUpdate();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_ListView.SelectedItem != null)
            {

                Zayavka_edit Zayavka_Edit = new Zayavka_edit(_ListView.SelectedItem as DataRowView);
                Zayavka_Edit.ShowDialog();

                NewUpdate();
            }
            else MessageBox.Show("Строка не выбрана!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (_ListView.SelectedItem != null)
            {

                if (MessageBox.Show("Удалить строку?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataRowView row = _ListView.SelectedItem as DataRowView;

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING] WHERE [REQUEST_FOR_SHIPPING_ID] = '{0}'" , row["REQUEST_FOR_SHIPPING_ID"].ToString());  //id

                        String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = new SqlCommand(query, connection);

                        DataTable tempTable = new DataTable();
                        adapter.Fill(tempTable);

                        NewUpdate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else MessageBox.Show("Строка не выбрана!");
        
        }
    }
}
