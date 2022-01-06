

class Ville
{
    public int? ID { get; set; }
    public string? nom;


    public Ville() { }
    public Ville(string n)
    {
        ID = DataBase.getCtxDb().villes?.Count() + 1;
        nom = n;
    }
}