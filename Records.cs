namespace JournalApiApp
{
    public class Records
    {
        public record UserData(string login, string password, int groupId);
        public record UpdateUserData(int id, string login, string password, int groupId);

        public record StudentData(string fullName, int studyGroupId, int userId);
        public record UserLogin(string login, string password);
        public record StringMessage(string text);
        public record IdData(int id);
        public record DoubleIntData(int id1, int id2);
        public record TripleIntData(int id1, int id2, int id3);
        public record UpdateStudentData(int studentId, string fullName, int studyGroupId, int userId);
        public record UpdateStudyGroupData(int groupId, string GroupName);
        public record UpdateSubjectData(int subjectId, string subjectName);
        public record GroupNameData(string groupName);
        public record LoginData(string login);
    }
}
