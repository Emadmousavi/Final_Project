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
using System.Windows.Shapes;

namespace Final_Project
{
	/// <summary>
	/// Interaction logic for user_menu.xaml
	/// </summary>
	public partial class user_menu : Window
	{
		Window creatingForm;
		public Window setCreatingForm
		{
			get { return creatingForm; }
			set { creatingForm = value; }
		}

		public user_menu()
		{
			InitializeComponent();
		}

		private void profile_MouseDown(object sender, MouseButtonEventArgs e)
		{
			user_profile w = new user_profile();
			w.Show();
			if (creatingForm != null)
				creatingForm.Close();
			this.Close();
		}

		private void Quit_MouseDown(object sender, MouseButtonEventArgs e)
		{
			MainWindow w = new MainWindow();
			w.Show();
			if (creatingForm != null)
				creatingForm.Close();
			this.Close();
		}

		private void cart_MouseDown(object sender, MouseButtonEventArgs e)
		{
			user_cart w = new user_cart();
			w.Show();
			if (creatingForm != null)
				creatingForm.Close();
			this.Close();
		}

		private void shopping_list_MouseDown(object sender, MouseButtonEventArgs e)
		{
			user_shopping_list w = new user_shopping_list();
			w.Show();
			if (creatingForm != null)
				creatingForm.Close();
			this.Close();
		}
	}
}
