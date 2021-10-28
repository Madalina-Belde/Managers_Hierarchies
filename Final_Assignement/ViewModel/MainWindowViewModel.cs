using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Final_Assignement.Command;
using Final_Assignement.Model;
using static Final_Assignement.Model.Employee;

namespace Final_Assignement.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private bool isAddManagerPanelVisible;
        public bool IsAddManagerPanelVisible
        {
            get { return isAddManagerPanelVisible; }
            set { isAddManagerPanelVisible = value; Notify(); }
        }

        private bool isAddEmployeePanelVisible;
        public bool IsAddEmployeePanelVisible
        {
            get { return isAddEmployeePanelVisible; }
            set { isAddEmployeePanelVisible = value; Notify(); }
        }

        private bool isErrorMessageVisible;
        public bool IsErrorMessageVisible
        {
            get { return isErrorMessageVisible; }
            set { isErrorMessageVisible = value; Notify(); }
        }

        private string managerName=string.Empty;
        public string ManagerName
        {
            get { return managerName; }
            set { managerName = value; Notify(); }
        }

        private string employeeName=string.Empty;
        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; Notify(); }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; Notify(); }
        }

        private Manager selectedManager;
        public Manager SelectedManager
        {
            get { return selectedManager; }
            set { selectedManager = value; Notify(); }
        }

        private string department=string.Empty;
        public string Department
        {
            get { return department; }
            set { department = value; Notify(); }
        }

        private string phoneNumber=string.Empty;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; Notify(); }
        }

        public ICommand AddNewManagerCommand { get; private set; }
        public ICommand CloseErrorMessageCommand { get; private set; }
        public ICommand AddEmployeeCommand { get; private set; }
        public ICommand AddNewEmployeeCommand { get; private set; }

        private Manager _manager;
        public Manager Manager
        {
            get
            {
                return _manager;
            }
            set
            {
                _manager = value;
                Notify();
            }
        }
        private ObservableCollection<Manager> managers;

        public MainWindowViewModel()
        {

            Manager = new Manager("M1");

            Managers = new ObservableCollection<Manager>()
            {
                new Manager("M2", new ObservableCollection<Employee>()
                {
                    new Employee("Bun Marian"){ Department="HR", Manager="M1", PhoneNumber="021678243"},
                    new Employee("Popescu Alina"){ Department="ADAS", Manager="M3", PhoneNumber="0721354765"},
                    new Employee("Covaci Alin"){ Department="INF", Manager="M2", PhoneNumber="0786235987"}
                }),
                new Manager("M3", new ObservableCollection<Employee>()
                { 
                    new Employee("Dutu Cristian") { Department = "VNI", Manager = "M3", PhoneNumber = "0745921345" }, 
                    new Employee("Rusu Ioana"){ Department="IBS", Manager="M2", PhoneNumber="0257354926"}
                }),
                new Manager("M4", new ObservableCollection<Employee>()
                { 
                    new Employee("Pop Ioan"){ Department = "SWP", Manager = "M1", PhoneNumber = "0745921675" },
                    new Employee("Ilie Dana"){ Department = "VNI", Manager = "M2", PhoneNumber = "0745354045"} })
            };
            AddNewManagerCommand = new RelayCommand(AddNewManager, param => !string.IsNullOrEmpty(ManagerName));
            AddNewEmployeeCommand = new RelayCommand(AddNewEmployee, param => !string.IsNullOrEmpty(EmployeeName));
            AddEmployeeCommand = new RelayCommand(AddEmployee, CanAddEmployee);
            CloseErrorMessageCommand = new RelayCommand(param => IsErrorMessageVisible = false, param => true);
        }

        public ObservableCollection<Manager> Managers
        {
            get
            {
                return managers;
            }
            set
            {
                managers = value;
                Notify();
            }
        }

        /* Add manager procedure */
        private ICommand _AddManagerCommand;
        public ICommand AddManagerCommand
        {
            get
            {
                if(_AddManagerCommand == null)
                {
                    _AddManagerCommand = new RelayCommand(AddManagerExecute, CanAddManagerExecute, false);
                }
                return _AddManagerCommand;
            }
        }

        private void AddEmployee(object param)
        {
            if (SelectedManager == null)
            {
                IsErrorMessageVisible = true;
                ErrorMessage = "A manager must be selected first!";
            }
            else
            {
                IsAddEmployeePanelVisible = true;
                EmployeeName = string.Empty;
            }
        }

        private bool CanAddEmployee(object param)
        {
            return true;
        }

        private void AddNewEmployee(object param)
        {
            if (SelectedManager != null && SelectedManager.Employees != null)
            {
                foreach (Employee employee in SelectedManager.Employees)
                {
                    if (employee.EmployeeName.ToLower() == EmployeeName.ToLower())
                    {
                        IsErrorMessageVisible = true;
                        ErrorMessage = "An employee with the same name already exists for the selected manager!";
                        break;
                    }
                }
            }
            else
            {
                if(SelectedManager.Employees==null)
                {
                    SelectedManager.Employees = new ObservableCollection<Employee>();
                }
                else
                {
                    if(SelectedManager==null)
                    {
                        return;
                    }
                }
            }
            
            if(!IsNumeric(PhoneNumber))
            {
                IsErrorMessageVisible = true;
                ErrorMessage = "Please introduce a correct phone number!";
                return;
            }
            SelectedManager.Employees.Add(new Employee(EmployeeName)
            {
                Manager = SelectedManager.ManagerName,
                Department = Department,
                EmployeeName = EmployeeName,
                PhoneNumber = PhoneNumber
            });
            IsAddEmployeePanelVisible = false;
            EmployeeName = string.Empty;
            Department = string.Empty;
            PhoneNumber = string.Empty;
            SelectedManager = null;
        }

        public static bool IsNumeric(string input)
        {
            int number;
            return int.TryParse(input, out number);
        }

        private void AddManagerExecute(object parameter)
        {
            IsAddManagerPanelVisible = true;

            /* add this Manager object to the Managers collection */
            return;
            Managers.Add(Manager);
        }

        private void AddNewManager(object param)
        {
            foreach (Manager manager in Managers)
            {
                if (manager.ManagerName.ToLower() == ManagerName.ToLower())
                {
                    IsErrorMessageVisible = true;
                    ErrorMessage = "A manager with the same name already exists!";
                    break;
                }
            }
            Managers.Add(new Manager(ManagerName));
            IsAddManagerPanelVisible = false;
            ManagerName = string.Empty;
            SelectedManager = null;
        }

        private bool CanAddManagerExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Manager.ManagerName);
        }

        /* Add employee procedure */

        private ObservableCollection<Employee> employees;

        /* this is the property */
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                Notify();
            }
        }

        private Employee _emp;
        public Employee Employee
        {
            get
            {
                return _emp;
            }
            set
            {
                _emp = value;
                Notify();
            }
        }
    }
}
