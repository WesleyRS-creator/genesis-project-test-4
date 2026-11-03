namespace TestMVCSharpC.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

public class TestMVCSharpCContext : DbContext
{
  public DbSet<Adresse> Adresses { get; set; }
  public DbSet<Participer> Participers { get; set; }
  public DbSet<Passeport> Passeports { get; set; }
  public DbSet<Personne> Personnes { get; set; }
  public DbSet<Projet> Projets { get; set; }
  public DbSet<VFicheCompletePersonne> VFicheCompletePersonnes { get; set; }
  public DbSet<VProjetsEtEquipe> VProjetsEtEquipes { get; set; }
  public DbSet<VSynthetiseGlobal> VSynthetiseGlobals { get; set; }
  
  public TestMVCSharpCContext(DbContextOptions<TestMVCSharpCContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<VFicheCompletePersonne>(entity =>
    {
      entity.HasNoKey();
      entity.ToView("v_fiche_complete_personne"); 
    });
    modelBuilder.Entity<VProjetsEtEquipe>(entity =>
    {
      entity.HasNoKey();
      entity.ToView("v_projets_et_equipes"); 
    });
    modelBuilder.Entity<VSynthetiseGlobal>(entity =>
    {
      entity.HasNoKey();
      entity.ToView("v_synthetise_global"); 
    });
    
  }
}
