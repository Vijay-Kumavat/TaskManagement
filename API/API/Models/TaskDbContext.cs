using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Customize the TaskEntity mapping
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            // Table name is automatically 'TaskEntities' but you can change it if needed
            entity.ToTable("TaskEntity");

            // Column configurations
            entity.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();  // Specifies auto-increment

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);  // Set a maximum length for the Title

            entity.Property(e => e.Status)
                .HasMaxLength(int.MaxValue);

            entity.Property(e => e.Description)
                .HasMaxLength(int.MaxValue);  // Optional: Specify max length or leave it unbounded for NVARCHAR(MAX)

            entity.Property(e => e.DueDate)
                .IsRequired();  // Ensure the DueDate is required
        });
    }
}

