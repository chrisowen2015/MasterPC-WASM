using Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public partial class MasterPcdbContext : DbContext
{
    public MasterPcdbContext()
    {
    }

    public MasterPcdbContext(DbContextOptions<MasterPcdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cpu> Cpus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("Connection_String"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
