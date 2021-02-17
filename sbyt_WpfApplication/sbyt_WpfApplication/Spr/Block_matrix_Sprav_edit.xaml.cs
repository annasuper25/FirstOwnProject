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
    /// Логика взаимодействия для Block_matrix_Sprav_edit.xaml
    /// </summary>
    public partial class Block_matrix_Sprav_edit : Window
    {
        public DataRowView inputRow = null;


        //add
        public Block_matrix_Sprav_edit()
        {
           
            InitializeComponent();
            UpdateData();
            UpdateData1();
        }

         //edit
        public Block_matrix_Sprav_edit(DataRowView _row)
        {
            inputRow = _row;
          
            InitializeComponent();
            UpdateData();
            UpdateData1();
            Fill();
        }

        private void UpdateData()
        {
            string query = String.Format("SELECT distinct dbo.BLOCKS.BLOCK_NAME AS nameFROM,[BLOCK_ID_FROM] FROM dbo.BLOCK_MATRIX  INNER JOIN  dbo.BLOCKS ON dbo.BLOCK_MATRIX.BLOCK_ID_FROM = dbo.BLOCKS.BLOCK_ID INNER JOIN dbo.BLOCKS as BLto ON dbo.BLOCK_MATRIX.BLOCK_ID_TO = BLto.BLOCK_ID");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox1.DisplayMemberPath = " nameFROM ";
            comboBox1.SelectedValuePath = "BLOCK_ID_FROM";
            comboBox1.ItemsSource = tempTable.DefaultView;

        }

        private void UpdateData1()
        {
            string query = String.Format("SELECT distinct BLto.BLOCK_NAME AS nameTO,[BLOCK_ID_TO] FROM dbo.BLOCK_MATRIX  INNER JOIN  dbo.BLOCKS ON dbo.BLOCK_MATRIX.BLOCK_ID_FROM = dbo.BLOCKS.BLOCK_ID INNER JOIN dbo.BLOCKS as BLto ON dbo.BLOCK_MATRIX.BLOCK_ID_TO = BLto.BLOCK_ID");
            String connectionString = "Data Source=ANUTA;Initial Catalog=sbyt;Integrated Security=True";

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, connection);

            DataTable tempTable = new DataTable();

            adapter.Fill(tempTable);

            connection.Close();


            comboBox2.DisplayMemberPath = " nameTO ";
            comboBox2.SelectedValuePath = "BLOCK_ID_TO";
            comboBox2.ItemsSource = tempTable.DefaultView;
        }

         private void Fill()
        {
            //textBox1.Text = inputRow["nameFROM"].ToString();
            string cbVal = inputRow["BLOCK_ID_FROM"].ToString();
            string cbVal2 = inputRow["BLOCK_ID_TO"].ToString();
          //  textBox2.Text = inputRow["nameTO"].ToString();
            textBox3.Text = inputRow["DISTANCE"].ToString();
           
             //задаем значение для combobox1
            comboBox1.SelectedValue = cbVal;
            comboBox2.SelectedValue = cbVal2;
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
              //  string col1 = textBox1.Text;
              //  string col2 = textBox2.Text;
                string col3 = textBox3.Text;
              //  string textBoxValue = textBox1.SelectedText.ToString();
              //  textBox1

                string query = String.Format("UPDATE [sbyt].[dbo].[BLOCK_MATRIX]   SET [DISTANCE] ={0} WHERE [BLOCK_ID_FROM]= '{1}' and [BLOCK_ID_TO]= '{2}'", col3, inputRow["BLOCK_ID_FROM"].ToString(), inputRow["BLOCK_ID_TO"].ToString());  //id
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
                //string col1 = textBox1.Text;
              //  string textBoxValue = textBox1.SelectedText.ToString();
                string comBoxValue = comboBox1.SelectedValue.ToString();
                string comBoxValue2 = comboBox2.SelectedValue.ToString();
              //  string col2 = textBox2.Text;
                string col3 = textBox3.Text;

                string query = String.Format("INSERT INTO [sbyt].[dbo].[BLOCK_MATRIX]  ([BLOCK_ID_FROM] ,[BLOCK_ID_TO],[DISTANCE]) VALUES ('{0}', '{1}', {2})", comBoxValue, comBoxValue2, col3);
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
