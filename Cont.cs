using System.Text;

namespace CompanieAeriana;

public class Cont
{
    internal string username;
    private string password;
    private List<Rezervare> istoric_rezervari;

    public Cont(string username, string password)
    {
        this.username = username;
        this.password = password;
        this.istoric_rezervari = new List<Rezervare>();
        
    }

    public bool Egal(Cont c)
    {
        return c.username == this.username && c.password == this.password;
    }
    
}