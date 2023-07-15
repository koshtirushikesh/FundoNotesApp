using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaborationRepository
    {
        public CollaborationEntity AddCollaboration(string collaborationEmail, int noteID, int userID);
        public List<CollaborationEntity> GetCollaborationByNoteID(int noteID, int userID);
        public CollaborationEntity RemoveCollaborationByNoteID(int noteID, int userID);
    }
}
