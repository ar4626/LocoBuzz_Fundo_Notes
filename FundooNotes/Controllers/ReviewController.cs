using Common_Layer.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private IReviewRepository repo;

        public ReviewController(IReviewRepository repo)
        {
            this.repo = repo;
        }

        [HttpPut("Update")]
        public ActionResult updateFnameLname (string email,string fname, string lname)
        {
            try
            {
                var response = repo.updateFnameLname(email, fname, lname);
                if (response != null)
                {
                    return Ok(new ResModel<UserEntity> { Success = true, Message = "User updated Successfully", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<UserEntity> { Success = false, Message = "User Doesnot exist", Data = null });
                }
            }catch(Exception  ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("getUser")]
        public ActionResult getuser(string fname)
        {
            try
            {
                var response = repo.showUser(fname);
                if (response != null)
                {
                    return Ok(new ResModel<List<UserEntity>> { Success = true, Message = "User Fetched Successfully", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<List<UserEntity>> { Success = false, Message = "User not Present", Data = null });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("getCount")]
        public ActionResult getCount()
        {
            try
            {
                var response = repo.countUser();
                if (response != 0)
                {
                    return Ok(new ResModel<int> { Success = true, Message = $"User count is {response}", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<int> { Success = false, Message = "No user Found in user table.", Data = response });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
