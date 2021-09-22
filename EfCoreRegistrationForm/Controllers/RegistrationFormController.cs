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

            var regForm = new RegistrationFormDto()
            {
                QuestionAnswers = _context.QuestionAnswers.ToList()
            };
            return View(regForm);
        }
        public IActionResult FillForm(int id)
        {
            var regForm = _context.QuestionAnswers.Include(c => c.Question).
                ThenInclude(q => q.Options).FirstOrDefault(r => r.RegistrationId == id);
            var RegistrationFormDto = new RegistrationFormDto()
            {

            };

            return View(RegistrationFormDto);
        }
        [HttpPost]
        public IActionResult FillForm(RegistrationFormDto registrationForm)
        {
            foreach (var question in registrationForm.QuestionAnswers)
            {

                _context.QuestionAnswers.Update(question);
            }
            //var tmpRng = _context.CompanyBrokers.Where(b => b.BrokerId == brokerCreate.Broker.Id);
            //_context.CompanyBrokers.RemoveRange(tmpRng);
            //if (brokerCreate.CompanyIds != null)
            //{
            //}

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
