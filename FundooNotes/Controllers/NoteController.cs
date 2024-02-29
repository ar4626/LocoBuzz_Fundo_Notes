using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Manager_Layer.Interface;
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

        [Authorize]
        [HttpGet]
        [Route ("GetAllNotes")]
        public ActionResult GetAllNotes()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                List<NoteEntity> noteList = noteManager.GetAllNotes(userId);
                if (noteList.Count==0)
                {
                    return BadRequest(new ResModel<List<NoteEntity>> { Success = false, Message = "No Notes are present for the User", Data = null });
                }
                else
                {
                    return Ok(new ResModel<List<NoteEntity>> { Success = true, Message = "Notes Fetched", Data = noteList });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<List<NoteEntity>> { Success = false, Message = ex.Message, Data = null });
            }

        }

        [Authorize]
        [HttpPut]
        [Route("UpdateNoteById")]
        public ActionResult UpdateNoteById(int noteId, UpdateNoteModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var note = noteManager.UpdateNoteByNoteId(noteId, model, userId);
                if(note == null)
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Note Updation Failed", Data = null });
                }
                else
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = $"Note {noteId} Updated Successfully ", Data = note });
                }
            }catch(Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });

            }
        }

        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public ActionResult IsTrash(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var check = noteManager.IsTrash(noteId, userId);

                if (check == true)
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Moved To Trash", Data = null });
                }
                else if (check == false)
                {
                    return Ok(new ResModel<NoteEntity> { Success = false, Message = $"Note Moved Out of Trash", Data = null });
                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = $"Note Doesn't Exist ", Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public ActionResult IsArchive(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var check = noteManager.IsArchive(noteId, userId);

                if (check == true)
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Moved To Archive", Data = null });
                }
                else if (check == false)
                {
                    return Ok(new ResModel<NoteEntity> { Success = false, Message = $"Note Moved Out of Trash", Data = null });
                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = $"Note Doesn't Exist ", Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpDelete ("DeleteTrashed")]
        public ActionResult DeleteTrashed()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var check = noteManager.DeleteTrashed(userId);
                if (check == true)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Trash Cleared Successfully", Data = true });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = $"Something went wrong try again.", Data = false });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }


    }
}
