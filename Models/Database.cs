using Microsoft.EntityFrameworkCore;

class DataBase : DbContext
{
    public DbSet<Societe>? societes { get; set; }
    public DbSet<Autocar>? autocars { get; set; }
    public DbSet<Utilisateur>? utilisateurs { get; set; }
    public DbSet<Ville>? villes { get; set; }
    public DbSet<Abonnement>? abonnements { get; set; }

    public DbSet<Proposition>? propositions { get; set; }

    public DbSet<Vote>? votes { get; set; }
    public string DataPath { get; }

    static DataBase? DbConsumer = null;

    public DataBase()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DataPath = System.IO.Path.Join(path, "data.db");
        Console.WriteLine(DataPath);
    }


    public static DataBase getCtxDb()
    {
        if (DbConsumer == null)
        {
            DbConsumer = new DataBase();
        }

        return DbConsumer;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DataPath}");


}