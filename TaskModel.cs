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

        [Column("taskName")] // Fixed typo in column name from "tastName" to "taskName"
        public string TaskName { get; set; }

        [Column("taskStatus")]
        public bool TaskStatus { get; set; }

        // Added ToString method to prevent deletion-related error
        public override string ToString()
        {
            return $"Id: {Id}, TaskName: {TaskName}, TaskStatus: {TaskStatus}";
        }
    }

}