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

namespace sbyt_WpfApplication.Spr
{
    /// <summary>
    /// Логика взаимодействия для ZBI_Sprav.xaml
    /// </summary>
    public partial class ZBI_Sprav : UserControl
    {


        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(ZBI_Sprav), new PropertyMetadata(null));

        



        public ZBI_Sprav()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ZBI_Sprav_Loaded);   
        }

        void ZBI_Sprav_Loaded(object sender, RoutedEventArgs e)
        {
            NewUpdate();
        }

        private void NewUpdate()
        {
            try
            {
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = "SELECT [JBI_ID] ,[JBI_NAME] ,[UNITS] ,[PRICE_PER_UNIT] ,[LENGTH] ,[WIDTH] ,[HEIGHT] ,[sbyt].[dbo].[CATALOG_JBI].VEHICLE_CODE, [WEIGHT] ,[KIND]+' '+[MODEL] as VEHICLE_NAME ,[TIME_TO_LOAD] ,[TIME_TO_UNLOAD] FROM [sbyt].[dbo].[CATALOG_JBI], CATALOG_VEHICLE where CATALOG_JBI.VEHICLE_CODE=CATALOG_VEHICLE.VEHICLE_CODE";


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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ZBI_Sprav_edit ZBI_S_Edit = new ZBI_Sprav_edit();
            ZBI_S_Edit.ShowDialog();

            NewUpdate();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_ListView.SelectedItem != null)
            {

                ZBI_Sprav_edit ZBI_S_Edit = new ZBI_Sprav_edit(_ListView.SelectedItem as DataRowView);
                ZBI_S_Edit.ShowDialog();

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

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[CATALOG_JBI] WHERE [JBI_ID]= {0}", row["JBI_ID"].ToString());  //id

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
