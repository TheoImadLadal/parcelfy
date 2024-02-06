﻿namespace parcelfy.Infrastructure.Persistence;

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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ParcelTrackerHistoryEntity>(entity =>
		{
			entity.HasKey(p => new { p.ParcelId, p.EventCode });
			entity.ToTable("parcel_track_history");
			entity.Property(e => e.EventCode)
				.HasMaxLength(3)
				.IsFixedLength();
			entity.Property(e => e.EventDate)
				.HasColumnType("datetime");
			entity.Property(e => e.EventMessage)
				.HasColumnType("text");
			entity.Property(e => e.ParcelId)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.Product)
				.HasMaxLength(50)
				.IsUnicode(false);
			entity.Property(e => e.URL)
				.HasMaxLength(255)
				.IsUnicode(false);
		});
	}
}
