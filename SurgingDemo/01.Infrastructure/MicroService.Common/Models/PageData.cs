using System;
using System.Collections.Generic;
using System.Text;

namespace MicroService.Common.Models
{
    public class PageData:LoginUser
    {
        public PageData()
        {
        }
        public PageData(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex = 1;
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize = 20;
        /// <summary>
        /// 总页数
        /// </summary>
        public int RowCount
        {
            get
            {
                return (int)Math.Ceiling((decimal)Total / (decimal)PageSize);
            }
        }

        /// <summary>
        /// 总行数
        /// </summary>
        public int Total { set; get; }

        /// <summary>
        /// 数据集
        /// </summary>
        public object Data { set; get; }

        /// <summary>
        /// 权限
        /// </summary>
        public object Power { set; get; }
        /// <summary>
        /// 用于显示数据列标记的集合
        /// </summary>
        public object Identifys { get; set; }
    }
}
