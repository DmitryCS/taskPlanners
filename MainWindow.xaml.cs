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
using System.Threading;

namespace PlannerHRN
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		class ProcessFCFS
		{
			public int id { get; set; }
			public int cpu { get; set; }			
			public int waitingTime{ get; set; }
			public int burstTime{ get; set; }			
		}

		class ProcessHRN : IComparable<ProcessHRN>
		{
			public int id { get; set; }
			public int cpu { get; set; }		
			public int waitingTime { get; set; }
			public int burstTime { get; set; }
			public double responseRatio { get; set; }			
			public int CompareTo(ProcessHRN x)
			{
				return x.responseRatio.CompareTo(responseRatio);
			}
		}
		List<ProcessFCFS> processesFCFS = new List<ProcessFCFS>();
		List<ProcessHRN> proccessesHRN = new List<ProcessHRN>();		
		Random rand = new Random();
		int lastIndex = 0, algo = 0;
		public MainWindow()
		{
			InitializeComponent();
			
			new Thread(() =>
			{
				Thread.CurrentThread.IsBackground = true;
				while (true)
				{
					if(algo == 0)
					{
						foreach (ProcessFCFS el in processesFCFS)
						{
							el.waitingTime++;							
						}						
						myDataGrid.Dispatcher.BeginInvoke((Action)(() =>
						{
							myDataGrid.ItemsSource = null;
							myDataGrid.ItemsSource = processesFCFS;
							if (processesFCFS.Count > 0)
							{
								if (processesFCFS[0].burstTime > 1)
								{
									processesFCFS[0].burstTime--;
									processesFCFS[0].waitingTime--;
								}
								else
								{
									processesFCFS.RemoveAt(0);
									if (processesFCFS.Count > 0)
										processesFCFS[0].cpu = 100;
								}
							}

						}));
						Thread.Sleep(500);
					}
					else if(algo == 1)
					{
						foreach (ProcessHRN el in proccessesHRN)
						{
							el.waitingTime++;
							el.responseRatio = 1.0 * (el.waitingTime + el.burstTime) / el.burstTime;
							el.responseRatio = Math.Truncate(el.responseRatio * 1000) / 1000;
							//el.cpu = Math.Abs(el.cpu + rand.Next() % 200 - 100) % 100;						
						}
						if (proccessesHRN.Count > 0)
						{
							proccessesHRN[0].cpu = 0;
							proccessesHRN.Sort();
							proccessesHRN[0].cpu = 100;
						}
						myDataGrid.Dispatcher.BeginInvoke((Action)(() =>
						{
							myDataGrid.ItemsSource = null;
							myDataGrid.ItemsSource = proccessesHRN;
							if (proccessesHRN.Count > 0)
							{
								if (proccessesHRN[0].burstTime > 1)
								{
									proccessesHRN[0].burstTime--;
									proccessesHRN[0].waitingTime--;
								}
								else
								{
									proccessesHRN.RemoveAt(0);
								}
							}

						}));
						Thread.Sleep(500);
					}
				}					
			}).Start();
			
		}

		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if(algo == 1)
			{
				proccessesHRN.Add(new ProcessHRN
				{
					id = lastIndex++,//(a.Count == 0) ? 0:a[a.Count-1].id+1,
					cpu = 0,
					waitingTime = 0,
					//cpu = rand.Next() % 100,				
					burstTime = 1 + rand.Next() % 100,
					responseRatio = 0
				});
				proccessesHRN[0].cpu = 0;
				proccessesHRN.Sort();
				proccessesHRN[0].cpu = 100;
				myDataGrid.ItemsSource = null;
				myDataGrid.ItemsSource = proccessesHRN;
			}			
			else if(algo == 0)
			{
				processesFCFS.Add(new ProcessFCFS
				{
					id = lastIndex++,//(a.Count == 0) ? 0:a[a.Count-1].id+1,
					cpu = 0,
					waitingTime = 0,					
					burstTime = 1 + rand.Next() % 30					
				});
				processesFCFS[0].cpu = 100;								
				myDataGrid.ItemsSource = null;
				myDataGrid.ItemsSource = processesFCFS;
			}
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			string inputTime = textBox1.GetLineText(0);
			int result = -1;
			try
			{
				result = Int32.Parse(inputTime);				
			}
			catch (FormatException)
			{
				Console.WriteLine($"Unable to parse '{inputTime}'");
			}
			if (result != -1)
			{
				if(algo == 0)
				{
					processesFCFS.Add(new ProcessFCFS
					{
						id = lastIndex++,//(a.Count == 0) ? 0 : a[a.Count - 1].id + 1,
						cpu = 0,
						waitingTime = 0,
						burstTime = result						
					});
					myDataGrid.ItemsSource = null;
					myDataGrid.ItemsSource = processesFCFS;
				}
				else if(algo == 1)
				{
					proccessesHRN.Add(new ProcessHRN
					{
						id = lastIndex++,//(a.Count == 0) ? 0 : a[a.Count - 1].id + 1,
						cpu = 0,
						waitingTime = 0,
						//cpu = rand.Next() % 100,
						burstTime = result,
						responseRatio = 0
					});
					proccessesHRN[0].cpu = 0;
					proccessesHRN.Sort();
					proccessesHRN[0].cpu = 100;
					myDataGrid.ItemsSource = null;
					myDataGrid.ItemsSource = proccessesHRN;
				}				
			}
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (algo != 0)
			{
				//a.Clear();
				myDataGrid.ItemsSource = null;
			}				
			algo = 0;
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			if(algo != 1)
			{
				//processesFCFS.Clear();
				myDataGrid.ItemsSource = null;
			}				
			algo = 1;
		}
	}
}