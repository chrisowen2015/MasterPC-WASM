﻿using Api.Data.Entities;
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

    public virtual DbSet<Case> Cases { get; set; }
    public virtual DbSet<CpuCooler> CpuCoolers { get; set; }
    public virtual DbSet<Cpu> Cpus { get; set; }
    public virtual DbSet<Gpu> Gpus { get; set; }
    public virtual DbSet<Motherboard> Motherboards { get; set; }
    public virtual DbSet<PSU> PSUs { get; set; }
    public virtual DbSet<Ram> Rams { get; set; }
    public virtual DbSet<Storage> Storages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("Connection_String"));
    } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
