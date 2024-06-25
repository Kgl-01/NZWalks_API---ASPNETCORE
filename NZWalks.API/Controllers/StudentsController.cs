using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "Yogi", "Karthik", "Tanisi", "Kavya", "Yashwanth" };
            return Ok(studentNames);
        }
    }

}
