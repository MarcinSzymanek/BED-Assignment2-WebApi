using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ModelsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ModelsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Models
        [HttpGet]
        public List<ModelDto> GetModels()
        {
            var models = from m in _context.Models
                select m;
            List<ModelDto> modelDtos = new List<ModelDto>();
            foreach (Model m in models)
            {
                ModelDto mDto = _mapper.Map<ModelDto>(m);
                modelDtos.Add(mDto);
            }
                     
            return modelDtos;
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelDtoFull>> GetModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            // Explicit loading: 
            // Makes sure related data is loaded
            _context.Entry(model)
                .Collection(m => m.Jobs)
                .Load();
            _context.Entry(model)
                .Collection(m => m.Expenses)
                .Load();

            ModelDtoFull retModel = _mapper.Map<ModelDtoFull>(model);
            if (model.Jobs != null)
            {
                retModel.Jobs = new List<JobDtoSimple>();
                foreach (Job j in model.Jobs)
                {
                    JobDtoSimple job = _mapper.Map<JobDtoSimple>(j);
                    retModel.Jobs.Add(job);
                }
            }

            if (model.Expenses != null)
            {
                retModel.Expenses = new List<ExpenseDto>();
                foreach (Expense e in model.Expenses)
                {
                    ExpenseDto expense = _mapper.Map<ExpenseDto>(e);
                    retModel.Expenses.Add(expense);
                }
            }
            return retModel;
        }

        
        // POST: api/Models
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModelDto>> PostModel(ModelDto model)
        {

            Model newModel = _mapper.Map<Model>(model);
            _context.Models.Add(newModel);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetModel",  model);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(long id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelExists(long id)
        {
            return _context.Models.Any(e => e.ModelId == id);
        }
    }
}
