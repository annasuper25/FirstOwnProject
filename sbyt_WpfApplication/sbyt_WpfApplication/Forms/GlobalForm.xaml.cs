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
using System.Data.OleDb;
using System.Configuration;
using sbyt_WpfApplication.Spr;
using sbyt_WpfApplication.Input_Docs;
using sbyt_WpfApplication.Output_Docs;
using sbyt_WpfApplication.Forms;


namespace sbyt_WpfApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ZBI_Spr_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();

            ZBI_Sprav zbi_s = new ZBI_Sprav();
            contentCont.Content = zbi_s;
        }

        private void ClearContentControl()
        {
            contentCont.Content = null;
        }

        private void VEHICLE_Spr_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();

            VEHICLE_Sprav veh_s = new VEHICLE_Sprav();
            contentCont.Content = veh_s;
        }

        private void Manufactory_Spr_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();

            Manufactory_Sprav manuf_s = new Manufactory_Sprav();
            contentCont.Content = manuf_s;

        }

        private void Address_Spr_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();

            Address_Sprav addr_s = new Address_Sprav();
            contentCont.Content = addr_s;
        }

        private void Customers_Spr_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();
            Customers_Sprav customers_s = new Customers_Sprav();
            contentCont.Content = customers_s;
        }

        private void Blocks_Spr_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();
            Blocks_Sprav blocks_s = new Blocks_Sprav();
            contentCont.Content = blocks_s;
        }

        private void Block_matrix_Spr_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();
            Block_matrix_Sprav block_matr_s = new Block_matrix_Sprav();
            contentCont.Content = block_matr_s;
        }

        private void Contract_in_doc_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();

            Contract_in_doc contr_in_doc = new Contract_in_doc();
            contentCont.Content = contr_in_doc;
        }

        private void God_Otchet_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();

            God_Otchet_8 god_ot_output = new God_Otchet_8();
            contentCont.Content = god_ot_output;
        }

        private void Mes_plan_otgr_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();

            Mes_plan_otgr_2 mes_plan_output = new Mes_plan_otgr_2();
            contentCont.Content = mes_plan_output;

        }

        private void Otchet_dost_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();
            Otchet_dost_6 otchet_dost_output = new Otchet_dost_6();
            contentCont.Content = otchet_dost_output;
        }

        private void Otchet_post_sklad_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();
            Otchet_post_sklad_3 otchet_post_output = new Otchet_post_sklad_3();
            contentCont.Content = otchet_post_output;
        }

        private void Otchet_samovyvoz_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();
            Otchet_samovyvoz_7 otchet_sam_output = new Otchet_samovyvoz_7();
            contentCont.Content = otchet_sam_output;
        }

        private void God_plan_post_Click(object sender, RoutedEventArgs e)
        {
            //очищаем
            ClearContentControl();
            God_plan_post_1 god_plan_post = new God_plan_post_1();
            contentCont.Content = god_plan_post;
        }

       
        private void Mes_plan_vip_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Mes_plan_vip_in_doc mes_plan_vip = new Mes_plan_vip_in_doc();
            contentCont.Content = mes_plan_vip;
        }

        private void Zayavka_otgr_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Zayavka_otgr zayavka_otg = new Zayavka_otgr();
            contentCont.Content = zayavka_otg;
        }

   
        private void Spravka_Click(object sender, RoutedEventArgs e)
        {
            Spravka spr = new Spravka();
             spr.ShowDialog();
        }

        private void Grafik_dost_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Grafik_dost_4 grafic = new Grafik_dost_4();
            contentCont.Content = grafic;
        }

        private void Ved_dvizh_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Vedomost_dvizh_5 vedomost = new Vedomost_dvizh_5();
            contentCont.Content = vedomost;
        }

        private void SF_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            SF_in_doc sf = new SF_in_doc();
            contentCont.Content = sf;
        }

        private void SF_ACTS_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            SF_ACTS_in_doc acts = new SF_ACTS_in_doc();
            contentCont.Content = acts;
        }

        private void Graf_otgr_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Graf_otgr_in_doc graf = new Graf_otgr_in_doc();
            contentCont.Content = graf;
        }

        private void Ved_ost_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Ved_ost_in_doc ved_ost = new Ved_ost_in_doc();
            contentCont.Content = ved_ost;
        }

        private void Waybill_in_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Waybill_in_In_doc waybill_in = new Waybill_in_In_doc();
            contentCont.Content = waybill_in;
        }

        private void Waybill_out_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Waybill_out_In_doc waybill_out = new Waybill_out_In_doc();
            contentCont.Content = waybill_out;
        }

        private void Vehicle_register_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Vehicle_Register_in_doc veh_reg = new Vehicle_Register_in_doc();
            contentCont.Content = veh_reg;
        }

        private void GlobalForm_click(object sender, RoutedEventArgs e)
        {           
           ClearContentControl();         
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void O_Progr_Click(object sender, RoutedEventArgs e)
        {
            O_Programme o_progr = new O_Programme();
            o_progr.ShowDialog();
        }

        private void Address_matrix_Spr_Click(object sender, RoutedEventArgs e)
        {
            ClearContentControl();
            Address_matrix_Sprav addr_matrix = new Address_matrix_Sprav();
            contentCont.Content = addr_matrix;
        }

       
        
    }
}
