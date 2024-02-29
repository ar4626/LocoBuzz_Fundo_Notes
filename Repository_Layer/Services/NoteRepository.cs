﻿using Common_Layer.RequestModel;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<NoteEntity> GetAllNotes(int userId)
        {
            if(userId != null)
            {
                List<NoteEntity> noteList = new List<NoteEntity>();
                noteList = context.NoteTable.Where(note => note.UserId == userId && note.IsTrash == false && note.IsArchive == false).ToList();
                return noteList;
            }
            throw new Exception("User Not Authorised");

        }

        public NoteEntity UpdateNoteByNoteId(int noteId, UpdateNoteModel model, int userId)
        {
            if (userId != null)
            {
                var note = context.NoteTable.SingleOrDefault(a => a.NoteId == noteId && a.UserId == userId );
                if (note != null)
                {
                    note.Title = model.Title;
                    note.Description = model.Description;
                    context.NoteTable.Update(note);
                    context.SaveChanges();
                    return note;
                }
                else
                {
                    throw new Exception("Note Doesn't Exist");
                }
            }
            else
            {
                throw new Exception("User Is not Authenticated");
            }
        }

        public bool IsTrash(int noteId, int userId)
        {
            if (userId != null)
            {
                var note = context.NoteTable.SingleOrDefault(a => a.NoteId == noteId && a.UserId == userId);
                if (note != null)
                {
                    if (note.IsTrash==true)
                    {
                        note.IsTrash = false;
                    }
                    else
                    { 
                        note.IsTrash = true;
                    }
                    //context.NoteTable.Update(note);
                    context.SaveChanges();
                    return note.IsTrash;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("User Is not Authenticated");
            }
        }

        public bool IsArchive(int noteId, int userId)
        {
            if (userId != null)
            {
                var note = context.NoteTable.SingleOrDefault(a => a.NoteId == noteId && a.UserId == userId);
                if (note != null)
                {
                    if (note.IsArchive == true)
                    {
                        note.IsArchive = false;
                    }
                    else
                    {
                        note.IsArchive = true;
                    }
                    //context.NoteTable.Update(note);
                    context.SaveChanges();
                    return note.IsArchive;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("User Is not Authenticated");
            }
        }

        public bool DeleteTrashed(int userId)
        {
            if (userId != null)
            {
                var notesToDelete = context.NoteTable.Where(a => a.IsTrash == true && a.UserId == userId).ToList();
                //for deleting each note that is Trashed
                foreach (var notes in notesToDelete)
                {
                    context.NoteTable.Remove(notes);
                }
                context.SaveChanges();
                if (notesToDelete.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("User Is not Authenticated");
            }
        }
    }
}
