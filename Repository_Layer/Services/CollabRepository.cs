using Common_Layer.Utility;
using Repository_Layer.Context;
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
            var notes = context.NoteTable.FirstOrDefault(b => b.NoteId == noteId);
            var user = context.UserTable.FirstOrDefault(c=>c.UserId ==  userId);
            Mail mail = new Mail();
            if (note != null)
            {
                    note.IsRemove = false;
                    context.CollabTable.Update(note);
                    context.SaveChanges();
                    mail.SendCollabMail(email, notes.Title, user.Email, user.FName);
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
            mail.SendCollabMail(email, notes.Title, user.Email, user.FName);
            return entity;
        }

        public bool RemoveCollab (int collabId)
        {
            var note = context.CollabTable.FirstOrDefault(a=>a.CollabId==collabId);
            if (note != null)
            {
                note.IsRemove = true;
                context.Update(note);
                context.SaveChanges();
                return (true);
            }
            throw new Exception ("Request Failed No Collab with this collab ID");
        }
    }
}
