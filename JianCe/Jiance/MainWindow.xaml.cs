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
using System.Reflection;

namespace Jiance
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            MessageBox.Show("11111");
            
#endif
        }

        public List<Biaos> shujuyuan_biaos
        {
            set
            {
                biaos_dataGrid.ItemsSource = value;
            }
            get
            {
                return (List<Biaos>)biaos_dataGrid.ItemsSource;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.F5)
            {
                using (ShujukuDataContext shujuku=new ShujukuDataContext())
                {
                    tabsUI.SelectedIndex = 0;
                    var biaos = shujuku.ExecuteQuery<Biaos>(
                        @"SELECT a.name biaoming, b.rows xintiaoshu FROM sysobjects a WITH(NOLOCK)
                            JOIN sysindexes b WITH(NOLOCK) ON b.id = a.id WHERE a.xtype = 'U ' AND b.indid IN (0, 1) 
                            ORDER By a.name ASC");
                    if (shujuyuan_biaos==null)
                    {
                        shujuyuan_biaos = biaos.ToList();
                    }
                    else
                    {
                        foreach (var biao in biaos)
                        {
                            shujuyuan_biaos.Where(z => z.biaoming.Equals(biao.biaoming)).Single().xintiaoshu = biao.xintiaoshu;
                        }
                    }
                    var ls = shujuyuan_biaos;
                    shujuyuan_biaos = null;
                    shujuyuan_biaos = ls;
                }
            }
        }

        private void biaos_dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var dg = sender as DataGrid;
            var xuandehang = dg.SelectedItem as Biaos;
            if (dg!=null && dg.SelectedIndex!=-1 && xuandehang.bianhua>0)
            {
                using (ShujukuDataContext shujuku = new ShujukuDataContext())
                {
                    Type t = Assembly.Load("Jiance").GetType("Jiance."+xuandehang.biaoming);
                    var ls = shujuku.ExecuteQuery(t, @"select top "+xuandehang.bianhua+" * from " + xuandehang.biaoming);
                    xiangxiUI.ItemsSource = null;
                    xiangxiUI.ItemsSource = ls;
                }
               
            }
        }

    }
}
