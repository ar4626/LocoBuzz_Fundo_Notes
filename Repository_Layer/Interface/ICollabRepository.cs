using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface ICollabRepository
    {
        public CollabEntity CreateCollab(int userId, int noteId, string email);
        public bool RemoveCollab(int collabId);

    }
}
