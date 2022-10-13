using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Hubs;
using Assignment2.Models;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Assignment2.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        // Database context
        private readonly DataContext _context;
        // Hub context used to send messages to the clients (html page here)
        private readonly IHubContext<MessageHub> _hub;
        private readonly IMapper _mapper;
        public ExpensesController(DataContext context, IMapper mapper, IHubContext<MessageHub> hub)
        {
            _context = context;
            _hub = hub;
            _mapper = mapper;
        }

        // As per API specification, only POST request for expenses

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobDtoWExpenses>> PostExpense(ExpenseDtoPost expense)
        {
            var model = _context.Find<Model>(expense.ModelId);
            var job = _context.Find<Job>(expense.JobId);
            

            if (model == null || job == null)
            {
                if (model == null)
                    return NotFound("Model not found");
                return NotFound("Job not found");
            }

            _context.Entry(job)
                .Collection(j => j.Models)
                .Load();

            if (!job.Models.Contains(model))
            {
                return NotFound("model not assigned to this job");
            }         
            _context.Expenses.Add(_mapper.Map<Expense>(expense));
            await _context.SaveChangesAsync();
            
            // Display a message via message hub
            var modelNames = from m in _context.Models
               // where m.ModelId == expense.ModelId
                select m.FirstName;
            List<string> mName = modelNames.ToList();
            var customerName = from j in _context.Jobs
                where j.JobId == expense.JobId
                select j.Customer;
            List<string> sCustomer = customerName.ToList();

            await _hub.Clients.All.SendAsync("NotifyMessage", expense, mName[0], sCustomer[0]);


            _context.Entry(job)
                .Collection(j => j.Expenses)
                .Load();

            return Accepted(_mapper.Map<JobDtoWExpenses>(job));
        }
        
        private bool ExpenseExists(long id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }

    }
}
