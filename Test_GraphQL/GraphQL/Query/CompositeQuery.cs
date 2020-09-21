using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class CompositeQuery : ObjectGraphType<object>
{
    public CompositeQuery(IEnumerable<IGraphQueryMarker> graphQueryMarkers)
    {
        Name = "CompositeQuery";
        foreach(var marker in graphQueryMarkers)
        {
            var q = marker as ObjectGraphType<object>;
            foreach(var f in q.Fields)
            {
                AddField(f);
            }
        }
    }
}