using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.AccountAuthority.Service.Common.Pagination
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }

        int TotalItems { get; set; }
    }
}
