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
        public event PropertyChangedEventHandler PropertyChanged;

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
        public Manager(string secondName, string name, string middleName, string telephone, string dataPassport)
            : base(secondName, name, middleName, telephone, dataPassport) { }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Manager() : this("", "", "", "", "") { }

        #endregion

        #region Методы

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

                    managerOrders.Add(new Manager(args[0], args[1], args[2], args[3], args[4]));
                }
                reader.Close();
            }

            return managerOrders;
        }

        /// <summary>
        /// То с чем не разобрался - должен обновлять местную БД
        /// </summary>
        /// <param name="prop"></param>
        private void onPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion

    }
}
