using Microsoft.EntityFrameworkCore;


public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _context;

    public NoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetAll()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<IEnumerable<Note>> GetActiveNotes()
    {
        return await _context.Notes.Where(n => !n.IsArchived).ToListAsync();
    }

    public async Task<IEnumerable<Note>> GetArchivedNotes()
    {
        return await _context.Notes.Where(n => n.IsArchived).ToListAsync();
    }

    public async Task<Note> GetById(int id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task Add(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Note note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note != null)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Archive(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note != null)
        {
            note.IsArchived = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task Unarchive(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note != null)
        {
            note.IsArchived = false;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Note>> GetNotesByCategory(string category)
    {
        return await _context.Notes.Where(n => n.Categories.Contains(category) && !n.IsArchived).ToListAsync();
    }
}
