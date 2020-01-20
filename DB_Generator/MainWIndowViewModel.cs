using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DB_Generator
{
    class MainWIndowViewModel : INotifyPropertyChanged
    {
        public DelegateCommand AddCommand { get; set; }
        public DelegateCommand ReplaceCommand { get; set; }        
        public event PropertyChangedEventHandler PropertyChanged;
        private bool isEnable; 
        public bool doWork = true;         
        private string countriesmessage;        
        private string companiesmessage;        
        private string customersmessage;        
        private string flightsmessage;        
        private string ticketsmessage;        
        private string companies;
        private string customers;
        private string administrators;
        private string flights;
        private string tickets;
        private string countries;
        private int barValue;
        private int maxPb = 100;
        public bool IsEnable
        {
            get
            {
                return isEnable;
            }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
            }
        }
        public int MaxPb
        {
            get
            {
                return maxPb;
            }
            set
            {
                maxPb = value;
                OnPropertyChanged("MaxPb");
            }
        }

        public int BarValue
        {
            get
            {
                return barValue;
            }
            set
            {
                barValue = value;
                OnPropertyChanged("BarValue");
            }
        }

        public string Countriesmessage
        {
            get
            {
                return countriesmessage;
            }
            set
            {
                countriesmessage = value;
                OnPropertyChanged("Countriesmessage");
            }
        }
        public string Companiesmessage
        {
            get
            {
                return companiesmessage;
            }
            set
            {
                companiesmessage = value;
                OnPropertyChanged("Companiesmessage");
            }
        }
        public string Customersmessage
        {
            get
            {
                return customersmessage;
            }
            set
            {
                customersmessage = value;
                OnPropertyChanged("Customersmessage");
            }
        }
        public string Flightsmessage
        {
            get
            {
                return flightsmessage;
            }
            set
            {
                flightsmessage = value;
                OnPropertyChanged("Flightsmessage");
            }
        }
        public string Ticketsmessage
        {
            get
            {
                return ticketsmessage;
            }
            set
            {
                ticketsmessage = value;
                OnPropertyChanged("Ticketsmessage");
            }
        }
        public string Companies
        {
            get
            {
                return companies;                
            }
            set
            {
                companies = value;                
                OnPropertyChanged("Companies"); 
            }
        }
        public string Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
                OnPropertyChanged("Customers");
            }
        }
        public string Administrators
        {
            get
            {
                return administrators;
            }
            set
            {
                administrators = value;
                OnPropertyChanged("Administrators");
            }
        }
        public string Flights
        {
            get
            {
                return flights;
            }
            set
            {
                flights = value;
                OnPropertyChanged("Flights");
            }
        }
        public string Tickets
        {
            get
            {
                return tickets;
            }
            set
            {
                tickets = value;
                OnPropertyChanged("Tickets");
            }
        }
        public string Countries
        {
            get
            {
                return countries;
            }
            set
            {
                countries = value;
                OnPropertyChanged("Countries");
            }
        }



        public MainWIndowViewModel()
        {
            AddCommand = new DelegateCommand(ExecuteAddCommand, CanExecute);
            ReplaceCommand = new DelegateCommand(ExecuteReplaceCommand, CanExecute);
            isEnable = true;
        }

        private void ExecuteReplaceCommand()
        {
            doWork = false;            
            IsEnable = false;            
            DataDB data = new DataDB();
            data.DeleteDB();
            IsEnable = true;           
            BarValue = 0;            
            Countriesmessage = "Data has been deleted!";
            Customersmessage = "";
            Companiesmessage = "";
            Ticketsmessage = "";
            Flightsmessage = "";            
        }

        private bool CanExecute()
        {
            return true;
        }

        private void ExecuteAddCommand()
        {
            doWork = true;
            IsEnable = false;                   
            int total = 0;
            int _Companies = 0;
            int _Customers = 0;
            int _Flights = 0;
            int _Tickets = 0;
            int _Countries = 0;

            try
            {
                _Companies = Convert.ToInt32(Companies);
                _Customers = Convert.ToInt32(Customers);
                _Flights = Convert.ToInt32(Flights);
                _Tickets =  Convert.ToInt32(Tickets);
                _Countries =  Convert.ToInt32(Countries);
                total = _Companies + _Customers + _Flights*_Companies + _Tickets + _Countries;                
            }
            catch (Exception)
            {
                
            }
            DataDB data = new DataDB();

            Task t = Task.Run(() =>
            {
                do
                {
                    BarValue = data.GetCounter() * 100 / total;
                    Countriesmessage = data.getMessegeCountries(_Countries);
                    Companiesmessage = data.getMessegeCompanies(_Companies);
                    Customersmessage = data.getMessegeCustomers(_Customers);
                    Flightsmessage = data.getMessegeFlights(_Flights * _Companies);
                    Ticketsmessage = data.getMessegeTickets(_Tickets);
                    Thread.Sleep(1);

                } while (data.GetCounter() <= total && doWork);
                            

            }); 
            AddDB(data,_Companies,_Customers,_Flights,_Tickets,_Countries, total);            
        }

        private async void AddDB(DataDB data, int companies,int customers,int flights,int tickets, int countries, int total)
        {
            await Task.Run(() =>
                {
                    data.AddCountriesToDB(countries);
                    data.AddCompaniesToDB(companies);
                    data.AddCustomersToDB(customers);
                    data.AddFlightsToDB(flights);
                    data.AddTicketsToDB(tickets);
                    doWork = false;                    
                    IsEnable = true;
                });            
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));               
            }
        }
        

        
    }
}
