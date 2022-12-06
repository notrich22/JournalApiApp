using JournalApiApp.Model.Entities.Journal;
using JournalApiApp.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace JournalApiApp.Controllers
{
    public class StudyGroupController
    {
        private StudyGroupService studyGroupService;
        public StudyGroupController(StudyGroupService studyGroupService)
        {
            this.studyGroupService = studyGroupService;
        }
        public async Task GetStudyGroupList(HttpContext context)
        {
            List<StudyGroup> studyGroupList = await studyGroupService.GetStudyGroupsAsync();
            await context.Response.WriteAsJsonAsync(studyGroupList);
        }
        [Authorize]
        public async Task GetStudyGroupById(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request.Query["id"]);
            StudyGroup sg = await studyGroupService.GetStudyGroupByIdAsync(id);
            await context.Response.WriteAsJsonAsync(sg);
        }
        [Authorize(Roles ="admin")]
        public async Task AddStudyGroup(HttpContext context)
        {
            try { 
            StudyGroup sg = await context.Request.ReadFromJsonAsync<StudyGroup>();
            await studyGroupService.AddStudyGroup(sg);
            }catch(Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}
