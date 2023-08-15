namespace SchoolCoreApi.Entities
{
    public class RoleEventCreation
    {
        public int eventAuthorizationID { get; set; }
        public int eventID { get; set; }
        public int userID { get; set; }
        public string json { get; set; }
        public string spType { get; set; }
    }
}