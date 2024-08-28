using AspDotNetCoreProject.Context.Company;

namespace AspDotNetCoreProject.API.GraphQL.Types
{
    public class GraphQLType
    {
        public ObjectType<CompanyEntity> Company { get; }

        public GraphQLType(CompanyType companyType)
        {
            Company = companyType;
        }
    }
}
