using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using workshop_1.Data;
using workshop_1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();



// Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//  Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

await SeedRolesAndUsersAsync(app);

app.Run();

static async Task SeedRolesAndUsersAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = new[] { "Admin", "Teacher", "Student" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
    // Create a default admin user
    string adminEmail = "admin@rsweb.com";
    var admin = await userManager.FindByEmailAsync(adminEmail);

    if (admin == null)
    {
        admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(admin, "Admin123!");
        await userManager.AddToRoleAsync(admin, "Admin"); 
    }

    // --- TEACHER USER ---
    string teacherEmail = "teacher1@rsweb.com";
    var teacherUser = await userManager.FindByEmailAsync(teacherEmail);

    if (teacherUser == null)
    {
        teacherUser = new ApplicationUser
        {
            UserName = teacherEmail,
            Email = teacherEmail,
            EmailConfirmed = true,

            // IMPORTANT: match an existing Teacher from seed data (Id = 1..5)
            TeacherId = 1
        };

        await userManager.CreateAsync(teacherUser, "Teacher123!");
        await userManager.AddToRoleAsync(teacherUser, "Teacher");
    }

    string studentEmail = "student1@rsweb.com";
    var studentUser = await userManager.FindByEmailAsync(studentEmail);

    if (studentUser == null)
    {
        studentUser = new ApplicationUser
        {
            UserName = studentEmail,
            Email = studentEmail,
            EmailConfirmed = true,
            StudentId = 1 
        };

        await userManager.CreateAsync(studentUser, "Student123!");
        await userManager.AddToRoleAsync(studentUser, "Student");
    }


}