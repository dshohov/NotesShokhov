using Microsoft.EntityFrameworkCore;
using NotesShokhov.Data;
using NotesShokhov.Interfaces;
using NotesShokhov.Models;

namespace NotesShokhov.Repositories
{
    public class NoteRepository : INoteRepository
    {
        //For work with database
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //All methods are asynchronous and work with the database
        //so as not to block threads while the database responds.
        //The base work is I/O operations and they must be asynchronous.
        public async Task<IQueryable<Note>> GetAllNotesAsync()
        {
            var allNotes = await Task.Run(() => _context.Notes);
            return allNotes;
        }       
        public async Task<bool> AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<bool> UpdateAsync(Note editNote)
        {
            //Find note with equals id
            var existingNote = await _context.Notes.FirstOrDefaultAsync(x => x.Id == editNote.Id);

            if (existingNote != null)
            {
                existingNote.Title = editNote.Title;
                existingNote.Text = editNote.Text;
                return await SaveAsync();
            }
            return false;
        }
    }
}
