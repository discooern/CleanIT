using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entities;
using DataAccess;

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PrivatePersonRepository PrivatePersonRepository = new();
        public CompanyRepository CompanyRepository = new();
        public CleaningAppointmentRepository CleaningAppointmentRepository = new();
        public const int PrivatePrice = 150;
        public MainWindow()
        {
            InitializeComponent();

            // Runs FillComboBox1 method to fil combobox1 with content
            PrivatePersonList = new();
            DataContext = this;
            FillComboBox1();

            // Runs FillComboBox2 method to fil combobox2 with content
            CompanyList = new();
            DataContext = this;
            FillComboBox2();



        }
        // Creates A PrivatePerson List then uses
        // privatepersonrepository.GetAll method
        // to get all rows from privatepersons table in database
        private void FillComboBox1()
        {
            PrivatePersonRepository privatepersonrepository = new();

            List<PrivatePerson> privatepersons = privatepersonrepository.GetAll();

            PrivatePersonList.Clear();

            foreach (PrivatePerson privateperson in privatepersons)
            {
                PrivatePersonList.Add(privateperson);
            }
        }
        // Creates A PrivatePerson List then uses
        // CompanyRepository.GetAll method
        // to get all rows from Companies table in database
        private void FillComboBox2()
        {
            CompanyRepository = new();

            List<Company> companies = CompanyRepository.GetAll();

            CompanyList.Clear();

            foreach (Company company in companies)
            {
                CompanyList.Add(company);
            }
        }

        public ObservableCollection<PrivatePerson> PrivatePersonList { get; set; }
        public ObservableCollection<Company> CompanyList { get; set; }
        public ObservableCollection<CleaningAppointment> CleaningAppointmentList { get; set; }

        private void AddCompany(object sender, RoutedEventArgs e)
        {
            // Adds new Company to the database and combobox
            foreach (Company company in CompanyList)
            {
                // Checks if a customer with a phonenumber you are
                // trying to add already exists within the database
                if (CompanyPhoneBox.Text == Convert.ToString(company.Phone))
                {
                    MessageBox.Show("The PhoneNumber you are trying to use is already in use by another customer please try another", "PhoneNumber", MessageBoxButton.OK);
                    goto outer;
                }
            }
            // Checks if the input boxes has the correct datatype values
            try
            {
                if (CompanyNameBox.Text.Length > 0 && SeNumberBox.Text.Length > 0 && CompanyPhoneBox.Text.Length > 0)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you Sure you want to Register Your Company as {CompanyNameBox.Text} with phone number {CompanyPhoneBox.Text}", "Register", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Makes a new Company object and distributes values to it
                        // then adds it to the database via repository
                        Company CompanyAdd = new()
                        {
                            CompanyName = CompanyNameBox.Text,
                            SeNumber = Convert.ToInt32(SeNumberBox.Text),
                            Phone = Convert.ToInt32(CompanyPhoneBox.Text)
                        };
                        CompanyRepository.Add(CompanyAdd);
                        FillComboBox2();
                        Clear();
                    }
                }
                else
                {
                    MessageBox.Show("You are missing input in one or several fields", "Missing Input", MessageBoxButton.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Input Please Try Again", "Error", MessageBoxButton.OK);
            }
        outer:;
        }


        // Adds new PrivatePerson to database And combobox
        private void AddPrivatePerson(object sender, RoutedEventArgs e)
        {
            // Checks if a customer with a phonenumber you are
            // trying to add already exists within the database
            foreach (PrivatePerson privatePerson in PrivatePersonList)
            {
                if (privatePerson.Phone == Convert.ToInt32(PhoneBox.Text))
                {
                    MessageBox.Show("The PhoneNumber you are trying to use is already in use by another customer please try another", "PhoneNumber", MessageBoxButton.OK);
                    goto outer;
                }
            }

            // Checks if the input boxes has the correct datatype values
            try
            {
                // Checks if input boxes has input
                if (FirstnameBox.Text.Length > 0 && LastnameBox.Text.Length > 0 && AddressBox.Text.Length > 0 && PostalcodeBox.Text.Length > 0 && PhoneBox.Text.Length > 0)
                {
                    MessageBoxResult result = MessageBox.Show($"Are you Sure you want to Register as {FirstnameBox.Text} with phone number {PhoneBox.Text}", "Register", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        // Makes a new privateperson object and distributes values to it
                        // then adds it to the database via repository
                        PrivatePerson PrivatePersonAdd = new()
                        {
                            Firstname = FirstnameBox.Text,
                            Lastname = LastnameBox.Text,
                            Address = AddressBox.Text,
                            Postalcode = Convert.ToInt32(PostalcodeBox.Text),
                            Phone = Convert.ToInt32(PhoneBox.Text)
                        };
                        PrivatePersonRepository.Add(PrivatePersonAdd);
                        FillComboBox1();
                        Clear();
                    }
                }
                else
                {
                    MessageBox.Show("You are missing input in one or several fields", "Missing Input", MessageBoxButton.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Input Please Try Again", "Error", MessageBoxButton.OK);
            }
        outer:;
        }

        // Adds appointment to cleaningappointments
        private void AddAppointmentBtn(object sender, RoutedEventArgs e)
        {
            // Checks if all requirements for input is true if yes then asks you if you want to add it
            if (AppointmentDescBox.Text.Length > 0 && HourPriceBox.Text.All(char.IsDigit) && HourPriceBox.Text.Length > 0 && (PrivatePersonBox.SelectedIndex > -1 || CompanyBox.SelectedIndex > -1))
            {
                MessageBoxResult result = MessageBox.Show($"Are you Sure you want to Add this Appointment with Hourly Price of {HourPriceBox.Text}", "Register Appointment", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    // checks if user has selected a PrivatePerson
                    if (PrivatePersonBox.SelectedIndex > -1)
                    {
                        //Makes a new appointment object and distributes values to
                        //it then adds it to the database via Repository
                        PrivatePerson selectedPrivatePerson = PrivatePersonBox.SelectedItem as PrivatePerson;
                        CleaningAppointment CleaningAppointmentAdd = new()
                        {
                            CustomerId = selectedPrivatePerson.PrivatePersonId,
                            Company = false,
                            HourPrice = PrivatePrice,
                            AppointmentDesc = AppointmentDescBox.Text
                        };
                        CleaningAppointmentRepository.Add(CleaningAppointmentAdd);
                        Clear();
                        Appointments win1 = new(CleaningAppointmentAdd.CustomerId, CleaningAppointmentAdd.Company, true);
                        this.Visibility = Visibility.Hidden;
                        win1.Show();
                    }
                    // checks if user has selected a Company
                    else if (CompanyBox.SelectedIndex > -1)
                    {
                        //Makes a new appointment object and distributes values to
                        //it then adds it to the database via Repository
                        Company selectedCompany = CompanyBox.SelectedItem as Company;
                        CleaningAppointment CleaningAppointmentAdd = new()
                        {
                            CustomerId = selectedCompany.CompanyId,
                            Company = true,
                            HourPrice = Convert.ToInt32(HourPriceBox.Text),
                            AppointmentDesc = AppointmentDescBox.Text
                        };
                        CleaningAppointmentRepository.Add(CleaningAppointmentAdd);
                        Clear();
                        Appointments win1 = new(CleaningAppointmentAdd.CompanyId, CleaningAppointmentAdd.Company, true);
                        this.Visibility = Visibility.Hidden;
                        win1.Show();
                    }

                }
            }
            else
            {
                MessageBox.Show("You are missing input in one or several fields", "Missing Input", MessageBoxButton.OK);
            }
        }
        //Basic clear method *Clears all textboxes*
    private void Clear()
        {
            PrivatePersonBox.SelectedIndex = -1;
            FirstnameBox.Text = "";
            LastnameBox.Text = "";
            AddressBox.Text = "";
            PostalcodeBox.Text = "";
            PhoneBox.Text = "";

            CompanyBox.SelectedIndex = -1;
            CompanyNameBox.Text = "";
            SeNumberBox.Text = "";
            CompanyPhoneBox.Text = "";

            AppointmentDescBox.Text = "";
            HourPriceBox.Text = "";
        }

        // Makes sure that only 1 combobox can have a selection at a time
        private void PrivatePersonBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PrivatePersonBox.SelectedIndex > -1)
            {
                CompanyBox.SelectedIndex = -1;
                HourPriceBox.IsReadOnly = true;
                HourPriceBox.Text = PrivatePrice.ToString();
                AppointmentDescBox.Text = "";
            }
        }
        // Makes sure that only 1 combobox can have a selection at a time
        private void CompanyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CompanyBox.SelectedIndex > -1)
            {
                PrivatePersonBox.SelectedIndex = -1;
                HourPriceBox.IsReadOnly = false;
                HourPriceBox.Text = "";
                AppointmentDescBox.Text = "";
            }
        }

        // button to see appointments
        private void AppointmentsBtn(object sender, RoutedEventArgs e)
        {
            Appointments win1 = new(1, false, false);
            this.Visibility = Visibility.Hidden;
            win1.Show();
        }
        
    }
}
