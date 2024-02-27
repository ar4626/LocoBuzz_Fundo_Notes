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

    }
}
