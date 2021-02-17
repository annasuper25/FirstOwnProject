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
    /// Логика взаимодействия для Grafik_dost_4.xaml
    /// </summary>
    public partial class Grafik_dost_4 : UserControl
    {
         public DataRowView inputRow = null;

        public DataTable MySource
        {
            get { return (DataTable)GetValue(MySourceProperty); }
            set { SetValue(MySourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySourceProperty =
            DependencyProperty.Register("MySource", typeof(DataTable), typeof(Grafik_dost_4), new PropertyMetadata(null));

        public Grafik_dost_4()
        {
            InitializeComponent();
           
        }

       
       
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate == null) return;
            DateTime data = Convert.ToDateTime (datePicker1.SelectedDate);
            NewUpdate(data);
        }

        private void NewUpdate(DateTime data)
        {
            try
            {
               // if (data == '') return;
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
                string query = String.Format("SELECT cast([DATE] as varchar(max)) as [DATE],[KIND]+'  '+[MODEL] as TS_Name ,[CUSTOMER_NAME] ,[ADDRESS].[NAME] as addr,[JBI_NAME] ,[UNITS] ,[AMOUNT], SumDistance, CAST(SumDistance/[AVERAGE_SPEED] as numeric(8,3))*60 as minuty_v_puti ,[AMOUNT]*[TIME_TO_LOAD] as time_to_load_jbi, [AMOUNT]*[TIME_TO_UNLOAD] as time_to_unload_jbi FROM [sbyt].[dbo].[REQUEST_FOR_SHIPPING]  INNER JOIN CONTRACTS ON [REQUEST_FOR_SHIPPING].CONTRACT_ID=CONTRACTS.CONTRACT_ID  INNER JOIN CUSTOMERS ON CONTRACTS.[CUSTOMER_ID]=CUSTOMERS.[CUSTOMER_ID]  INNER JOIN CATALOG_JBI ON [REQUEST_FOR_SHIPPING].[JBI_ID]=CATALOG_JBI.[JBI_ID]  LEFT OUTER JOIN [ADDRESS] ON CONTRACTS.[DELIVERY_ADDRESS_ID]=[ADDRESS].[ADDRES_ID]  LEFT OUTER JOIN [CATALOG_VEHICLE] ON [CATALOG_VEHICLE].[VEHICLE_CODE]=CATALOG_JBI.[VEHICLE_CODE]  INNER JOIN (SELECT dbo.BLOCKS.BLOCK_NAME AS nameFROM, BLto.BLOCK_NAME AS nameTO, DISTANCE, [BLOCK_ID_FROM], [BLOCK_ID_TO]  ,  [ADDRESS].[ADDRES_ID] as addrID_from, tb2.[ADDRES_ID] as addrID_to, [ADDRESS].[DISTANCE_BLOCK] as dis1, tb2.[DISTANCE_BLOCK] as dis2,[ADDRESS].[DISTANCE_BLOCK]+tb2.[DISTANCE_BLOCK]+DISTANCE as SumDistance FROM dbo.BLOCK_MATRIX  INNER JOIN  dbo.BLOCKS ON dbo.BLOCK_MATRIX.BLOCK_ID_FROM = dbo.BLOCKS.BLOCK_ID INNER JOIN dbo.BLOCKS as BLto ON dbo.BLOCK_MATRIX.BLOCK_ID_TO = BLto.BLOCK_ID inner join [ADDRESS] on [ADDRESS].[BLOCK_ID]=BLOCK_MATRIX.[BLOCK_ID_FROM] inner join [ADDRESS] as tb2 on tb2.[BLOCK_ID]=BLOCK_MATRIX.[BLOCK_ID_TO]) TBL1 ON TBL1.addrID_from=CONTRACTS.[DELIVERY_ADDRESS_ID] AND TBL1.addrID_to = 3 WHERE DATE='{0}' AND [DELIVERY-PICKUP]=1  AND [REQUEST_FOR_SHIPPING].[JBI_ID]=3", data);

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

                for (int ColNo = 0; ColNo <= tempTable.Columns.Count - 1; ColNo++)
                {
                }
            //    String connectionString2 = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";
            //    string query2 = String.Format("SELECT [ADDRESS].[ADDRES_ID] as addrID_from, tb2.[ADDRES_ID] as addrID_to,[ADDRESS].[DISTANCE_BLOCK]+tb2.[DISTANCE_BLOCK]+DISTANCE as SumDistance FROM dbo.BLOCK_MATRIX  INNER JOIN  dbo.BLOCKS ON dbo.BLOCK_MATRIX.BLOCK_ID_FROM = dbo.BLOCKS.BLOCK_ID INNER JOIN dbo.BLOCKS as BLto ON dbo.BLOCK_MATRIX.BLOCK_ID_TO = BLto.BLOCK_ID inner join [ADDRESS] on [ADDRESS].[BLOCK_ID]=BLOCK_MATRIX.[BLOCK_ID_FROM] inner join [ADDRESS] as tb2 on tb2.[BLOCK_ID]=BLOCK_MATRIX.[BLOCK_ID_TO] where ([ADDRESS].[ADDRES_ID]=3 or tb2.[ADDRES_ID] =3) and tb2.[ADDRES_ID]!=[ADDRESS].[ADDRES_ID]");
            //    System.Data.SqlClient.SqlConnection connection2 = new System.Data.SqlClient.SqlConnection(connectionString2);

            //    connection2.Open();

            //    SqlDataAdapter adapter2 = new SqlDataAdapter();

            //    adapter2.SelectCommand = new SqlCommand(query2, connection2);

            //    DataTable tempTable2 = new DataTable("MyTable");

            //    adapter2.Fill(tempTable2);

            //    connection2.Close();

            //    connection2 = null;

            //    foreach (DataRow row1 in tempTable2.Rows)
            //{
            //    foreach (DataRow row2 in tempTable2.Rows)
            //    {
            //        try
            //        {
            //            int minRast = 0;
            //            int Rast1 = Convert.ToInt32(row1["SumDistance"]);
            //            int Rast2 = Convert.ToInt32(row1["SumDistance"]);
            //            if (Rast2 <= Rast1)
            //            {
            //                minRast = Rast2;
            //                tempTable2.Rows.Add(new object[] { Rast1, Rast2 });
            //                return;
            //                //break;
            //            }
            //            else
            //            {
            //                minRast = Rast1;
            //                tempTable2.Rows.Add(new object[] { Rast1, Rast2 });
            //                break;
            //            }
            //           // tempTable2.Rows.Add(new object[] { Rast1, Rast2 });
            //        }
            //        catch
            //        {
            //            int Rast1 = 0;
            //            int Rast2 = 0;
            //        }
            //       // tempTable2.Rows.Add(new object[] { Rast1, Rast2 });
            //    }

            //    }
                             

                _ListView.ItemsSource = tempTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
