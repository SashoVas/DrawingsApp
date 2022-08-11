namespace DrawingsApp.Messages.User
{
    public class RemoveRoleFromUserMessage
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }
    }
}
