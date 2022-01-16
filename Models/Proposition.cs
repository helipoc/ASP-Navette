class Proposition
{
    public int? ID { get; set; }
    public string? h_Dep { get; set; }
    public string? h_Arr { get; set; }

    public Ville? villeDep { get; set; }
    public Ville? villeDar { get; set; }

    public int? nbVote { get; set; }

    public Proposition() { }
    public Proposition(string hd, string ha, Ville vd, Ville va)
    {
        ID = DataBase.getCtxDb().propositions?.Count() + 1;
        h_Dep = hd;
        h_Arr = ha;
        villeDep = vd;
        villeDar = va;
        nbVote = 1;
    }

}