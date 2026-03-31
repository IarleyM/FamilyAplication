using FamilyApplication.Data;
using FamilyApplication.Repositories;
using FamilyApplication.Services;
using FamilyGroupApplication.Repositories;
using FamilyGroupApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar Repositories e Services (DI)
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IFamilyGroupRepository, FamilyGroupRepository>();
builder.Services.AddScoped<IFamilyGroupService, FamilyGroupService>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FamilyApplication API V1");
        c.RoutePrefix = string.Empty; // Isso faz o Swagger abrir na raiz (https://localhost:7000/)
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Criar banco de dados automaticamente (UMA ÚNICA VEZ)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        // Verifica se o banco pode ser conectado
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("✅ Conectado ao PostgreSQL com sucesso!");

            // Apenas garante que o banco existe (cria se não existir)
            dbContext.Database.EnsureCreated();
            Console.WriteLine("✅ Banco de dados verificado/criado!");
        }
        else
        {
            Console.WriteLine("❌ Não foi possível conectar ao PostgreSQL!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao conectar ao banco: {ex.Message}");
    }
}

app.Run();