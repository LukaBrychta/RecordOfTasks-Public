using Dapper;
using Microsoft.Data.Sqlite;
using RecordOfTasks.Repository.Models;
using SQLitePCL;

namespace RecordOfTasks.Repository
{
    /// <summary>
    /// Provides services for interacting with the database, 
    /// including operations such as connecting, querying, and updating.
    /// </summary>
    public class DatabaseService
    {
        /// <summary>
        /// Stores the connection string used to connect to the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService"/> class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string used to connect to the database.</param>
        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Initializes the database by creating the necessary tables and seeding initial data.
        /// </summary>
        /// <returns>An asynchronous operation.</returns>
        /// <remarks>
        /// This method reads SQL scripts for creating the database schema and seeding initial data from 
        /// the "Scripts/Schema.sql" and "Scripts/SeedData.sql" files respectively. It then executes these
        /// scripts on the SQLite database. The method also ensures that the necessary SQLite batteries 
        /// (native libraries) are initialized using `Batteries.Init()`.
        /// </remarks>
        public async Task InitializeDatabaseAsync()
        {
            Batteries.Init();

            using (var connection = new SqliteConnection(_connectionString))
            {
                Console.WriteLine("cesta");
                Console.WriteLine(Directory.GetCurrentDirectory());

                await connection.OpenAsync();
            // CREATE table
            var schemaScript = File.ReadAllText("../RecordOfTasks.Repository/Scripts/Schema.sql");
                var command = new SqliteCommand(schemaScript, connection);
                await command.ExecuteNonQueryAsync();

                // CREATE seed data
                var schemaSeedData = File.ReadAllText("../RecordOfTasks.Repository/Scripts/SeedData.sql");
                command = new SqliteCommand(schemaSeedData, connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Adds a new task to the database.
        /// </summary>
        /// <param name="taskItem">The task item to be added.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task CreateTaskItemAsync(TaskItem taskItem)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO [Tasks] ( Company, Description, Creator, Assignee, ReportedDate, DueDate, Priority, Status)" +
                                    " VALUES (@Company, @Description, @Creator, @Assignee, @ReportedDate, @DueDate, @Priority, @Status)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Company", taskItem.Company);
                    command.Parameters.AddWithValue("@Description", taskItem.Description);
                    command.Parameters.AddWithValue("@Creator", taskItem.Creator);
                    command.Parameters.AddWithValue("@Assignee", taskItem.Assignee);
                    command.Parameters.AddWithValue("@ReportedDate", taskItem.ReportedDate);
                    command.Parameters.AddWithValue("@DueDate", taskItem.DueDate);
                    command.Parameters.AddWithValue("@Priority", taskItem.Priority.ToString());
                    command.Parameters.AddWithValue("@Status", taskItem.Status.ToString());

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Retrieves all task items from the database.
        /// </summary>
        /// <returns>A list of all tasks.</returns>
        public async Task<List<TaskItem>> GetAllTaskItemsAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks]";
                return (await connection.QueryAsync<TaskItem>(query)).ToList();
            }
        }

