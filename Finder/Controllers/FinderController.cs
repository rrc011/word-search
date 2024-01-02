using Finder.Models;
using Finder.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Finder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinderController : ControllerBase
    {
        private readonly IWordFinder finderRepo;

        public FinderController(IWordFinder finderRepo)
        {
            this.finderRepo = finderRepo;
        }

        [HttpGet("generate/{length}")]
        public IActionResult Generate(int length)
        {
            var grid = finderRepo.GenerateMatrix(length);
            return Ok(grid);
        }
        
        [HttpGet()]
        public IActionResult GetMatrix()
        {
            var grid = finderRepo.GetMatrix();
            return Ok(grid);
        }

        [HttpPost]
        public IActionResult FindMatch([FromBody] List<LetterDto> input)
        {
            return Ok(finderRepo.FindAndMatch(input));
        }
        
        [HttpPost("findAndSelectWord")]
        public IActionResult FindAndSelectWord([FromBody] string word)
        {
            return Ok(finderRepo.FindAndSelectWord(word.ToUpper()));
        }
    }
}
