using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Interface
{
    public interface ICollabManager
    {
        public CollabEntity CreateCollab(int userId, int noteId, string email);
        public bool RemoveCollab(int noteId, string email);

    }
}
