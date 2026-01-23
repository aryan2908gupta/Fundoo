using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

using ModelLayer.Entities;
using ModelLayer.Dto;

namespace BusinessLogicLayer.Mapping
{
    public class NoteMapper : Profile
    {
        public NoteMapper() {
            CreateMap<Note, NoteResponseDto>()
                .ReverseMap();

            CreateMap<NoteRequestDto, Note>();
        } 
    }
}
