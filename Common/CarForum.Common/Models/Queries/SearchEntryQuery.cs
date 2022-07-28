using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarForum.Common.Models.Queries
{
    public class SearchEntryQuery:IRequest<List<SearchEntryViewModel>>
    {
        public string Searchtext { get; set; }
        public SearchEntryQuery()
        {

        }

        public SearchEntryQuery(string searchtext)
        {
            Searchtext = searchtext;
        }
    }
}
