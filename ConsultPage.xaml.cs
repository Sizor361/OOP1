﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOP1
{
    /// <summary>
    /// Логика взаимодействия для ConsultPage.xaml
    /// </summary>
    public partial class ConsultPage : Page
    {
        public ConsultPage()
        {
            InitializeComponent();
        }

        private void ChangePhone(object sender, KeyboardFocusChangedEventArgs e)
        {
            Consult consult = new Consult();
            
            consult.Rewrite();
        }
    }
}