


using System.Security.Cryptography;
using System.Text;

class Societe
{

    public int? ID { get; set; }
    public string? nom { get; set; }
    public string? fix { get; set; }
    public string? login { get; set; }
    public string? mdp { get; set; }

    public Societe() { }
    public Societe(string n, string tel, string log, string md)
    {
        ID = DataBase.getCtxDb().societes?.Count() + 1;
        nom = n;
        fix = tel;
        login = log;
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(md));

            mdp = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

    }
}