using RecordOfTasks.Repository;
using RecordOfTasks.Repository.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RecordOfTasks.Services.Services
{
    /// <summary>
    /// Provides services for managing messages related to tasks, including creation, editing, 
    /// deletion, and retrieval of messages.
    /// </summary>
    public class MessageService
    {
        private readonly DatabaseService _databaseService;

        /// <summary>
        /// Gets or sets the error message, if validation fails.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageService"/> class with a reference 
        /// to the <see cref="DatabaseService"/>.
        /// </summary>
        /// <param name="databaseService">The database service for interacting with the database.</param>
        public MessageService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Creates a new message and saves it to the database after validation.
        /// </summary>
        /// <param name="message">The message object to be created.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <remarks>
        /// If the message fails validation, it sets the <see cref="ErrorMessage"/> property.
        /// </remarks>
        public async Task CreateMessageAsync(Message message)
        {
            ErrorMessage = null;
            ValidateMessage(message);
            if (ErrorMessage == null)
            {
                await _databaseService.CreateMessageAsync(message);
            }
        }

        /// <summary>
        /// Edits an existing message in the database after validation.
        /// </summary>
        /// <param name="message">The message object to be edited.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <remarks>
        /// If the message fails validation, it sets the <see cref="ErrorMessage"/> property.
        /// </remarks>
        public async Task EditMessageAsync(Message message)
        {
            ErrorMessage = null;
            ValidateMessage(message);
            if (ErrorMessage == null)
            {
                await _databaseService.EditMessageAsync(message);
            }
        }

        /// <summary>
        /// Deletes a message from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the message to be deleted.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task DeleteMessageByIdAsync(int id)
        {
            ErrorMessage = null;
            await _databaseService.DeleteMessageByIdAsync(id);
        }

        /// <summary>
        /// Retrieves all messages associated with a specific task by task ID.
        /// </summary>
        /// <param name="taskId">The ID of the task to retrieve messages for.</param>
        /// <returns>A list of messages related to the specified task.</returns>
        public async Task<List<Message>> GetMessagesByTaskIdAsync(int taskId)
        {
            ErrorMessage = null;
            return await _databaseService.GetMessagesByTaskIdAsync(taskId);
        }

        /// <summary>
        /// Validates a message object, checking if it meets the required criteria.
        /// </summary>
        /// <param name="message">The message object to be validated.</param>
        /// <remarks>
        /// If the message fails validation, the <see cref="ErrorMessage"/> property is set with 
        /// the validation errors.
        /// </remarks>
        private void ValidateMessage(Message message)
        {
            var context = new ValidationContext(message);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(message, context, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                ErrorMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
            }
        }
    }
}
