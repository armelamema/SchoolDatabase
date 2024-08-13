using SchoolDatabase.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace SchoolDatabase.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext Blog = new SchoolDbContext();
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            using (MySqlConnection Conn = Blog.AccessDatabase())
            {
                Conn.Open();
                using (MySqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM teachers";
                    using (MySqlDataReader ResultSet = cmd.ExecuteReader())
                    {
                        List<Teacher> Teachers = new List<Teacher>();
                        while (ResultSet.Read())
                        {
                            Teacher NewTeacher = new Teacher
                            {
                                TeacherId = (int)ResultSet["teacherid"],
                                TeacherFname = ResultSet["teacherfname"].ToString(),
                                TeacherLname = ResultSet["teacherlname"].ToString(),
                                EmployeeNumber = ResultSet["employeenumber"].ToString(),
                                HireDate = ResultSet["hiredate"].ToString(),
                                Salary = ResultSet["salary"].ToString()
                            };
                            Teachers.Add(NewTeacher);
                        }
                        return Teachers;
                    }
                }
            }
        }

        /// <summary>
        /// Finds a teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>A teacher object</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = null;
            using (MySqlConnection Conn = Blog.AccessDatabase())
            {
                Conn.Open();
                using (MySqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader ResultSet = cmd.ExecuteReader())
                    {
                        if (ResultSet.Read())
                        {
                            NewTeacher = new Teacher
                            {
                                TeacherId = (int)ResultSet["teacherid"],
                                TeacherFname = ResultSet["teacherfname"].ToString(),
                                TeacherLname = ResultSet["teacherlname"].ToString(),
                                EmployeeNumber = ResultSet["employeenumber"].ToString(),
                                HireDate = ResultSet["hiredate"].ToString(),
                                Salary = ResultSet["salary"].ToString()
                            };
                        }
                    }
                }
            }
            return NewTeacher;
        }

        /// <summary>
        /// Adds a new teacher to the database
        /// </summary>
        /// <param name="newTeacher">The teacher object to be added</param>
        /// <returns>HTTP response message indicating success or failure</returns>
        [HttpPost]
        public IHttpActionResult AddTeacher(Teacher newTeacher)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (MySqlConnection Conn = Blog.AccessDatabase())
            {
                Conn.Open();
                using (MySqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES (@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";
                    cmd.Parameters.AddWithValue("@TeacherFname", newTeacher.TeacherFname);
                    cmd.Parameters.AddWithValue("@TeacherLname", newTeacher.TeacherLname);
                    cmd.Parameters.AddWithValue("@EmployeeNumber", newTeacher.EmployeeNumber);
                    cmd.Parameters.AddWithValue("@HireDate", newTeacher.HireDate);
                    cmd.Parameters.AddWithValue("@Salary", newTeacher.Salary);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return Ok(); // Success response
                    else
                        return BadRequest("Failed to add teacher."); // Error response
                }
            }
        }

        /// <summary>
        /// Deletes a teacher from the database
        /// </summary>
        /// <param name="id">The teacher ID to be deleted</param>
        /// <returns>HTTP response message indicating success or failure</returns>
        [HttpDelete]
        public IHttpActionResult DeleteTeacher(int id)
        {
            using (MySqlConnection Conn = Blog.AccessDatabase())
            {
                Conn.Open();
                using (MySqlCommand cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM teachers WHERE teacherid = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        return Ok(); // Success response
                    else
                        return NotFound(); // Not found response
                }
            }
        }
    }
        }
    
