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
using System.Windows.Shapes;

namespace Gym
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string connectionString = MainWindow.connectionString;
        public Window1()
        {
            InitializeComponent();
            DataTable s = ДанныеИзБД("select Тренер.Фамилия, Тренер.Имя, Тренер.Отчество, Тренер.ид_тренера from Тренер");
            a4.Items.Clear();
            for (int i = 0; i < s.Rows.Count; i++)
            {
                a4.Items.Add(s.Rows[i][0] + " "+ (s.Rows[i][1]+"")[0] + ". " + (s.Rows[i][2] + "")[0] + ". ");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (a1.Text!="" & a2.Text != "" & a3.Text != "" & a4.SelectedIndex!=-1)
            {
                DataTable s = ДанныеИзБД("select Фамилия,Имя, Отчество, ид_тренера from Тренер");
              
                string h = "";
                for (int i = 0; i < s.Rows.Count; i++)
                {
                   if(s.Rows[i][0] + " " + (s.Rows[i][1] + "")[0] + ". " + (s.Rows[i][2] + "")[0] + ". " == a4.SelectedItem+"")
                   {

                        MessageBox.Show(s.Rows[i][0] + " " + (s.Rows[i][1] + "")[0] + ". " + (s.Rows[i][2] + "")[0] + ". | == |" + a4.SelectedItem + "|"+s.Rows[i][3]);
                        h = s.Rows[i][3]+"";
                   }
                }

                if (ЗаполнениеБД($"insert Клиент values({h}, '{a1.Text}', '{a2.Text}', '{a3.Text}')")) MessageBox.Show("Клиент добавлен");
                else MessageBox.Show("Ошибка при добавлении");
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
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
    }
}
