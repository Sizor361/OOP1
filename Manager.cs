using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OOP1
{
    internal class Manager : DataBase
    {

        #region Переменные

        public ObservableCollection<Manager> managerOrders = new ObservableCollection<Manager>();

        #endregion

        #region Конструкторы

        /// <summary>
        /// Полный констуктор на основе БД
        /// </summary>
        /// <param name="secondName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="telephone">Телефон</param>
        /// <param name="dataPassport">Данные паспорта</param>
        public Manager(string secondName, string name, string middleName, string telephone, string dataPassport,
            DateTime timeChangeOrder, string whichDataChange, string typeOfChange, string whoChanged)
            : base(secondName, name, middleName, telephone, dataPassport, timeChangeOrder, whichDataChange, typeOfChange, whoChanged)
        {
            
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Manager()
        {
            TimeChangeOrder = DateTime.Now;
            TypeOfChange = "Новая запись";
            WhoChanged = "Менеджер";
        }

        #endregion

        #region Методы

        /// <summary>
        /// Перезаписываем в базу данных по новой
        /// </summary>
        /// <param name="managerOrders"></param>
        public void Rewrite(ObservableCollection<Manager> managerOrders)
        {

            using (StreamWriter writer = new StreamWriter(pathToFile))
            {
                for (int i = 0; i < managerOrders.Count; i++)
                {
                    writer.WriteLine(managerOrders[i].WriteOrder());
                }
                writer.Close();
            }
        }

        /// <summary>
        /// Обновление базы данных (не ограниченные права)
        /// </summary>
        /// <param name="dataBase">Передаем сюда ObservableCollection, который будем потом получать обратно со значениями из БД</param>
        /// <returns></returns>
        public ObservableCollection<Manager> RefreshDB()
        {
            if (managerOrders != null)
            {
                managerOrders.Clear();
            }

            using (StreamReader reader = new StreamReader(pathToFile))
            {
                while (!reader.EndOfStream)
                {
                    string[] args = reader.ReadLine().Split('#');

                    managerOrders.Add(new Manager(args[0], args[1], args[2], args[3], args[4], Convert.ToDateTime(args[5]), args[6], args[7], args[8]));
                }
                reader.Close();
            }

            return managerOrders;
        }

        #endregion

    }
}
