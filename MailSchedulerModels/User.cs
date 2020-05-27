namespace MailSchedulerModels {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MailId { get; set; }
        public bool SentPrimaryMail { get; set; }
    }
}
