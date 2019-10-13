namespace Huawei.ToDo.Entities.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ToDoDb : DbContext
    {
        public ToDoDb()
            : base("name=ToDoDb")
        {
        }

        public virtual DbSet<ToDoList> ToDoList { get; set; }
        public virtual DbSet<TodoListItem> TodoListItem { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoList>()
                .HasMany(e => e.TodoListItem)
                .WithRequired(e => e.ToDoList)
                .WillCascadeOnDelete(false);
        }
    }
}
