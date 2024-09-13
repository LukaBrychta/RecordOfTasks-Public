using System.ComponentModel.DataAnnotations;

namespace RecordOfTasks.Repository.Models
{
    /// <summary>
    /// Represents an item in a checklist associated with a task.
    /// </summary>
    public class ChecklistItem
    {
        /// <summary>
        /// A unique identifier for the checklist item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A unique identifier of the associated task.
        /// This links the checklist item to a specific task.
        /// </summary>
        [Required(ErrorMessage = "Task ID is required")] 
        public int TaskId { get; set; }

        /// <summary>
        /// A description of the checklist item.
        /// This provides details about the specific action or requirement.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")] 
        public string Description { get; set; }

        /// <summary>
        /// A value indicating whether the checklist item is completed.
        /// True if the item is completed, otherwise false.
        /// </summary>
        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// A due date for the checklist item.
        /// This is the deadline by which the item should be completed.
        /// </summary>
        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime DueDate { get; set; } = DateTime.Today;
    }
}