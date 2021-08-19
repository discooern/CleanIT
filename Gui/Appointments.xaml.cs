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
    /// Interaction logic for Appointments.xaml
    /// </summary>
    public partial class Appointments : Window
    {
        public int OrderId = 0;
        public CleaningAppointmentRepository CleaningAppointmentRepository = new();
        public CleaningOrderRepository CleaningOrderRepository = new();
        public int Id { get; set; }
        public bool Company { get; set; }
        public bool Specific { get; set; }
        public Appointments(int id, bool company, bool specific)
        {
            InitializeComponent();

            CleaningAppointmentList = new();
            DataContext = this;

            this.Id = id;
            this.Company = company;
            this.Specific = specific;

            if (!Specific)
            {
                CleaningAppointmentRepository cleaningappointmentrepository = new();
                List<CleaningAppointment> cleaningappointments = cleaningappointmentrepository.GetAll();
                CleaningAppointmentList.Clear();

                foreach (CleaningAppointment cleaningappointment in cleaningappointments)
                {
                    CleaningAppointmentList.Add(cleaningappointment);
                }
            }
            else if (Specific && Company)
            {
                CleaningAppointmentRepository cleaningappointmentrepository = new();
                List<CleaningAppointment> cleaningappointments = cleaningappointmentrepository.GetAll();
                CleaningAppointmentList.Clear();

                foreach (CleaningAppointment cleaningappointment in cleaningappointments)
                {
                    if (cleaningappointment.CompanyId == Id)
                    {
                        CleaningAppointmentList.Add(cleaningappointment);
                    }
                }
            }
            else if (Specific && !Company)
            {
                CleaningAppointmentRepository cleaningappointmentrepository = new();
                List<CleaningAppointment> cleaningappointments = cleaningappointmentrepository.GetAll();
                CleaningAppointmentList.Clear();

                foreach (CleaningAppointment cleaningappointment in cleaningappointments)
                {
                    if (cleaningappointment.CustomerId == Id)
                    {
                        CleaningAppointmentList.Add(cleaningappointment);
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win1 = new();
            this.Visibility = Visibility.Hidden;
            Specific = false;
            win1.Show();
        }
        public ObservableCollection<CleaningAppointment> CleaningAppointmentList { get; set; }
        public ObservableCollection<CleaningOrder> CleaningOrderList { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OrderAdd win1 = new(OrderId, Company);
            this.Visibility = Visibility.Hidden;
            win1.Show();
        }

        private void AppointmentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CleaningAppointment selectedcleaningappointment = AppointmentBox.SelectedItem as CleaningAppointment;

            OrderId = selectedcleaningappointment.CleaningAppointmentId;
            Company = selectedcleaningappointment.Company;
        }
    }
}
