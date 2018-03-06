using System.Collections.Generic;

namespace Questionable.Queries.QuestionSearch
{
    public class PagedResult<T> : IPagedResult<T>
    {
        public int Count { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}