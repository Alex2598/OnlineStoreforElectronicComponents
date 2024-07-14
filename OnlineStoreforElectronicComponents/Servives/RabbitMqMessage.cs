public class RabbitMqMessage
{
    public List<string> to { get; set; } = new List<string>();
    public List<string> bcc { get; set; } = new List<string>();
    public List<string> cc { get; set; } = new List<string>();
    public string from { get; set; } = "";
    public string displayName { get; set; } = "";
    public string replyTo { get; set; } = "";
    public string replyToName { get; set; } = "";
    public string subject { get; set; } = "";
    public string body { get; set; } = "";
}