namespace VIVU.Logic.Queries.Interfaces;
public interface IBlogQueries
{
    Task<List<Blog>?> GetAll();
    Task<List<Blog>?> GetByParentId(int parentId);
    Task<Blog?> GetById(int Id);
}

