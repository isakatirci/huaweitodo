using Huawei.Todo.WPF.Helpers;
using Huawei.Todo.WPF.Models;
using Huawei.ToDo.Entities.Models;
using Huawei.ToDo.Service.Abstract;
using Huawei.ToDo.Service.Concrete;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Huawei.Todo.WPF.ViewModels
{
    public class ToDoListItemViewModel : BindableBase
    {    
        private ITodoListItemSevice service = new TodoListItemSevice(new ToDoDb());
        private readonly ToDoList _todoList;

        public ObservableCollection<string> StatusList { get; set; }

        public ObservableCollection<ToDoListItemModel> ToDoListItems { get; set; }

        private ToDoListItemModel _toDoListItem;
        public ToDoListItemModel ToDoListItem
        {
            get { return _toDoListItem; }
            set { SetProperty(ref _toDoListItem, value); }
        }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand EditCommand { get; private set; }


        private IEnumerable<ToDoListItemModel> GetAll(int toDoListId)
        {

            return service.GetAll(x => x.ToDoListId == toDoListId).Select(x =>
             new ToDoListItemModel
             {
                 Deadline = x.Deadline.HasValue ? x.Deadline.Value.ToString("dd.MM.yyyy") : "",
                 Description = x.Description,
                 Name = x.Name,
                 Status = x.Status,
                 Id = x.Id
             });
        }

        public ToDoListItemViewModel(ToDoList todoList)
        {
            ToDoListItems = new ObservableCollection<ToDoListItemModel>();
            StatusList = new ObservableCollection<string>();
            StatusList.AddRange(new string[] { "Complete", "Expired", "Cancelled" });
            ToDoListItems.AddRange(GetAll(todoList.Id));
            ToDoListItem = new ToDoListItemModel();
            SaveCommand = new DelegateCommand<Object>(Save);
            DeleteCommand = new DelegateCommand<Object>(Delete);
            EditCommand = new DelegateCommand<Object>(Edit);
            _todoList = todoList;
        }

        private void Edit(object obj)
        {
            Helper.InvokeAction(() => {

                System.Collections.IList items = (System.Collections.IList)obj;
                var selectedItems = items.Cast<ToDoListItemModel>(); 

                if (selectedItems.Count() ==  0)
                {
                    Helper.Error("Item not selected");
                    return;
                }

                int id = selectedItems.First().Id;

                var temp = service.FirstOrDefault(x => x.Id == id);
                if (temp == null)
                {
                    Helper.Error("Item not found");
                    return;
                }           

                ToDoListItem = new ToDoListItemModel() {
                    Id = temp.Id,
                    Deadline = temp.Deadline.HasValue ? temp.Deadline.Value.ToString("dd.MM.yyyy") : "",
                    Description = temp.Description,
                    Name = temp.Name,
                    Status = temp.Status                    
                };
            });
        }

        private void Delete(object obj)
        {
            Helper.InvokeAction(() => {

                System.Collections.IList items = (System.Collections.IList)obj;
                var selectedItems = items.Cast<ToDoListItemModel>();

                if (selectedItems.Count() == 0)
                {
                    Helper.Error("Item not selected");
                    return;
                }
                int id = selectedItems.First().Id;
                var temp = service.FirstOrDefault(x => x.Id == id);
                if (temp == null)
                {
                    Helper.Error("Item not found");
                    return;
                }
                service.Remove(temp);
                service.SaveChanges();
                ToDoListItems.Clear();
                ToDoListItem = new ToDoListItemModel();
                ToDoListItems.AddRange(GetAll(_todoList.Id));
            });
        }

        private void Save(object obj)
        {
            Helper.InvokeAction(() => {

                DateTime now = Helper.DateParse(this.ToDoListItem.Deadline);
                int id = this.ToDoListItem.Id;
                var temp = service.FirstOrDefault(x=>x.Id == id);
                if (temp == null)
                {
                    temp = new TodoListItem();                             
                }
                temp.Name = this.ToDoListItem.Name;
                temp.Deadline = now;
                temp.Status = this.ToDoListItem.Status;               
                temp.Description = this.ToDoListItem.Description;
                temp.ToDoListId = _todoList.Id;
                if (id == 0)
                {
                    temp.CreateDate = DateTime.Now;
                    service.Add(temp);
                }
                service.SaveChanges();
                ToDoListItems.Clear();
                ToDoListItem = new ToDoListItemModel();
                ToDoListItems.AddRange(GetAll(_todoList.Id));
            });
        }


    
    }
}
