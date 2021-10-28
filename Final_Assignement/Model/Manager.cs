using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignement.Model
{
    class Manager : ViewModel.ViewModelBase
    {
        private ObservableCollection<Employee> employees;

        public string ManagerName
        {
            get;
            set;
        }
        public Manager(string managerName, ObservableCollection<Employee> assignedEmployeeList)
        {
            ManagerName = managerName;
            employees = assignedEmployeeList;
        }

        public Manager(string managerName)
        {
            ManagerName = managerName;
        }

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
                Notify("Employees");
            }
        }
    }
}
