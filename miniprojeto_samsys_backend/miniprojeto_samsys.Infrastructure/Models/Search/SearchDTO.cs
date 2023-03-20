using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miniprojeto_samsys.Infrastructure.Helpers;

namespace miniprojeto_samsys.Infrastructure.Models.Search
{
    public class SearchDTO
    {
        
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<Parameter>? SearchParameters { get; set; }
        public List<Parameter>? SortingParameters { get; set; }

        public void Validate()
        {
            if (PageSize > 20)
            {
                PageSize = 20;
            }

            if (PageSize <= 5)
            {
                PageSize = 5;
            }

            if (CurrentPage <= 0)
            {
                CurrentPage = 1;
            }
        }
    }
}