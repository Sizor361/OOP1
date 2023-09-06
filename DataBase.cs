using System;
using System.Collections.Generic;
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

        public List<DataBase> allRecords = new List<DataBase>();
        public static string pathToFile;
        public ushort indexDB = 0;

        #endregion

        #region Объявление статических переменных

        static DataBase()
        {
            pathToFile = "DataBase.txt";
        }


        #endregion

        #region Поля

        public string secondName;

        private string name;

        private string middleName;

        private string telephone;

        private string dataPassport;

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

        public virtual List<DataBase> RefreshDB()
        {

            if (allRecords != null)
            {
                allRecords.Clear();
            }

            //using (StreamReader sr = new StreamReader(pathToFile))
            //{
            //    while (!sr.EndOfStream)
            //    {
            //        string[] args = sr.ReadLine().Split('#');

            //        allRecords.Add(new DataBase(args[0], args[1], args[2], args[3], args[4]));
                    
            //    }
            //    sr.Close();
            //}

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

        public string WriteOrder()
        {
            return $"{SecondName}#{Name}#{MiddleName}#{Telephone}#{DataPassport}#";
        }

        #endregion

    }
}
