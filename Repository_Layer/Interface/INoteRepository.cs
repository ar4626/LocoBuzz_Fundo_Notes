using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface INoteRepository
    {
        public NoteEntity AddNotes(AddNoteModel model, int userId);

        public List<NoteEntity> GetAllNotes(int userId);
        public NoteEntity UpdateNoteByNoteId(int noteId, UpdateNoteModel model,int userId);
        public bool IsTrash(int noteId, int userId);
        public bool IsArchive(int noteId, int userId);
        public bool IsPin(int noteId, int userId);
        public string AddColor(int noteId, int userId, string color);


        public bool EmptyTrash(int userId);
        public bool DeleteNote(int userId, int noteId);
        public bool UploadImage(string filepath, int noteId, int userId);





    }
}
