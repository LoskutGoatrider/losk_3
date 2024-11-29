using losk_3.BasaSQL;
using losk_3.Services;
using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using losk_3.BasaSQL; 

namespace losk_3.Pages
{
        /// <summary>
        /// Логика взаимодействия для Autho.xaml
        /// </summary>
        public partial class Autho : Page
        {
                int click;
                public Autho()
                {
                        InitializeComponent();
                        click = 0;
                }

                private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
                {
                        NavigationService.Navigate(new Client(null, null));
                }


                private void btnEnter_Click(object sender, RoutedEventArgs e)
                {
                        click += 1;
                        string login = tbLogin.Text.Trim();
                        string password = tbPassword.Text.Trim();
                        //string hashPassword = Hash.HashPassword(password);

                        var db = Helper.GetContext();

                        var user = db.Users.Where(x => x.Login == login && x.Password == password).FirstOrDefault();
                        if (click == 1)
                        {
                                if (user != null)
                                {
                                        MessageBox.Show("Вы вошли под: " + user.Role.name.ToString());
                                        LoadPage(user.Role.name.ToString(), user);
                                        tbLogin.Text = "";
                                        tbPassword.Text = "";
                                        tbCaptcha.Text = "";
                                        tblCaptcha.Visibility = Visibility.Hidden;
                                        tbCaptcha.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                        MessageBox.Show("Вы ввели логин или пароль неверно!");
                                        GenerateCapctcha();
                                        tbPassword.Text = "";
                                        tbCaptcha.Text = "";
                                }
                                
                        }
                        else if (click > 1)
                        {
                                if (user != null && tbCaptcha.Text == tblCaptcha.Text)
                                {
                                        MessageBox.Show("Вы вошли под: " + user.Role.name.ToString());
                                        LoadPage(user.Role.name.ToString(), user);
                                        tbLogin.Text = "";
                                        tbPassword.Text = "";
                                        tbCaptcha.Text = "";
                                        tblCaptcha.Visibility = Visibility.Hidden;
                                        tbCaptcha.Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                        MessageBox.Show("Введите данные заново!");
                                        GenerateCapctcha();
                                        tbPassword.Text = "";
                                        tbCaptcha.Text = "";
                                }
                        }



                }

                private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
                {
                        NavigationService.Navigate(new Client(null, null));
                }
                private void GenerateCapctcha()
                {
                        tbCaptcha.Visibility = Visibility.Visible;
                        tblCaptcha.Visibility = Visibility.Visible;

                        string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
                        tblCaptcha.Text = capctchaText;
                        tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
                }
                private void LoadPage(string _role, Users user)
                {
                        click = 0;
                        switch (_role)
                        {
                                case "Оператор":
                                        NavigationService.Navigate(new Client(user, _role));
                                        break;
                                case "Администратор":
                                        NavigationService.Navigate(new AdminPage(user, _role));
                                        break;
                                case "Пользователь":
                                        NavigationService.Navigate(new UserPage(user, _role));
                                        break;

                        }
                }


        }

}
