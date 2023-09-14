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

        public static string pathToFile;

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

        protected DateTime timeChangeOrder;

        protected string whichDataChange;

        protected string typeOfChange;

        protected string whoChanged;

        #endregion

        #region Свойства

        public string SecondName { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Telephone { get; set; }

        public string DataPassport { get; set; }

        public DateTime TimeChangeOrder { get; set; }

        public string WhichDataChange { get; set; }

        public string TypeOfChange { get; set; }

        public string WhoChanged { get; set; }

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
        /// <param name="timeChangeOrder">Время изменения</param>
        /// <param name="whichDataChange">Какие данные изменились</param>
        /// <param name="typeOfChange">Тип изменений</param>
        /// <param name="whoChanged">Кто изменил</param>
        public DataBase(string secondName, string name, string middleName, string telephone, string dataPassport, 
            DateTime timeChangeOrder, string whichDataChange, string typeOfChange, string whoChanged)
        {
            SecondName = secondName;
            Name = name;
            MiddleName = middleName;
            Telephone = telephone;
            DataPassport = dataPassport;
            TypeOfChange = typeOfChange;
            WhoChanged = whoChanged;
            TimeChangeOrder = timeChangeOrder;
            WhichDataChange = whichDataChange;
        }

        /// <summary>
        /// Констуктор для вывода на экран
        /// </summary>
        /// <param name="secondName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="telephone">Телефон</param>
        /// <param name="dataPassport">Паспортные данные</param>
        public DataBase(string secondName, string name, string middleName, string telephone, string dataPassport) : 
            this (secondName, name, middleName, telephone, dataPassport, new DateTime(), "", "", "")
        {
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public DataBase() : this("", "", "", "", "", new DateTime(), "", "", "")
        {

        }

        #endregion

        #region Методы

        /// <summary>
        /// Порядок записи в БД
        /// </summary>
        /// <returns></returns>
        protected string WriteOrder()
        {
            return $"{SecondName}#{Name}#{MiddleName}#{Telephone}#{DataPassport}#{TimeChangeOrder}#{WhichDataChange}#{TypeOfChange}#{WhoChanged}#";
        }

        #endregion

    }
}
