using Microsoft.AspNetCore.Authorization;
using static JournalApiApp.Controllers.ApiMessages.Records;
using System.Data;
using JournalApiApp.LogicServices;

namespace JournalApiApp.Controllers.BusinessLogicControllers
{
    public class NotesController
    {
        private NotesLogicService logicService;
        public NotesController(NotesLogicService logicService)
        {
            this.logicService = logicService;
        }
        [Authorize]
        public async Task GetAllNotes(HttpContext context)
        {
            var Notes = await logicService.GetNotes();
            await context.Response.WriteAsJsonAsync(Notes);
        }
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
        [Authorize(Roles = "teacher,admin")]
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
    }
}
