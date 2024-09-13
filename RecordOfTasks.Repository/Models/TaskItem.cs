using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecordOfTasks.Repository.Models
{
    /// <summary>
    /// Represents a task item in the system, including details such as company, creator, assignee, and status.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// A unique identifier for the task item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the company associated with the task.
        /// </summary>
        [Required(ErrorMessage = "Company is required")]
        [StringLength(100, ErrorMessage = "Company cannot exceed 100 characters")]
        public string Company { get; set; }

        /// <summary>
        /// A description of the task, outlining what needs to be done.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        /// <summary>
        /// The name of the user who created the task.
        /// </summary>
        [Required(ErrorMessage = "Creator is required")]
        [StringLength(50, ErrorMessage = "Creator name cannot exceed 50 characters")]
        public string Creator { get; set; }

        /// <summary>
        /// The name of the user to whom the task is assigned.
        /// </summary>
        [Required(ErrorMessage = "Assignee is required")]
        [StringLength(50, ErrorMessage = "Assignee name cannot exceed 50 characters")]
        public string Assignee { get; set; }

        /// <summary>
        /// The date the task was reported. Defaults to the current date.
        /// </summary>
        public DateTime ReportedDate { get; } = DateTime.Today;

        /// <summary>
        /// The due date for completing the task. Defaults to the current date.
        /// </summary>
        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime DueDate { get; set; } = DateTime.Today;

        /// <summary>
        /// The priority level of the task. Defaults to <see cref="PriorityLevel.Low"/>.
        /// </summary>
        [Required(ErrorMessage = "Priority is required")]
        [EnumDataType(typeof(PriorityLevel), ErrorMessage = "Invalid priority value. Allowed values are Low, Medium, or High.")]
        public PriorityLevel Priority { get; set; } = PriorityLevel.Low;

        /// <summary>
        /// The current status of the task. Defaults to <see cref="StatusLevel.New"/>.
        /// </summary>
        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(StatusLevel), ErrorMessage = "Invalid status value. Allowed values are New, InProgress, or Completed.")]
        public StatusLevel Status { get; set; } = StatusLevel.New;

        /// <summary>
        /// Represents the priority level of the task, indicating its importance.
        /// </summary>
        public enum PriorityLevel
        {
            /// <summary>
            /// The task has a low priority.
            /// </summary>
            Low,

            /// <summary>
            /// The task has a medium priority.
            /// </summary>
            Medium,

            /// <summary>
            /// The task has a high priority.
            /// </summary>
            High
        }

        /// <summary>
        /// Represents the current status of the task.
        /// </summary>
        public enum StatusLevel
        {
            /// <summary>
            /// The task is newly created and not yet started.
            /// </summary>
            New,

            /// <summary>
            /// The task is currently in progress.
            /// </summary>
            InProgress,

            /// <summary>
            /// The task has been completed.
            /// </summary>
            Completed
        }
    }
}