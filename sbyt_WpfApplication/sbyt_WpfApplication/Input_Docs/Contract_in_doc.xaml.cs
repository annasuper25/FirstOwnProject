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
    /// Логика взаимодействия для Contract_in_doc.xaml
    /// </summary>
    public partial class Contract_in_doc : UserControl
    {
        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Contract_in_doc), new PropertyMetadata(null));

        public Contract_in_doc()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Contract_in_doc_Loaded);
        }

        void Contract_in_doc_Loaded(object sender, RoutedEventArgs e)
        {
            NewUpdate();
            NewUpdatePril();
        }

        private void NewUpdate()
        {
            try
            {
               
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = "select CONTRACT_ID,CUSTOMER_ID, CUSTOMER_NAME, DELIVERY ,DELIVERY_ADDRESS_ID,NAME, DATE_OPEN,DATE_CLOSE, SUM(summ) as MySum from(SELECT [CONTRACTS].[CONTRACT_ID],[CONTRACTS].[CUSTOMER_ID], CUSTOMER_NAME, cast([DELIVERY-PICKUP] AS INT) DELIVERY ,[DELIVERY_ADDRESS_ID],NAME, cast([DATE_OPEN] as varchar(max)) as DATE_OPEN, cast([DATE_CLOSE] as varchar(max)) as [DATE_CLOSE], AMOUNT * PRICE_PER_UNIT_FACT AS summ  FROM [sbyt].[dbo].[CONTRACTS] left outer join dbo.[CONTRACT-JBI] on CONTRACTS.CONTRACT_ID=[CONTRACT-JBI].CONTRACT_ID INNER JOIN CUSTOMERS ON CUSTOMERS.CUSTOMER_ID=[CONTRACTS].CUSTOMER_ID LEFT OUTER JOIN ADDRESS ON ADDRESS.ADDRES_ID=[CONTRACTS].[DELIVERY_ADDRESS_ID]) as Table1 group by CONTRACT_ID,CUSTOMER_ID, CUSTOMER_NAME, DELIVERY ,DELIVERY_ADDRESS_ID,NAME, DATE_OPEN,DATE_CLOSE";
                             
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

                        DateTime tempTime = Convert.ToDateTime(row["DATE_OPEN"].ToString());
                      
                        // if (row["DELIVERY-PICKUP"].ToString() == 'true')
                        //  bool bc = ;
                        //int f = Convert.ToBoolean(row["DELIVERY-PICKUP"].ToString());

                        row["DATE_OPEN"] = tempTime.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                       // MessageBox.Show(ex.Message);
                    }

                }
                     foreach (DataRow row1 in tempTable.Rows)
                {
                    try
                    {
                                         
                        DateTime tempTime2 = Convert.ToDateTime(row1["DATE_CLOSE"].ToString());
                                    
                        row1["DATE_CLOSE"] = tempTime2.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                      //  MessageBox.Show(ex.Message);
                    }

                }

                _ListView.ItemsSource = tempTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewUpdatePril(int contrID = 0)
        {
            if (contrID == 0) return;
            String connectionString1 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
            string query1 = String.Format("SELECT [CONTRACT-JBI].[JBI_ID],[JBI_NAME],[CONTRACT_ID] ,[AMOUNT] ,[PRICE_PER_UNIT_FACT] FROM [sbyt].[dbo].[CONTRACT-JBI] INNER JOIN CATALOG_JBI ON CATALOG_JBI.[JBI_ID]=[CONTRACT-JBI].[JBI_ID] WHERE [CONTRACT_ID] = {0} ", contrID);

            System.Data.SqlClient.SqlConnection connection1 = new System.Data.SqlClient.SqlConnection(connectionString1);
            connection1.Open();
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            adapter1.SelectCommand = new SqlCommand(query1, connection1);
            DataTable tempTable1 = new DataTable();
            adapter1.Fill(tempTable1);
            connection1.Close();
            connection1 = null;

            _ListView1.ItemsSource = tempTable1.DefaultView;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Contracts_edit Contr_Edit = new Contracts_edit();
            Contr_Edit.ShowDialog();

            NewUpdate();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_ListView.SelectedItem != null)
            {

                Contracts_edit Contr_Edit = new Contracts_edit(_ListView.SelectedItem as DataRowView);
                Contr_Edit.ShowDialog();

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

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[CONTRACTS] WHERE [CONTRACT_ID]= {0}", row["CONTRACT_ID"].ToString());  //id

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

        private void _ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (_ListView.SelectedValue == null) return;

                DataRowView _row = _ListView.SelectedValue as DataRowView;
                int contrID = Convert.ToInt32(_row["CONTRACT_ID"]);
                NewUpdatePril(contrID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            try
            {
                if (_ListView.SelectedValue == null) return;

                DataRowView _row = _ListView.SelectedValue as DataRowView;
                int contrID = Convert.ToInt32(_row["CONTRACT_ID"]);
                
                Contracts_jbi_edit Contr_jbi_Edit = new Contracts_jbi_edit();
                 Contr_jbi_Edit.ShowDialog();

                 NewUpdatePril(contrID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
                       
             
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
            if (_ListView1.SelectedItem != null)
            {
                DataRowView _row = _ListView.SelectedValue as DataRowView;
                int contrID = Convert.ToInt32(_row["CONTRACT_ID"]);
                Contracts_jbi_edit Contr_jbi_Edit = new Contracts_jbi_edit(_ListView1.SelectedItem as DataRowView);
                Contr_jbi_Edit.ShowDialog();

                NewUpdatePril(contrID);
            }
            else MessageBox.Show("Строка не выбрана!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (_ListView1.SelectedItem != null)
            {

                if (MessageBox.Show("Удалить строку?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        DataRowView row = _ListView1.SelectedItem as DataRowView;

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[CONTRACT-JBI] WHERE [CONTRACT_ID]= {0} and JBI_ID = {1}", row["CONTRACT_ID"].ToString(), row["JBI_ID"].ToString());  //id

                        String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                        System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = new SqlCommand(query, connection);

                        DataTable tempTable = new DataTable();
                        adapter.Fill(tempTable);

                        DataRowView _row = _ListView.SelectedValue as DataRowView;
                        int contrID = Convert.ToInt32(_row["CONTRACT_ID"]);
                        NewUpdatePril(contrID);
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
