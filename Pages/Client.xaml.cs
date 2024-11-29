﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using losk_3.BasaSQL;
namespace losk_3.Pages
{
        /// <summary>
        /// Логика взаимодействия для Client.xaml
        /// </summary>
       
        
                public partial class Client : Page
                {
                        public Client(Users user, string role)
                        {
                                InitializeComponent();
                                lbl_Role.Content = role;
                                lbl_WelcomeMessage.Content = $"Добро пожаловать, {user.Login}";
                        }
                }
        
}