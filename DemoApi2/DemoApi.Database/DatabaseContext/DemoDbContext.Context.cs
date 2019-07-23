﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DemoApi.Database.DatabaseContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EverliveEntities : DbContext
    {
        public EverliveEntities()
            : base("name=EverliveEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AlbumDetail> AlbumDetails { get; set; }
        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<AnnouncementComment> AnnouncementComments { get; set; }
        public virtual DbSet<AnnouncementCommentDetail> AnnouncementCommentDetails { get; set; }
        public virtual DbSet<AnnouncementDetail> AnnouncementDetails { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<MediaComment> MediaComments { get; set; }
        public virtual DbSet<MediaCommentDetail> MediaCommentDetails { get; set; }
        public virtual DbSet<MediaDetai> MediaDetais { get; set; }
        public virtual DbSet<MediaFavorite> MediaFavorites { get; set; }
        public virtual DbSet<MediaWishList> MediaWishLists { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFollow> UserFollows { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
    }
}
