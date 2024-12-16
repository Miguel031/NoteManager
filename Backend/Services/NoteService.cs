using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class NoteService : INoteService
{
    private readonly AppDbContext _context;

    public NoteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetAllNotes()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note> GetNoteById(int id)
    {
        return await _context.Notes.FindAsync(id);
    }

    public async Task<Note> CreateNote(Note note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<Note> UpdateNote(int id, Note note)
    {
        var existingNote = await _context.Notes.FindAsync(id);
        if (existingNote == null)
        {
            return null;
        }

        existingNote.Content = note.Content;
        existingNote.Tag = note.Tag;
        existingNote.Categories = note.Categories;
        existingNote.IsArchived = note.IsArchived;

        await _context.SaveChangesAsync();
        return existingNote;
    }

    public async Task<bool> ArchiveNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return false;
        }

        note.IsArchived = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnarchiveNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return false;
        }

        note.IsArchived = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
        {
            return false;
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Note>> GetNotesByCategory(string category)
    {
        return await _context.Notes
                             .Where(n => n.Categories.Contains(category))
                             .ToListAsync();
    }
}
