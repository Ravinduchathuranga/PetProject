using SQLite;

namespace PetProject
{
    [Table("Task")]
    public class TaskModel
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("taskName")]
        public string TaskName { get; set; } = string.Empty;

        [Column("taskStatus")]
        public bool TaskStatus { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, TaskName: {TaskName}, TaskStatus: {TaskStatus}";
        }
    }
}