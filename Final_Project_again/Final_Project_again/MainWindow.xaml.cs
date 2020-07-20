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

namespace Final_Project_again
{
	public static class check_conditions
	{
		public static bool code(string str)
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

		public static bool email_check(string email)
		{
			string pattern = @"[a-zA-Z0-9]*@[a-zA-Z0-9]*\.[a-zA-Z0-9]*";
			Regex re = new Regex(pattern);
			return re.IsMatch(email);
		}
	}

	public static class Current_user
	{
		public static string FullName;
		public static string E_mail;
		public static string NationalCode;
		public static string PhoneNumber;
		public static string Password;
		public static string image_uri;
		public static string cart;
		public static string shopping_list;
		public static int admin_login_counter = 0;
	}
	public partial class MainWindow : Window
	{
		string connection_string = " Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\emad&javad\\Desktop\\visual studio\\Final_Project_again\\Final_Project_again\\database.mdf\";Integrated Security=True;Connect Timeout=30";
		bool FullName_Is_added = false;
		bool Email_Is_added = false;
		bool FullName_eror = false;
		bool Email_error = false;
		bool Phone_eror = false;
		bool NationalCode_eror = false;
		bool Password_eror = false;
		bool Confirm_eror = false;
		public MainWindow()
		{
			InitializeComponent();
			
			//SqlConnection sqlConnection = new SqlConnection(connection_string);
			//sqlConnection.Open();
			//SqlCommand sqlCommand = new SqlCommand("delete from Receipt", sqlConnection);
			//sqlCommand.ExecuteNonQuery();
			//sqlCommand.Dispose();
			//sqlCommand = new SqlCommand("delete from Food_Menu", sqlConnection);
			//sqlCommand.ExecuteNonQuery();
			//sqlCommand.Dispose();
			//sqlCommand = new SqlCommand("delete from Users", sqlConnection);
			//sqlCommand.ExecuteNonQuery();
			//sqlCommand.Dispose();
			//sqlConnection.Close();

		}

		private void fullName_LostFocus(object sender, RoutedEventArgs e)
		{
			FullName_Is_added = false;
			FullName_eror = false;
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select FullName from Users", sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				if (fullName.Text == sqlDataReader.GetValue(0).ToString())
				{
					FullName_Is_added = true;
					sqlConnection.Close();
					sqlCommand.Dispose();
					sqlDataReader.Close();
					break;
				}
			}
			sqlConnection.Close();
			sqlCommand.Dispose();
			sqlDataReader.Close();


			if (Regex.IsMatch(fullName.Text, "^[a-zA-Z][a-zA-Z\\s]*$") && !FullName_Is_added)
			{
				lb1.Foreground = Brushes.Green;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
				full_btn.Background = brush;
				full_btn.Visibility = Visibility.Visible;
			}
			else if (fullName.Text == "")
			{
				lb1.Foreground = Brushes.Gray;
				full_btn.Visibility = Visibility.Hidden;
			}
			else
			{
				full_btn.Visibility = Visibility.Visible;
				lb1.Foreground = Brushes.Red;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\warning.png"));
				full_btn.Background = brush;
				FullName_eror = true;



			}

		}

		private void email_LostFocus(object sender, RoutedEventArgs e)
		{
			Email_Is_added = false;
			Email_error = false;
			SqlConnection sqlConnection = new SqlConnection(connection_string);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand("select E_mail from Users", sqlConnection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				if (email.Text == sqlDataReader.GetValue(0).ToString())
				{
					Email_Is_added = true;
					sqlConnection.Close();
					sqlCommand.Dispose();
					sqlDataReader.Close();
					break;
				}
			}
			sqlConnection.Close();
			sqlCommand.Dispose();
			sqlDataReader.Close();


			if (check_conditions.email_check(email.Text) && !Email_Is_added)
			{
				lb2.Foreground = Brushes.Green;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
				email_btn.Background = brush;
				email_btn.Visibility = Visibility.Visible;
			}
			else
			{
				lb2.Foreground = Brushes.Red;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\warning.png"));
				email_btn.Background = brush;
				email_btn.Visibility = Visibility.Visible;
				Email_error = true;


			}
			if (email.Text == "")
			{
				lb2.Foreground = Brushes.Gray;
				email_btn.Visibility = Visibility.Hidden;
			}
		}

