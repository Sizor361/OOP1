using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace OOP1
{
    class Consult : DataBase, INotifyPropertyChanged
    {
        public Consult(string secondName, string name, string middleName, string telephone, string dataPassport)
            :base(secondName, name, middleName, telephone, dataPassport) { }

        public Consult() :this("","","","","") { }
        
        List<Consult> consults = new List<Consult>();
        List<Manager> manager = new List<Manager>();    

        public override string Telephone
        {
            get => base.Telephone;
            set
            {
                base.Telephone = value;
                onPropertyChanged("Telephone");
            }
        }

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

                    allRecords.Add(new Consult(args[0], args[1], args[2], args[3], "*****"));
                }
                sr.Close();
            }

            return allRecords;
        }

        public void RefreshDBA()
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

                    allRecords.Add(new Consult(args[0], args[1], args[2], args[3], args[4]));
                }
                sr.Close();
            }
        }


        public void Rewrite()
        {
            RefreshDBA();

            using (StreamWriter sw = new StreamWriter(pathToFile))
            {
                for (int i = 0; i < allRecords.Count; i++)
                {
                    sw.WriteLine(allRecords[i].WriteOrder());
                }

                sw.Close();
           
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void onPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }

        }

    }
}
