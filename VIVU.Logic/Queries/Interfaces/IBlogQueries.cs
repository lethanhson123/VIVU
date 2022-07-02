using VIVU.Shared.Model;

namespace VIVU.Logic.Queries.Interfaces;
public interface IBlogQueries
{
    IEnumerable<BlogModel> Get();
    IEnumerable<BlogModel> Get(BlogQueryModel query);
    Task<BlogDetailModel> GetDetail(int Id);
}

