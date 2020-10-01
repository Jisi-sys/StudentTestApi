using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.SqlClient;
using DataAccessLayer.Common;
using System.Data;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;

namespace DataAccessLayer
{
    public class StudentDAL : IStudentServices
    {


        DbConnectionOp dBConnectionOp = new DbConnectionOp();

        public void UpdateStudent(int id, Student student)
        {
            List<SqlParameter> parameters = new List<SqlParameter>

                {
                        new SqlParameter("@id",id),
                        new SqlParameter("@name", student.Name),
                        new SqlParameter("@course",student.Course),
                        new SqlParameter("@Action", "UPDATE")
                };
            dBConnectionOp.ExecuteNonQuery("TrialStudSP", parameters);
        }


        public void AddNewStudent(Student student)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
              {
                    new SqlParameter("@name", student.Name),
                    new SqlParameter("@course",student.Course),
                    new SqlParameter("@Action", "INSERT")
              };
            dBConnectionOp.ExecuteNonQuery("TrialStudSP", parameters);

        }


        public IEnumerable<Student> GetAllStudent()
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {

             new SqlParameter("@Action", "SELECT")
            };
            using (var reader = dBConnectionOp.GetDataReader("TrialStudSP", parameters))
            {
                while (reader.Read())
                {
                    yield return new Student
                    {
                        Id = reader.IsDBNull("Id")?0: reader.GetInt32("Id"),
                        Name = reader.IsDBNull("Name")?string.Empty:reader.GetString("Name"),
                        Course = reader.IsDBNull("Course") ? string.Empty : reader.GetString("Course"),
                    };
                }

            }

        }
        public void DeleteStudent(int id)
        {

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                  new SqlParameter("@id",id),
                  new SqlParameter("@Action", "DELETE")
            };
            dBConnectionOp.ExecuteNonQuery("TrialStudSP", parameters);

        }


        public IEnumerable<Student> GetStudentById(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
               new SqlParameter("@id",id),
               new SqlParameter("@Action", "SELECTBYID")
            };
            using (var reader = dBConnectionOp.GetDataReader("TrialStudSP", parameters))
            {
                //Console.WriteLine("HEllo");
                while (reader.Read())
                { 
                yield return new Student
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Course = reader.GetString("Course")
                    };

                }
                
            }
        }
    
  
    }
    
}
       
