using BusinessLeyer.Interface;
using CommanLayer;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;

namespace FundoNotesApplication.Controllers
{
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }
        [HttpPost("add-label")]
        public IActionResult AddLabel(string labelName, int noteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.FindFirst("UserID").Value);
                LableEntity lableEntity = labelBusiness.AddLable(labelName, noteID, userID);

                if (lableEntity != null)
                {
                    return Ok(new ResponseModel<LableEntity> { status = true, message = "Label added succesfully", response = lableEntity });
                }
                return BadRequest(new ResponseModel<string> { status = false, message = "Label not added succesfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("update-label")]
        public IActionResult UpdateLabel(string labelName, int noteID)
        {
            try
            {
                int userID = Convert.ToInt32(User.FindFirst("UserID").Value);
                LableEntity lableEntity = labelBusiness.UpdateLabel(labelName, noteID, userID);

                if (lableEntity != null)
                {
                    return Ok(new ResponseModel<LableEntity> { status = true, message = "label updated succesfully", response = lableEntity });
                }

                return BadRequest(new ResponseModel<LableEntity> { status=false , message = "label not updatd succesfully"});
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
