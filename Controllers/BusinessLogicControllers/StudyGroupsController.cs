using JournalApiApp.Model.Entities.Journal;
using Microsoft.AspNetCore.Authorization;
using static JournalApiApp.Controllers.ApiMessages.Records;
using System.Data;
using JournalApiApp.LogicServices;

namespace JournalApiApp.Controllers.BusinessLogicControllers
{
    public class StudyGroupsController
    {
        private StudyGroupsLogicService logicService;
        public StudyGroupsController(StudyGroupsLogicService logicService)
        {
            this.logicService = logicService;
        }
        public async Task GetGroups(HttpContext context)
        {
            var groups = await logicService.GetGroups();
            await context.Response.WriteAsJsonAsync(groups);
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
        public async Task ShowStudyGroups(HttpContext context)
        {
            await context.Response.WriteAsJsonAsync(await logicService.ShowStudyGroups());
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


    }
}
