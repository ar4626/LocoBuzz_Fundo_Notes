using Manager_Layer.Interface;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repository;
        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }

        public LabelEntity AddLabel(int userId, string labelName, int NoteId)
        {
            return repository.AddLabel(userId, labelName, NoteId);
        }
        public LabelEntity EditLabel(int userId, string labelName, int LabelId)
        {
            return repository.EditLabel(userId, labelName, LabelId);
        }
        public bool DeleteLabel(int userId, int LabelId)
        {
            return repository.DeleteLabel(userId, LabelId);
        }
        public List<LabelEntity> GetAllLabels(int userId)
        {
            return repository.GetAllLabels(userId);
        }




    }
}
