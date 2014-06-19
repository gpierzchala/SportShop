using System;

namespace SportShop.WebUI.Models
{
    public class PaginingInfo
    {
        public int TotalItems { get; set; }
        public int ItemstPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        { get { return (int) Math.Ceiling((decimal) TotalItems/ItemstPerPage); } }
    }
}