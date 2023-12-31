﻿using BusinessLeyer.Interface;
using CommanLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundoNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBusiness noteBusiness, IDistributedCache distributedCache)
        {
            this.noteBusiness = noteBusiness;
            this.distributedCache = distributedCache;
        }

        [Authorize]
        [HttpPost("add-note")]
        public IActionResult AddNotes(NoteModel noteModel)
        {
            try
            {
                string userID = User.FindFirst("UserID").Value;
                int userIDInt = Convert.ToInt32(userID);

                var result = noteBusiness.AddingNote(userIDInt, noteModel);
                if (result != null)
                {
                    return Ok(new ResponseModel<NoteModel> { status = true, message = "note added succesfully", response = noteModel });
                }
                return BadRequest(new ResponseModel<NoteModel> { status = false, message = "note not added" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get-all-note")]
        public IActionResult GetAllNote()
        {
            try
            {
                List<NoteEntity> notes = noteBusiness.GetAllNotes();
                if (notes != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity>> { status = true, message = "All Notes are featch successfully", response = notes });
                }

                return BadRequest(new ResponseModel<string> { status = false, message = "All Notes are not featch successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get-all-notes-by-userid")]
        public IActionResult GetAllNoteByUserID()
        {
            try
            {
                string userID = User.FindFirst("UserID").Value;
                int userIDInt = Convert.ToInt32(userID);

                List<NoteEntity> notesByUserID = noteBusiness.GetAllNotesByUserID(userIDInt);
                if (notesByUserID != null)
                {
                    return Ok(new ResponseModel<List<NoteEntity>> { status = true, message = "All Notes by userid are featch successfully", response = notesByUserID });
                }
                return BadRequest(new ResponseModel<string> { status = false, message = "All notes by user id are not featch successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get-note-by-noteID")]
        public IActionResult GetNoteByNoteID(int noteID)
        {
            try
            {
                string userID = User.FindFirst("UserID").Value;
                int userIDInt = Convert.ToInt32(userID);

                NoteEntity noteEntity = noteBusiness.GetNoteByNoteID(noteID, userIDInt);
                if (noteEntity != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { status = true, message = "note by noteID featched succesfully", response = noteEntity });
                }
                return BadRequest(new ResponseModel<String> { status = false, message = "note by noteID not featched succesfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get-all-note-by-redis")]
        public async Task<IActionResult> GetAllNotesUSingRedis()
        {
            try
            {
                var CasheKey = "NotesList";

                List<NoteEntity> noteList;
                byte[] RediesNoteList = await distributedCache.GetAsync(CasheKey);

                if (RediesNoteList != null)
                {
                    var serializedNoteList = Encoding.UTF8.GetString(RediesNoteList);
                    noteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);
                }
                else
                {
                    noteList = noteBusiness.GetAllNotes();
                    var serializedNoteList = JsonConvert.SerializeObject(noteList);
                    var redisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await distributedCache.SetAsync(CasheKey, redisNoteList, options);
                }
                return Ok(noteList); //new ResponseModel<List<NoteEntity>> { status = true, message = "Get all notes", response = noteList }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<NoteEntity> { status = false, message = ex.Message });
            }

        }

        [HttpPatch("update-note")]
        public IActionResult UpdateNote(int noteID, string descripction)
        {
            try
            {
                string userID = User.FindFirst("UserID").Value;
                int userIDInt = Convert.ToInt32(userID);

                NoteEntity noteEntity = noteBusiness.UpdateNote(noteID, userIDInt, descripction);
                if (noteEntity != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { status = true, message = "note updated succesfully", response = noteEntity });
                }

                return BadRequest(new ResponseModel<NoteEntity> { status = true, message = "note not updated succesfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("archive-unarchive")]
        public IActionResult ArchiveAndUnArchive(int noteID)
        {
            try
            {
                string userID = User.FindFirst("UserID").Value;
                int userIDInt = Convert.ToInt32(userID);

                NoteEntity noteEntity = noteBusiness.ArchiveAndUnArchive(noteID, userIDInt);
                if (noteEntity != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { status = true, message = "Note Archive/UnArchive is succesfull", response = noteEntity });
                }

                return BadRequest(new ResponseModel<NoteEntity> { status = true, message = "Note Archive/UnArchive is not succesfull" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("pin-unpinned")]
        public IActionResult PinedAndUnPinned(int noteID)
        {
            try
            {
                string userID = User.FindFirst("UserID").Value;
                int userIDInt = Convert.ToInt32(userID);

                NoteEntity noteEntity = noteBusiness.PindAndUnPinned(noteID, userIDInt);
                if (noteEntity != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { status = true, message = "Note pin/unpinned is succesfull", response = noteEntity });
                }

                return BadRequest(new ResponseModel<NoteEntity> { status = false, message = "Note pin/unpined is not succesfull" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("trash-untrash")]
        public IActionResult TrashAndUnTrash(int noteID)
        {
            try
            {
                string userID = User.FindFirst("UserID").Value;
                int userIDInt = Convert.ToInt32(userID);

                NoteEntity noteEntity = noteBusiness.TrashAndUnTrash(noteID, userIDInt);

                if (noteEntity != null)
                {
                    return Ok(new ResponseModel<NoteEntity> { status = true, message = "trash/untrash is succesfull", response = noteEntity });
                }

                return BadRequest(new ResponseModel<NoteEntity> { status = false, message = "trash/untrash is not succesfull" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("delete-note")]
        public IActionResult deleteNoteByNoteID(int noteID)
        {
            if (noteBusiness.deleteNoteByNoteID(noteID) != null)
            {
                return Ok(new ResponseModel<int> { status = true, message = "note remove succesfully", response = noteID });
            }

            return BadRequest(new ResponseModel<int> { status = false, message = "note not remove succesfully", response = noteID });
        }

        [HttpPost("upload-image")]
        public IActionResult UploadeImage(string filePath, int noteID)
        {
            int userID = Convert.ToInt32(User.FindFirst("UserID").Value);

            string uplodeResult = noteBusiness.UploadeImage(filePath, noteID, userID);
            if (uplodeResult != null)
            {
                return Ok(new ResponseModel<int> { status = true, message = "note remove succesfully", response = noteID });
            }

            return BadRequest(new ResponseModel<int> { status = true, message = "note remove succesfully", response = noteID });
        }
    }
}
