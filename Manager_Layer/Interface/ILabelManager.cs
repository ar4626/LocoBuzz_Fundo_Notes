using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interface
{
    public interface ILabelManager
    {
        public LabelEntity AddLabel(int userId, string labelName, int NoteId);

    }
}
