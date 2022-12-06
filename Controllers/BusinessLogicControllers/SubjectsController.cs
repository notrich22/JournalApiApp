using JournalApiApp.Model.Entities.Journal;
using Microsoft.AspNetCore.Authorization;
using static JournalApiApp.Controllers.ApiMessages.Records;
using System.Data;
using JournalApiApp.LogicServices;

namespace JournalApiApp.Controllers.BusinessLogicControllers
{
    public class SubjectsController
    {
        private SubjectsLogicService logicService;
        public SubjectsController(SubjectsLogicService logicService)
        {
            this.logicService = logicService;
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
        public async Task ShowSubjects(HttpContext context)
        {
            await context.Response.WriteAsJsonAsync(await logicService.ShowSubjects());
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
