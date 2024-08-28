using AspDotNetCoreProject.Context.Company;

namespace AspDotNetCoreProject.API.GraphQL.Types
{
    public class CompanyType : ObjectType<CompanyEntity>
    {
        protected override void Configure(IObjectTypeDescriptor<CompanyEntity> descriptor)
        {
            descriptor.Field(c => c.CompanyId).Type<NonNullType<IdType>>();
            descriptor.Field(c => c.Name).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Email).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.MobileNumber).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.StreetNumber).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.StreetName).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.City).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.District).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.State).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Country).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Pincode).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.Latitude).Type<DecimalType>();
            descriptor.Field(c => c.Longitude).Type<DecimalType>();
            descriptor.Field(c => c.IsDeleted).Type<NonNullType<BooleanType>>();
            descriptor.Field(c => c.CreatedBy).Type<NonNullType<StringType>>();
            descriptor.Field(c => c.CreatedOn).Type<NonNullType<DateTimeType>>();
            descriptor.Field(c => c.UpdatedBy).Type<StringType>();
            descriptor.Field(c => c.UpdatedOn).Type<DateTimeType>();
        }
    }
}
