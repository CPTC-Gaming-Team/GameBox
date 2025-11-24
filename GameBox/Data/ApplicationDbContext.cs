using GameBox.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GameBox.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        // Track Users Listings
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<GameModel> GameModels { get; set; }

        //new join entities
        public DbSet<UserFollower> UserFollowers { get; set; }
        public DbSet<GameWatcher> GameWatchers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //UserFollower configuration
            builder.Entity<UserFollower>()
                .HasKey(uf => new { uf.UserId, uf.FollowerId });

            builder.Entity<UserFollower>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollower>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            //GameWatcher configuration

            builder.Entity<GameWatcher>()
                .HasKey(gw => new { gw.GameId, gw.UserId });

            builder.Entity<GameWatcher>()
                .HasOne(gw => gw.Game)
                .WithMany(g => g.Watchers)
                .HasForeignKey(gw => gw.GameId);

            builder.Entity<GameWatcher>()
                .HasOne(gw => gw.User)
                .WithMany(u => u.WatchedGames)
                .HasForeignKey(gw => gw.UserId);

            //Prevent cascade delete issues to avoid multiple cascade paths error
            builder.Entity<GameModel>()
                .HasOne(g => g.Uploader)
                .WithMany()
                .HasForeignKey(g => g.UploaderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
