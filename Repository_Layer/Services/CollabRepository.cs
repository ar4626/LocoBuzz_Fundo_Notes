﻿using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class CollabRepository:ICollabRepository
    {
        private readonly FundooContext context;

        public CollabRepository(FundooContext context)
        {
            this.context = context;
        }

        public CollabEntity CreateCollab(int userId, int noteId, string email)
        {
            var note = context.CollabTable.FirstOrDefault(a => a.CollabEmail == email && a.NoteId==noteId && a.CollabEmail == email && a.IsRemove == true);
            if (note != null)
            {
                    note.IsRemove = false;
                    context.CollabTable.Update(note);
                    context.SaveChanges();
                    return note;
            }
            CollabEntity entity = new CollabEntity();
            entity.UserId = userId;
            entity.NoteId = noteId;
            entity.CollabEmail = email;
            entity.IsRemove = false;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }
}
