using RecordOfTasks.Repository;
using RecordOfTasks.Repository.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordOfTasks.Services.Services
{
    /// <summary>
    /// Provides services for managing task items, including creation, editing, deletion, 
    /// and retrieval of tasks.
    /// </summary>
    public class TaskItemService
    {
        private readonly DatabaseService _databaseService;

        /// <summary>
        /// Gets or sets the error message, if validation fails.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItemService"/> class with a reference 
        /// to the <see cref="DatabaseService"/>.
        /// </summary>
        /// <param name="databaseService">The database service for interacting with the database.</param>
        public TaskItemService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Creates a new task item and saves it to the database after validation.
        /// </summary>
        /// <param name="taskItem">The task item to be created.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <remarks>
        /// If the task item fails validation, it sets the <see cref="ErrorMessage"/> property.
        /// </remarks>
        public async Task CreateTaskItemAsync(TaskItem taskItem)
        {
            ErrorMessage = null;
            ValidateTaskItem(taskItem);
            if (ErrorMessage == null)
            {
                await _databaseService.CreateTaskItemAsync(taskItem);
            }
        }

        /// <summary>
        /// Edits an existing task item in the database after validation.
        /// </summary>
        /// <param name="taskItem">The task item to be edited.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <remarks>
        /// If the task item fails validation, it sets the <see cref="ErrorMessage"/> property.
        /// </remarks>
        public async Task EditTaskItemAsync(TaskItem taskItem)
        {
            ErrorMessage = null;
            ValidateTaskItem(taskItem);
            if (ErrorMessage == null)
            {
                await _databaseService.EditTaskItemAsync(taskItem);
            }
        }

        /// <summary>
        /// Deletes a task item from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the task item to be deleted.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task DeleteTaskItemAsync(int id)
        {
            ErrorMessage = null;
            await _databaseService.DeleteTaskItemAsync(id);
        }

        /// <summary>
        /// Retrieves all task items from the database.
        /// </summary>
        /// <returns>A list of all task items.</returns>
        public async Task<List<TaskItem>> GetAllTaskItemsAsync()
        {
            ErrorMessage = null;
            return await _databaseService.GetAllTaskItemsAsync();
        }

        /// <summary>
        /// Retrieves the most recently created task item.
        /// </summary>
        /// <returns>The last task item, or null if none exist.</returns>
        public async Task<TaskItem> GetLastTaskItemAsync()
        {
            ErrorMessage = null;
            return await _databaseService.GetLastTaskItemAsync();
        }

        /// <summary>
        /// Retrieves all task items that are not completed.
        /// </summary>
        /// <returns>A list of not completed task items.</returns>
        public async Task<List<TaskItem>> GetNotCompleteTasksAsync()
        {
            ErrorMessage = null;
            return await _databaseService.GetNotCompleteTasksAsync();
        }

        /// <summary>
        /// Retrieves all task items that are not completed and are past their deadline.
        /// </summary>
        /// <returns>A list of overdue task items that are not completed.</returns>
        public async Task<List<TaskItem>> GetNotCompleteTasksAfterTheDeadlineAsync()
        {
            ErrorMessage = null;
            return await _databaseService.GetNotCompleteTasksAfterTheDeadlineAsync();
        }

        /// <summary>
        /// Retrieves all task items that are not completed, have checklist items, and are past their deadline.
        /// </summary>
        /// <returns>A list of tasks with incomplete checklist items that are overdue.</returns>
        public async Task<List<TaskItem>> GetNotCompleteTasksWithChecklistAfterTheDeadlineAsync()
        {
            ErrorMessage = null;
            return await _databaseService.GetNotCompleteTasksWithChecklistAfterTheDeadlineAsync();
        }

        /// <summary>
        /// Retrieves all task items by company name.
        /// </summary>
        /// <param name="company">The name of the company.</param>
        /// <returns>A list of task items for the specified company.</returns>
        public async Task<List<TaskItem>> GetTaskItemsByCompanyAsync(string company)
        {
            ErrorMessage = null;
            return await _databaseService.GetTaskItemsByCompanyAsync(company);
        }

        /// <summary>
        /// Retrieves all task items by the creator's name.
        /// </summary>
        /// <param name="creator">The name of the creator.</param>
        /// <returns>A list of task items created by the specified user.</returns>
        public async Task<List<TaskItem>> GetTaskItemsByCreatorAsync(string creator)
        {
            ErrorMessage = null;
            return await _databaseService.GetTaskItemsByCreatorAsync(creator);
        }

        /// <summary>
        /// Retrieves all task items by assignee name.
        /// </summary>
        /// <param name="assignee">The name of the assignee.</param>
        /// <returns>A list of task items assigned to the specified user.</returns>
        public async Task<List<TaskItem>> GetTaskItemsByAssigneeAsync(string assignee)
        {
            ErrorMessage = null;
            return await _databaseService.GetTaskItemsByAssigneeAsync(assignee);
        }

        /// <summary>
        /// Retrieves a task item by its ID.
        /// </summary>
        /// <param name="id">The ID of the task item.</param>
        /// <returns>The task item with the specified ID, or null if not found.</returns>
        public async Task<TaskItem> GetTaskItemByIdAsync(int id)
        {
            ErrorMessage = null;
            return await _databaseService.GetTaskItemByIdAsync(id);
        }

        /// <summary>
        /// Validates a task item object, checking if it meets the required criteria.
        /// </summary>
        /// <param name="taskItem">The task item to be validated.</param>
        /// <remarks>
        /// If the task item fails validation, the <see cref="ErrorMessage"/> property is set with 
        /// the validation errors.
        /// </remarks>
        private void ValidateTaskItem(TaskItem taskItem)
        {
            var context = new ValidationContext(taskItem);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(taskItem, context, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                ErrorMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
            }
        }
    }
}
