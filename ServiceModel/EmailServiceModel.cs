namespace ServiceModel {
    public class EmailServiceModel
    {
        public string FromAddress { get; set; } 
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
