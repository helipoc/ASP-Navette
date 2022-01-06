class Autocar
{
    public int? ID { get; set; }
    public Boolean? clim { get; set; }
    public Boolean? wifi { get; set; }
    public string? image { get; set; }

    public string? modele { get; set; }
    public int? capacite { get; set; }

    public Societe? owner { get; set; }


    public Autocar() { }

    public Autocar(Boolean c, Boolean w, string img, string mod, int cap, Societe o)
    {
        ID = DataBase.getCtxDb()?.autocars?.Count() + 1;
        clim = c;
        wifi = w;
        image = img;
        modele = mod;
        capacite = cap;
        owner = o;
    }
}