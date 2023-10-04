using Microsoft.EntityFrameworkCore;
using NotesShokhov.Data;
using NotesShokhov.Interfaces;
using NotesShokhov.Models;

namespace NotesShokhov.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<Note>> GetAllNotesAsync()
        {
            var allNotes = _context.Notes;
            return await Task.FromResult(allNotes);
        }       
        public async Task<bool> AddAsync(Note note)
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
