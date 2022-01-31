using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gym
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timer.Start();

            qw.ItemsSource = ДанныеИзБДвГрид("select Тренер.Фамилия, Тренер.Имя, Тренер.Отчество, Тренер.ид_тренера from Тренер");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (SearchTB.Text != "") CrB.Visibility = Visibility.Visible;
            else CrB.Visibility = Visibility.Hidden;

            SearchB_MouseLeftButtonDown(null, null);
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle a = (Rectangle)sender;
            a.Fill = Brushes.Black;
            a.Opacity = 0.25;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle a = (Rectangle)sender;
            a.Fill = Brushes.Transparent;
            a.Opacity = 0.1;
        }

        private void SearchB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SearchTB.Text != "")
            {
                qw.ItemsSource = ДанныеИзБДвГрид($"select Фамилия, Имя, Отчество from Тренер where  Имя like '%{SearchTB.Text}%' or Фамилия like '%{SearchTB.Text}%' or  Отчество like '%{SearchTB.Text}%'");
            }
            else
                qw.ItemsSource = ДанныеИзБДвГрид($"select Фамилия, Имя, Отчество from Тренер");

        }

        private void AddB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window2 a = new Window2();
            a.Show();
        }
        private void CrB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SearchTB.Text = "";
            Ass.Visibility = Visibility.Visible;
        }

        private void HelpB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock a = (TextBlock)sender;
            a.Visibility = Visibility.Hidden;
        }


        private void SearchTB_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "")
                Ass.Visibility = Visibility.Visible;
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox a = (TextBox)sender;
            if (a.Text == "") Ass.Visibility = Visibility.Visible;
            else Ass.Visibility = Visibility.Hidden;
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

        public DataTable ДанныеИзБД(string selectSQL)
        {
            try
            {
                DataTable dataTable = new DataTable("Table_1");
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = selectSQL;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch
            {
                return null;
            }
        }

        public DataView ДанныеИзБДвГрид(string selectSQL)
        {
            DataTable phonesTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(selectSQL, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.InsertCommand = new SqlCommand("Table_1", connection);
                connection.Open();
                adapter.Fill(phonesTable);
                //   AdminMenu_ClassMenu_Datagrid.ItemsSource = phonesTable.DefaultView;
                return phonesTable.DefaultView;
            }
            catch
            {
                return null;
            }
        }
    }
}