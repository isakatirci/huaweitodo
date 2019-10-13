namespace Huawei.ToDo.Entities.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TodoListItem")]
    public partial class TodoListItem
    {
        public int Id { get; set; }

        public int ToDoListId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Deadline { get; set; }

        public string Status { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateDate { get; set; }

        public virtual ToDoList ToDoList { get; set; }
    }
}
