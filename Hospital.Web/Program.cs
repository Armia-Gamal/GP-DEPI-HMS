using Hospital.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Hospital.Utilities;
using Hospital.Repositories.Interfaces;
using Hospital.Repositories.Implementation;
using Microsoft.AspNetCore.Identity.UI.Services;
using Hospital.Services;


internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();
		builder.Services.AddDbContext<HospitalDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

		builder.Services.AddIdentity<IdentityUser, IdentityRole>().
			AddEntityFrameworkStores<HospitalDbContext>();
		builder.Services.AddScoped<IDbInitializer, DbInitializer>();
		builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
		builder.Services.AddScoped<IEmailSender, EmailSender>();
		builder.Services.AddTransient<IHospitalInfo, HospitalInfoService>();
        
        builder.Services.AddTransient<IContactService, ContactService>();
        builder.Services.AddTransient<IRoomService, RoomService>();
        builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
        builder.Services.AddRazorPages();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		DataSedding();
		app.UseRouting();

		app.UseAuthorization();
		app.MapRazorPages();

        app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
		app.MapControllerRoute(
    name: "admin",
    pattern: "{Area=admin}/{controller=Hospitals}/{action=Index}/{id?}");



		app.Run();

		void DataSedding()
		{
			using (var scope = app.Services.CreateScope())
			{
				var dbInitializer = scope.ServiceProvider.
					GetRequiredService<IDbInitializer>();
				dbInitializer.Initialize();
			}
		}
	}
}