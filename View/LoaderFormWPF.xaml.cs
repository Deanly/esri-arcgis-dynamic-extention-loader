using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DynamicDllLoader
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    /// 

    public partial class LoaderFormWPF : UserControl
    {
        public LoaderFormWPF()
        {
            InitializeComponent();
        }
    
        private Injector injector = new Injector();

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("filepath: " + tbx_filepath.Text);

            try
            {
                ltx_classes.Items.Clear();
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Catched");
            }
            switch ( injector.Load(tbx_filepath.Text) )
            {
                case 0:
                    display_label.Content = "Select your class";
                    onLoad();
                    break;
                case -1:
                    display_label.Content = "Not found file..";
                    break;
                case -2:
                    display_label.Content = "Fail load to memory..";
                    break;
            }
        }

        protected void onLoad()
        {

            List<string> list = injector.GetTypeList();

            list.ForEach(delegate(string str)
            {
                ltx_classes.Items.Add(str);
            });
        }

        private void btn_select_class_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = injector.GetTypeList();

            display_label.Content = list[injector.getItemOrder()];
        }
        private void btn_execute_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = injector.GetTypeList();
            display_label.Content = list[injector.getItemOrder()] + '.' + tbx_method.Text;

            switch( injector.Excute(tbx_method.Text) )
            {
                case 1:  display_label.Content = display_label.Content + " running.."; break;
                case -1: display_label.Content = "Miss!! count of parameters"; break;
                case -2: display_label.Content = "Not found method.."; break;
            }

                
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> list = injector.GetTypeList();

            string curItem = ltx_classes.SelectedItem.ToString();
            int index = list.FindIndex(x => x.StartsWith(curItem));

            injector.SelectClass(index);
        }

        private void tbx_filepath_TextChanged(object sender, RoutedEventArgs e)
        {
            //ReadOnlyTB.Text = ReadWriteTB.Text;
        }
        private void tbx_method_TextChanged(object sender, RoutedEventArgs e)
        {
            //ReadOnlyTB.Text = ReadWriteTB.Text;
        }
    }
}
