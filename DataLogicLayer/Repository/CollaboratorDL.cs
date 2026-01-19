using DataLogicLayer.Data;
using DataLogicLayer.Interface;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataLogicLayer.Repository
{
    public class CollaboratorDL : ICollaboratorDL
    {
        private readonly ApplicationDbContext context;

        public CollaboratorDL(ApplicationDbContext context)
        {
            this.context = context;
        }
        //  Add Collaborator
        public async Task<Collaborator> AddCollaboratorAsync(Collaborator collaborator)
        {
            await context.Collaborators.AddAsync(collaborator);
            await context.SaveChangesAsync();
            return collaborator;
        }

        public async Task<IEnumerable<Collaborator>> GetCollaboratorsByNoteIdAsync(int noteId, int userId)
        {
            // Security: note should belong to userId
            var noteBelongsToUser = await context.Notes
                .AnyAsync(n => n.NotesId == noteId && n.UserId == userId);

            if (!noteBelongsToUser)
                return new List<Collaborator>();

            return await context.Collaborators
                .Where(c => c.NoteId == noteId)
                .ToListAsync();
        }

        //  Remove Collaborator (only note owner's access)
        public async Task<bool> RemoveCollaboratorAsync(int collaboratorId, int userId)
        {
            // Find collaborator with its Note
            var collaborator = await context.Collaborators
                .Include(c => c.Notes) // Note navigation
                .FirstOrDefaultAsync(c => c.CollaboratorId == collaboratorId);

            if (collaborator == null)
                return false;

            // Security: only owner of that note can remove collaborator
            if (collaborator.Notes.UserId != userId)
                return false;

            context.Collaborators.Remove(collaborator);
            await context.SaveChangesAsync();
            return true;
        }

        //  Check if Collaborator exists for Note
        public async Task<bool> IsCollaboratorExistsAsync(int noteId, string email)
        {
            return await context.Collaborators
                .AnyAsync(c => c.NoteId == noteId && c.Email == email);
        }
    }

}
