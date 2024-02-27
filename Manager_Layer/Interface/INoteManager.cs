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
    }
}
