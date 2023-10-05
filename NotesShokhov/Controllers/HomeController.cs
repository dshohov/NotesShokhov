using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotesShokhov.Helpers;
using NotesShokhov.Interfaces;
using NotesShokhov.Models;
using NotesShokhov.ViewModels;
using System.Diagnostics;

namespace NotesShokhov.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHubContext<NoteHubSignalR> _hubContext;

        public HomeController(INoteRepository noteRepository, IHubContext<NoteHubSignalR> hubContext)
        {
            _noteRepository = noteRepository;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _noteRepository.GetAllNotesAsync();
            var sortNotes = notes.OrderByDescending(x => x.DateCreate);
            ViewData["CountNotes"] = sortNotes.Count();
            return View(sortNotes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(int lastId,string title, string text)
        {
            var note = new Note() {Title = title, Text = text, DateCreate = DateTime.UtcNow};
            await _noteRepository.AddAsync(note);
            var newNote = new Note { Id = lastId+1,Title = title, Text = text, DateCreate = DateTime.UtcNow };

            // Отправьте новую заметку всем подключенным клиентам
            await _hubContext.Clients.All.SendAsync("ReceiveNewNote", newNote);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditNote(int id, string title, string text)
        {
            var editNote = new Note() { Id = id, Title = title, Text = text};
            await _noteRepository.UpdateAsync(editNote);
            await _hubContext.Clients.All.SendAsync("ReceiveUpdateNoteMessage", "<h5>Some note have update!</h5>");

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}