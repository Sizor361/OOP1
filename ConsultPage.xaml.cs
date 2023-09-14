using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
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
    /// Логика взаимодействия для ConsultPage.xaml
    /// </summary>
    public partial class ConsultPage : Page
    {
        ObservableCollection<Consult> consultsOrders;

        List<Consult> oldConsultOrders = new List<Consult>();

        Consult consult = new Consult();

        public ConsultPage()
        {
            InitializeComponent();

            RefreshConsult();

            FillOldConsultOrders();

            DataContext = this;
        }

        public void RefreshConsult()
        {
            consultsOrders = consult.RefreshDB();

            DataBaseConsultList.ItemsSource = consultsOrders;
        }

        private void FillOldConsultOrders()
        {

            if (oldConsultOrders != null)
            {
                oldConsultOrders.Clear();
            }

            for (int i = 0; i < consultsOrders.Count; i++)
            {
                oldConsultOrders.Add(consultsOrders[i]);
            }

        }

        /// <summary>
        /// Проверяется запись телефона на корректность и если всё ок - то будет запись в БД
        /// </summary>
        private void CheckAndWrite()
        {
            bool isOkay = true;

            for (int i = 0; i < consultsOrders.Count; i++)
            {
                bool space = false;

                char[] array = consultsOrders[i].Telephone.ToCharArray();

                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j] == ' ')
                    {
                        space = true;
                        break;
                    }
                }

                if (consultsOrders[i].Telephone == string.Empty || space)
                {
                    isOkay = false;
                    break;
                }
            }

            if (isOkay)
            {
                WriteChanges();
                FillOldConsultOrders();
                consult.Rewrite(consultsOrders);
                RefreshConsult();
            }
        }

        private void ChangePhone(object sender, RoutedEventArgs e)
        {
            CheckAndWrite();

        }

        private void WriteChanges()
        {

            for (int i = 0; i < oldConsultOrders.Count; i++)
            {
                if (consultsOrders[i].Telephone != oldConsultOrders[i].Telephone)
                {
                    WriteNewData(i);
                    consultsOrders[i].WhichDataChange = $"Телефон c {oldConsultOrders[i].Telephone} на {consultsOrders[i].Telephone}";
                }
            }
        }

        private void WriteNewData(int i)
        {
            consultsOrders[i].TimeChangeOrder = DateTime.Now;
            consultsOrders[i].TypeOfChange = "Правка";
            consultsOrders[i].WhoChanged = "Консультант";
        }
    }
}