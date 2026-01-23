using AutoMapper;
using BusinessLogicLayer.Interface;
using BusinessLogicLayer.Mapping;
using DataLogicLayer.Interface;
using DataLogicLayer.Repository;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Dto;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Repository
{
    public class NoteBL : INoteBL
    {
        private readonly INoteDL noteDL;
        private readonly IMapper mapper;    

        public NoteBL(IMapper mapper,INoteDL noteDL)
        {
            this.noteDL = noteDL;
            this.mapper = mapper;
        }

        public async Task<NoteResponseDto> CreateNoteAsync(NoteCreateRequestDto dto, int userId)
        {
            var note =  mapper.Map<Note>(dto);
            await noteDL.CreateNoteAsync(note);
            return mapper.Map<NoteResponseDto>(note);
        }

        // Ek baar dekhna zaroor hain
     public async Task<IEnumerable<NoteResponseDto>> GetAllNotesAsync(int userId, bool? archived, bool? trashed)
        {
            var notes = await noteDL.GetAllNotesAsync(userId, archived, trashed);

            return mapper.Map<IEnumerable<NoteResponseDto>>(notes);
        }


        public async Task<NoteResponseDto> GetNoteByIdAsync(int noteId, int userId) { 

            var responeNote = await  noteDL.GetNoteByIdAsync(noteId, userId);
            if (responeNote == null) {
                throw new Exception("Note not Found");
            }
            return mapper.Map<NoteResponseDto>(responeNote);
        }

        public async Task<NoteResponseDto> UpdateNoteAsync(int noteId, int userId, NoteUpdateRequestDto dto)
        {
            var note = await noteDL.GetNoteByIdAsync(noteId, userId);

            if (note == null)
                throw new Exception("Note not found");

            note.Title = dto.Title;
            note.Description = dto.Description;
            note.Colour = dto.Colour ?? note.Colour;
            note.Reminder = dto.Reminder;
            note.UpdatedAt = DateTime.UtcNow;

            var updated = await noteDL.UpdateNoteAsync(note);
            return mapper.Map<NoteResponseDto>(updated);
        }

        public async Task<bool> MoveToTrashAsync(int noteId, int userId)
        {
            return await noteDL.MoveToTrashAsync(noteId,userId);
        }

        public async Task<bool> RestoreAsync(int noteId, int userId)
        {
            return await noteDL.RestoreAsync(noteId,userId);
        }

      public async Task<bool> PermanentDeleteAsync(int noteId, int userId)
        {
            return await noteDL.PermanentDeleteAsync(noteId,userId);
        }

        public async Task<bool> ArchiveAsync(int noteId, int userId)
        {
            return await noteDL.ArchiveAsync(noteId,userId);
        }

        public async Task<bool> UnarchiveAsync(int noteId, int userId)
        {
            return await noteDL.UnarchiveAsync(noteId,userId);    
        }

       public async Task<bool> PinAsync(int noteId, int userId)
        {
            return await noteDL.PinAsync(noteId,userId);  
        }
     public async Task<bool> UnpinAsync(int noteId, int userId)
        {
            return await noteDL.UnpinAsync(noteId,userId) ;
        }

      public  async Task<bool> ChangeColorAsync(int noteId, int userId, string color)
        {
            return await noteDL.ChangeColorAsync(noteId,userId,color);
        }
    }
}
