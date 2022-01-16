class Vote
{
    public int? ID { get; set; }
    public Utilisateur? user { get; set; }

    public Proposition? proposition { get; set; }

    public Vote() { }
    public Vote(Utilisateur u, Proposition p)
    {
        ID = DataBase.getCtxDb().votes?.Count() + 1;
        user = u;
        proposition = p;
    }

}