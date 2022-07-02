using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIVU.Shared.Model;

namespace VIVU.Logic.Queries.Interfaces
{
    public interface ITagQueries
    {
        IEnumerable<TagModel> Get();
        IEnumerable<TagModel> Get(TagQueryModel query);
        Task<TagDetailModel> GetDetail(int Id);
    }
}
