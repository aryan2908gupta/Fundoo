using ModelLayer.Entities;
using System;
using System.Collections.Generic;
//using System.Reflection.Emit;
using System.Text;

namespace DataLogicLayer.Interface
{
    public interface ILabelDL
    {
        Task<Label> CreateLabelAsync(Label label);
        Task<IEnumerable<Label>> GetLabelsByUserIdAsync(int userId);
        Task<IEnumerable<Note>> GetNotesByLabelIdAsync(int labelId, int userId);
        Task<Label> UpdateLabelAsync(Label label);
        Task<bool> DeleteLabelAsync(int labelId, int userId);
        Task<bool> LabelExistsAsync(int labelId, int userId);
    }
}
