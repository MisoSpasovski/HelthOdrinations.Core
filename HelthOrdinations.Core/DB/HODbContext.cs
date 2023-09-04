using System;
using HelthOrdinations.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HelthOrdinations.Core.DB
{
	public class HODbContext: DbContext
    {

        public HODbContext(DbContextOptions<HODbContext> options) : base(options)
        {
        }

        public virtual DbSet<LocationsInfo> Locations { get; set; }
        public virtual DbSet<OrdinationsTypeInfo> OrdinationsType { get; set; }
        public virtual DbSet<LocationDetailsInfo> LocationDetails { get; set; }
        public virtual DbSet<UserInfo> Users { get; set; }
        public virtual DbSet<UserStatusInfo> UserStatus { get; set; }
        public virtual DbSet<RoleTypeInfo> RoleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }
    }
}

