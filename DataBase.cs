using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OOP1
{
    class DataBase
    {

        #region Переменные

        static List<DataBase> allRecords;
        static string pathToFile;
        static ushort indexDB;

        #endregion

        #region Объявление переменных

        static DataBase()
        {
            allRecords = new List<DataBase>();
            pathToFile = "DataBase.txt";
            indexDB = 0;
        }

        #endregion

        #region Поля

        private string secondName;

        private string name;

        private string middleName;

        private string telephone;

        private string dataPassport;

        #endregion

        #region Свойства

        protected string SecondName { get; set; }

        protected string Name { get; set; }

        protected string MiddleName { get; set; }

        protected string Telephone { get; set; }

        protected string DataPassport { get; set; }

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
        protected DataBase(string secondName, string name, string middleName, string telephone, string dataPassport)
        {
            this.secondName = secondName;
            this.name = name;
            this.middleName = middleName;
            this.telephone = telephone;
            this.dataPassport = dataPassport;
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public DataBase() : this("", "", "", "", "")
        {

        }

        #endregion

        #region Методы

        public List<DataBase> RefreshDB()
        {

            if (allRecords != null)
            {
                allRecords.Clear();
            }

            using (StreamReader sr = new StreamReader(pathToFile))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    allRecords.Add(new DataBase(args[0], args[1], args[2], args[3], args[4]));
                }
                sr.Close();
            }

            return allRecords;
        }

        public void NewRecord()
        {
            using (StreamWriter sw = new StreamWriter(pathToFile, true))
            {
                sw.WriteLine($"{secondName}#{name}#{middleName}#" +
                    $"{telephone}#{dataPassport}#");
                sw.Close();
            }
        }

        #endregion

    }
}
