﻿using System;
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
    /// Логика взаимодействия для Block_Sprav_edit.xaml
    /// </summary>
    public partial class Block_Sprav_edit : Window
    {
        public DataRowView inputRow = null;


        //add
        public Block_Sprav_edit()
        {
           
            InitializeComponent();
        }

        //edit
        public Block_Sprav_edit(DataRowView _row)
        {
            inputRow = _row;
          
            InitializeComponent();
            Fill();
        }

         private void Fill()
        {
            textBox1.Text = inputRow["BLOCK_ID"].ToString();
            textBox2.Text = inputRow["BLOCK_NAME"].ToString();          
        }

         private void button_OK_Click(object sender, RoutedEventArgs e)
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


                 string query = String.Format("UPDATE [sbyt].[dbo].[BLOCKS]   SET [BLOCK_NAME] ='{0}' WHERE [BLOCK_ID]= {1} ", col2, inputRow["BLOCK_ID"].ToString());  //id
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
                 
                 string query = String.Format("INSERT INTO [sbyt].[dbo].[BLOCKS] ([BLOCK_NAME]) VALUES ('{0}')", col2);
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
