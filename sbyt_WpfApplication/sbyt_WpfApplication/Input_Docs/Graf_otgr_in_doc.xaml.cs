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
    /// Логика взаимодействия для Graf_otgr_in_doc.xaml
    /// </summary>
    public partial class Graf_otgr_in_doc : UserControl
    {
        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Graf_otgr_in_doc), new PropertyMetadata(null));


        public Graf_otgr_in_doc()
        {
            InitializeComponent();
            UpdateContract();
        }

        private void UpdateContract()
        {
            string query = String.Format(" SELECT distinct [CONTR_ID] from [sbyt].[dbo].[SHIPPING_SCHEDULE]");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox1.DisplayMemberPath = "CONTR_ID";
            comboBox1.SelectedValuePath = "CONTR_ID";
            comboBox1.ItemsSource = tempTable.DefaultView;

        }

        private void NewUpdate(int contract = 0)
        {
            try
            {

                if (contract == 0) return;

                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = String.Format("SELECT [SHIPPING_SCHEDULE_ID],[SHIPPING_SCHEDULE].CONTR_ID, [CONTRACTS].[CUSTOMER_ID], [CUSTOMER_NAME], [SHIPPING_SCHEDULE].JBI_ID, [JBI_NAME], UNITS, [AMOUNT],[SHIPPING_MONTH] ,[SHIPPING_YEAR] FROM [sbyt].[dbo].[SHIPPING_SCHEDULE] left outer join [CONTRACTS] on [CONTRACTS].CONTRACT_ID= [SHIPPING_SCHEDULE].CONTR_ID inner join [CUSTOMERS] on [CUSTOMERS].[CUSTOMER_ID]= [CONTRACTS].[CUSTOMER_ID] inner join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID] = [SHIPPING_SCHEDULE].[JBI_ID] WHERE [SHIPPING_SCHEDULE].[CONTR_ID] ={0} order by [SHIPPING_YEAR],[SHIPPING_MONTH] ", contract);

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

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue == null) return;
                int contract = Convert.ToInt32(comboBox1.SelectedValue);
                //UpdateMonth(contract);
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

                int contract = Convert.ToInt32(comboBox1.SelectedValue);                
                NewUpdate(contract);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Graf_otgr_edit Graf_otgr_Edit = new Graf_otgr_edit();
            Graf_otgr_Edit.ShowDialog();
            int contract = Convert.ToInt32(comboBox1.SelectedValue);

            NewUpdate(contract);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_ListView.SelectedItem != null)
            {

                Graf_otgr_edit Graf_otgr_Edit = new Graf_otgr_edit(_ListView.SelectedItem as DataRowView);
                Graf_otgr_Edit.ShowDialog();

                int contract = Convert.ToInt32(comboBox1.SelectedValue);
                NewUpdate(contract);
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

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[SHIPPING_SCHEDULE] WHERE [SHIPPING_SCHEDULE_ID]= '{0}'", row["SHIPPING_SCHEDULE_ID"].ToString());  //id

                        String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = new SqlCommand(query, connection);

                        DataTable tempTable = new DataTable();
                        adapter.Fill(tempTable);

                        int contract = Convert.ToInt32(comboBox1.SelectedValue);
                        NewUpdate(contract);
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
