using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        public LableEntity AddLable(string LableName, int noteID, int UserID);
        public LableEntity UpdateLabel(string labelName, int noteID, int userID);
    }
}
