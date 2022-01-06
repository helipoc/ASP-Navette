class Abonnement
{
    public int? ID { get; set; }
    public DateTime? d_Debut { get; set; }
    public DateTime? d_Fin { get; set; }
    public DateTime? h_Dep { get; set; }
    public DateTime? h_Arr { get; set; }
    public double? prix { get; set; }
    public string? type { get; set; }

    public Ville? villeDep { get; set; }
    public Ville? villeDar { get; set; }

    public List<Utilisateur>? adheres = null;



    public Abonnement() { }
    public Abonnement(int d, DateTime dd, DateTime df, DateTime hd, DateTime ha, double pr,
    string tp, Ville vd, Ville va)
    {
        ID = d;
        d_Debut = dd;
        d_Fin = df;
        h_Dep = hd;
        h_Arr = ha;
        prix = pr;
        type = tp;
        villeDep = vd;
        villeDar = va;
        adheres = new List<Utilisateur>();

    }




}