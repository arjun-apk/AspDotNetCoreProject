namespace AspDotNetCoreProject.API.GraphQL.Mutations
{
    public class GraphQLMutation
    {
        public CompanyMutation Company { get; }

        public GraphQLMutation(CompanyMutation companyMutation)
        {
            Company = companyMutation;
        }
    }
}
