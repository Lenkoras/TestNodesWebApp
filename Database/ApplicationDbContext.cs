using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Journal> Journals { get; set; }

        /// [WARNING] Required ctor! Do not remove!
        /// <summary>
        /// Creates a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            /// this constructor is used for DbContext configuration
        }

        public ApplicationDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>()
                .HasOne(n => n.Tree)
                .WithMany(n => n.TreeNodes)
                .HasForeignKey(n => n.TreeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Node>()
                .HasOne(n => n.ParentNode)
                .WithMany(n => n.Children)
                .HasForeignKey(n => n.ParentNodeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
