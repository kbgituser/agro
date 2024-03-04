using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.PaginatedList
{
    public class PaginatedListExtension
    {
        
        //public static PaginatedList<TDestination> ToMappedPagedList<TSource, TDestination>(this PaginatedList<TSource> list, IMapper mapper)
        //{
        //    IEnumerable<TDestination> sourceList = mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(list);
        //    PaginatedList<TDestination> pagedResult = new StaticPagedList<TDestination>(sourceList, list.GetMetaData());
        //    return pagedResult;

        //}
    }
}
