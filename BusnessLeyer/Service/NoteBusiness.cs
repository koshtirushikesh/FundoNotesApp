using CommanLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLeyer.Service
{
    public class NoteBusiness :INoteRepository
    {
        private readonly INoteRepository noteRepository;
        public NoteBusiness(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public NoteModel AddingNote(int userID, NoteModel noteModel)
        {
            return noteRepository.AddingNote(userID, noteModel);
        }
    }
}
