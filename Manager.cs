using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP1
{
    internal class Manager : DataBase
    {
        public Manager(string secondName, string name, string middleName, string telephone, string dataPassport)
            : base(secondName, name, middleName, telephone, dataPassport) { }

        public Manager() : this("", "", "", "", "") { }


        public override List<DataBase> RefreshDB()
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

                    allRecords.Add(new Manager(args[0], args[1], args[2], args[3], args[4]));

                }
                sr.Close();
            }

            return allRecords;
        }
    }
}
