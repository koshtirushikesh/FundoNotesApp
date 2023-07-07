using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        public NoteModel AddingNote(int userID, NoteModel noteModel);
    }
}
