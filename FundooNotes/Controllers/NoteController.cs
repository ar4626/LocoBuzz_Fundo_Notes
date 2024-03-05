using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using GreenPipes.Caching;
using Manager_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteManager noteManager;
        private readonly IDistributedCache cache;
        private readonly ILogger<UserController> logger;



        public NoteController(INoteManager noteManager, IDistributedCache cache, ILogger<UserController> logger)
        {
            this.cache = cache;
            this.noteManager = noteManager;
            this.logger = logger;
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
        public async Task<ActionResult> GetAllNotes()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                string email = User.FindFirst("Email").Value;
                
                string cacheKey = userId.ToString();
                // Trying to get data from the Redis cache
                byte[] cachedData = await cache.GetAsync(cacheKey);
                List<NoteEntity> mergedList = new List<NoteEntity>();

                if (cachedData != null)
                {
                    // If the data is found in the cache, encode and deserialize cached data.
                    var cachedDataString = Encoding.UTF8.GetString(cachedData);
                    mergedList = JsonSerializer.Deserialize<List<NoteEntity>>(cachedDataString);
                }
                else
                {
                    // If the data is not found in the cache, then fetch data from database
                    mergedList = noteManager.GetAllNotes(userId);

                    // Serializing the data
                    string cachedDataString = JsonSerializer.Serialize(mergedList);
                    var dataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                    // Setting up the cache options
                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                    // Add the data into the cache
                    await cache.SetAsync(cacheKey, dataToCache, options);
                }

                if (mergedList.Count==0)
                {
                    return BadRequest(new ResModel<List<NoteEntity>> { Success = false, Message = "No Notes are present for the User", Data = null });
                }
                else
                {
                    return Ok(new ResModel<List<NoteEntity>> { Success = true, Message = "Notes Fetched", Data = mergedList });
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing GetAllNotes.");
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
        [HttpPut]
        [Route("Pin")]
        public ActionResult IsPin(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var check = noteManager.IsPin(noteId, userId);

                if (check == true)
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Note Moved To Pin", Data = null });
                }
                else if (check == false)
                {
                    return Ok(new ResModel<NoteEntity> { Success = false, Message = $"Note Pin Out of Trash", Data = null });
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
        [HttpPut("Add-Color")]
        public ActionResult AddColor(int noteId,string color)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = noteManager.AddColor(noteId, userId, color);
                if (response != null)
                {
                    return Ok(new ResModel<string> { Success = true, Message = $"Color Added", Data = response});
                }
                else
                {
                    return BadRequest(new ResModel<string> { Success = false, Message = $"Color Addition Failed", Data = null });
                }
            }
                catch(Exception ex)
            {
                return BadRequest(new ResModel<string> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpDelete ("EmptyTrash")]
        public ActionResult EmptyTrash()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var check = noteManager.EmptyTrash(userId);
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

        [Authorize]
        [HttpDelete("DeleteNoteId")]
        public ActionResult DeleteByNoteId(int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var check = noteManager.DeleteNote(userId,noteId);
                if (check == true)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Note Deleted Successfully", Data = true });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = $"Note Deletion Failed", Data = false });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }

        [Authorize]
        [HttpPut("UploadImage")]
        public ActionResult UploadImage(string path, int noteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var check = noteManager.UploadImage(path,noteId,userId);
                if (check == true)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Image Uploaded Successfully", Data = true });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = $"Image Upload Failed", Data = false });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
       


    }
}
