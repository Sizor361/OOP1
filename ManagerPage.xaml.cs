using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;

namespace OOP1
{
    /// <summary>
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page, NewOrder, ChangeOrder
    {

        #region Переменные

        Manager manager = new Manager();

        ObservableCollection<Manager> managerOrders;

        List<Manager> oldManagerOrders = new List<Manager>();

        #endregion

        #region Конструктор этого класса

        public ManagerPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обновление БД
        /// </summary>
        public void RefreshManager()
        {
            managerOrders = manager.RefreshDB();

            DataBaseManagerList.ItemsSource = managerOrders;
        }

        /// <summary>
        /// Часть другого метода для записи доп. инфы при изменении записи
        /// </summary>
        /// <param name="i">Счётчик цикла</param>
        private void WriteNewData(int i)
        {
            managerOrders[i].TimeChangeOrder = DateTime.Now;
            managerOrders[i].TypeOfChange = "Правка";
            managerOrders[i].WhoChanged = "Менеджер";
        }

        #endregion

        #region Интерфейс NewOrder

        /// <summary>
        /// Добавляем в БД новую запись
        /// </summary>
        public void AddNewOrder()
        {
            managerOrders.Add(new Manager());
            DataBaseManagerList.ItemsSource = managerOrders;
            FillOldOrders();
        }

        #endregion

        #region Интерфейс ChangeOrder

        /// <summary>
        /// Проверяется записи на корректность и если всё ок - то будет запись в БД
        /// </summary>
        public void CheckAndWrite()
        {
            bool isOkay = true;

            for (int i = 0; i < managerOrders.Count; i++)
            {
                bool space = false;

                char[] secondName = managerOrders[i].SecondName.ToCharArray();
                char[] name = managerOrders[i].Name.ToCharArray();
                char[] middleName = managerOrders[i].MiddleName.ToCharArray();
                char[] telephone = managerOrders[i].Telephone.ToCharArray();
                char[] dataPassport = managerOrders[i].DataPassport.ToCharArray();

                for (int j = 0; j < secondName.Length; j++)
                {
                    if (secondName[j] == ' ')
                    {
                        space = true;
                        break;
                    }
                }

                for (int j = 0; j < name.Length; j++)
                {
                    if (name[j] == ' ')
                    {
                        space = true;
                        break;
                    }
                }

                for (int j = 0; j < middleName.Length; j++)
                {
                    if (middleName[j] == ' ')
                    {
                        space = true;
                        break;
                    }
                }

                for (int j = 0; j < telephone.Length; j++)
                {
                    if (telephone[j] == ' ')
                    {
                        space = true;
                        break;
                    }
                }

                for (int j = 0; j < dataPassport.Length; j++)
                {
                    if (dataPassport[j] == ' ')
                    {
                        space = true;
                        break;
                    }
                }


                if (managerOrders[i].SecondName == string.Empty ||
                    managerOrders[i].Name == string.Empty ||
                    managerOrders[i].MiddleName == string.Empty ||
                    managerOrders[i].Telephone == string.Empty ||
                    managerOrders[i].DataPassport == string.Empty ||
                    space)
                {
                    isOkay = false;
                    break;
                }
            }

            if (isOkay)
            {
                WriteChanges();
                FillOldOrders();
                manager.Rewrite(managerOrders);
                RefreshManager();
            }
        }

        /// <summary>
        /// При измененнии записи показываем информацию здесь
        /// </summary>
        public void WriteChanges()
        {
            for (int i = 0; i < oldManagerOrders.Count; i++)
            {

                if (managerOrders[i].Name != oldManagerOrders[i].Name)
                {
                    WriteNewData(i);
                    managerOrders[i].WhichDataChange = $"Имя c {oldManagerOrders[i].Name} на {managerOrders[i].Name}";
                }
                else if (managerOrders[i].SecondName != oldManagerOrders[i].SecondName)
                {
                    WriteNewData(i);
                    managerOrders[i].WhichDataChange = $"Фамилия c {oldManagerOrders[i].SecondName} на {managerOrders[i].SecondName}";
                }
                else if (managerOrders[i].MiddleName != oldManagerOrders[i].MiddleName)
                {
                    WriteNewData(i);
                    managerOrders[i].WhichDataChange = $"Отчество c {oldManagerOrders[i].MiddleName} на {managerOrders[i].MiddleName}";
                }
                else if (managerOrders[i].Telephone != oldManagerOrders[i].Telephone)
                {
                    WriteNewData(i);
                    managerOrders[i].WhichDataChange = $"Телефон c {oldManagerOrders[i].Telephone} на {managerOrders[i].Telephone}";
                }
                else if (managerOrders[i].DataPassport != oldManagerOrders[i].DataPassport)
                {
                    WriteNewData(i);
                    managerOrders[i].WhichDataChange = $"Паспортные данные c {oldManagerOrders[i].DataPassport} на {managerOrders[i].DataPassport}";
                }
            }
        }

        /// <summary>
        /// Обновляем старую БД чтобы узнать что изменилось
        /// </summary>
        public void FillOldOrders()
        {
            if (oldManagerOrders != null)
            {
                oldManagerOrders.Clear();
            }

            int counter = 0;

            if (managerOrders != null)
            {
                do
                {
                    oldManagerOrders.Add(managerOrders[counter]);
                    counter++;
                } while (counter < managerOrders.Count);
            }
        }

        #endregion

        #region События

        /// <summary>
        /// Изменяем поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeData(object sender, RoutedEventArgs e)
        {
            CheckAndWrite();
        }

        /// <summary>
        /// Кликаем на кнопку добавить сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewOrder(object sender, RoutedEventArgs e)
        {
            AddNewOrder();
        }

        /// <summary>
        /// Когда заходим на страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterIntoPage(object sender, RoutedEventArgs e)
        {
            RefreshManager();
            FillOldOrders();
        }

        #endregion

    }
}
