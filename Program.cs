var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

int? count = DataBase.getCtxDb().villes?.Count();
/*var all = from c in DataBase.getCtxDb().villes select c;
DataBase.getCtxDb().villes?.RemoveRange(all);
DataBase.getCtxDb().SaveChanges();*/

if (count == 0)
{
    Console.WriteLine("[+] Ville tables is empty Seeding ... ");
    String[] villes = new String[] { "ALHAJEB", "AGADIR", "ALHOCEIMA", "ASSAZAG", "AZILAL", "BENIMELLAL", "BENSLIMANE", "BOUJDOUR", "BOULEMANE", "BERRECHID", "CASABLANCA", "CHEFCHAOUEN", "CHTOUKAAITBAHA", "CHICHAOUA", "ELJADIDA", "ELKELAADESSRAGHNAS", "ERRACHIDIA", "ESSAOUIRA", "ESSEMARA", "FES", "FIGUIG", "GUELMIM", "IFRANE", "KENITRA", "KHEMISSET", "KHENIFRA", "KHOURIBGA", "LAAYOUNE", "LARACHE", "MOHAMMEDIA", "MARRAKECH", "MEKNES", "NADOR", "OUARZAZATE", "OUJDA", "OUEDEDDAHAB", "RABAT", "SALE", "SKHIRATTEMARA", "SEFROU", "SAFI", "SETTAT", "SIDIKACEM", "TANGER", "TANTAN", "TAOUNAT", "TAROUDANNT", "TATA", "TAZA", "TETOUAN", "TIZNIT" };
    foreach (String v in villes)
    {
        Ville tmp = new Ville(v);
        DataBase.getCtxDb().Add(tmp);
        DataBase.getCtxDb().SaveChanges();
    }

    Console.WriteLine("[+] Seeded Villes dataset");

}




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

app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
