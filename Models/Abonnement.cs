class Abonnement
{
    public int? ID { get; set; }
    public DateTime? d_Debut { get; set; }
    public DateTime? d_Fin { get; set; }
    public string? h_Dep { get; set; }
    public string? h_Arr { get; set; }
    public decimal? prix { get; set; }
    public string? type { get; set; }

    public Ville? villeDep { get; set; }
    public Ville? villeDar { get; set; }

    public Autocar? voiture { get; set; }

    public Societe? soc { get; set; }



    public Abonnement() { }
    public Abonnement(DateTime dd, DateTime df, string hd, string ha, decimal pr, Ville vd, Ville va, Autocar v, Societe s)
    {
        ID = DataBase.getCtxDb().abonnements?.Count() + 1;
        d_Debut = dd;
        d_Fin = df;
        h_Dep = hd;
        h_Arr = ha;
        prix = pr;
        villeDep = vd;
        villeDar = va;
        voiture = v;
        soc = s;

    }




}