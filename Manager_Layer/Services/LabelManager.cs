﻿using Manager_Layer.Interface;
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

    }
}
