using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class CollaborationRepository : ICollaborationRepository
    {
        private readonly FundoContext fundoContext;
        

        public CollaborationRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public CollaborationEntity AddCollaboration(string collaborationEmail, int noteID, int userID)
        {
            try
            {
                var checkEmail = fundoContext.Collaboration.Where(x => x.CollaborationEmail == collaborationEmail && x.NoteID == noteID && x.UserID == userID);

                if (checkEmail != null)
                {
                    CollaborationEntity collaborationEntity = new CollaborationEntity();

                    collaborationEntity.CollaborationEmail = collaborationEmail;
                    collaborationEntity.UserID = userID;
                    collaborationEntity.NoteID = noteID;

                    fundoContext.Add(collaborationEntity);
                    fundoContext.SaveChanges();

                    return collaborationEntity;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CollaborationEntity> GetCollaborationByNoteID(int noteID, int userID)
        {
            try
            {
                List<CollaborationEntity> collaborationEntity = fundoContext.Collaboration.Where(x => x.NoteID == noteID && x.UserID == userID).ToList();

                if (collaborationEntity != null)
                {
                    return collaborationEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CollaborationEntity RemoveCollaborationByNoteID(int noteID, int userID)
        {
            try
            {
                CollaborationEntity collaborationEntity = fundoContext.Collaboration.Where(x => x.NoteID == noteID && x.UserID == userID).FirstOrDefault();
                if (collaborationEntity != null)
                {
                    fundoContext.Remove(collaborationEntity);
                    fundoContext.SaveChanges();
                    return collaborationEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
