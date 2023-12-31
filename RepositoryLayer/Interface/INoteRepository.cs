﻿using CommanLayer;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        public NoteModel AddingNote(int userID, NoteModel noteModel);
        public List<NoteEntity> GetAllNotes();
        public List<NoteEntity> GetAllNotesByUserID(int userID);
        public NoteEntity GetNoteByNoteID(int noteID, int userID);
        public NoteEntity UpdateNote(int noteID, int userID, string descripction);
        public NoteEntity ArchiveAndUnArchive(int noteID, int UserID);
        public NoteEntity PindAndUnPinned(int noteID, int UserID);
        public NoteEntity TrashAndUnTrash(int noteID, int UserID);
        public string deleteNoteByNoteID(int noteID);
        public string UploadeImage(string filePath, int noteID, int userID);
    }
}
