using System.Collections.Generic;
using System.Threading.Tasks;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotes();
    Task<Note> GetNoteById(int id);
    Task<Note> CreateNote(Note note);
    Task<Note> UpdateNote(int id, Note note);
    Task<bool> ArchiveNote(int id);
    Task<bool> UnarchiveNote(int id);
    Task<bool> DeleteNote(int id);
    Task<IEnumerable<Note>> GetNotesByCategory(string category);
}
