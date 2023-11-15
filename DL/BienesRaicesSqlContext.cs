using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class BienesRaicesSqlContext : DbContext
{
    public BienesRaicesSqlContext()
    {
    }

    public BienesRaicesSqlContext(DbContextOptions<BienesRaicesSqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Costo> Costos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Estatus> Estatuses { get; set; }

    public virtual DbSet<EstatusContrato> EstatusContratos { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Ubicacion> Ubicacions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vendedor> Vendedors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BienesRaicesSQL.mssql.somee.com; Database= BienesRaicesSQL; TrustServerCertificate=True; User ID=Yuugo12_SQLLogin_2; Password=uxc7qee2xp;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("Cliente");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.NumeroContrato);

            entity.ToTable("Contrato");

            entity.Property(e => e.NumeroContrato)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaFinContrato).HasColumnType("date");
            entity.Property(e => e.FechaInicioContrato).HasColumnType("date");
            entity.Property(e => e.IdEstatusContrato).HasColumnName("IdEstatus_Contrato");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contrato_Cliente");

            entity.HasOne(d => d.IdCostoNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdCosto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contrato_Costo");

            entity.HasOne(d => d.IdEstatusContratoNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdEstatusContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contrato_Estatus");
        });

        modelBuilder.Entity<Costo>(entity =>
        {
            entity.HasKey(e => e.IdCosto);

            entity.ToTable("Costo");

            entity.Property(e => e.Letras)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPagoNavigation).WithMany(p => p.Costos)
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Costo");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion);

            entity.ToTable("Direccion");

            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Numeroexterior)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Direccion");
        });

        modelBuilder.Entity<Estatus>(entity =>
        {
            entity.HasKey(e => e.IdEstatus);

            entity.ToTable("Estatus");

            entity.Property(e => e.IdEstatus).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstatusContrato>(entity =>
        {
            entity.HasKey(e => e.IdEstatusContrato);

            entity.ToTable("Estatus_Contrato");

            entity.Property(e => e.IdEstatusContrato)
                .ValueGeneratedOnAdd()
                .HasColumnName("IdEstatus_Contrato");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago);

            entity.ToTable("MetodoPago");

            entity.Property(e => e.IdMetodoPago).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago);

            entity.ToTable("Pago");

            entity.Property(e => e.DiasPago)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK_Roles");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ubicacion>(entity =>
        {
            entity.HasKey(e => e.IdUbicacion);

            entity.ToTable("Ubicacion");

            entity.Property(e => e.Desarrollo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lote)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Manzana)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroContrato)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstatusNavigation).WithMany(p => p.Ubicacions)
                .HasForeignKey(d => d.IdEstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ubicacion_Cliente_Estatus");

            entity.HasOne(d => d.NumeroContratoNavigation).WithMany(p => p.Ubicacions)
                .HasForeignKey(d => d.NumeroContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ubicacion_NumeroContrato");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Username, "UQ__Usuario__536C85E44AB2445C").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");

            entity.HasOne(d => d.IdVendedorNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdVendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Vendedor");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.IdVendedor);

            entity.ToTable("Vendedor");

            entity.HasIndex(e => e.Email, "UQ__Vendedor__A9D10534BFC6A2B9").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RFC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
