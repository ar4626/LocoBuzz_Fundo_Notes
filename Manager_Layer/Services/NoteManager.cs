using Common_Layer.RequestModel;
using Manager_Layer.Interface;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository repository;

        public NoteManager(INoteRepository repository)
        {
            this.repository = repository;
        }

        public NoteEntity AddNotes(AddNoteModel model, int userId)
        {
            return repository.AddNotes(model,userId);
        }

        public List<NoteEntity> GetAllNotes(int userId)
        {
            return repository.GetAllNotes(userId);
        }
        public List<NoteEntity> GetCollabNotes(string email)
        {
            return repository.GetCollabNotes(email);
        }

        public NoteEntity UpdateNoteByNoteId(int noteId, UpdateNoteModel model, int UserId)
        {
            return repository.UpdateNoteByNoteId(noteId, model, UserId);
        }
        public bool IsTrash(int noteId, int userId)
        {
            return repository.IsTrash(noteId, userId);
        }
        public bool IsArchive(int noteId, int userId)
        {
            return repository.IsArchive(noteId, userId);
        }
        public bool IsPin(int noteId, int userId)
        {
            return repository.IsPin(noteId, userId);
        }
        public string AddColor(int noteId, int userId, string color)
        {
            return repository.AddColor(noteId, userId, color);
        }


        public bool EmptyTrash(int userId)
        {
            return repository.EmptyTrash(userId);
        }
        public bool DeleteNote(int userId, int noteId)
        {
            return repository.DeleteNote(userId, noteId);
        }
        public bool UploadImage(string filepath, int noteId, int userId)
        {
            return repository.UploadImage(filepath, noteId, userId);
        }






    }
}
