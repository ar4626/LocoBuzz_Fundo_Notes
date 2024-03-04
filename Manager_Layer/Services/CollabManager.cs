using Manager_Layer.Interface;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager_Layer.Services
{
    public class CollabManager : ICollabManager
    {
        private readonly ICollabRepository repository;

        public CollabManager(ICollabRepository repository)
        {
            this.repository = repository;
        }

        public CollabEntity CreateCollab(int userId, int noteId, string email)
        {
            return repository.CreateCollab(userId, noteId, email);
        }

    }
}
