using JournalApiApp.Model.Entities.Journal;
using JournalApiApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Diagnostics;
using static JournalApiApp.Records;

namespace JournalApiApp.Controllers
{
    public class BusinessLogicController
    {
        private BusinessLogicService logicService;
        public BusinessLogicController(BusinessLogicService logicService)
        {
            this.logicService = logicService;
        }
        //Unauthorized
        public async Task GetLessons(HttpContext context) {
            var lessons = await logicService.GetLessonsAsync();
            await context.Response.WriteAsJsonAsync(lessons);
        }
        public async Task GetGroups(HttpContext context)
        {
            var groups = await logicService.GetGroupsAsync();
            await context.Response.WriteAsJsonAsync(groups);
        }
        public async Task GetStudents(HttpContext context)
        {
            var students = await logicService.GetStudentsAsync();
            await context.Response.WriteAsJsonAsync(students);
        }
        public async Task GetStudentsByGroupAsync(HttpContext context)
        {
            try {
                IdData idData = await context.Request.ReadFromJsonAsync<IdData>();
                var students = await logicService.GetStudentsByGroupAsync(idData.id);
                await context.Response.WriteAsJsonAsync(students);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        //student
        [Authorize]
        public async Task GetAllNotes(HttpContext context)
        {
            var Notes = await logicService.GetNotesAsync();
            await context.Response.WriteAsJsonAsync(Notes);
        }
        //[Authorize(Roles = "admin || user || teacher")]
        public async Task GetAllNotesByStudent(HttpContext context)
        {
            try
            {
                IdData idData = await context.Request.ReadFromJsonAsync<IdData>();
                var notes = await logicService.GetNotesByStudentAsync(idData.id);
                await context.Response.WriteAsJsonAsync(notes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public async Task GetNotesByLessonForConcreteStudent(HttpContext context)
        {
            try
            {
                DoubleIntData idData = await context.Request.ReadFromJsonAsync<DoubleIntData>();
                var notes = await logicService.GetNotesByLessonForConcreteStudent(idData.id1, idData.id2);
                await context.Response.WriteAsJsonAsync(notes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public async Task GetNotesByLesson(HttpContext context)
        {
            try
            {
                IdData idData = await context.Request.ReadFromJsonAsync<IdData>();
                var notes = await logicService.GetNotesByLesson(idData.id);
                await context.Response.WriteAsJsonAsync(notes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //teacher
        public async Task AddNoteForStudent(HttpContext context)
        {
            TripleIntData noteData = await context.Request.ReadFromJsonAsync<TripleIntData>();
            await logicService.AddNote(noteData.id1, noteData.id2, noteData.id3);
        }
        public async Task UpdateNoteForStudent(HttpContext context)
        {
            DoubleIntData noteData = await context.Request.ReadFromJsonAsync<DoubleIntData>();
            await logicService.UpdateNote(noteData.id1, noteData.id2);

        }
        //admin
        public async Task AddStudent(HttpContext context)
        {
            StudentData studentData = await context.Request.ReadFromJsonAsync<StudentData>();
            Student newStudent = await logicService.AddStudent(studentData.FullName, studentData.GroupId, studentData.UserId);
            await context.Response.WriteAsJsonAsync<Student>(newStudent);
        }
    }
}
