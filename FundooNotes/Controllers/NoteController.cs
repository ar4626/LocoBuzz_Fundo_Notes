using Common_Layer.RequestModel;
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
    public class NoteController : ControllerBase
    {
        private readonly INoteManager noteManager;

        public NoteController(INoteManager noteManager)
        {
            this.noteManager = noteManager;
        }

        [Authorize]
        [HttpPost]
        [Route("AddNote")]
        public ActionResult AddNote(AddNoteModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);

                var note = noteManager.AddNotes(model, userId );
                if (note != null)
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Added Successfully", Data = note });
                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Something Went Wrong", Data = null });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
