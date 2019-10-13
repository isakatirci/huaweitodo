using Huawei.Todo.WPF.Helpers;
using Huawei.ToDo.Entities.Models;
using Huawei.ToDo.Service.Abstract;
using Huawei.ToDo.Service.Concrete;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Huawei.Todo.WPF.Models
{
    public class ToDoListItemModel {

        private string _name;
        private string _description;
        private string _deadline;
        private string _status;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return _name; }
            set { _name = value; this.OnPropertyChanged("Name"); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; this.OnPropertyChanged("Description"); }
        }

        public string Deadline
        {
            get { return _deadline; }
            set { _deadline = value; this.OnPropertyChanged("Deadline"); }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; this.OnPropertyChanged("Status"); }
        }

        public int Id { get; internal set; }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
