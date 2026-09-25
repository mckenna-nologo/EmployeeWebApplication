using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeWebApplication
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < TotalPages; }
        }

        public static PaginatedList<T> Create(
            List<T> source,
            int pageIndex,
            int pageSize)
        {
            // Get the total number of items
            int count = source.Count;

            // Work out where this page should start
            int itemsToSkip = (pageIndex - 1) * pageSize;

            // Get only the items for this page
            List<T> items = source
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToList();

            // Create and return the paginated list
            return new PaginatedList<T>(
                items,
                count,
                pageIndex,
                pageSize);
        }
    }
}