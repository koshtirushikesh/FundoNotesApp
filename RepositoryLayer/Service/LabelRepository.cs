using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FundoContext fundoContext;
        public LabelRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public LableEntity AddLable(string lableName, int noteID, int userID)
        {
            try
            {
                var result = fundoContext.Note.Where(x => x.UserID == userID && x.NoteID == noteID).FirstOrDefault();
                if (result != null)
                {
                    LableEntity lableEntity = new LableEntity();
                    lableEntity.LableName = lableName;
                    lableEntity.UserID = userID;
                    lableEntity.NoteID = result.NoteID;

                    fundoContext.Add(lableEntity);
                    fundoContext.SaveChanges();
                    return lableEntity;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LableEntity UpdateLabel(string labelName, int noteID, int userID)
        {
            try
            {
                LableEntity lableEntity = fundoContext.Lable.Where(x => x.UserID == userID && x.NoteID == noteID).FirstOrDefault();

                if (lableEntity != null)
                {
                    lableEntity.LableName = labelName;

                    fundoContext.SaveChanges();
                    return lableEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LableEntity RemoveLabel(int labelID, int noteID, int userID)
        {
            try
            {
                LableEntity lableEntity = fundoContext.Lable.Where(x => x.LableID == labelID && x.NoteID == noteID && x.UserID == userID).FirstOrDefault();

                if (lableEntity != null)
                {
                    fundoContext.Remove(lableEntity);
                    fundoContext.SaveChanges();

                    return lableEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LableEntity> GetAllNoteByLabelID(int noteID, int userID)
        {
            try
            {
                List<LableEntity> userLabelResult = fundoContext.Lable.Where(x => x.NoteID == noteID && x.UserID == userID).ToList();
                if (userLabelResult != null)
                {
                    return userLabelResult;
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
