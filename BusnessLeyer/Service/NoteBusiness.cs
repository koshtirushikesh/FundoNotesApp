using BusinessLeyer.Interface;
using CommanLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;

namespace BusinessLeyer.Service
{
    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepository noteRepository;
        public NoteBusiness(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public NoteModel AddingNote(int userID, NoteModel noteModel)
        {
            try { return noteRepository.AddingNote(userID, noteModel); }catch (Exception ex) { throw ex; }
        }

        public NoteEntity ArchiveAndUnArchive(int noteID, int UserID)
        {
            try { return noteRepository.ArchiveAndUnArchive(noteID,UserID); }catch(Exception ex) { throw ex; }      
        }

        public NoteEntity PindAndUnPinned(int noteID, int UserID)
        {
            try { return noteRepository.PindAndUnPinned(noteID,UserID); }catch(Exception ex) { throw ex; };
        }

        public NoteEntity TrashAndUnTrash(int noteID, int UserID)
        {
            try { return noteRepository.TrashAndUnTrash(noteID,UserID); }catch(Exception ex) { throw ex; }
        }
    }
}
