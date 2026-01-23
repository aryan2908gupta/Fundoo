using ModelLayer.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interface
{
    public interface INoteBL
    {
        Task<NoteResponseDto> CreateNoteAsync(NoteCreateRequestDto dto, int userId);
        Task<IEnumerable<NoteResponseDto>> GetAllNotesAsync(int userId, bool? archived, bool? trashed);

        Task<NoteResponseDto> GetNoteByIdAsync(int noteId, int userId);

        Task<NoteResponseDto> UpdateNoteAsync(int noteId, int userId, NoteUpdateRequestDto dto);

        Task<bool> MoveToTrashAsync(int noteId, int userId);
        Task<bool> RestoreAsync(int noteId, int userId);
        Task<bool> PermanentDeleteAsync(int noteId, int userId);

        Task<bool> ArchiveAsync(int noteId, int userId);
        Task<bool> UnarchiveAsync(int noteId, int userId);

        Task<bool> PinAsync(int noteId, int userId);
        Task<bool> UnpinAsync(int noteId, int userId);

        Task<bool> ChangeColorAsync(int noteId, int userId, string color);
    }
}
