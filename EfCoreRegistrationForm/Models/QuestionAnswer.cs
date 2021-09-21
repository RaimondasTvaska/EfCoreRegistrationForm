namespace EfCoreRegistrationForm.Models
{
    public class QuestionAnswer
    {
        public int RegistrationId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}
