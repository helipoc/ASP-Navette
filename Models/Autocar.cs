class Autocar
{
    public int? ID { get; set; }
    public Boolean? clim { get; set; }
    public Boolean? wifi { get; set; }
    public string? modele { get; set; }
    public int? capacite { get; set; }

    public Societe? owner { get; set; }


    public Autocar() { }

    public Autocar(Boolean c, Boolean w, string mod, int cap, Societe o)
    {
        ID = DataBase.getCtxDb()?.autocars?.Count() + 1;
        clim = c;
        wifi = w;
        modele = mod;
        capacite = cap;
        owner = o;
    }
}