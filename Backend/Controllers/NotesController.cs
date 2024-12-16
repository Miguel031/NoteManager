using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
    {
        var notes = await _noteService.GetAllNotes();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetNoteById(int id)
    {
        var note = await _noteService.GetNoteById(id);
        if (note == null)
        {
            return NotFound();
        }
        return Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<Note>> Post(Note note)
    {
        var createdNote = await _noteService.CreateNote(note);
        return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, createdNote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Note note)
    {
        var updatedNote = await _noteService.UpdateNote(id, note);
        if (updatedNote == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> Archive(int id)
    {
        var success = await _noteService.ArchiveNote(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPatch("{id}/unarchive")]
    public async Task<IActionResult> Unarchive(int id)
    {
        var success = await _noteService.UnarchiveNote(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _noteService.DeleteNote(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotesByCategory(string category)
    {
        var notes = await _noteService.GetNotesByCategory(category);
        if (!notes.Any())
        {
            return NotFound(new { message = "No notes found for the given category." });
        }
        return Ok(notes);
    }
}
