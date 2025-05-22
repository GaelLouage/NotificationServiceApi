namespace Infrastructuur.Models
{
    public class EmailDataEntity
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public string Subject { get; set; } 
        public string Body { get; set; }
        public int SmtpPort { get; set; }
        public bool EnableSsl { get; set; }
    }
}
