﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Soitin2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MopidyContext : DbContext
    {
        public MopidyContext()
            : base("name=MopidyContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PlaylistSet> PlaylistSet { get; set; }
        public virtual DbSet<TrackSet> TrackSet { get; set; }
        public virtual DbSet<SupplementalPlaylistSet> SupplementalPlaylistSet { get; set; }
        public virtual DbSet<PlaylistView> PlaylistView { get; set; }
        public virtual DbSet<SupplementalPlaylistView> SupplementalPlaylistView { get; set; }
        public virtual DbSet<CurrentPlaylistSet> CurrentPlaylistSet { get; set; }
        public virtual DbSet<QueuelistSet> QueuelistSet { get; set; }
        public virtual DbSet<SettingsSet> SettingsSet { get; set; }
        public virtual DbSet<QueueHistory> QueueHistory { get; set; }
    }
}