		private void phoneNumber_LostFocus(object sender, RoutedEventArgs e)
		{
			Phone_eror = false;
			if (Regex.IsMatch(phoneNumber.Text, "^09[0-9]{9}") || Regex.IsMatch(phoneNumber.Text, "^9[0-9]{9}") || Regex.IsMatch(phoneNumber.Text, "^\\+9809[0-9]{9}") || Regex.IsMatch(phoneNumber.Text, "^00989[0-9]{9}"))
			{
				lb4.Foreground = Brushes.Green;
				phoneNumber_btn.Visibility = Visibility.Visible;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
				phoneNumber_btn.Background = brush;
				phoneNumber_btn.Visibility = Visibility.Visible;
			}

			else
			{
				lb4.Foreground = Brushes.Red;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\warning.png"));
				phoneNumber_btn.Background = brush;
				phoneNumber_btn.Visibility = Visibility.Visible;
				Phone_eror = true;


				if (phoneNumber.Text == "")
				{
					lb4.Foreground = Brushes.Gray;
					phoneNumber_btn.Visibility = Visibility.Hidden;
					brush = new ImageBrush();
					brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
					phoneNumber_btn.Background = brush;

				}
			}
		}

		private void nationalCode_LostFocus(object sender, RoutedEventArgs e)
		{
			NationalCode_eror = false;
			try
			{
				if (check_conditions.code(nationalCode.Text))
				{
					lb3.Foreground = Brushes.Green;
					var brush = new ImageBrush();
					brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
					nationalCode_btn.Background = brush;
					nationalCode_btn.Visibility = Visibility.Visible;
				}
				else
					throw new Exception();
			}
			catch
			{
				lb3.Foreground = Brushes.Red;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\warning.png"));
				nationalCode_btn.Background = brush;
				nationalCode_btn.Visibility = Visibility.Visible;
				NationalCode_eror = true;



				if (nationalCode.Text == "")
				{
					lb3.Foreground = Brushes.Gray;
					nationalCode_btn.Visibility = Visibility.Hidden;
					brush = new ImageBrush();
					brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
					nationalCode_btn.Background = brush;
				}
			}
		}

		private void password_LostFocus(object sender, RoutedEventArgs e)
		{
			Password_eror = false;
			string str1 = @"[a-zA-Z]+[0-9]+[\.\-\\_\!\@\#\$\%\^\&\*]+";
			string str2 = @"[0-9]+[a-zA-Z]+[\.\-\\_\!\@\#\$\%\^\&\*]+";
			string str3 = @"[\.\-\\_\!\@\#\$\%\^\&\*]+[0-9]+[a-zA-Z]+";
			string str4 = @"[a-zA-Z]+[\.\-\\_\!\@\#\$\%\^\&\*]+[0-9]+";
			string str5 = @"[0-9]+[\.\-\\_\!\@\#\$\%\^\&\*]+[a-zA-Z]+";
			string str6 = @"[\.\-\\_\!\@\#\$\%\^\&\*]+[a-zA-Z]+[0-9]+";
			if (Regex.IsMatch(password.Password, str1) || Regex.IsMatch(password.Password, str2) || Regex.IsMatch(password.Password, str3) || Regex.IsMatch(password.Password, str4) || Regex.IsMatch(password.Password, str5) || Regex.IsMatch(password.Password, str6))
			{
				lb5.Foreground = Brushes.Green;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
				password_btn.Background = brush;
				password_btn.Visibility = Visibility.Visible;
			}

			else
			{
				lb5.Foreground = Brushes.Red;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\warning.png"));
				password_btn.Background = brush;
				password_btn.Visibility = Visibility.Visible;
				Password_eror = true;


			}
			if (password.Password == "")
			{
				lb5.Foreground = Brushes.Gray;
				password_btn.Visibility = Visibility.Hidden;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
				password_btn.Background = brush;
			}
		}

