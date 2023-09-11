using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace OOP1
{
    abstract class DataBase
    {

        #region Переменные

        protected static string pathToFile;

        #endregion

        #region Объявление статических переменных

        static DataBase()
        {
            pathToFile = "DataBase.txt";
        }


        #endregion

        #region Поля

        protected string secondName;

        protected string name;

        protected string middleName;

        protected string telephone;

        protected string dataPassport;

        #endregion

        #region Свойства

        public string SecondName { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public virtual string Telephone { get; set; }

        public string DataPassport { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Базовый констуктор
        /// </summary>
        /// <param name="secondName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="telephone">Телефон</param>
        /// <param name="dataPassport">Паспортные данные</param>
        public DataBase(string secondName, string name, string middleName, string telephone, string dataPassport)
        {
            SecondName = secondName;
            Name = name;
            MiddleName = middleName;
            Telephone = telephone;
            DataPassport = dataPassport;
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public DataBase() : this("", "", "", "", "")
        {

        }

        #endregion

        #region Методы

        /// <summary>
        /// Новая запись в БД
        /// </summary>
        protected void NewRecord()
        {
            using (StreamWriter sw = new StreamWriter(pathToFile, true))
            {
                sw.WriteLine($"{secondName}#{name}#{middleName}#" +
                    $"{telephone}#{dataPassport}#");
                sw.Close();
            }
        }

        /// <summary>
        /// Порядок записи в БД
        /// </summary>
        /// <returns></returns>
        protected string WriteOrder()
        {
            return $"{SecondName}#{Name}#{MiddleName}#{Telephone}#{DataPassport}#";
        }

        #endregion

    }
}
