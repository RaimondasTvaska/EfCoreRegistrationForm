using System.Collections.Generic;

namespace EfCoreRegistrationForm.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Answer> Options { get; set; }
    }
}
