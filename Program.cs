using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//Add Api Versioning 
builder.Services.AddApiVersioning(options =>
{
    // Returns all version with depricated versions

    options.ReportApiVersions = true;

    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);


    options.ApiVersionReader = ApiVersionReader.Combine(

        //Query Strying type
        new QueryStringApiVersionReader("api-version"),

        //Request Heardes Type
        new HeaderApiVersionReader("Accept-Version"),

        //Media Type
        new MediaTypeApiVersionReader("api-version")
    );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();





app.MapControllers();

app.Run();
