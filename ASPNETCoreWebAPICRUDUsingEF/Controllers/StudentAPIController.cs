using ASPNETCoreWebAPICRUDUsingEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ASPNETCoreWebAPICRUDUsingEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly AspDotNetCoreDBContext context;

        public StudentAPIController(AspDotNetCoreDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetStudent()
        {
            var data = await context.Students.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var data = await context.Students.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(data);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student std)
        {
            await context.Students.AddAsync(std);
            await context.SaveChangesAsync();
            return Ok(std);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> UpdateSudent(int id,Student std)
        {
            if(id !=std.Id)
            {
                return BadRequest();
            }
            else
            {
                context.Entry(std).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(std);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteSudent(int id)
        {
            var std=await context.Students.FindAsync(id);    
            if(std == null)
            {
                return NotFound();
            }
            else
            {
                context.Students.Remove(std);
                await context.SaveChangesAsync();

                return Ok(std);
            }
        }
    }
}
