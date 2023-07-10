using CommanLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLeyer.Interface
{
    public interface INoteBusiness
    {
        public NoteModel AddingNote(int userID, NoteModel noteModel);
        public NoteEntity ArchiveAndUnArchive(int noteID, int UserID);
        public NoteEntity PindAndUnPinned(int noteID, int UserID);
        public NoteEntity TrashAndUnTrash(int noteID, int UserID);
    }
}
