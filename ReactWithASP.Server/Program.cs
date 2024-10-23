using ReactWithASP.Server.Services;
using ReactWithASP.Server.Services.AuthServices;
using ReactWithASP.Server.Services.GroupServices;
using ReactWithASP.Server.Services.LecturerServices;
using ReactWithASP.Server.Services.ProgrammeServices;
using ReactWithASP.Server.Services.StudentServices;
using ReactWithASP.Server.Services.SubjectServices;

var builder = WebApplication.CreateBuilder(args);


var config = builder.Configuration;
var mysqlDb = config["MySQL:Db"];
var mysqlUser = config["MySQL:User"];
var mysqlPassword = config["MySQL:Password"];
var mysqlConn = $"server=localhost;port=3306;user={mysqlUser};password={mysqlPassword};database={mysqlDb};CharSet=utf8;TreatTinyAsBoolean=false";

var services = builder.Services;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mysqlConn, ServerVersion.AutoDetect(mysqlConn)
));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.Name = "Antiforgery";
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SuppressXFrameOptionsHeader = false;
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        },
        OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        }
    };
});

services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
});

services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.WithOrigins("https://localhost:5173").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

services.AddControllersWithViews();

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IGetService<StudentDto>, GetStudentService>();
services.AddScoped<ISaveService<StudentDto>, SaveStudentService>();
services.AddScoped<IGetService<LecturerDto>, GetLecturerService>();
services.AddScoped<ISaveService<LecturerDto>, SaveLecturerService>();
services.AddScoped<IGetService<ProgrammeDto>, GetProgrammeService>();
services.AddScoped<ISaveService<ProgrammeDto>, SaveProgrammeService>();
services.AddScoped<IGetService<GroupDto>, GetGroupService>();
services.AddScoped<ISaveService<GroupDto>, SaveGroupService>();
services.AddScoped<IGetService<SubjectDto>, GetSubjectService>();
services.AddScoped<ISaveService<SubjectDto>, SaveSubjectService>();

services.AddScoped<IDeleteService<StudentDto>, DeleteStudentService>();
services.AddScoped<IDeleteService<LecturerDto>, DeleteLecturerService>();
services.AddScoped<IDeleteService<ProgrammeDto>, DeleteProgrammeService>();
services.AddScoped<IDeleteService<GroupDto>, DeleteGroupService>();
services.AddScoped<IDeleteService<SubjectDto>, DeleteSubjectService>();

var app = builder.Build();
app.UseCors("AllowAll");
app.UseDefaultFiles();
//app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var antiforgery = app.Services.GetRequiredService<IAntiforgery>();
app.Use((context, next) =>
{
    if (HttpMethods.IsPost(context.Request.Method) || HttpMethods.IsDelete(context.Request.Method) ||
        HttpMethods.IsPut(context.Request.Method) || HttpMethods.IsPatch(context.Request.Method))
        return next(context);
    var tokens = antiforgery.GetAndStoreTokens(context);

    if (context.Request.Path.Value != null && context.Request.Path.Value.Contains("signin"))
        return next(context);

    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!, new CookieOptions
    {
        HttpOnly = false,
        Secure = true,
        SameSite = SameSiteMode.Lax
    });

    return next(context);
});

app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();