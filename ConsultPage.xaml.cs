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
using System.IO;

namespace OOP1
{
    /// <summary>
    /// Логика взаимодействия для ConsultPage.xaml
    /// </summary>
    public partial class ConsultPage : Page, ChangeOrder
    {
        #region Переменные

        ObservableCollection<Consult> consultsOrders;

        List<Consult> oldConsultOrders = new List<Consult>();

        Consult consult = new Consult();

        #endregion

        #region События

        /// <summary>
        /// Изменяем номер телефона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangePhone(object sender, RoutedEventArgs e)
        {
            CheckAndWrite();
        }

        /// <summary>
        /// Когда заходим на страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterIntoPage(object sender, RoutedEventArgs e)
        {
            RefreshConsult();
            FillOldOrders();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обновление базы данных
        /// </summary>
        public void RefreshConsult()
        {
            consultsOrders = consult.RefreshDB();

            DataBaseConsultList.ItemsSource = consultsOrders;
        }

        /// <summary>
        /// Часть другого метода для записи доп. инфы при изменении записи
        /// </summary>
        /// <param name="i">Передать счётчик цикла</param>
        private void WriteNewData(int i)
        {
            consultsOrders[i].TimeChangeOrder = DateTime.Now;
            consultsOrders[i].TypeOfChange = "Правка";
            consultsOrders[i].WhoChanged = "Консультант";
        }

        #endregion

        #region Конструктор этого класса

        public ConsultPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Интерфейс ChangeOrder - изменения записи

        /// <summary>
        /// Проверяется запись телефона на ввод и если всё ок - то будет запись в БД
        /// </summary>
        public void CheckAndWrite()
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
                FillOldOrders();
                consult.Rewrite(consultsOrders);
                RefreshConsult();
            }
        }

        /// <summary>
        /// Запись об изменении файла
        /// </summary>
        public void WriteChanges()
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

        /// <summary>
        /// Старая база данных, для того, чтобы показывать что изменилось
        /// </summary>
        public void FillOldOrders()
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

        #endregion
        
    }
}