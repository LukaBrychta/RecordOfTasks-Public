using RecordOfTasks.Repository;
using RecordOfTasks.Repository.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordOfTasks.Services.Services
{
    /// <summary>
    /// Provides services for managing checklist items related to tasks, including creation, editing, 
    /// deletion, and retrieval of checklist items.
    /// </summary>
    public class ChecklistItemService
    {
        private readonly DatabaseService _databaseService;

        /// <summary>
        /// Gets or sets the error message, if validation fails.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecklistItemService"/> class with a reference 
        /// to the <see cref="DatabaseService"/>.
        /// </summary>
        /// <param name="databaseService">The database service for interacting with the database.</param>
        public ChecklistItemService(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Creates a new checklist item and saves it to the database after validation.
        /// </summary>
        /// <param name="checklistItem">The checklist item object to be created.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <remarks>
        /// If the checklist item fails validation, it sets the <see cref="ErrorMessage"/> property.
        /// </remarks>
        public async Task CreateChecklistItemAsync(ChecklistItem checklistItem)
        {
            ErrorMessage = null;
            ValidateChecklistItem(checklistItem);
            if (ErrorMessage == null)
            {
                await _databaseService.CreateChecklistItemAsync(checklistItem);
            }
        }

        /// <summary>
        /// Edits an existing checklist item in the database after validation.
        /// </summary>
        /// <param name="checklistItem">The checklist item object to be edited.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <remarks>
        /// If the checklist item fails validation, it sets the <see cref="ErrorMessage"/> property.
        /// </remarks>
        public async Task EditChecklistItemAsync(ChecklistItem checklistItem)
        {
            ErrorMessage = null;
            ValidateChecklistItem(checklistItem);
            if (ErrorMessage == null)
            {
                await _databaseService.EditChecklistItemAsync(checklistItem);
            }
        }

        /// <summary>
        /// Deletes a checklist item from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the checklist item to be deleted.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task DeleteChecklistItemByIdAsync(int id)
        {
            ErrorMessage = null;
            await _databaseService.DeleteChecklistItemByIdAsync(id);
        }

        /// <summary>
        /// Retrieves all checklist items associated with a specific task by task ID.
        /// </summary>
        /// <param name="taskId">The ID of the task to retrieve checklist items for.</param>
        /// <returns>A list of checklist items related to the specified task.</returns>
        public async Task<List<ChecklistItem>> GetChecklistItemsByTaskIdAsync(int taskId)
        {
            ErrorMessage = null;

            return await _databaseService.GetChecklistItemsByTaskIdAsync(taskId);
        }

        /// <summary>
        /// Validates a checklist item object, checking if it meets the required criteria.
        /// </summary>
        /// <param name="checklistItem">The checklist item object to be validated.</param>
        /// <remarks>
        /// If the checklist item fails validation, the <see cref="ErrorMessage"/> property is set with 
        /// the validation errors.
        /// </remarks>
        private void ValidateChecklistItem(ChecklistItem checklistItem)
        {
            var context = new ValidationContext(checklistItem);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(checklistItem, context, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                ErrorMessage = string.Join("; ", validationResults.Select(vr => vr.ErrorMessage));
            }
        }
    }
}
