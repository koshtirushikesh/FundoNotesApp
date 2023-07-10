using CommanLayer;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        public NoteModel AddingNote(int userID, NoteModel noteModel);
        public List<NoteEntity> GetAllNotes();
        public NoteEntity ArchiveAndUnArchive(int noteID, int UserID);
        public NoteEntity PindAndUnPinned(int noteID, int UserID);
        public NoteEntity TrashAndUnTrash(int noteID, int UserID);
    }
}
