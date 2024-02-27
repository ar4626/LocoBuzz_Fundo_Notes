using Common_Layer.RequestModel;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly FundooContext context;

        public NoteRepository(FundooContext context)
        {
            this.context = context;
        }

        public NoteEntity AddNotes(AddNoteModel model ,int userId)
        {
            NoteEntity entity = new NoteEntity();
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Color = "FFFFFF";
            entity.Image = "";
            entity.IsArchive = false;
            entity.IsPin = false;
            entity.IsTrash = false;
            entity.CreatedAt = DateTime.Now;
            entity.LastUpdatedAt = DateTime.Now;
            entity.UserId = userId;
            context.NoteTable.Add(entity);
            context.SaveChanges();

            return entity;
        }
    }
}
