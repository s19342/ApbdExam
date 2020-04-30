using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ExamTask.DTOs.Requests;
using ExamTask.DTOs.Responses;
using ExamTask.Models;

namespace ExamTask.Services
{
    public class SqlServerDbService : IDbService
    {
        public int CheckTaskType(int index)
        {
            int sendBack = 0;

            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19342;Integrated Security=True"))
            {
                connection.Open();

                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText = @"select * from 
                                            TaskType t
                                            where t.IdTaskType=@index";
                    command.Parameters.AddWithValue("@index", index);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sendBack = 1;
                        }
                    }
                }
            }

            return sendBack;
        }

        public int InsertTaskType(TaskType tt)
        {

            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19342;Integrated Security=True"))
            {
                connection.Open();
                var tran = connection.BeginTransaction();

                try
                {

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = tran;

                        command.CommandText = @"Insert INTO TaskType(IdTaskType, Name)
                                                VALUES(@idtask, @nameGiven)";
                        command.Parameters.AddWithValue("@idTask", tt.IdTaskType);
                        command.Parameters.AddWithValue("@nameGiven", tt.Name);

                        command.ExecuteNonQuery();
                        tran.Commit();
                        return 1;
                    }
                }
                catch (SqlException e)
                {
                    tran.Rollback();
                    return 0;
                }
            }



        }

        public List<TaskResponse> ListAssigned(string index)
        {
            List<TaskResponse> listOfTasks = null;
            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19342;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select a.Name as TName, a.Description, a.Deadline, y.Name as TTName
                                            from TeamMember t inner join Task a on t.IdTeamMember=a.IdAssignedTo AND t.IdTeamMember=@index
                                            inner join TaskType y on a.IdTaskType= y.IdTaskType;";
                    command.Parameters.AddWithValue("@index", index);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TaskResponse tr = new TaskResponse
                            {
                                TaskName = reader["TName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Deadline = DateTime.Parse(reader["Deadline"].ToString()),
                                TaskType = reader["TTName"].ToString()                                
                            };

                            listOfTasks.Add(tr);
                        }
                    }
                }
            }

            return listOfTasks;
        }

        public List<TaskResponse> ListCreated(string index)
        {
            List<TaskResponse> listOfTasks = null;
            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19342;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select a.Name as TName, a.Description, a.Deadline, y.Name as TTName
                                            from TeamMember t inner join Task a on t.IdTeamMember=a.IdCreator AND t.IdTeamMember=@index
                                            inner join TaskType y on a.IdTaskType= y.IdTaskType;";
                    command.Parameters.AddWithValue("@index", index);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TaskResponse tr = new TaskResponse
                            {
                                TaskName = reader["TName"].ToString(),
                                Description = reader["Description"].ToString(),
                                Deadline = DateTime.Parse(reader["Deadline"].ToString()),
                                TaskType = reader["TTName"].ToString()
                            };

                            listOfTasks.Add(tr);
                        }
                    }
                }
            }
            return listOfTasks;
        }

        public int UpdateTask(TaskTypeDTO ttdto, string index)
        {
            int sendBack = 0;

            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19342;Integrated Security=True"))
            {
                connection.Open();
                var tran = connection.BeginTransaction();

                try
                {

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = tran;

                        command.CommandText = @"Update Task
                                                Set Name=@name, Descrption=@desc, Deadline=@dead,IdProject=@idp,idAssignedTo=@ida, IdCreator=@idc, IdTaskType=@idt
                                                Where IdTask=@index";
                        command.Parameters.AddWithValue("@name", ttdto.Name);
                        command.Parameters.AddWithValue("@desc", ttdto.Description);
                        command.Parameters.AddWithValue("@dead", ttdto.Deadline);
                        command.Parameters.AddWithValue("@idp", ttdto.IdProject);
                        command.Parameters.AddWithValue("@ida", ttdto.IdAssignedTo);
                        command.Parameters.AddWithValue("@idc", ttdto.IdCreator);
                        command.Parameters.AddWithValue("@idt", ttdto.TaskTypeGiven.IdTaskType);
                        command.Parameters.AddWithValue("@index", index);
                        
                        command.ExecuteNonQuery();
                        tran.Commit();
                        return 1;
                    }
                }
                catch (SqlException e)
                {
                    tran.Rollback();
                    return 0;
                }
            }
        }
    }
}
