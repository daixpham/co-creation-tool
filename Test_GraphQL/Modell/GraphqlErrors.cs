using System.Collections.Generic;

public class GraphqlErrors{
    public GraphqlErrors(){
        Errors = new List<string>();
    }
    public List<string> Errors{get;set;}
}