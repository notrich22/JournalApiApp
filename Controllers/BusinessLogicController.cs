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
            var lessons = await logicService.GetLessons();
            await context.Response.WriteAsJsonAsync(lessons);
        }
        public async Task GetGroups(HttpContext context)
        {
            var groups = await logicService.GetGroups();
            await context.Response.WriteAsJsonAsync(groups);
        }
        public async Task GetStudents(HttpContext context)
        {
            var students = await logicService.GetStudents();
            await context.Response.WriteAsJsonAsync(students);
        }
        public async Task GetStudentsByGroupAsync(HttpContext context)
        {
            try {
                IdData idData = await context.Request.ReadFromJsonAsync<IdData>();
                var students = await logicService.GetStudentsByGroup(idData.id);
                await context.Response.WriteAsJsonAsync(students);
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        //student
        [Authorize]
        public async Task GetAllNotes(HttpContext context)
        {
            var Notes = await logicService.GetNotes();
            await context.Response.WriteAsJsonAsync(Notes);
        }
        //[Authorize(Roles = "admin || user || teacher")]
        [Authorize]
        public async Task GetAllNotesByStudent(HttpContext context)
        {
            try
            {
                IdData idData = await context.Request.ReadFromJsonAsync<IdData>();
                var notes = await logicService.GetNotesByStudent(idData.id);
                await context.Response.WriteAsJsonAsync(notes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        //teacher
        [Authorize(Roles ="teacher,admin")]
        public async Task AddNoteForStudent(HttpContext context)
        {
            TripleIntData noteData = await context.Request.ReadFromJsonAsync<TripleIntData>();
            await logicService.AddNote(noteData.id1, noteData.id2, noteData.id3);
        }
        [Authorize(Roles = "teacher,admin")]
        public async Task UpdateNoteForStudent(HttpContext context)
        {
            DoubleIntData noteData = await context.Request.ReadFromJsonAsync<DoubleIntData>();
            await logicService.UpdateNote(noteData.id1, noteData.id2);

        }
        //admin
        //STUDENT CRUD
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
        //STUDYGROUP CRUD
        [Authorize(Roles = "admin")]
        public async Task AddStudyGroup(HttpContext context)
        {
            GroupNameData studyGroupData = await context.Request.ReadFromJsonAsync<GroupNameData>();
            StudyGroup newStudyGroup = await logicService.AddStudyGroup(studyGroupData.groupName);
            await context.Response.WriteAsJsonAsync(newStudyGroup);
        }
        [Authorize(Roles = "admin")]
        public async Task ShowStudyGroup(HttpContext context)
        {
            IdData studyGroupId = await context.Request.ReadFromJsonAsync<IdData>();
            StudyGroup studyGroup = await logicService.ShowStudyGroup(studyGroupId.id);
            await context.Response.WriteAsJsonAsync(studyGroup);
        }
        [Authorize(Roles = "admin")]
        public async Task UpdateStudyGroup(HttpContext context)
        {
            UpdateStudyGroupData newStudyGroup = await context.Request.ReadFromJsonAsync<UpdateStudyGroupData>();
            StudyGroup studyGroup = await logicService.UpdateStudyGroup(newStudyGroup.groupId,
                newStudyGroup.GroupName);
            await context.Response.WriteAsJsonAsync(studyGroup);
        }
        [Authorize(Roles = "admin")]
        public async Task DeleteStudyGroup(HttpContext context)
        {
            IdData studyGroupId = await context.Request.ReadFromJsonAsync<IdData>();
            await logicService.DeleteStudyGroup(studyGroupId.id);
        }
        //STUDYGROUP CRUD
        [Authorize(Roles = "admin")]
        public async Task AddSubject(HttpContext context)
        {
            GroupNameData subjectData = await context.Request.ReadFromJsonAsync<GroupNameData>();
            Subject newSubject = await logicService.AddSubject(subjectData.groupName);
            await context.Response.WriteAsJsonAsync(newSubject);
        }
        [Authorize(Roles = "admin")]
        public async Task ShowSubject(HttpContext context)
        {
            IdData subjectId = await context.Request.ReadFromJsonAsync<IdData>();
            Subject subject = await logicService.ShowSubject(subjectId.id);
            await context.Response.WriteAsJsonAsync(subject);
        }
        [Authorize(Roles = "admin")]
        public async Task UpdateSubject(HttpContext context)
        {
            UpdateSubjectData newSubject = await context.Request.ReadFromJsonAsync<UpdateSubjectData>();
            Subject subject = await logicService.UpdateSubject(newSubject.subjectId,
                newSubject.subjectName);
            await context.Response.WriteAsJsonAsync(subject);
        }
        [Authorize(Roles = "admin")]
        public async Task DeleteSubject(HttpContext context)
        {
            IdData subjectId = await context.Request.ReadFromJsonAsync<IdData>();
            await logicService.DeleteSubject(subjectId.id);
        }
    }
}
