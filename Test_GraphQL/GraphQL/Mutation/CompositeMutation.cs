using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GraphQL;
using GraphQL.Types;
public class CompositeMutation : ObjectGraphType
{
    public CompositeMutation(IEnumerable<IGraphMutationMarker> graphMutationMarkers)
    {
        Name = "CompositeMutation";
        foreach(var marker in graphMutationMarkers)
        {
            var m = marker as ObjectGraphType;
            foreach(var f in m.Fields)
            {
                AddField(f);
            }
        }
    }
}