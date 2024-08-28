using AspDotNetCoreProject.API.Attribute;
using AspDotNetCoreProject.API.GraphQL.Mutations;
using AspDotNetCoreProject.API.GraphQL.Queries;
using AspDotNetCoreProject.API.GraphQL.Types;
using AspDotNetCoreProject.Application;
using AspDotNetCoreProject.Context.Account;
using AspDotNetCoreProject.Context.Common;
using AspDotNetCoreProject.Context.Company;
using AspDotNetCoreProject.Context.Database;
using AspDotNetCoreProject.Context.User;
using AspDotNetCoreProject.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration);

//builder.Services.AddSingleton<IDbConnection>(options =>
//{
//    var connectionString = options.GetRequiredService<IOptions<AppSettings>>().Value.DatabaseConfig.ConnectionString;
//    return new MySqlConnection(connectionString);
//});
builder.Services.AddScoped<IDatabaseManager, DatabaseManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddScoped<CompanyType>();
builder.Services.AddScoped<CompanyQuery>();
builder.Services.AddScoped<CompanyMutation>();

builder.Services.AddScoped<GraphQLQuery>();
builder.Services.AddScoped<GraphQLMutation>();

var jwtSettings = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
var key = Encoding.ASCII.GetBytes(jwtSettings!.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<GraphQLQuery>()
    .AddMutationType<GraphQLMutation>()
    .AddType<GraphQLType>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});
app.UseGraphQLGraphiQL();

app.Run();
