using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.ParcelTrackersRepository;

public static class QueryableExtensions
{
	public static DbSet<T> BuildMockDbSet<T>(this IQueryable<T> sourceList) where T : class
	{
		var mockSet = new Mock<DbSet<T>>();
		mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(sourceList.Provider);
		mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(sourceList.Expression);
		mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(sourceList.ElementType);
		mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(sourceList.GetEnumerator());

		return mockSet.Object;
	}
}
