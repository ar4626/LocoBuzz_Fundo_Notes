using Common_Layer.ResponseModel;
using Manager_Layer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;

        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }

        [Authorize]
        [HttpPost]
        [Route("AddLabel")]
        public ActionResult AddLabel(int noteId, string labelName)
        {
            int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
            var response = labelManager.AddLabel(userId, labelName,noteId);
            if (response!=null)
            {
                return Ok(new ResModel<LabelEntity> { Success = true, Message = "Label Added Successfully", Data = response });
            }

            return BadRequest(new ResModel<LabelEntity> { Success = false, Message = "Something Went Wrong", Data = response });
           
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllLabel")]
        public ActionResult GetAllLabel()
        {
            int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
            HashSet<LabelEntity> labels = labelManager.GetAllLabels(userId);
            if (labels.Count!=0)
            {
                return Ok(new ResModel<HashSet<LabelEntity>> { Success = true, Message = "Label Fetched Successfully", Data = labels });
            }
            return BadRequest(new ResModel<HashSet<LabelEntity>> { Success = false, Message = "Something Went Wrong", Data = labels });
        }

        [Authorize]
        [HttpPut]
        [Route("EditLabel")]
        public ActionResult EditLabel(int labelId, string labelName)
        {
            int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
            var response = labelManager.EditLabel(userId, labelName,labelId);
            if (response!=null)
            {
                return Ok(new ResModel<List<LabelEntity>> { Success = true, Message = "Label Updated Successfully", Data = response });
            }
            return BadRequest(new ResModel<List<LabelEntity>> { Success = false, Message = "Something Went Wrong", Data = response });

        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteLabel")]
        public ActionResult EditLabel(int labelId)
        {
            int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
            var response = labelManager.DeleteLabel(userId, labelId);
            if (response == true)
            {
                return Ok(new ResModel<bool> { Success = true, Message = "Label Deleted Successfully", Data = response });
            }
            return BadRequest(new ResModel<bool> { Success = false, Message = "Something Went Wrong", Data = response });
        }
    }
}
