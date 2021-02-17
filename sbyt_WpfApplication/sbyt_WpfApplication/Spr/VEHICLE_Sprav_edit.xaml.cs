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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace sbyt_WpfApplication.Spr
{
    /// <summary>
    /// Логика взаимодействия для VEHICLE_Sprav_edit.xaml
    /// </summary>
    public partial class VEHICLE_Sprav_edit : Window
    {
        public DataRowView inputRow = null;

        //add
        public VEHICLE_Sprav_edit()
        {
            InitializeComponent();

        }

         //edit
        public VEHICLE_Sprav_edit(DataRowView _row)
        {
            inputRow = _row;
          
            InitializeComponent();
           
           // UpdateData();
            Fill();
        }

        private void Fill()
        {
            textBox1.Text = inputRow["KIND"].ToString();
            textBox2.Text = inputRow["MODEL"].ToString();
            textBox3.Text = inputRow["CARRYING_CAPACITY"].ToString();
            textBox4.Text = inputRow["PLATFORM_LENGTH"].ToString();
            textBox5.Text = inputRow["PLATFORM_WIDTH"].ToString();
            textBox6.Text = inputRow["HEIGHT_RACK"].ToString();
            textBox7.Text = inputRow["FUEL_CONSUMPTION"].ToString();
            textBox8.Text = inputRow["AVERAGE_SPEED"].ToString();


            //string cbVal =   inputRow["BLOCK_ID"].ToString();

            //задаем значение для combobox1
            //     comboBox1.SelectedValue = cbVal;

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (inputRow == null)
                Add();
            else
                Editing();
        }


        private void Editing()
        {
            try
            {
                string col1 = textBox1.Text;
                string col2 = textBox2.Text;
                string col3 = textBox3.Text.Replace(',', '.');
                //string col4;
                //if (textBox4.Text.Replace(',', '.') == "")
                //    col4= "NULL";
                //else
                //     col4 = textBox4.Text.Replace(',', '.');
                string col4 = textBox4.Text.Replace(',', '.');
                string col5 = textBox5.Text.Replace(',', '.');
                string col6 = textBox6.Text.Replace(',', '.');
                string col7 = textBox7.Text.Replace(',', '.');
                string col8 = textBox8.Text.Replace(',', '.');

                string query = String.Format("UPDATE [sbyt].[dbo].[CATALOG_VEHICLE] SET [KIND] = '{0}',[MODEL] = '{1}',[CARRYING_CAPACITY] = {2},[PLATFORM_LENGTH] = {3},[PLATFORM_WIDTH] = {4},[HEIGHT_RACK] ={5} ,[FUEL_CONSUMPTION] = {6},[AVERAGE_SPEED] ={7} WHERE [VEHICLE_CODE]= {8}", col1, col2, col3, col4, col5, col6, col7, col8, inputRow["VEHICLE_CODE"].ToString());  //id
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, connection);

                DataTable tempTable = new DataTable();
                adapter.Fill(tempTable);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Add()
        {
            try
            {
                string col1 = textBox1.Text;
                string col2 = textBox2.Text;
                string col3 = textBox3.Text.Replace(',', '.');
                string col4 = textBox4.Text.Replace(',', '.');
                string col5 = textBox5.Text.Replace(',', '.');
                string col6 = textBox6.Text.Replace(',', '.');
                string col7 = textBox7.Text.Replace(',', '.');
                string col8 = textBox8.Text.Replace(',', '.');

                string query = String.Format("INSERT INTO [sbyt].[dbo].[CATALOG_VEHICLE] ([KIND],[MODEL],[CARRYING_CAPACITY],[PLATFORM_LENGTH],[PLATFORM_WIDTH] ,[HEIGHT_RACK] ,[FUEL_CONSUMPTION] ,[AVERAGE_SPEED]) VALUES ('{0}', '{1}', {2}, {3}, {4}, {5}, {6}, {7})", col1, col2, col3, col4, col5, col6, col7, col8);
                String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

                System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, connection);

                DataTable tempTable = new DataTable();
                adapter.Fill(tempTable);
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



    }
}
