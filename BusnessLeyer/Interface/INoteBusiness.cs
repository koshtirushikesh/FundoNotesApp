using CommanLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLeyer.Interface
{
    public interface INoteBusiness
    {
        public NoteModel AddingNote(int userID, NoteModel noteModel);
    }
}
