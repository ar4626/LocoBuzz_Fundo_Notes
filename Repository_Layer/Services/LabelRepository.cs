using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository_Layer.Services
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FundooContext context;
        public LabelRepository(FundooContext context)
        {
            this.context = context;
        }   

        public LabelEntity AddLabel(int userId, string labelName, int NoteId)
        {
            try
            {
                var label = context.LabelTable.FirstOrDefault(a => a.LabelName == labelName && a.NoteId == NoteId);
                if (label == null)
                {
                    LabelEntity entity = new LabelEntity();
                    entity.UserId = userId;
                    entity.LabelName = labelName;
                    entity.NoteId = NoteId;
                    context.LabelTable.Add(entity);
                    context.SaveChanges();
                    return entity;
                }return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<LabelEntity> GetAllLabels(int userId)
        {
            return context.LabelTable.Where(a=>a.UserId==userId).ToList();
        }

        public List<LabelEntity> EditLabel(int userId, string labelName, int LabelId)
        {
            try
            {
                var filteredUser = context.LabelTable.Where(a => a.UserId == userId);
                if (filteredUser != null)
                {
                    List<LabelEntity> label = context.LabelTable.Where(b => b.LabelName == labelName).ToList();
                    if(label.Count != 0)
                    {
                        foreach(LabelEntity entity in label)
                        {
                            entity.LabelName = labelName;
                            context.Update(entity);
                        }
                        context.SaveChanges();
                        return label;
                    }
                    else { return null; }
                }
                throw new Exception("Label Doesn't Exist");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteLabel(int userId, int LabelId)
        {
            try
            {
                var filteredUser = context.LabelTable.Where(a=>a.UserId == userId);
                if (filteredUser != null)
                {
                    var label = context.LabelTable.SingleOrDefault(a => a.LabelId == LabelId);
                    if (label != null)
                    {
                        context.LabelTable.Remove(label);
                        context.SaveChanges();
                        return true;
                    }
                    else { return false; }
                }
                throw new Exception("Label Doesn't Exist");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
