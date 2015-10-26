namespace DAL
{
  using System.Data;

  public interface IConnectionFactory
  {
    IDbConnection Create();
  }
}