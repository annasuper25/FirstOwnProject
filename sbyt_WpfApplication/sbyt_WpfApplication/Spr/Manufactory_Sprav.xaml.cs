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
    /// Логика взаимодействия для Manufactory_Sprav.xaml
    /// </summary>
    public partial class Manufactory_Sprav : UserControl
    {
        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Manufactory_Sprav), new PropertyMetadata(null));



        public Manufactory_Sprav()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Manufactory_Sprav_Loaded);
        }

        void Manufactory_Sprav_Loaded(object sender, RoutedEventArgs e)
        {
            NewUpdate();
                        
        }

        private void NewUpdate()
        {
            try
            {
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = "select * FROM [sbyt].[dbo].[MANUFACTORY]";

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (_ListView.SelectedItem != null)
            {

                Manufactory_Sprav_edit manuf_s_Edit = new Manufactory_Sprav_edit(_ListView.SelectedItem as DataRowView);
                manuf_s_Edit.ShowDialog();

                NewUpdate();
            }
            else MessageBox.Show("Строка не выбрана!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manufactory_Sprav_edit manuf_s_Edit = new Manufactory_Sprav_edit();
            manuf_s_Edit.ShowDialog();

            NewUpdate();
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

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[MANUFACTORY] WHERE [MANUFACTORY_ID]= {0} ", row["MANUFACTORY_ID"].ToString());  //id

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
