using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;
using AutoMapper;

namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public JobsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Jobs
        // Per specification it returns a list of jobs with assigned model names, if any
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobWModelNames>>> GetJobs()
        {
            List<JobWModelNames> allJobs = new List<JobWModelNames>();
            foreach (Job j in _context.Jobs)
            {
                _context.Entry(j).Collection(job => job.Models).Load();
                JobWModelNames jobWModelNames = _mapper.Map<JobWModelNames>(j);
                jobWModelNames.ModelNames = new List<string>();
                if (j.Models == null)
                {
                    allJobs.Add(jobWModelNames);
                    break;
                }

                foreach (Model m in j.Models)
                {
                    string s = m.FirstName + " " + m.LastName;
                    jobWModelNames.ModelNames.Add(s);
                }
                allJobs.Add(jobWModelNames);
            }
            return allJobs;
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDtoWExpenses>> GetJob(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            _context.Entry(job)
                .Collection(j => j.Models)
                .Load();
            _context.Entry(job)
                .Collection(j => j.Expenses)
                .Load();
            

            JobDtoWExpenses retJob = _mapper.Map<JobDtoWExpenses>(job);
            retJob.Expenses = new List<ExpenseDto>();
            foreach (Expense e in job.Expenses)
            { 
                ExpenseDto eDto = _mapper.Map<ExpenseDto>(e); 
                retJob.Expenses.Add(eDto);
            }

            return retJob;
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<JobDtoSimple>> PutJob(long id, JobDtoUpdate job)
        {
            Job? target = await _context.Jobs.FindAsync(id);
            if (target == null)
            {
                return BadRequest();
            }

            target.StartDate = job.StartDate;
            target.Days = job.Days;
            target.Location = job.Location;
            target.Comments = job.Comments;
            
            _context.Entry(target).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            JobDtoSimple retJ = _mapper.Map<JobDtoSimple>(target);

            return retJ;
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobDtoNoId>> PostJob(JobDtoNoId job)
        {
            job.Models ??= new List<Model>();
            job.Expenses ??= new List<Expense>();
            _context.Jobs.Add(_mapper.Map<Job>(job));
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostJob",job);
        }

        // PUT: api/Jobs/AssignModel/1/2
        [HttpPut]
        [Route("api/Jobs/AssignModel/{jobId}/{modelId}")]
        public async Task<ActionResult<Job>> PostAssignModel(long jobId, long modelId)
        {
            var model = _context.Models.Single(m => m.ModelId == modelId);
            var job = _context.Jobs.Single(j => j.JobId == jobId);
            if (job.Models == null)
            {
                job.Models = new List<Model>();
            }
            job.Models.Add(model);
            await _context.SaveChangesAsync();

            return Accepted(model);
        }

        // PUT: api/Jobs/RemoveModel/1/2
        [HttpPut]
        [Route("api/Jobs/RemoveModel/{jobId}/{modelId}")]
        public async Task<ActionResult<Job>> PostRemoveModel(long jobId, long modelId)
        {
            var job = _context.Jobs.Single(j => j.JobId == jobId);
            _context.Entry(job)
                .Collection(j => j.Models)
                .Load();
            var model = _context.Models.Single(m => m.ModelId == modelId);
            job.Models.Remove(model);
            await _context.SaveChangesAsync();

            return job;
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}
