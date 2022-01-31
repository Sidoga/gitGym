using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Gym
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (a1.Text != "" & a2.Text != "" & a3.Text != "" )
            {
                if (ЗаполнениеБД($"insert Тренер values('{a1.Text}', '{a2.Text}', '{a3.Text}')")) MessageBox.Show("Тренер добавлен");
                else MessageBox.Show("Ошибка при добавлении");
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
        string connectionString = MainWindow.connectionString;
        public bool ЗаполнениеБД(string СтрокаЗаполнения)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand(СтрокаЗаполнения, con);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
