using EfCoreRegistrationForm.Models;
using System.Collections.Generic;

namespace EfCoreRegistrationForm.Dtos
{
    public class RegistrationFormDto
    {
        public int RegistrationId { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