		private void confirmPass_LostFocus(object sender, RoutedEventArgs e)
		{
			Confirm_eror = false;
			if (confirmPass.Password == password.Password && password.Password != "")
			{
				lb6.Foreground = Brushes.Green;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
				confirm_btn.Background = brush;
				confirm_btn.Visibility = Visibility.Visible;
			}

			else
			{
				lb6.Foreground = Brushes.Red;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\warning.png"));
				confirm_btn.Background = brush;
				confirm_btn.Visibility = Visibility.Visible;
				Confirm_eror = true;



			}

			if (confirmPass.Password == password.Password && password.Password == "")
			{
				lb6.Foreground = Brushes.Gray;
				confirm_btn.Visibility = Visibility.Hidden;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
				confirm_btn.Background = brush;
			}
			else if (confirmPass.Password == "")
			{
				lb6.Foreground = Brushes.Gray;
				confirm_btn.Visibility = Visibility.Hidden;
				var brush = new ImageBrush();
				brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\tick.png"));
				confirm_btn.Background = brush;
			}

		}

		private void sign_in_email_LostFocus(object sender, RoutedEventArgs e)
		{

		}

		private void sign_in_password_LostFocus(object sender, RoutedEventArgs e)
		{

		}

		private void sign_up_Click(object sender, RoutedEventArgs e)
		{
			if (lb1.Foreground == Brushes.Green && lb2.Foreground == Brushes.Green && lb3.Foreground == Brushes.Green && lb4.Foreground == Brushes.Green && lb5.Foreground == Brushes.Green && lb6.Foreground == Brushes.Green)
			{
				MessageBox.Show("You Signed Up Successfully");
				SqlConnection sqlConnection = new SqlConnection(connection_string);
				sqlConnection.Open();
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO Users (FullName, E_mail, NationalCode,PhoneNumber,_Password,Cart,Order_Number) VALUES (@FullName,@E_mail,@NationalCode,@PhoneNumber,@_Password,@Cart,@Order_Number)", sqlConnection);
				sqlCommand.Parameters.AddWithValue("@FullName", fullName.Text);
				sqlCommand.Parameters.AddWithValue("@E_mail", email.Text);
				sqlCommand.Parameters.AddWithValue("@NationalCode", nationalCode.Text);
				sqlCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber.Text);
				sqlCommand.Parameters.AddWithValue("@_Password", password.Password);
				sqlCommand.Parameters.AddWithValue("@Cart", "");
				sqlCommand.Parameters.AddWithValue("@Order_Number", "1");
				sqlCommand.ExecuteNonQuery();
			}

