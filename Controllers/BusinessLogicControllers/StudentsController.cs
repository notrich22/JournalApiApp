using JournalApiApp.LogicServices;
using JournalApiApp.Model.Entities.Journal;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using static JournalApiApp.Controllers.ApiMessages.Records;

namespace JournalApiApp.Controllers.BusinessLogicControllers
{
    public class StudentsController
    {
        private StudentsLogicService logicService;
        public StudentsController(StudentsLogicService logicService)
        {
            this.logicService = logicService;
        }
        public async Task GetStudents(HttpContext context)
        {
            var students = await logicService.GetStudents();
            await context.Response.WriteAsJsonAsync(students);
        }
        public async Task GetStudentsByGroupAsync(HttpContext context)
        {
            try
            {
                IdData idData = await context.Request.ReadFromJsonAsync<IdData>();
                var students = await logicService.GetStudentsByGroup(idData.id);
                await context.Response.WriteAsJsonAsync(students);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        [Authorize(Roles = "admin")]
        public async Task AddStudent(HttpContext context)
        {
            StudentData studentData = await context.Request.ReadFromJsonAsync<StudentData>();
            Student newStudent = await logicService.AddStudent(studentData.fullName, studentData.studyGroupId, studentData.userId);
            await context.Response.WriteAsJsonAsync<Student>(newStudent);
        }
        [Authorize(Roles = "admin")]
        public async Task ShowStudent(HttpContext context)
        {
            IdData studentId = await context.Request.ReadFromJsonAsync<IdData>();
            Student student = await logicService.ShowStudent(studentId.id);
            await context.Response.WriteAsJsonAsync(student);
        }
        [Authorize(Roles = "admin")]
        public async Task ShowStudents(HttpContext context)
        {
            await context.Response.WriteAsJsonAsync(logicService.ShowStudents());
        }
        [Authorize(Roles = "admin")]
        public async Task UpdateStudent(HttpContext context)
        {
            UpdateStudentData newStudent = await context.Request.ReadFromJsonAsync<UpdateStudentData>();
            Student student = await logicService.UpdateStudent(newStudent.studentId,
                                                                newStudent.fullName,
                                                                newStudent.studyGroupId,
                                                                newStudent.userId);
            await context.Response.WriteAsJsonAsync(student);
        }
        [Authorize(Roles = "admin")]
        public async Task DeleteStudent(HttpContext context)
        {
            IdData studentId = await context.Request.ReadFromJsonAsync<IdData>();
            await logicService.DeleteStudent(studentId.id);
        }
    }
}
