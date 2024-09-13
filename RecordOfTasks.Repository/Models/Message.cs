using System.ComponentModel.DataAnnotations;

namespace RecordOfTasks.Repository.Models
{
    /// <summary>
    /// Represents a message associated with a task.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// A unique identifier for the message.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A unique identifier of the associated task.
        /// This links the message to a specific task.
        /// </summary>
        [Required(ErrorMessage = "Task ID is required")]
        public int TaskId { get; set; }

        /// <summary>
        /// The name of the user who created the message.
        /// </summary>
        [Required(ErrorMessage = "User name is required")]
        [StringLength(50, ErrorMessage = "User name cannot exceed 50 characters")]
        public string NameUser { get; set; }

        /// <summary>
        /// The content of the message.
        /// This represents the text or information conveyed in the message.
        /// </summary>
        [Required(ErrorMessage = "Message text is required")]
        [StringLength(1000, ErrorMessage = "Message text cannot exceed 1000 characters")]
        public string MessageText { get; set; }

        /// <summary>
        /// The timestamp indicating when the message was created.
        /// Defaults to the current date and time when the message is instantiated.
        /// </summary>
        public DateTime Timestamp { get; } = DateTime.Now;
    }
}