			else
			{
				if (confirmPass.Password != password.Password)
				{
					lb6.Foreground = Brushes.Red;
					var brush = new ImageBrush();
					brush.ImageSource = new BitmapImage(new Uri(@"C:\Users\emad&javad\Desktop\visual studio\Final_Project\Final_Project\images\warning.png"));
					confirm_btn.Background = brush;
					confirm_btn.Visibility = Visibility.Visible;
				}
				MessageBox.Show("Something went wrong");

			}
		}

		private void sign_in_Click(object sender, RoutedEventArgs e)
		{
			string password = null;
			for (int i = 0; i < Current_user.admin_login_counter % 10; i++)
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
			if (Regex.IsMatch(sign_in_email.Text, ".?admin.?"))
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
			if (check_conditions.email_check(sign_in_email.Text))
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


			while (sqlDataReader.Read())
			{
				
				Current_user.FullName = sqlDataReader.GetValue(0).ToString();
				Current_user.E_mail = sqlDataReader.GetValue(1).ToString();
				Current_user.NationalCode = sqlDataReader.GetValue(2).ToString();
				Current_user.PhoneNumber = sqlDataReader.GetValue(3).ToString();
				Current_user.Password = sqlDataReader.GetValue(4).ToString();
				Current_user.image_uri = sqlDataReader.GetValue(5).ToString();
				Current_user.shopping_list = sqlDataReader.GetValue(6).ToString();
				Current_user.cart = sqlDataReader.GetValue(7).ToString();

				sqlDataReader.Close();
				sqlCommand.Dispose();
				
				sqlCommand = new SqlCommand("IF EXISTS(SELECT * from Food_Menu) SELECT 1 ELSE SELECT 0", sqlConnection);
				sqlDataReader = sqlCommand.ExecuteReader();
				sqlDataReader.Read();
				if ((int)sqlDataReader.GetValue(0) == 1)
				{
					MessageBox.Show("You Signed In Successfully As User");
					user_desktop w = new user_desktop();
					w.Show();
					this.Close();
					sqlDataReader.Close();
					sqlCommand.Dispose();
					sqlConnection.Close();
					return;
				}
				else
				{
					MessageBox.Show("There is No Food,Please wait till admin put Food in Menu");
					sqlDataReader.Close();
					sqlCommand.Dispose();
					sqlConnection.Close();
					return;
				}
			}
			MessageBox.Show("Wrong Email or Password!");
			sqlDataReader.Close();
			sqlCommand.Dispose();
			sqlConnection.Close();
		}

		private void full_btn_MouseEnter(object sender, MouseEventArgs e)
		{

			if (FullName_eror)
			{
				FullName_br.Visibility = Visibility.Visible;
				if (FullName_Is_added)
				{
					FullName_lb.Content = "This Name is already used";
				}
				else
					FullName_lb.Content = "Wrong Name format";
			}
		}

		private void full_btn_MouseLeave(object sender, MouseEventArgs e)
		{
			FullName_br.Visibility = Visibility.Hidden;

		}

		private void email_btn_MouseEnter(object sender, MouseEventArgs e)
		{
			if (Email_error)
			{
				Email_br.Visibility = Visibility.Visible;
				if (Email_Is_added)
				{
					Email_lb.Content = "This Email is already used";
				}
				else
					Email_lb.Content = "Wrong Email format";
			}
		}

		private void email_btn_MouseLeave(object sender, MouseEventArgs e)
		{
			Email_br.Visibility = Visibility.Hidden;

		}

		private void nationalCode_btn_MouseEnter(object sender, MouseEventArgs e)
		{
			if (NationalCode_eror)
			{
				NationalCode_br.Visibility = Visibility.Visible;
				NationalCode_lb.Content = "Not valid";
			}
		}

		private void nationalCode_btn_MouseLeave(object sender, MouseEventArgs e)
		{
			NationalCode_br.Visibility = Visibility.Hidden;

		}

		private void phoneNumber_btn_MouseEnter(object sender, MouseEventArgs e)
		{
			if (Phone_eror)
			{
				Phone_br.Visibility = Visibility.Visible;
				Phone_lb.Content = "This is not valid Phone Number";
			}
		}

		private void phoneNumber_btn_MouseLeave(object sender, MouseEventArgs e)
		{
			Phone_br.Visibility = Visibility.Hidden;

		}

		private void password_btn_MouseEnter(object sender, MouseEventArgs e)
		{
			if (Password_eror)
			{
				Password_br.Visibility = Visibility.Visible;
				Password_lb.Content = "Password should consists letter,digit,sign";
			}

		}

		private void password_btn_MouseLeave(object sender, MouseEventArgs e)
		{
			Password_br.Visibility = Visibility.Hidden;

		}

		private void confirm_btn_MouseEnter(object sender, MouseEventArgs e)
		{

			if (Confirm_eror)
			{
				Confirm_br.Visibility = Visibility.Visible;
				Confirm_lb.Content = "Not match with Password";
			}
		}

		private void confirm_btn_MouseLeave(object sender, MouseEventArgs e)
		{
			Confirm_br.Visibility = Visibility.Hidden;

		}
	}
}

