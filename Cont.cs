using System.Reflection.Metadata;
using System.Text;

namespace CompanieAeriana;

public class Cont
{
    private string nume,prenume;
    private string cnp;
    
    internal string username;
    private string password;
    private List<Rezervare> istoric_rezervari;

    public Cont(string nume, string prenume, string cnp, string username, string password)
    {
        this.nume = nume;
        this.prenume = prenume;
        this.cnp = cnp;
        this.username = username;
        this.password = password;
        this.istoric_rezervari = new List<Rezervare>();
        
    }

    public Cont(string username, string password)
    {
        this.username = username;
        this.password = password;
        
        this.nume = string.Empty;
        this.prenume = string.Empty;
        this.cnp = string.Empty;
        
        this.istoric_rezervari = new List<Rezervare>();
    }

    public bool Egal(Cont c)
    {
        return c.username == this.username && c.password == this.password;
    }

    public string getPassword()
    {
        return password;
    }
    
    internal bool CNPisValid(string CNP)
    {
        int sex, an, luna, zi, judet, cod, control;
        if (CNP.Length != 13) return false;
        foreach (char c in CNP)
            if (c < '0' || c > '9') return false;
    
        sex = CNP[0] - '0';
        if (sex != 1 && sex != 2 && sex != 5 && sex != 6) return false;

        if (sex < 3) //oameni nascuti pana in 2000
            an = int.Parse("19" + CNP[1..3]);
        else 
            an = int.Parse("20" + CNP[1..3]);

        if (an > DateTime.Now.Year) return false;
        
    
        luna = int.Parse(cnp[3..5]);
        if (luna == 0 || luna > 12) return false;
    
        zi = int.Parse(cnp[5..7]);
        if (zi == 0 || zi > 31) return false;

        if (luna == 2) //februarie, vad de an bisect
            if (an % 4 != 0)
                if (zi > 28) return false;
                else ;
            else 
            if (an % 100 != 0)
                if (zi > 29) return false;
                else ;
            else 
            if (an % 400 != 0)
                if (zi > 28) return false;
                else ;
            else ;
        else 
        if (luna == 4 || luna == 6 || luna == 9 || luna == 11) 
            if (zi > 30) return false;
            else ;
    
        judet = int.Parse(CNP[7..9]);
        if ( !((judet >= 1 && judet <= 46) || judet == 51 || judet == 52) ) return false;
    
        cod = int.Parse(CNP[9..12]);
        if (cod == 0) return false;

        control = cnp[12] - '0';
        string constanta = "279146358279";
    
        int suma = 0;
        for (int i = 0; i <= 11; i++)
            suma += (CNP[i] - '0') * (constanta[i] - '0');
    
        if (suma%11 == 10)
            if (control != 1)
                return false;
            else ;
        else 
        if (control != (suma % 11)) 
            return false;

        return true;
    }
}