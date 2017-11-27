using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace recru_it.Controllers
{
    public class Note
    {
        public string Title { get; set; }
        public string Contents { get; set; }
    }
    
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        [HttpGet("")]
        public IEnumerable<Note> List(string username)
        {
            return new List<Note>{
                new Note{Title = "Shopping list", Contents="Some Apples"},
                new Note{Title = "Thoughts on .NET Core", Contents="To follow..."}
             };
        }
    }
}