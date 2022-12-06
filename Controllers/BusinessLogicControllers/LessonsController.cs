using JournalApiApp.LogicServices;

namespace JournalApiApp.Controllers.BusinessLogicControllers
{
    public class LessonsController
    {
        private LessonsLogicService logicService;
        public LessonsController(LessonsLogicService logicService)
        {
            this.logicService = logicService;
        }
        public async Task GetLessons(HttpContext context)
        {
            var lessons = await logicService.GetLessons();
            await context.Response.WriteAsJsonAsync(lessons);
        }

    }
}
