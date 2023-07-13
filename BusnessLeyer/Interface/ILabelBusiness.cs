using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace BusinessLeyer.Interface
{
    public interface ILabelBusiness
    {
        public LableEntity AddLable(string LableName, int noteID, int UserID);
        public LableEntity UpdateLabel(string labelName, int noteID, int userID);
        public LableEntity RemoveLabel(int labelID, int noteID, int userID);
        public List<LableEntity> GetAllNoteByLabelID(int noteID, int userID);
    }
}
