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
    /// Логика взаимодействия для Vehicle_Register_in_doc.xaml
    /// </summary>
    public partial class Vehicle_Register_in_doc : UserControl
    {
        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Vehicle_Register_in_doc), new PropertyMetadata(null));

        public Vehicle_Register_in_doc()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Vehicle_Register_in_doc_Loaded);
        }

        void Vehicle_Register_in_doc_Loaded(object sender, RoutedEventArgs e)
        {
            NewUpdate();
        }

        private void NewUpdate()
        {
            try
            {

                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = "SELECT [VEHICLE_ID] ,[VEHICLE_REGISTER].[VEHICLE_CODE],[KIND], [MODEL],[TECHNICAL_CONDITION] ,CAST([VEHICLE_DATE] as varchar(max)) as [VEHICLE_DATE] FROM [sbyt].[dbo].[VEHICLE_REGISTER] inner join [CATALOG_VEHICLE] on [CATALOG_VEHICLE].[VEHICLE_CODE]= [VEHICLE_REGISTER].[VEHICLE_CODE]";
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

                        DateTime tempTime = Convert.ToDateTime(row["VEHICLE_DATE"].ToString());

                        row["VEHICLE_DATE"] = tempTime.ToShortDateString();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Vehicle_register_edit veh_reg_edit = new Vehicle_register_edit();
            veh_reg_edit.ShowDialog();

            NewUpdate();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_ListView.SelectedItem != null)
            {
                Vehicle_register_edit veh_reg_edit = new Vehicle_register_edit(_ListView.SelectedItem as DataRowView);
                veh_reg_edit.ShowDialog();

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

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[VEHICLE_REGISTER] WHERE [VEHICLE_ID]= {0}", row["VEHICLE_ID"].ToString());  //id

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
