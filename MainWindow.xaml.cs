using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace OOP1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConsultPage consultPage = new ConsultPage();
        ManagerPage managerPage = new ManagerPage();

        public MainWindow()
        {
            InitializeComponent();

            DataBase consult = new Consult();
            DataBase manager = new Manager();

            managerPage.DataBaseManagerList.ItemsSource = manager.RefreshDB();
            consultPage.DataBaseConsultList.ItemsSource = consult.RefreshDB();
        }

        private void Consult_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = consultPage;
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = managerPage;
        }
    }
}