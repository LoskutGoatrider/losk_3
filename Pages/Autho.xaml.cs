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
using System.Windows.Threading;

namespace losk_3.Pages
{
        /// <summary>
        /// Логика взаимодействия для Autho.xaml
        /// </summary>
        public partial class Autho : Page
        {
                private DispatcherTimer timer;
                private int remainingTime;
                int click;
                public Autho()
                {

                        InitializeComponent();
                        CreateTimer();
                        click = 0;
                }
                private void CreateTimer()
                {
                        timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromSeconds(1);
                        timer.Tick += Timer_Tick;
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
                        string hashPassword = Hash.HashPassword(password);

                        var db = Helper.GetContext();



                        var user = db.Users.Where(x => x.Login == login && x.Password == hashPassword).FirstOrDefault();
                        if (click == 1)
                        {
                                if (!IsAccessAllowed())
                                {
                                        MessageBox.Show("Доступ к системе в данный момент запрещён. Пожалуйста, приходите в рабочие часы с 9:00 до 18:00.",
                                            "Ошибка доступа", MessageBoxButton.OK, MessageBoxImage.Warning);

                                        BlockControls();
                                        remainingTime = 30;
                                        txtBlockTimer.Visibility = Visibility.Visible;
                                        timer.Start();
                                        return;
                                }
                                if (user != null)
                                {
                                        MessageBox.Show("Вы вошли под: " + user.Role.name.ToString());
                                        LoadPage(user.Role.name.ToString(), user);
                                        tbLogin.Text = "";
                                        tbPassword.Text = "";
                                        tbCaptcha.Text = "";
                                        tblCaptcha.Visibility = Visibility.Hidden;
                                        tbCaptcha.Visibility = Visibility.Hidden;
                                        MessageBox.Show(GreetUser(user));
                                        LoadPage(user.Role.name.ToString(), user); 
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
                                if (click == 3)
                                {
                                        BlockControls();

                                        remainingTime = 10;
                                        txtBlockTimer.Visibility = Visibility.Visible;
                                        txtBlockTimer.Text = $"Оставшееся время: {remainingTime} секунд";

                                        timer.Start();
                                }
                                if (user != null && tbCaptcha.Text == tblCaptcha.Text)
                                {
                                        MessageBox.Show("Вы вошли под: " + user.Role.name.ToString());
                                        LoadPage(user.Role.name.ToString(), user);
                                        tbLogin.Text = "";
                                        tbPassword.Text = "";
                                        tbCaptcha.Text = "";
                                        tblCaptcha.Visibility = Visibility.Hidden;
                                        tbCaptcha.Visibility = Visibility.Hidden;
                                        MessageBox.Show(GreetUser(user));
                                        LoadPage(user.Role.name.ToString(), user);
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
                private void BlockControls()
                {
                        tbLogin.IsEnabled = false;
                        tbPassword.IsEnabled = false;
                        tbCaptcha.IsEnabled = false;
                        btnEnterGuests.IsEnabled = false;
                        btnEnter.IsEnabled = false;
                }

                private void UnlockControls()
                {
                        tbLogin.IsEnabled = true;
                        tbPassword.IsEnabled = true;
                        tbCaptcha.IsEnabled = true;
                        btnEnterGuests.IsEnabled = true;
                        btnEnter.IsEnabled = true;
                        tbLogin.Clear();
                        tbPassword.Clear();
                        tblCaptcha.Text = "Text";
                        tbCaptcha.Text = "";
                        tbCaptcha.Visibility = Visibility.Hidden;
                        tblCaptcha.Visibility = Visibility.Hidden;
                        click = 0;
                }

                private void Timer_Tick(object sender, EventArgs e)
                {
                        remainingTime--;

                        if (remainingTime <= 0)
                        {
                                timer.Stop();
                                UnlockControls();
                                txtBlockTimer.Visibility = Visibility.Hidden;
                                return;
                        }

                        txtBlockTimer.Text = $"Оставшееся время: {remainingTime} секунд";
                }
                private bool IsAccessAllowed()
                {
                        DateTime now = DateTime.Now;
                        TimeSpan startTime = new TimeSpan(9, 0, 0);  // 9:00
                        TimeSpan endTime = new TimeSpan(18, 0, 0);    // 18:00
                        TimeSpan currentTime = now.TimeOfDay;

                        return currentTime >= startTime && currentTime <= endTime;
                }

                private string GreetUser(Users user)
                {
                        DateTime now = DateTime.Now;
                        string timeOfDay = null;
                        string lastName = user.Technicians.LastName.ToString();
                        string firstName = user.Technicians.FirstName.ToString();
                        string middleName = user.Technicians.MiddleName.ToString();  

                        if (now.Hour >= 9 && now.Hour < 12)
                        {
                                timeOfDay = "Доброе Утро!";
                        }
                        else if (now.Hour >= 12 && now.Hour < 16)
                        {
                                timeOfDay = "Добрый День!";
                        }
                        else if (now.Hour >= 16 && now.Hour < 18)
                        {
                                timeOfDay = "Добрый Вечер!";
                        }

                        string fullName = $"{lastName} {firstName}" + (string.IsNullOrEmpty(middleName) ? "" : $" {middleName}");

                        return $"{timeOfDay}\nДобро пожаловать {fullName}";
                }

        }

}
