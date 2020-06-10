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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Final_Project
{
	public class emad
	{
		public int age { get; set; }
		public javad javad { get; set; }

	}
	public class javad
	{
		public int ag { get; set; }

	}
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			emad emi = new emad() { age = 20 };
			javad javi = new javad() { ag = 19 };
			emi.javad = javi;
			this.DataContext = emi;
		}
	}
}
