using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DataAccess;
using Entities;

namespace Gui
{
    /// <summary>
    /// Interaction logic for OrderAdd.xaml
    /// </summary>
    public partial class OrderAdd : Window
    {
        public CleaningOrderRepository CleaningOrderRepository = new();
        public CleaningAppointmentRepository CleaningAppointmentRepository = new();

        public int IdOrder { get; set; }
        public bool CompanyOrder { get; set; }
        public OrderAdd(int idorder, bool companyorder)
        {
            InitializeComponent();

            this.IdOrder = idorder;
            this.CompanyOrder = companyorder;

            CleaningOrderList = new();
            DataContext = this;
            LoadOrders();

            CleaningAppointmentList = new();
            DataContext = this;
            LoadAppointments();
        }
        private void LoadOrders()
        {
            CleaningOrderRepository cleaningorderrepository = new();

            List<CleaningOrder> cleaningorders = cleaningorderrepository.GetAll();

            CleaningOrderList.Clear();

            foreach (CleaningOrder cleaningorder in cleaningorders)
            {
                CleaningOrderList.Add(cleaningorder);
            }
        }
        private void LoadAppointments()
            {
                CleaningAppointmentRepository cleaningappointmentrepository = new();
                List<CleaningAppointment> cleaningappointments = cleaningappointmentrepository.GetAll();
                CleaningAppointmentList.Clear();

                foreach (CleaningAppointment cleaningappointment in cleaningappointments)
                {
                    CleaningAppointmentList.Add(cleaningappointment);
                }
            }
        public ObservableCollection<CleaningOrder> CleaningOrderList { get; set; }
        public ObservableCollection<CleaningAppointment> CleaningAppointmentList { get; set; }

        private void Back(object sender, RoutedEventArgs e)
        {
            Appointments win1 = new(1, false, false);
            this.Visibility = Visibility.Hidden;
            win1.Show();
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            foreach (CleaningOrder cleaningorder in CleaningOrderList)
            {
                if (Convert.ToDateTime(CleaningDateBox.Text) == cleaningorder.CleaningDate && IdOrder == cleaningorder.OrderId)
                {
                    MessageBox.Show("you already have an order on this date", "Date order", MessageBoxButton.OK);
                    goto outer;
                }
            }
                    // Checks if all requirements for input is true if yes then asks you if you want to add it
                    if (CleaningDateBox.Text != null && HoursBox.Text.All(char.IsDigit))
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you Sure you want to Add this Order at date {CleaningDateBox.Text} for {HoursBox.Text} Hours", "Register Appointment", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            
                                //Makes a new Order object and distributes values to
                                //it then adds it to the database via Repository
                                CleaningOrder CleaningOrderAdd = new()
                                {
                                    OrderId = IdOrder,
                                    CleaningDate = Convert.ToDateTime(CleaningDateBox.SelectedDate.Value),
                                    Hours = Convert.ToInt32(HoursBox.Text),
                                    Completion = false,
                                    Company = CompanyOrder
                                };
                                CleaningOrderRepository.Add(CleaningOrderAdd);
                                Appointments win1 = new(1, false, false);
                                this.Visibility = Visibility.Hidden;
                                win1.Show();
                            }
                      
                    }
                    else
                    {
                       MessageBox.Show("You are missing input in one or several fields", "Missing Input", MessageBoxButton.OK);
                    }

        outer:;
        }
    }
}
