using BizNest.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.UI
{
    public class PageLinkBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        public string FirstPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NextPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PreviousPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long totalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long totalPages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int draw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PagingInfo PaginationHeader
        {
            get
            {
                int _totalCount = Convert.ToInt32(totalCount);
                int _totalPages = Convert.ToInt32(totalPages);
                int _page = Convert.ToInt32(page);
                int _pageSize = Convert.ToInt32(pageSize);


                return new PagingInfo(_totalCount, _totalPages, _page, _pageSize, PreviousPage, NextPage, draw, summary);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="routeName"></param>
        /// <param name="args"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSizeNo"></param>
        /// <param name="totalRecordCount"></param>
        /// <param name="draw"></param>
        /// <param name="summary"></param>
        public PageLinkBuilder(object urlHelper, string routeName, JObjectHelper args, long pageNo, long pageSizeNo,
            long totalRecordCount, int draw = 1, string summary = "")
        {
            page = pageNo;
            pageSize = pageSizeNo;
            totalCount = totalRecordCount;
            this.draw = draw;
            this.summary = summary;
            totalPages = totalRecordCount > 0 ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0;
            args.Add("pageSize", pageSize);
            args.Add("page", 1);
            var p1 = args.ToObject();
            args.Add("page", page - 1);
            var p2 = args.ToObject();
            args.Add("page", page + 1);
            var p3 = args.ToObject();
            args.Add("page", totalPages);
            var p4 = args.ToObject();
            //FirstPage = urlHelper.HttpRouteUrl(routeName, p1);
            //PreviousPage = page > 1 ? urlHelper.HttpRouteUrl(routeName, p2) : "";
            //NextPage = page < totalPages ? urlHelper.HttpRouteUrl(routeName, p3) : "";
            //LastPage = urlHelper.HttpRouteUrl(routeName, p4);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlHelper"></param>
        /// <param name="routeName"></param>
        /// <param name="args"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSizeNo"></param>
        /// <param name="totalRecordCount"></param>
        /// <param name="draw"></param>
        /// <param name="summary"></param>
       // public PageLinkBuilder(object urlHelper, string routeName, JObjectHelper args, long pageNo, long pageSizeNo,
       //long totalRecordCount, int draw = 1, string summary = "")
       // {
       //     this.draw = draw;
       //     this.summary = summary;
       //     page = pageNo;
       //     pageSize = pageSizeNo;
       //     totalCount = totalRecordCount;
       //     totalPages = totalRecordCount > 0 ? (int)Math.Ceiling(totalRecordCount / (double)pageSize) : 0;
       //     args.Add("pageSize", pageSize);
       //     args.Add("page", 1);
       //     var p1 = args.ToObject();
       //     args.Add("page", page - 1);
       //     var p2 = args.ToObject();
       //     args.Add("page", page + 1);
       //     var p3 = args.ToObject();
       //     args.Add("page", totalPages);
       //     var p4 = args.ToObject();
       //     //FirstPage = urlHelper.Link(routeName, p1);
       //     //PreviousPage = page > 1 ? urlHelper.Link(routeName, p2) : "";
       //     //NextPage = page < totalPages ? urlHelper.Link(routeName, p3) : "";
       //     //LastPage = urlHelper.Link(routeName, p4);
       // }
    }



    /// <summary>
    /// 
    /// </summary>
    public class PagingInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ItemStart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ItemEnd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PreviousPageLink { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NextPageLink { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Summary { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="totalPages"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="previousPageLink"></param>
        /// <param name="nextPageLink"></param>
        /// <param name="draw"></param>
        /// <param name="summary"></param>
        public PagingInfo(int totalCount, int totalPages, int currentPage,
            int pageSize, string previousPageLink, string nextPageLink, int draw = 1, string summary = "")
        {
            this.TotalCount = totalCount;
            this.TotalPages = totalPages;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.PreviousPageLink = previousPageLink;
            this.NextPageLink = nextPageLink;
            this.Draw = draw;
            this.Summary = summary;

            SetPageItems();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PagingInfo Default()
        {
            return new PagingInfo(1, 1, 1, 1, "", "");
        }


        /// <summary>
        /// 
        /// </summary>
        public void SetPageItems()
        {
            ItemEnd = PageSize * CurrentPage;
            ItemStart = ItemEnd - PageSize + 1;
            if (ItemEnd > TotalCount)
            {
                ItemEnd = TotalCount;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SummaryPage
        {
            get
            {
                var msg = string.Format("<p>Showing {0} to {1} of {2} records | Total Pages: {3}</p>",
                    ItemStart, ItemEnd, TotalCount, TotalPages);
                if (TotalCount > 0) return msg;
                else return "";
            }
        }
    }

}
