using System;
namespace SportSrore.Models.ViewModels
{
    public class PagingInfo
    {
        public int TotalItem { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => 
            (int)Math.Ceiling((decimal)TotalItem / ItemsPerPage);
    }
}
