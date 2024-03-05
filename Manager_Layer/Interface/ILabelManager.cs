using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interface
{
    public interface ILabelManager
    {
        public LabelEntity AddLabel(int userId, string labelName, int NoteId);
        public List<LabelEntity> EditLabel(int userId, string labelName, int LabelId);
        public bool DeleteLabel(int userId, int LabelId);
        public HashSet<LabelEntity> GetAllLabels(int userId)
;



    }
}
