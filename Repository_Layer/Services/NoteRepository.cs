using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common_Layer.RequestModel;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly FundooContext context;

        public NoteRepository(FundooContext context)
        {
            this.context = context;
        }

        public NoteEntity AddNotes(AddNoteModel model ,int userId)
        {
            NoteEntity entity = new NoteEntity();
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Color = "FFFFFF";
            entity.Image = "";
            entity.IsArchive = false;
            entity.IsPin = false;
            entity.IsTrash = false;
            entity.CreatedAt = DateTime.Now;
            entity.LastUpdatedAt = DateTime.Now;
            entity.UserId = userId;
            context.NoteTable.Add(entity);
            context.SaveChanges();

            return entity;
        }

        public List<NoteEntity> GetAllNotes(int userId)
        {
            var filterUser = context.NoteTable.Where(x => x.UserId == userId);
            var user = context.UserTable.FirstOrDefault(a=>a.UserId == userId);
            if(filterUser != null)
            {
                List<NoteEntity> noteList = new List<NoteEntity>();
                noteList = filterUser.Where(note => note.IsTrash == false && note.IsArchive == false).ToList();
                List<NoteEntity> collabList = GetCollabNotes(user.Email);
                List<NoteEntity> finalList = noteList.Concat(collabList).ToList();
                return finalList;

            }
            throw new Exception("User Not Authorised");

        }
        public List<NoteEntity> GetCollabNotes(string email)
        {
            var filterNote = context.CollabTable.Where(a=>a.CollabEmail == email).ToList();
            if (filterNote != null)
            {
                List<NoteEntity> collabList = new List<NoteEntity>();
                foreach (var colab in filterNote)
                {
                    var Checknote = context.NoteTable.FirstOrDefault(b => b.NoteId == colab.NoteId);
                    NoteEntity note = new NoteEntity();
                    note.Title = Checknote.Title;
                    note.Description = Checknote.Description;
                    note.NoteId = colab.NoteId;
                    collabList.Add(note);
                }
                return collabList;
            }
            else
                return null;
        }

        public NoteEntity UpdateNoteByNoteId(int noteId, UpdateNoteModel model, int userId)
        {
            var note = context.NoteTable.SingleOrDefault(a => a.NoteId == noteId && a.UserId == userId );
            if (note != null)
            {
                note.Title = model.Title;
                note.Description = model.Description;
                context.NoteTable.Update(note);
                context.SaveChanges();
                return note;
            }
            else
            {
                throw new Exception("Note Doesn't Exist");
            }
        }

        public bool IsTrash(int noteId, int userId)
        {

            var note = context.NoteTable.SingleOrDefault(a => a.NoteId == noteId && a.UserId == userId);
            if (note != null)
            {
                if (note.IsTrash==true)
                {
                    note.IsTrash = false;
                }
                else
                { 
                    note.IsTrash = true;
                }
                //context.NoteTable.Update(note);
                context.SaveChanges();
                return note.IsTrash;
            }
            else
            {
                return false;
            }
        }

        public bool IsArchive(int noteId, int userId)
        {
            var note = context.NoteTable.SingleOrDefault(a => a.NoteId == noteId && a.UserId == userId);
            if (note != null)
            {
                if (note.IsArchive == true)
                {
                    note.IsArchive = false;
                }
                else
                {
                    note.IsArchive = true;
                }
                //context.NoteTable.Update(note);
                context.SaveChanges();
                return note.IsArchive;
            }
            else
            {
                return false;
            }
        }

        public bool IsPin(int noteId, int userId)
        {
            var note = context.NoteTable.SingleOrDefault(a => a.NoteId == noteId && a.UserId == userId);
            if (note != null)
            {
                if (note.IsPin == true)
                {
                    note.IsPin = false;
                }
                else
                {
                    note.IsPin = true;
                }
                //context.NoteTable.Update(note);
                context.SaveChanges();
                return note.IsPin;
            }
            else
            {
                return false;
            }
        }

        public string AddColor(int noteId, int userId, string color)
        {
            try
            {
                var filteredUser = context.NoteTable.SingleOrDefault(a => a.UserId == userId);
                if (filteredUser != null)
                {
                    var note = context.NoteTable.SingleOrDefault(b => b.NoteId == noteId);
                    if (note != null)
                    {
                        note.Color = color;
                        return "Color Updated Successfully";
                    }
                    return "NoteId Not Found";
                }
                return "User Not Found";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool EmptyTrash(int userId)
        {
            List<NoteEntity> notesToDelete = context.NoteTable.Where(a => a.IsTrash == true && a.UserId == userId).ToList();
            int count = notesToDelete.Count;
            foreach (var notes in notesToDelete)
            {
                context.NoteTable.Remove(notes);
                count--;
            }
            context.SaveChanges();
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteNote(int userId, int noteId)
        {
            var noteToDelete = context.NoteTable.SingleOrDefault(a=>a.UserId==userId && a.NoteId==noteId);
            if (noteToDelete != null)
            {
                context.NoteTable.Remove(noteToDelete);
                context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("NoteId Not Present");
            }
        }

        public bool UploadImage(string filepath,int noteId, int userId)
        {
            try
            {
                var note = context.NoteTable.FirstOrDefault(a => a.UserId == userId && a.NoteId == noteId);
                if (note != null)
                {
                    Account account = new Account(
                        "dxycwp9mh",
                        "261612497618837",
                        "1Gfedn6FCVHvHlyM-99RxKCaV_M");

                    Cloudinary cloudinary = new Cloudinary(account);

                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(filepath),
                        PublicId = $"{note.Title}_{note.NoteId}",
                        Folder = "FundooNotes"
                    };

                    var Result = cloudinary.Upload(uploadParams);

                    note.LastUpdatedAt = DateTime.Now;
                    note.Image = Result.Url.ToString();
                    context.SaveChanges();
                    return true;
                }
                else
                { 
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
