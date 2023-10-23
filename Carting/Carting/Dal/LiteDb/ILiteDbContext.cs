using LiteDB;

namespace Carting.Dal.LiteDb
{
	public interface ILiteDbContext
	{
		LiteDatabase Database { get; }
	}
}
