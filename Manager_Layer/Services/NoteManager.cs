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
        public bool DeleteTrashed(int userId)
        {
            return repository.DeleteTrashed(userId);
        }




    }
}
