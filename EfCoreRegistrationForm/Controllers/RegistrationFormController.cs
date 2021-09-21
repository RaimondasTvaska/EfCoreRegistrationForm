using EfCoreRegistrationForm.Data;
using EfCoreRegistrationForm.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Edit(int id)
        {
            var registrationForm = _context.QuestionAnswers.Include(c => c.Question).
                ThenInclude(q => q.Options).FirstOrDefault(r => r.RegistrationId == id);
            var RegistrationFormDto = new RegistrationFormDto()
            {

            };

            return View(registrationForm);
        }
        [HttpPost]
        public IActionResult Edit(BrokerCreate brokerCreate)
        {
            var tmpRng = _context.CompanyBrokers.Where(b => b.BrokerId == brokerCreate.Broker.Id);
            _context.CompanyBrokers.RemoveRange(tmpRng);
            if (brokerCreate.CompanyIds != null)
            {
                foreach (var companyId in brokerCreate.CompanyIds)
                {
                    var companyBroker = new CompanyBroker()
                    {
                        Broker = brokerCreate.Broker,
                        CompanyId = companyId
                    };
                    _context.CompanyBrokers.Add(companyBroker);
                }
            }
            _context.Brokers.Update(brokerCreate.Broker);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
