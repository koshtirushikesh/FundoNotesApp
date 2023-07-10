﻿using CommanLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class NoteRepository : INoteRepository
    {
        private readonly FundoContext fundoContext;

        public NoteRepository(FundoContext fundoContext)
        {
            this.fundoContext = fundoContext;
        }

        public NoteModel AddingNote(int userID, NoteModel noteModel)
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
                noteEntity.UserID = userID;

                fundoContext.Add(noteEntity);
                fundoContext.SaveChanges();

                return noteModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteEntity> GetAllNotes()
        {
            try
            {
                List<NoteEntity> resultNotes = fundoContext.Note.ToList();
                // var resultLable = fundoContext.Lable.Where(x => x.UserID == userID).FirstOrDefault();
                return resultNotes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NoteEntity> GetAllNotesByUserID(int userID)
        {
            try
            {
                List<NoteEntity> resultNotes = fundoContext.Note.Where(x => x.UserID == userID).ToList();
                // var resultLable = fundoContext.Lable.Where(x => x.UserID == userID).FirstOrDefault();
                return resultNotes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity GetNoteByNoteID(int noteID,int userID)
        {
            try
            {
                var resultByNoteID = fundoContext.Note.Where(x => x.NoteID == noteID && x.UserID == userID).FirstOrDefault();
                return resultByNoteID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateNote(int noteID, string descripction)
        {
            try
            {
                var result = fundoContext.Note.Where(x => x.NoteID == noteID).FirstOrDefault();
                result.Description = descripction;

                fundoContext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public NoteEntity ArchiveAndUnArchive(int noteID, int UserID)
        {
            try
            {
                NoteEntity noteEntity = fundoContext.Note.Where(x => x.NoteID == noteID && x.UserID == UserID).FirstOrDefault();

                if (noteEntity.ISArchive == true)
                {
                    noteEntity.ISArchive = false;
                    fundoContext.SaveChanges();
                    return noteEntity;
                }

                if (noteEntity.ISArchive == false)
                {
                    noteEntity.ISArchive = true;
                    fundoContext.SaveChanges();
                    return noteEntity;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public NoteEntity PindAndUnPinned(int noteID, int UserID)
        {
            try
            {
                NoteEntity noteEntity = fundoContext.Note.Where(x => x.NoteID == noteID && x.UserID == UserID).FirstOrDefault();

                if (noteEntity.IsPinned == true)
                {
                    noteEntity.IsPinned = false;
                    fundoContext.SaveChanges();
                    return noteEntity;
                }

                if (noteEntity.IsPinned == false)
                {
                    noteEntity.IsPinned = true;
                    fundoContext.SaveChanges();
                    return noteEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity TrashAndUnTrash(int noteID, int UserID)
        {
            try
            {
                NoteEntity noteEntity = fundoContext.Note.Where(x => x.NoteID == noteID && x.UserID == UserID).FirstOrDefault();
                if (noteEntity.IsTrash == true)
                {
                    noteEntity.IsTrash = false;
                    fundoContext.SaveChanges();
                    return noteEntity;
                }

                if(noteEntity.IsTrash == false)
                {
                    noteEntity.IsTrash = true;
                    fundoContext.SaveChanges();
                    return noteEntity;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool deleteNoteByNoteID(int noteID)
        {
            try
            {
                var resultLabel = fundoContext.Lable.Where(x => x.NoteID == noteID).FirstOrDefault();
                var resultNote = fundoContext.Note.Where(x => x.NoteID == noteID).FirstOrDefault();

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public string AddLable(string LableName, int UserID ,int noteID)
        {
            try
            {
                var result = fundoContext.Note.Where(x => x.UserID == UserID && x.NoteID == noteID).FirstOrDefault();
                if (result != null)
                {
                    LableEntity lableEntity = new LableEntity();
                    lableEntity.LableName = LableName;
                    lableEntity.UserID = UserID;
                    lableEntity.NoteID = result.NoteID;

                    fundoContext.Add(lableEntity);
                    fundoContext.SaveChanges();
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
