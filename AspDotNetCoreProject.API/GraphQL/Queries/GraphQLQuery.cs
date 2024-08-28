namespace AspDotNetCoreProject.API.GraphQL.Queries
{
    public class GraphQLQuery
    {
        public CompanyQuery Company { get; }

        public GraphQLQuery(CompanyQuery companyQuery)
        {
            Company = companyQuery;
        }
    }
}
