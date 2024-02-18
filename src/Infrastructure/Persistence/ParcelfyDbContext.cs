namespace parcelfy.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public class ParcelfyDbContext : DbContext
{
	public virtual DbSet<ParcelTrackerHistoryEntity> ParcelTrackHistories { get; set; }

	public ParcelfyDbContext()
	{
	}

	public ParcelfyDbContext(DbContextOptions<ParcelfyDbContext> options)
		: base(options)
	{
	}
	public virtual DbSet<ParcelTrackerHistoryEntity> ParcelTrackHistoryEntities { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ParcelTrackerHistoryEntity>(entity =>
		{
			entity.HasKey(p => new { p.parcelid, p.eventcode });
			entity.ToTable("parceltrackinghistory");
			entity.Property(e => e.eventcode)
				.HasMaxLength(10)
				.IsFixedLength();
			entity.Property(e => e.eventdate)
				.HasColumnType("datetime");
			entity.Property(e => e.eventmessage)
				.HasColumnType("text");
			entity.Property(e => e.parcelid)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.product)
				.HasMaxLength(100)
				.IsUnicode(false);
			entity.Property(e => e.url)
				.HasMaxLength(255)
				.IsUnicode(false);
			entity.Property(e => e.isfinal)
				.HasColumnType("bool");
		});
	}
}
