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
    class Consult : DataBase, INotifyPropertyChanged
    {

        #region Переменные

        public ObservableCollection<Consult> consultsOrders = new ObservableCollection<Consult>();
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
        public Consult(string secondName, string name, string middleName, string telephone, string dataPassport)
            : base(secondName, name, middleName, telephone, dataPassport) 
        {
            base.telephone = telephone;
            onPropertyChanged("Telephone");
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Consult() : this("", "", "", "", "") 
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
        /// Обновление базы данных (ограниченные права)
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

                    consultsOrders.Add(new Consult(args[0], args[1], args[2], args[3], "****-******"));
                }
                reader.Close();
            }

            return consultsOrders;
        }

        /// <summary>
        /// Обновление базы данных (без ограничений прав)
        /// </summary>
        /// <param name="dataBase">Передаем сюда ObservableCollection, который будем потом получать обратно со значениями из БД</param>
        /// <returns></returns>
        public ObservableCollection<Consult> RefreshDBFull()
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

                    consultsOrders.Add(new Consult(args[0], args[1], args[2], args[3], args[4]));
                }
                reader.Close();
            }

            return consultsOrders;
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