using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Common.Pagination
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Items { get; set; }

        int TotalItems { get; set; }
    }
}
