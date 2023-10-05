using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotesShokhov.Helpers;
using NotesShokhov.Interfaces;
using NotesShokhov.Models;


namespace NotesShokhov.Controllers
{
    public class HomeController : Controller
    {
        //For work in database CRUD function
        private readonly INoteRepository _noteRepository;
        //For work with SignalR
        private readonly IHubContext<NoteHubSignalR> _hubContext;

        public HomeController(INoteRepository noteRepository, IHubContext<NoteHubSignalR> hubContext)
        {
            _noteRepository = noteRepository;
            _hubContext = hubContext;
        }

        //Main Page
        public async Task<IActionResult> Index()
        {

            var notes = await _noteRepository.GetAllNotesAsync();
            if(notes != null)
            {
                //Sort new notes in start, old notes in end
                var sortNotes = notes.OrderByDescending(x => x.DateCreate);
                //To show the total number of notes
                ViewData["CountNotes"] = sortNotes.Count();
                return View(sortNotes);
            }
            
            return RedirectToAction("Error");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNote(int lastId,string title, string text)
        {

            if(title != null && text!= null)
            {
                //Creating a note from elements
                var note = new Note() { Title = title, Text = text, DateCreate = DateTime.UtcNow };
                //If the note is not created, then there is no need to go further.
                if (await _noteRepository.AddNoteAsync(note))
                {
                    //Create New note for SignalR  
                    var newNote = new Note { Id = lastId + 1, Title = title, Text = text, DateCreate = DateTime.UtcNow };
                    //Send all users new note
                    await _hubContext.Clients.All.SendAsync("ReceiveNewNote", newNote);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Error"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNote(int id, string title, string text)
        {
            if(title != null && text != null && id > 0)
            {
                //Create edit note  
                var editNote = new Note() { Id = id, Title = title, Text = text };
                //Checking if the note has been updated, we write to all users
                //that everything worked out
                if (await _noteRepository.UpdateAsync(editNote))
                {
                    //Send message in SignalR that we update some note 
                    await _hubContext.Clients.All.SendAsync("ReceiveUpdateNoteMessage", "<h5>Some note have update!</h5>");
                    return RedirectToAction("Index");
                } 
            }
            return RedirectToAction("Error");

        }

        //If we have error we go to Error page
        public IActionResult Error()
        {
            return View();
        }

    }
}