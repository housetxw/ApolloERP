using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class PageListQuery 
    {
        private int _defaultPageSize = 20;
        private int _pageIndex = 1;



        /// <summary>
        ///     页码
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex <= 0 ? 1 : _pageIndex;

            set => _pageIndex = value;
        }

        /// <summary>
        ///     每页数量
        /// </summary>
        public int PageSize
        {
            get
            {
                if (_defaultPageSize > 200)
                {
                    return 200;
                }

                if (_defaultPageSize <= 0)
                {
                    return 20;
                }

                return _defaultPageSize;
            }
            set => _defaultPageSize = value;
        }


        /// <summary>
        /// 需要跳过多少条
        /// </summary>
        public int GetSkip()
        {
            return Math.Max(0, PageIndex - 1) * PageSize;
        }


    }
}
