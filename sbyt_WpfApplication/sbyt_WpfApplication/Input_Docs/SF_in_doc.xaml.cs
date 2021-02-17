using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.Configuration.Assemblies;

namespace sbyt_WpfApplication.Input_Docs
{
    /// <summary>
    /// Логика взаимодействия для SF_in_doc.xaml
    /// </summary>
    public partial class SF_in_doc : UserControl
    {
        //public static class ConfigurationManager { }
        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(SF_in_doc), new PropertyMetadata(null));

        public SF_in_doc()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(SF_in_doc_Loaded);
        }

        void SF_in_doc_Loaded(object sender, RoutedEventArgs e)
        {
            NewUpdate();
        }

        private void NewUpdate()
        {
            try
            {

                String connectionString = "Data Source=ANUTA; Initial Catalog=sbyt;Integrated Security=True";
              //  String connectionString = System.Configuration.
                   // ConnectionStrings["ConString"].ToString();
                  //  ConfigurationManager.ConnectionStrings["ConString"].ToString();
                string query = "SELECT [INVOICE_ID]  ,[INVOICE].[CONTRACT_ID] ,CONTRACTS.[CUSTOMER_ID] , [CUSTOMER_NAME],[INVOICE].[JBI_ID] ,[JBI_NAME] , UNITS, cast([DATE] as varchar(max)) as [DATE] ,[INVOICE].[AMOUNT] ,[PRICE_PER_UNIT_FACT]  ,[INVOICE].[AMOUNT]*[PRICE_PER_UNIT_FACT] as summa  FROM [sbyt].[dbo].[INVOICE]  inner join [CONTRACT-JBI] on [CONTRACT-JBI].[JBI_ID]=[INVOICE].[JBI_ID] and [CONTRACT-JBI].[CONTRACT_ID] = [INVOICE].[CONTRACT_ID]  left outer join [CATALOG_JBI] on [CATALOG_JBI].[JBI_ID]= [INVOICE].[JBI_ID]  LEFT OUTER JOIN CONTRACTS ON CONTRACTS.[CONTRACT_ID]= [INVOICE].[CONTRACT_ID]  LEFT OUTER JOIN CUSTOMERS ON CUSTOMERS.[CUSTOMER_ID]= CONTRACTS.[CUSTOMER_ID]";

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
            SF_in_doc_edit sf_edit = new SF_in_doc_edit();
            sf_edit.ShowDialog();

            NewUpdate();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_ListView.SelectedItem != null)
            {                
                SF_in_doc_edit sf_edit = new SF_in_doc_edit(_ListView.SelectedItem as DataRowView);
                sf_edit.ShowDialog();
               
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

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[INVOICE] WHERE [INVOICE_ID]= {0}", row["INVOICE_ID"].ToString());  //id

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
