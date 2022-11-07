using UserManagement.Common.EfCore.Contracts;

namespace UserManagement.Common.EfCore.Pagination
{
    public class PagedQueryBase : IPagedQuery
    {
        public int Page { get; set; }
        public int Results { get; set; }
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }
    }
}