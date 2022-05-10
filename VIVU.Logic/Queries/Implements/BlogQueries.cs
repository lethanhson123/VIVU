namespace VIVU.Logic.Queries.Implements;

public class BlogQueries : IBlogQueries
{
    public BlogQueries()
    {
    }
    public async Task<List<Blog>?> GetAll()
    {
        List<Blog> result = new List<Blog>();
        return result;
    }
    public async Task<List<Blog>?> GetByParentId(int parentId)
    {
        List<Blog> result = new List<Blog>();
        return result;
    }
    public async Task<Blog?> GetById(int Id)
    {
        Blog result = new Blog();
        return result;
    }
}

