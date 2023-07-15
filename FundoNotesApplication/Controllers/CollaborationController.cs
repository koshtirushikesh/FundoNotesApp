using BusinessLeyer.Interface;
using CommanLayer;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;

namespace FundoNotesApplication.Controllers
{
    public class CollaborationController : ControllerBase
    {
        private readonly ICollaborationBusiness collaborationBusiness;
        public CollaborationController(ICollaborationBusiness collaborationBusiness)
        {
            this.collaborationBusiness = collaborationBusiness;
        }

        [HttpPost("add-collaboration")]
        public IActionResult AddCollaboration(string collaborationEmail, int noteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.FindFirst("UserID").Value);
                CollaborationEntity collaborationEntity = collaborationBusiness.AddCollaboration(collaborationEmail, noteID, userID);
                if (collaborationEntity != null)
                {
                    return Ok(new ResponseModel<CollaborationEntity> { status = true, message = "Collaboration added succesfully", response = collaborationEntity });
                }
                return BadRequest(new ResponseModel<string> { status = true, message = "Collaboration not added succesfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("get-collaboration-by-noteID")]
        public IActionResult GetCollaborationByNoteID(int noteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.FindFirst("UserID").Value);
                List<CollaborationEntity> listOfCollaboration = collaborationBusiness.GetCollaborationByNoteID(noteID, userID);
                if (listOfCollaboration != null)
                {
                    return Ok(new ResponseModel<List<CollaborationEntity>> { status = true, message = "succesfully featch all collaboration email for this note id", response = listOfCollaboration });
                }

                return BadRequest(new ResponseModel<string> { status = false, message = "succesfully featch all collaboration email for this note id" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("remove-collaboration")]
        public IActionResult RemoveCollaborationByNoteID(int noteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.FindFirst("UserID").Value);
                CollaborationEntity Collaboration = collaborationBusiness.RemoveCollaborationByNoteID(noteID, userID);
                if (Collaboration != null)
                {
                    return Ok(new ResponseModel<CollaborationEntity> { status = true, message = "successfully remove collaboration", response = Collaboration });
                }
                return BadRequest(new ResponseModel<string> { status = false, message = "unsuccessful to remove collaboration" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
