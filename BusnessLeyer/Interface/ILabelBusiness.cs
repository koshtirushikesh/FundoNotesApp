using RepositoryLayer.Entity;

namespace BusinessLeyer.Interface
{
    public interface ILabelBusiness
    {
        public LableEntity AddLable(string LableName, int noteID, int UserID);
        public LableEntity UpdateLabel(string labelName, int noteID, int userID);
    }
}
