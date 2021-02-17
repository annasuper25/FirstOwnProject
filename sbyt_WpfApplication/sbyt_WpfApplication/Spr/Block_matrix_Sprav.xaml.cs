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
    /// Логика взаимодействия для Block_matrix_Sprav.xaml
    /// </summary>
    public partial class Block_matrix_Sprav : UserControl
    {
        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Block_matrix_Sprav), new PropertyMetadata(null));

        public Block_matrix_Sprav()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Block_matrix_Sprav_Loaded);
        }

        void Block_matrix_Sprav_Loaded(object sender, RoutedEventArgs e)
        {

            NewUpdate();
        }

        private void NewUpdate()
        {
            try
            {
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = "SELECT dbo.BLOCKS.BLOCK_NAME AS nameFROM, BLto.BLOCK_NAME AS nameTO, DISTANCE, [BLOCK_ID_FROM], [BLOCK_ID_TO]    FROM dbo.BLOCK_MATRIX  INNER JOIN  dbo.BLOCKS ON dbo.BLOCK_MATRIX.BLOCK_ID_FROM = dbo.BLOCKS.BLOCK_ID INNER JOIN dbo.BLOCKS as BLto ON dbo.BLOCK_MATRIX.BLOCK_ID_TO = BLto.BLOCK_ID";

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
            if (_ListView.SelectedItem != null)
            {

                Block_matrix_Sprav_edit Block_matrix_Edit = new Block_matrix_Sprav_edit(_ListView.SelectedItem as DataRowView);
                Block_matrix_Edit.ShowDialog();

                NewUpdate();
            }
            else MessageBox.Show("Строка не выбрана!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Block_matrix_Sprav_edit Block_matrix_Edit = new Block_matrix_Sprav_edit();
            Block_matrix_Edit.ShowDialog();

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

                        string query = String.Format("DELETE FROM [sbyt].[dbo].[BLOCK_MATRIX] WHERE [BLOCK_ID_FROM]= '{0}' and [BLOCK_ID_TO]= '{1}'", row["BLOCK_ID_FROM"].ToString(), row["BLOCK_ID_TO"].ToString());  //id

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
