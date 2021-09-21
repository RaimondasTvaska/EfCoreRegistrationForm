namespace EfCoreRegistrationForm.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public Question Option { get; set; }
    }
}
