using System.Security.Cryptography;
using System.Text;

class Utilisateur
{
    public int? ID { get; set; }
    public string? nom { get; set; }
    public string? telephone { get; set; }
    public string? login { get; set; }
    public string? mdp { get; set; }

    public List<Abonnement> adhere { get; set; } = new();


    public Utilisateur() { }

    public Utilisateur(string n, string tel, string log, string mp)
    {
        ID = DataBase.getCtxDb().utilisateurs?.Count() + 1;
        nom = n;
        telephone = tel;
        login = log;
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(mp));

            mdp = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}