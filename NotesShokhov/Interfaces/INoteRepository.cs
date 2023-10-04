using NotesShokhov.Models;

namespace NotesShokhov.Interfaces
{
    public interface INoteRepository
    {
        Task<IQueryable<Note>> GetAllNotesAsync();
        Task<bool> AddAsync(Note note);
        Task<bool> UpdateAsync(Note editNote);
        Task<bool> SaveAsync();
    }
}
