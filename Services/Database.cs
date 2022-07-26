using System;
using System.IO;
using System.Collections.Generic;
using DeerTime.Models;
using SQLite;
using System.Diagnostics;

namespace DeerTime.Services
{

    [Table("TaskItems")]

    public class TaskItemDB
    {
        [PrimaryKey, AutoIncrement]
        [Column("taskID")]
        public int taskID { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("SpentTime")]
        public int SpentTime { get; set; }
    }

    public class Database
    {
        private SQLiteConnection _db;

        public Database()
        {
            string[] paths = 
            {
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),                
                "DeerTime", "_avalonia.db"
            };
            Directory.CreateDirectory(Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                "DeerTime"
            ));
            _db = new SQLiteConnection(Path.Combine(paths));
            _db.CreateTable<TaskItemDB>();
        }

        public IEnumerable<TaskItem> GetItems()
        {
            return _db.Query<TaskItem>(
                "select * from TaskItems order by taskID desc");
        }

        public int AddItemDB(TaskItem NewItem)
        {
            var tidb = new TaskItemDB()
            {
                Description = NewItem.Description,
                SpentTime = NewItem.SpentTime
            };
            _db.Insert(tidb);
            Debug.WriteLine("Added to database: {0} == {1}", tidb.Description, tidb.taskID);
            return tidb.taskID;
        }

        public void UpdateTask(TaskItem NewItem)
        {
            var tidb = new TaskItemDB
            {
                taskID = NewItem.taskID,
                Description = NewItem.Description,
                SpentTime = NewItem.SpentTime
            };
            Debug.WriteLine(tidb);
            var results = _db.Update(tidb);
            _db.Commit();
            Debug.WriteLine("Task updated" + NewItem.taskID + " " + NewItem.Description + " " + NewItem.SpentTime);
        }
    }
}