

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAll();
    Task<IEnumerable<Note>> GetActiveNotes();
    Task<IEnumerable<Note>> GetArchivedNotes();
    Task<Note> GetById(int id);
    Task Add(Note note);
    Task Update(Note note);
    Task Delete(int id);
    Task Archive(int id);
    Task Unarchive(int id);
    Task<IEnumerable<Note>> GetNotesByCategory(string category);
}

