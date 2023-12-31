﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{
    public class FundoContext : DbContext
    {
        public FundoContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<NoteEntity> Note { get; set; }
        public DbSet<LableEntity> Lable { get; set; }
        public DbSet<CollaborationEntity> Collaboration { get; set; }
    }
}
