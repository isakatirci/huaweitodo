using System;
using System.Linq;
using Huawei.ToDo.Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Huawei.Todo.UnitTest
{
    [TestClass]
    public class TodoListItemUnitTest
    {
        [TestMethod]
        public void is_properly_add()
        {
            var _myContext = new ToDoDb();
            var toDoList = new ToDoList();
            _myContext.ToDoList.Add(toDoList);
            var insertItem = new TodoListItem { Name = "Name1", ToDoListId = toDoList.Id };
            _myContext.TodoListItem.Add(insertItem);
            _myContext.SaveChanges();
            var result = _myContext.TodoListItem.FirstOrDefault(x => x.Id == insertItem.Id);
            Assert.AreEqual(result == null, false);
        }
        [TestMethod]
        public void is_properly_deleted()
        {
            var _myContext = new ToDoDb();
            var toDoList = new ToDoList();
            _myContext.ToDoList.Add(toDoList);
            var insertItem = new TodoListItem { Name = "Name1", ToDoListId = toDoList.Id };
            _myContext.TodoListItem.Add(insertItem);
            _myContext.SaveChanges();
            var item = _myContext.TodoListItem.FirstOrDefault(x => x.Id == insertItem.Id);
            _myContext.TodoListItem.Remove(item);
            _myContext.SaveChanges();
            var result = _myContext.TodoListItem.FirstOrDefault(x => x.Id == insertItem.Id);
            Assert.AreEqual(result == null, true);
        }
        [TestMethod]
        public void is_properly_updated()
        {
            var _myContext = new ToDoDb();
            var toDoList = new ToDoList();
            _myContext.ToDoList.Add(toDoList);
            var insertItem = new TodoListItem { Name = "Name1", ToDoListId = toDoList.Id };
            _myContext.TodoListItem.Add(insertItem);
            _myContext.SaveChanges();
            var item = _myContext.TodoListItem.FirstOrDefault(x => x.Id == insertItem.Id);
            item.Name = "Name2";
            _myContext.SaveChanges();
            var result = _myContext.TodoListItem.FirstOrDefault(x => x.Name == "Name1" && x.Id == insertItem.Id);
            Assert.AreEqual(result == null, true);
        }
    }
}