        /// <summary>
        /// Retrieves all incomplete task items.
        /// </summary>
        /// <returns>A list of incomplete tasks.</returns>
        public async Task<List<TaskItem>> GetNotCompleteTasksAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks] WHERE [Status] != @Completed";

                return (await connection.QueryAsync<TaskItem>(query, new { Completed = "Completed" })).ToList();
            }
        }
        /// <summary>
        /// Retrieves all incomplete tasks that are past their deadline.
        /// </summary>
        /// <returns>A list of overdue tasks.</returns>
        public async Task<List<TaskItem>> GetNotCompleteTasksAfterTheDeadlineAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks] WHERE [Status] != @Completed AND [DueDate] < @currentDate";

                return (await connection.QueryAsync<TaskItem>(query, new { Completed = "Completed", currentDate = DateTime.Now.Date })).ToList();
            }
        }
        /// <summary>
        /// Retrieves all incomplete tasks with incomplete checklist items that are past their deadline.
        /// </summary>
        /// <returns>A list of incomplete tasks with incomplete checklist items that are overdue.</returns>
        public async Task<List<TaskItem>> GetNotCompleteTasksWithChecklistAfterTheDeadlineAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks]" +
                    " WHERE [Status] != @Completed" +
                    " AND [Id] IN ( SELECT [TaskId] FROM [ChecklistItems] WHERE [IsCompleted] = 0 AND [DueDate] < @currentDate)";

                return (await connection.QueryAsync<TaskItem>(query, new { Completed = "Completed", currentDate = DateTime.Now.Date })).ToList();
            }
        }

        /// <summary>
        /// Retrieves all tasks by company name.
        /// </summary>
        /// <param name="company">The name of the company.</param>
        /// <returns>A list of tasks for the specified company.</returns>
        public async Task<List<TaskItem>> GetTaskItemsByCompanyAsync(string company)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks] WHERE [Company] = @company";
                return (await connection.QueryAsync<TaskItem>(query, new { company = company })).ToList();
            }
        }

        /// <summary>
        /// Retrieves all tasks by creator name.
        /// </summary>
        /// <param name="creator">The name of the creator.</param>
        /// <returns>A list of tasks created by the specified user.</returns>
        public async Task<List<TaskItem>> GetTaskItemsByCreatorAsync(string creator)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks] WHERE [Creator] = @creator";
                return (await connection.QueryAsync<TaskItem>(query, new { creator = creator })).ToList();
            }
        }

        /// <summary>
        /// Retrieves all tasks by assignee name.
        /// </summary>
        /// <param name="assignee">The name of the assignee.</param>
        /// <returns>A list of tasks assigned to the specified user.</returns>
        public async Task<List<TaskItem>> GetTaskItemsByAssigneeAsync(string assignee)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks] WHERE [Assignee] = @assignee";
                return (await connection.QueryAsync<TaskItem>(query, new { assignee = assignee })).ToList();
            }
        }

        /// <summary>
        /// Retrieves the most recent task item by the highest ID.
        /// </summary>
        /// <returns>The last created task item, or null if not found.</returns>
        public async Task<TaskItem> GetLastTaskItemAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks] ORDER BY [Id] DESC LIMIT 1";
                return await connection.QueryFirstOrDefaultAsync<TaskItem>(query);
            }
        }

        /// <summary>
        /// Retrieves a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task.</param>
        /// <returns>The task with the specified ID, or null if not found.</returns>
        public async Task<TaskItem> GetTaskItemByIdAsync(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Tasks] WHERE [Id] = @id";
                return await connection.QuerySingleOrDefaultAsync<TaskItem>(query, new { id = id });
            }
        }

        /// <summary>
        /// Updates an existing task in the database.
        /// </summary>
        /// <param name="taskItem">The updated task item.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task EditTaskItemAsync(TaskItem taskItem)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE [Tasks] SET" +
                            " Company = @Company, " +
                            "Description = @Description, " +
                            "Creator = @Creator, " +
                            "Assignee = @Assignee, " +
                            "ReportedDate = @ReportedDate, " +
                            "DueDate = @DueDate, " +
                            "Priority = @Priority, " +
                            "Status = @Status " +
                            "WHERE Id = @Id";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Company", taskItem.Company);
                    command.Parameters.AddWithValue("@Description", taskItem.Description);
                    command.Parameters.AddWithValue("@Creator", taskItem.Creator);
                    command.Parameters.AddWithValue("@Assignee", taskItem.Assignee);
                    command.Parameters.AddWithValue("@ReportedDate", taskItem.ReportedDate);
                    command.Parameters.AddWithValue("@DueDate", taskItem.DueDate);
                    command.Parameters.AddWithValue("@Priority", taskItem.Priority.ToString());
                    command.Parameters.AddWithValue("@Status", taskItem.Status.ToString());
                    command.Parameters.AddWithValue("@Id", taskItem.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Deletes a task by its ID.
        /// </summary>
        /// <param name="id">The ID of the task to delete.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task DeleteTaskItemAsync(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM [Tasks] WHERE [Id] = @id";
                await connection.QueryAsync(query, new { id = id });
            }
        }

        /// <summary>
        /// Adds a new message to a task.
        /// </summary>
        /// <param name="message">The message object to be added.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task CreateMessageAsync(Message message)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO [Messages] (TaskId, MessageText, NameUser, Timestamp) VALUES (@TaskId, @MessageText, @NameUser, @Timestamp)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskId", message.TaskId);
                    command.Parameters.AddWithValue("@MessageText", message.MessageText);
                    command.Parameters.AddWithValue("@NameUser", message.NameUser);
                    command.Parameters.AddWithValue("@Timestamp", message.Timestamp);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Retrieves all messages related to a specific task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <returns>A list of messages for the specified task.</returns>
        public async Task<List<Message>> GetMessagesByTaskIdAsync(int taskId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [Messages] WHERE [TaskId] = @taskId";
                return (await connection.QueryAsync<Message>(query, new { taskId = taskId })).ToList();

            }
        }

        /// <summary>
        /// Updates an existing message in the database.
        /// </summary>
        /// <param name="message">The updated message object.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task EditMessageAsync(Message message)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE [Messages] SET " +
                            "NameUser = @NameUser, " +
                            "MessageText = @MessageText, " +
                            "Timestamp = @Timestamp " +
                            "WHERE [Id] = @Id";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NameUser", message.NameUser);
                    command.Parameters.AddWithValue("@MessageText", message.MessageText);
                    command.Parameters.AddWithValue("@Timestamp", message.Timestamp);
                    command.Parameters.AddWithValue("@Id", message.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Deletes a message by its ID.
        /// </summary>
        /// <param name="id">The ID of the message to delete.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task DeleteMessageByIdAsync(int Id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM [Messages] WHERE [Id] = @Id";
                await connection.QueryAsync(query, new { Id = Id });
            }
        }

        /// <summary>
        /// Adds a new checklist item to a task.
        /// </summary>
        /// <param name="checklistItem">The checklist item to be added.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task CreateChecklistItemAsync(ChecklistItem checklist)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO [ChecklistItems] (TaskId, Description, IsCompleted, DueDate) VALUES (@TaskId, @Description, @IsCompleted, @DueDate)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TaskId", checklist.TaskId);
                    command.Parameters.AddWithValue("@Description", checklist.Description);
                    command.Parameters.AddWithValue("@IsCompleted", checklist.IsCompleted);
                    command.Parameters.AddWithValue("@DueDate", checklist.DueDate);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Retrieves all checklist items related to a specific task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <returns>A list of checklist items for the specified task.</returns>
        public async Task<List<ChecklistItem>> GetChecklistItemsByTaskIdAsync(int taskId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM [ChecklistItems] WHERE [TaskId] = @taskId";
                return (await connection.QueryAsync<ChecklistItem>(query, new { taskId = taskId })).ToList();
            }
        }

        /// <summary>
        /// Updates an existing checklist item in the database.
        /// </summary>
        /// <param name="checklistItem">The updated checklist item object.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task EditChecklistItemAsync(ChecklistItem checklistitem)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE [ChecklistItems] SET " +
                            "Description = @Description, " +
                            "IsCompleted = @IsCompleted, " +
                            "DueDate = @DueDate " +
                            "WHERE [Id] = @Id";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Description", checklistitem.Description);
                    command.Parameters.AddWithValue("@IsCompleted", checklistitem.IsCompleted);
                    command.Parameters.AddWithValue("@DueDate", checklistitem.DueDate);
                    command.Parameters.AddWithValue("@Id", checklistitem.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Deletes a checklist item by its ID.
        /// </summary>
        /// <param name="id">The ID of the checklist item to delete.</param>
        /// <returns>An asynchronous operation.</returns>
        public async Task DeleteChecklistItemByIdAsync(int Id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM [ChecklistItems] WHERE [Id] = @Id";
                await connection.QueryAsync(query, new { Id = Id });
            }
        }
    }
}