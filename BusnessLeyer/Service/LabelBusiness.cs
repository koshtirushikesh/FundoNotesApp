using BusinessLeyer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;

namespace BusinessLeyer.Service
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepository labelRepository;
        public LabelBusiness(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public LableEntity AddLable(string LableName, int noteID, int UserID)
        {
            try { return labelRepository.AddLable(LableName, noteID, UserID); } catch (Exception ex) { throw ex; }
        }

        public LableEntity UpdateLabel(string labelName, int noteID, int userID)
        {
            try { return labelRepository.UpdateLabel(labelName, noteID, userID); }catch(Exception ex) { throw ex; }
        }
    }
}
