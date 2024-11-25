using Microsoft.EntityFrameworkCore;

namespace DatingApp.Helpers
{
    public class PagedList<T>:List<T>
    {
        public PagedList(IEnumerable<T> Items,int pageSize, int totalCount, int currantPage  )
        {
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int) Math.Ceiling(totalCount/(double)PageSize);
            CurrantPage = currantPage;
            AddRange( Items );
        }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrantPage { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source,int pageNumber,int pageSize)
        {
            var count = await source.CountAsync();

            var items=await source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items,pageSize,count,pageNumber);
        }
    }
}
