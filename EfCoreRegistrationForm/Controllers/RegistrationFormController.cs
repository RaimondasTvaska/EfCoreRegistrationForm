using EfCoreRegistrationForm.Data;
using EfCoreRegistrationForm.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EfCoreRegistrationForm.Controllers
{
    public class RegistrationFormController : Controller
    {
        private readonly DataContext _context;

        public RegistrationFormController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var questionsWithAnswersIds = _context.QuestionAnswers.Select(s => s.QuestionId).ToList();

            var questionsWithNoAnswers = _context.Questions.Include(q => q.Options).Where(q => !questionsWithAnswersIds.Contains(q.Id)).ToList();

            var questionAnswers = _context.QuestionAnswers.Include(qa => qa.Question).ThenInclude(q => q.Options).Include(qa => qa.Answer).ToList();

            foreach (var question in questionsWithNoAnswers)
            {
                questionAnswers.Add(new Models.QuestionAnswer()
                {
                    Question = question,
                    QuestionId = question.Id
                });
            }

            var regForm = new RegistrationFormDto()
            {
                QuestionAnswers = questionAnswers
            };
            return View(regForm);
        }
        //public IActionResult EditForm(int id)
        //{
        //    var questionAnswers = _context.QuestionAnswers.Include(c => c.Question).
        //        ThenInclude(q => q.Options);

        //    if (id !=0)
        //    {
        //        questionAnswers.Where(r => r.RegistrationId == id);
        //    }

        //    var RegistrationFormDto = new RegistrationFormDto()
        //    {
        //        QuestionAnswers = questionAnswers.ToList()
        //    };

        //    return View(RegistrationFormDto);
        //}
        [HttpPost]
        public IActionResult UpdateForm(RegistrationFormDto registrationForm)
        {

            if (registrationForm.RegistrationId != null)
            {
                foreach (var question in registrationForm.QuestionAnswers)
                {

                    _context.QuestionAnswers.Update(question);
                }

            }

            //var tmpRng = _context.CompanyBrokers.Where(b => b.BrokerId == brokerCreate.Broker.Id);
            //_context.CompanyBrokers.RemoveRange(tmpRng);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
