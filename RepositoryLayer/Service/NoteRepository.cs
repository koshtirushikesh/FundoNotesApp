using CommanLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRepository : INoteRepository
    {
        private readonly FundoContext fundoContext;

        public NoteRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public NoteModel AddingNote(int userID , NoteModel noteModel)
        {
            try
            {
                var result = fundoContext.Note.Where(x => x.UserID == userID).FirstOrDefault();

                NoteEntity noteEntity = new NoteEntity();

                noteEntity.Title = noteModel.Title;
                noteEntity.Description = noteModel.Description;
                noteEntity.Colour = noteModel.Colour;
                noteEntity.Reminder = noteModel.Reminder;
                noteEntity.ISArchive = noteModel.ISArchive;
                noteEntity.IsPinned = noteModel.IsPinned;
                noteEntity.IsTrash = noteEntity.IsTrash;
                noteEntity.CreatedAt = noteModel.CreatedAt;
                noteEntity.LastUpdatedAt = noteModel.LastUpdatedAt;

                return noteModel;

            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
