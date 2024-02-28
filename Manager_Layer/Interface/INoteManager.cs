using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interface
{
    public interface INoteManager
    {
        public NoteEntity AddNotes(AddNoteModel model, int userId);
        public List<NoteEntity> GetAllNotes(int userId);
        public NoteEntity UpdateNoteByNoteId(int noteId, UpdateNoteModel model, int UserId);
        public bool DeleteNote(int noteId, int userId);


    }
}
