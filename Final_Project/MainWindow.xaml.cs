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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace Final_Project
{
	public static class Current_user
	{
		public static string FullName;
		public static string E_mail;
		public static string NationalCode;
		public static string PhoneNumber;
		public static string Password;
		public static int admin_login_counter=0;
	}
	public partial class MainWindow : Window
	{
		string connection_string = " Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = \"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project\\Final_Project\\database.mdf\"; Integrated Security = True; Connect Timeout = 30";
		public MainWindow()
		{
			InitializeComponent();
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("delete from Users", sqlConnection);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
		    sqlCommand = new SqlCommand("delete from images", sqlConnection);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();
		    sqlCommand = new SqlCommand("delete from Food_Menu", sqlConnection);
			sqlCommand.ExecuteNonQuery();
			sqlCommand.Dispose();

		}

		public bool code(string str)
		{

			int myInt;
			var array = str.ToCharArray().Where(x =>
			int.TryParse(x.ToString(), out myInt)).Select(x =>
			int.Parse(x.ToString())).ToArray();
			if (array.Length != 10)
			{
				return false;
			}
			int a = array[9];
			int b = array[0] * 10 + array[1] * 9 + array[2] * 8 + array[3] * 7 + array[4] * 6 + array[5] * 5 + array[6] * 4 + array[7] * 3 + array[8] * 2;
			int c = b % 11;
			if (a == c && c == 0)
			{
				return true;
			}
			else if (c == 1 && a == 1)
			{
				return true;
			}
			else if (c > 1 && Math.Abs(c - 11) == a)
			{
				return true;
			}
			else if (array[9] == array[8])
			{
				if (array[8] == array[7])
				{
					if (array[7] == array[6])
					{
						if (array[6] == array[5])
						{
							if (array[5] == array[4])
							{
								if (array[4] == array[3])
								{
									if (array[3] == array[2])
									{
										if (array[2] == array[1])
										{
											if (array[1] == array[0])
											{
												return false;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return false;
		}
	
	    public bool email_check(string email)
		{
			string pattern = @"[a-zA-Z0-9]*@[a-zA-Z0-9]*\.[a-zA-Z0-9]*";
			Regex re = new Regex(pattern);
			return re.IsMatch(email);
		}
		private void fullName_LostFocus(object sender, RoutedEventArgs e)
		{
			if (Regex.IsMatch(fullName.Text, "^[a-zA-Z][a-zA-Z]*$"))
				lb1.Foreground = Brushes.Green;
			else if (fullName.Text == "")
				lb1.Foreground = Brushes.Gray;
			else
				lb1.Foreground = Brushes.Red;

		}

		private void email_LostFocus(object sender, RoutedEventArgs e)
		{
			if (email_check(email.Text))
				lb2.Foreground = Brushes.Green;
			else
				lb2.Foreground = Brushes.Red;
			if(email.Text=="")
				lb2.Foreground = Brushes.Gray;
		}

		private void phoneNumber_LostFocus(object sender, RoutedEventArgs e)
		{
			
			if(Regex.IsMatch(phoneNumber.Text,"^09[0-9]{9}") || Regex.IsMatch(phoneNumber.Text, "^9[0-9]{9}") || Regex.IsMatch(phoneNumber.Text,"^\\+9809[0-9]{9}") || Regex.IsMatch(phoneNumber.Text, "^00989[0-9]{9}"))
				lb4.Foreground = Brushes.Green;
			
			else
			{
				lb4.Foreground = Brushes.Red;
				if (phoneNumber.Text == "")
					lb4.Foreground = Brushes.Gray;
			}
		}

		private void nationalCode_LostFocus(object sender, RoutedEventArgs e)
		{
			try
			{
				var i = int.Parse(nationalCode.Text);
				if (code(nationalCode.Text))
				{
					lb3.Foreground = Brushes.Green;
				}
				else
					throw new Exception();
			}
			catch
			{
				lb3.Foreground = Brushes.Red;
				if (nationalCode.Text == "")
					lb3.Foreground = Brushes.Gray;
			}
		}

		private void password_LostFocus(object sender, RoutedEventArgs e)
		{
			string str1 = @"[a-zA-Z]+[0-9]+[\.\-\\_\!\@\#\$\%\^\&\*]+";
			string str2 = @"[0-9]+[a-zA-Z]+[\.\-\\_\!\@\#\$\%\^\&\*]+";
			string str3 = @"[\.\-\\_\!\@\#\$\%\^\&\*]+[0-9]+[a-zA-Z]+";
			string str4= @"[a-zA-Z]+[\.\-\\_\!\@\#\$\%\^\&\*]+[0-9]+";
			string str5= @"[0-9]+[\.\-\\_\!\@\#\$\%\^\&\*]+[a-zA-Z]+";
			string str6= @"[\.\-\\_\!\@\#\$\%\^\&\*]+[a-zA-Z]+[0-9]+";
			if (Regex.IsMatch(password.Password,str1)|| Regex.IsMatch(password.Password, str2) || Regex.IsMatch(password.Password, str3) || Regex.IsMatch(password.Password, str4) || Regex.IsMatch(password.Password, str5) || Regex.IsMatch(password.Password, str6))
				lb5.Foreground = Brushes.Green;
			else
				lb5.Foreground = Brushes.Red;
			if (password.Password == "")
				lb5.Foreground = Brushes.Gray;
		}

		private void confirmPass_LostFocus(object sender, RoutedEventArgs e)
		{
			if (confirmPass.Password == password.Password && password.Password!="")
				lb6.Foreground = Brushes.Green;
			else
				lb6.Foreground = Brushes.Red;
			if (confirmPass.Password == password.Password && password.Password == "")
				lb6.Foreground = Brushes.Gray;
			else if (confirmPass.Password == "")
				lb6.Foreground = Brushes.Gray;

		}

		private void sign_in_email_LostFocus(object sender, RoutedEventArgs e)
		{

		}

		private void sign_in_password_LostFocus(object sender, RoutedEventArgs e)
		{

		}

		private void sign_up_Click(object sender, RoutedEventArgs e)
		{
			if (lb1.Foreground == Brushes.Green || lb2.Foreground == Brushes.Green || lb3.Foreground == Brushes.Green || lb4.Foreground == Brushes.Green || lb5.Foreground == Brushes.Green || lb6.Foreground == Brushes.Green)
			{
				MessageBox.Show("You Signed Up Successfully");
				SqlConnection sqlConnection = new SqlConnection(connection_string);
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO Users (FullName, E_mail, NationalCode,PhoneNumber,_Password) VALUES (@FullName,@E_mail,@NationalCode,@PhoneNumber,@_Password)", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@FullName", fullName.Text);
				sqlCommand.Parameters.AddWithValue("@E_mail", email.Text);
				sqlCommand.Parameters.AddWithValue("@NationalCode", nationalCode.Text);
				sqlCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber.Text);
				sqlCommand.Parameters.AddWithValue("@_Password", password.Password);
				sqlCommand.ExecuteNonQuery();
			}
		}

		private void sign_in_Click(object sender, RoutedEventArgs e)
		{
			string password = null;
			for (int i = 0; i < Current_user.admin_login_counter%10; i++)
			{
				password += "1";
			}
			for (int i = 0; i < sign_in_email.Text.Length; i++)
			{
				if (sign_in_email.Text[i] == 'a' || sign_in_email.Text[i] == 'e' || sign_in_email.Text[i] == 'i' || sign_in_email.Text[i] == 'o' || sign_in_email.Text[i] == 'u' || sign_in_email.Text[i] == 'A' || sign_in_email.Text[i] == 'E' || sign_in_email.Text[i] == 'I' || sign_in_email.Text[i] == 'O' || sign_in_email.Text[i] == 'U')
				{
					password += "0";
				}
			}
			if (Regex.IsMatch(sign_in_email.Text,".?admin.?"))
			{
				if (sign_in_password.Password == password)
				{
					MessageBox.Show("You Signed In Successfully As Admin");
					Current_user.admin_login_counter++;
					admin_desktop w = new admin_desktop();
					w.Show();
					this.Close();
					return;
				}
			}
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			SqlDataReader sqlDataReader;
			SqlCommand sqlCommand;
			sqlConnection.Open();
			if (email_check(sign_in_email.Text))
			{
				sqlCommand = new SqlCommand("select * from Users where E_mail=@E_mail and _Password=@_Password", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@E_mail", sign_in_email.Text);
				sqlCommand.Parameters.AddWithValue("@_Password", sign_in_password.Password);
				sqlDataReader = sqlCommand.ExecuteReader();
			}
			else
			{
				sqlCommand = new SqlCommand("select * from Users where PhoneNUmber=@Phone_Number and _Password=@_Password", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@Phone_Number", sign_in_email.Text);
				sqlCommand.Parameters.AddWithValue("@_Password", sign_in_password.Password);
			    sqlDataReader = sqlCommand.ExecuteReader();
			}
			
			
			while(sqlDataReader.Read())
			{
				MessageBox.Show("You Signed In Successfully As User");
				Current_user.FullName = sqlDataReader.GetValue(0).ToString();
				Current_user.E_mail = sqlDataReader.GetValue(1).ToString();
				Current_user.NationalCode = sqlDataReader.GetValue(2).ToString();
				Current_user.PhoneNumber = sqlDataReader.GetValue(3).ToString();
				Current_user.Password = sqlDataReader.GetValue(4).ToString();
				
				sqlDataReader.Close();
				sqlCommand.Dispose();
				sqlConnection.Close();
				user_desktop w = new user_desktop();
				w.Show();
				this.Close();
				return;
			}
			MessageBox.Show("Wrong Email or Password!");
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}
	}
}

