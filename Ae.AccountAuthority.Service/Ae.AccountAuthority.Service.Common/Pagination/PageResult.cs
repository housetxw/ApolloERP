using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.AccountAuthority.Service.Common.Pagination
{
    public class PageResult<T> : IPageResult<T>
    {
        private IReadOnlyList<T> _items;

        public int TotalItems { get; set; }

        public IReadOnlyList<T> Items
        {
            get => _items ?? (_items = new List<T>());
            set => _items = value;
        }

        public PageResult() { }

        public PageResult(int totalItems, IReadOnlyList<T> items)
        {
            Items = items;
            TotalItems = totalItems;
        }

        public bool IsNotNullOrEmpty() => this.TotalItems > 0;

        public bool IsNullOrEmpty() => this.TotalItems == 0;

        public PageResult<T> ReturnPageResult<T>(int totalCount, IEnumerable<T> list)
        {
            var enumerable = list.ToList();
            return enumerable.Any()
                ? new PageResult<T>(totalCount, enumerable.ToList())
                : new PageResult<T>(0, new List<T>());
        }

    }
}
