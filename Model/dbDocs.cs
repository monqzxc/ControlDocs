using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DocsControl.Model
{
    public partial class dbDocs : DbContext
    {
        public dbDocs()
            : base("name=dbDocs")
        {
        }
       
        public virtual DbSet<DocData> DocDatas { get; set; }
        public virtual DbSet<DocType> DocTypes { get; set; }
        public virtual DbSet<Focal> Focals { get; set; }
        public virtual DbSet<Focalship> Focalships { get; set; }
        public virtual DbSet<Office> Offices { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Plantilla> Plantillas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Focal>()
                .HasMany(e => e.DocDatas)
                .WithRequired(e => e.Focal)
                .HasForeignKey(e => e.DocFromID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Focal>()
                .HasMany(e => e.DocDatas1)
                .WithRequired(e => e.Focal1)
                .HasForeignKey(e => e.DocToID)
                .WillCascadeOnDelete(false);
        }


    }
}
