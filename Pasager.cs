using System.Runtime.InteropServices.JavaScript;

namespace CompanieAeriana;

public class Pasager
{
    private string nume,prenume;
    private string cnp;
    private List<Rezervare> istoric_rezervari;
    private Cont cont;
    
    internal bool CNPisValid()
    {
        int sex, an, luna, zi, judet, cod, control;
        if (cnp.Length != 13) return false;
        foreach (char c in cnp)
            if (c < '0' || c > '9') return false;
    
        sex = cnp[0] - '0';
        if (sex != 1 && sex != 2 && sex != 5 && sex != 6) return false;

        if (sex < 3) //oameni nascuti pana in 2000
            an = int.Parse("19" + cnp[1..3]);
        else 
            an = int.Parse("20" + cnp[1..3]);

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
    
        judet = int.Parse(cnp[7..9]);
        if ( !((judet >= 1 && judet <= 46) || judet == 51 || judet == 52) ) return false;
    
        cod = int.Parse(cnp[9..12]);
        if (cod == 0) return false;

        control = cnp[12] - '0';
        string constanta = "279146358279";
    
        int suma = 0;
        for (int i = 0; i <= 11; i++)
            suma += (cnp[i] - '0') * (constanta[i] - '0');
    
        if (suma%11 == 10)
            if (control != 1)
                return false;
            else ;
        else 
        if (control != (suma % 11)) 
            return false;

        return true;
    }

    internal Pasager(string nume,string prenume, string CNP, Cont cont)
    {
        this.nume = nume;
        this.prenume = prenume;
        this.cnp = CNP;
        this.istoric_rezervari = new List<Rezervare>();
        this.cont = cont;
    }

    internal Pasager(string nume, string prenume,string cnp)
    {
        this.nume = nume;
        this.prenume = prenume;
        this.cnp = cnp;
    }
    
    

}