using Common_Layer.ResponseModel;
using Manager_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabManager collabManager;

        public CollabController(ICollabManager collabManager)
        {
            this.collabManager = collabManager;
        }

        [Authorize]
        [HttpPost]
        [Route("CreateCollab")]
        public ActionResult AddCollab (int noteId, string email)
        {
            int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
            var response = collabManager.CreateCollab(userId,noteId,email);
            if(response!=null)
            {
                return Ok(new ResModel<CollabEntity> { Success = true, Message = $"Collab Added Successfully with {email}", Data = response });
            }

            return BadRequest(new ResModel<CollabEntity> { Success = false, Message = "Something Went Wrong", Data = response });

        }
    }
}
