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
    /// Логика взаимодействия для Address_matrix_Sprav.xaml
    /// </summary>
    public partial class Address_matrix_Sprav : UserControl
    {

        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Address_matrix_Sprav), new PropertyMetadata(null));

        public Address_matrix_Sprav()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Address_matrix_Sprav_Loaded);
        }

        void Address_matrix_Sprav_Loaded(object sender, RoutedEventArgs e)
        {
            NewUpdate();
        }

        private void NewUpdate()
        {
            try
            {
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = "SELECT dbo.BLOCKS.BLOCK_NAME AS nameFROM, BLto.BLOCK_NAME AS nameTO, DISTANCE, [BLOCK_ID_FROM], [BLOCK_ID_TO] ,  [ADDRESS].[ADDRES_ID] as addrID_from,  [ADDRESS].NAME as Name_from, tb2.[ADDRES_ID] as addrID_to, tb2.[NAME] as Name_to, [ADDRESS].[DISTANCE_BLOCK], tb2.[DISTANCE_BLOCK],[ADDRESS].[DISTANCE_BLOCK]+tb2.[DISTANCE_BLOCK]+DISTANCE as SumDistance FROM dbo.BLOCK_MATRIX  INNER JOIN  dbo.BLOCKS ON dbo.BLOCK_MATRIX.BLOCK_ID_FROM = dbo.BLOCKS.BLOCK_ID INNER JOIN dbo.BLOCKS as BLto ON dbo.BLOCK_MATRIX.BLOCK_ID_TO = BLto.BLOCK_ID inner join [ADDRESS] on [ADDRESS].[BLOCK_ID]=BLOCK_MATRIX.[BLOCK_ID_FROM] inner join [ADDRESS] as tb2 on tb2.[BLOCK_ID]=BLOCK_MATRIX.[BLOCK_ID_TO] WHERE  [ADDRESS].[ADDRES_ID]!=tb2.[ADDRES_ID] UNION SELECT dbo.BLOCKS.BLOCK_NAME AS nameFROM, BLto.BLOCK_NAME AS nameTO, DISTANCE, [BLOCK_ID_FROM], [BLOCK_ID_TO],  [ADDRESS].[ADDRES_ID] as addrID_from,  [ADDRESS].NAME as Name_from, tb2.[ADDRES_ID] as addrID_to, tb2.[NAME] as Name_to, [ADDRESS].[DISTANCE_BLOCK], tb2.[DISTANCE_BLOCK],[ADDRESS].[DISTANCE_BLOCK]-tb2.[DISTANCE_BLOCK]+DISTANCE as SumDistance FROM dbo.BLOCK_MATRIX INNER JOIN  dbo.BLOCKS ON dbo.BLOCK_MATRIX.BLOCK_ID_FROM = dbo.BLOCKS.BLOCK_ID INNER JOIN dbo.BLOCKS as BLto ON dbo.BLOCK_MATRIX.BLOCK_ID_TO = BLto.BLOCK_ID inner join [ADDRESS] on [ADDRESS].[BLOCK_ID]=BLOCK_MATRIX.[BLOCK_ID_FROM] inner join [ADDRESS] as tb2 on tb2.[BLOCK_ID]=BLOCK_MATRIX.[BLOCK_ID_TO] WHERE  [ADDRESS].[ADDRES_ID]=tb2.[ADDRES_ID]";

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
    }
}
