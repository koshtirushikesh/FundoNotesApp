using BusinessLeyer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLeyer.Service
{
    public class CollaborationBusiness : ICollaborationBusiness
    {
        private readonly ICollaborationRepository collaborationRepository;
        public CollaborationBusiness(ICollaborationRepository collaborationRepository)
        {
            this.collaborationRepository = collaborationRepository;
        }


        public CollaborationEntity AddCollaboration(string collaborationEmail, int noteID, int userID)
        {
            try { return collaborationRepository.AddCollaboration(collaborationEmail, noteID, userID); } catch (Exception ex) { throw ex; }
        }

        public List<CollaborationEntity> GetCollaborationByNoteID(int noteID, int userID)
        {
            try { return collaborationRepository.GetCollaborationByNoteID(noteID, userID); } catch (Exception ex) { throw ex; }
        }

        public CollaborationEntity RemoveCollaborationByNoteID(int noteID, int userID)
        {
            try { return collaborationRepository.RemoveCollaborationByNoteID(noteID, userID); } catch (Exception ex) { throw ex; }
        }
    }
}
