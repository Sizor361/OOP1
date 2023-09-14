using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Linq;

namespace OOP1
{
    class Consult : DataBase
    {

        #region Переменные

        public ObservableCollection<Consult> consultsOrders = new ObservableCollection<Consult>();

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
        public Consult(string secondName, string name, string middleName, string telephone, string dataPassport,
            DateTime timeChangeOrder, string whichDataChange, string typeOfChange, string whoChanged)
            : base(secondName, name, middleName, telephone, dataPassport, timeChangeOrder, whichDataChange, typeOfChange, whoChanged) 
        {

        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Consult()
        {

        }

        #endregion

        #region Методы

        /// <summary>
        /// Перезаписываем в базу данных по новой
        /// </summary>
        /// <param name="consultsOrders"></param>
        public void Rewrite(ObservableCollection<Consult> consultsOrders)
        {

            using (StreamWriter writer = new StreamWriter(pathToFile))
            {
                for (int i = 0; i < consultsOrders.Count; i++)
                {
                    writer.WriteLine(consultsOrders[i].WriteOrder());
                }
                writer.Close();
            }
        }

        /// <summary>
        /// Обновление базы данных
        /// </summary>
        /// <param name="dataBase">Передаем сюда ObservableCollection, который будем потом получать обратно со значениями из БД</param>
        /// <returns></returns>
        public ObservableCollection<Consult> RefreshDB()
        {
            if (consultsOrders != null)
            {
                consultsOrders.Clear();
            }

            using (StreamReader reader = new StreamReader(pathToFile))
            {
                while (!reader.EndOfStream)
                {
                    string[] args = reader.ReadLine().Split('#');

                    consultsOrders.Add(new Consult(args[0], args[1], args[2], args[3], args[4], Convert.ToDateTime(args[5]), args[6], args[7], args[8]));
                }
                reader.Close();
            }

            return consultsOrders;
        }

        #endregion

    }
}