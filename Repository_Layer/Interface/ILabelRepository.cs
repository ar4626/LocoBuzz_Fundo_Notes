using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface ILabelRepository
    {
        public LabelEntity AddLabel(int userId, string labelName, int NoteId);

    }
}
