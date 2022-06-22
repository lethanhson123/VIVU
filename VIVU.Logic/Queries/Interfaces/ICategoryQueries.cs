using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Model;

namespace VIVU.Logic.Queries.Interfaces
{
    public interface ICategoryQueries
    {
        IEnumerable<CategoryModel> Get(string? keywords);
        IEnumerable<CategoryModel> Get(CategoryQueryModel query);
        Task<CategoryDetailModel> GetDetail(int Id);
    }
}
