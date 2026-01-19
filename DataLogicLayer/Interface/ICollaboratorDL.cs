using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLogicLayer.Interface
{
    public interface ICollaboratorDL
    {
        Task<Collaborator> AddCollaboratorAsync(Collaborator collaborator);
        Task<IEnumerable<Collaborator>> GetCollaboratorsByNoteIdAsync(int noteId, int userId);
        Task<bool> RemoveCollaboratorAsync(int collaboratorId, int userId);
        Task<bool> IsCollaboratorExistsAsync(int noteId, string email);
    }
}
