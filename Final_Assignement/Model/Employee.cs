using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Assignement.Model
{
    class Employee : ViewModel.ViewModelBase
    {
        private string _strEmployeeName = string.Empty;
        public string EmployeeName
        {
            get { return _strEmployeeName; }
            set { _strEmployeeName = value; Notify(); }
        }

        private string manager=string.Empty;
        public string Manager
        {
            get { return manager; }
            set { manager = value; Notify(); }
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

        public Employee(string employeename)
        {
            EmployeeName = employeename;
        }
    }
}
