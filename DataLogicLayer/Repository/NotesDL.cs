using DataLogicLayer.Data;
using DataLogicLayer.Interface;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace DataLogicLayer.Repository
{
   public class NotesDL : INoteDL
    {
        private readonly ApplicationDbContext context;
        public NotesDL(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            await context.Notes.AddAsync(note);
            context.SaveChangesAsync();
            return note;
        }
      public  async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await context.Notes.ToListAsync();
        }
        public async Task<Note> GetNoteByIdAsync(int noteId, int userId)
        {
            return await context.Notes.FirstOrDefaultAsync(n => n.NotesId == noteId && n.UserId == userId);
        }
        public async Task<Note> UpdateNoteAsync(Note note)
        {
            note.UpdatedAt = DateTime.UtcNow;
            context.Notes.Update(note);
            await context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> MoveToTrashAsync(int noteId, int userId)
        {
            var note = await GetNoteByIdAsync(noteId, userId);
            if (note == null) return false;

            note.IsTrash = true;
            note.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RestoreAsync(int noteId, int userId)
        {
            var note = await GetNoteByIdAsync(noteId, userId);
            if (note == null) return false;

            note.IsTrash = false;
            note.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Note>> GetTrashedAsync(int userId)
        {
            return await context.Notes.Where(n => n.UserId == userId && n.IsTrash == true).ToListAsync();
        }
        public async Task<bool> PermanentDeleteAsync(int noteId, int userId)
        {
            var note = await GetNoteByIdAsync(noteId, userId);
            if (note == null) return false;

            context.Notes.Remove(note);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ArchiveAsync(int noteId, int userId)
        {
            var note = await GetNoteByIdAsync(noteId, userId);
            if (note == null) return false;

            note.IsArchive = true;
            note.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UnarchiveAsync(int noteId, int userId)
        {
            var note = await GetNoteByIdAsync(noteId, userId);
            if (note == null) return false;

            note.IsArchive = false;
            note.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Note>> GetArchivedAsync(int userId)
        {
            return await context.Notes
                .Where(n => n.UserId == userId && n.IsArchive && !n.IsTrash)
                .ToListAsync();
        }
        public async Task<bool> PinAsync(int noteId, int userId)
        {
            var note = await GetNoteByIdAsync(noteId, userId);
            if (note == null) return false;

            note.IsPin = true;
            note.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> UnpinAsync(int noteId, int userId)
        {
            var note = await GetNoteByIdAsync(noteId, userId);
            if (note == null) return false;

            note.IsPin = false;
            note.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ChangeColorAsync(int noteId, int userId, string color)
        {
            var note = await GetNoteByIdAsync(noteId, userId);
            if (note == null) return false;

            note.Colour = color;
            note.UpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }


    }
}
