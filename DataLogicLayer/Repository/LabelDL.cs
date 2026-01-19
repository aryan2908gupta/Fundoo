using DataLogicLayer.Data;
using DataLogicLayer.Interface;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DataLogicLayer.Repository
{
   public class LabelDL : ILabelDL
    {
        private readonly ApplicationDbContext context;

        public LabelDL(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Label> CreateLabelAsync(Label label)
        {
            await context.Labels.AddAsync(label);
            await context.SaveChangesAsync();
            return label;
        }

        public async Task<IEnumerable<Label>> GetLabelsByUserIdAsync(int userId)
        {
            return await context.Labels
                .Where(l => l.UserId == userId)
                .ToListAsync();
        }

        public async Task<Label> UpdateLabelAsync(Label label)
        {
            context.Labels.Update(label);
            await context.SaveChangesAsync();
            return label;
        }

        //  Get notes by labelId (for a specific user)
        public async Task<IEnumerable<Note>> GetNotesByLabelIdAsync(int labelId, int userId)
        {
            // label should belong to same user (security)
            var labelExists = await context.Labels
                .AnyAsync(l => l.LabelId == labelId && l.UserId == userId);

            if (!labelExists)
                return new List<Note>();

            // Many-to-many: Notes <-> Labels
            return await context.Notes
                .Where(n => n.UserId == userId && n.Labels.Any(l => l.LabelId == labelId))
                .ToListAsync();
        }
        public async Task<bool> DeleteLabelAsync(int labelId, int userId)
        {
            var label = await context.Labels
                .FirstOrDefaultAsync(l => l.LabelId == labelId && l.UserId == userId);

            if (label == null)
                return false;

            context.Labels.Remove(label);
            await context.SaveChangesAsync();
            return true;
        }

        //  Check if label exists for a user
        public async Task<bool> LabelExistsAsync(int labelId, int userId)
        {
            return await context.Labels
                .AnyAsync(l => l.LabelId == labelId && l.UserId == userId);
        }
    }
}
