﻿using System.Runtime.InteropServices.JavaScript;

namespace CompanieAeriana;

public class Pasager
{
    private string nume;
    private string CNP;
    private List<Rezervare> istoric_rezervari;
    private Cont cont;
    
    bool CNPisValid(string cnp)
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
        {
            if (DateTime.IsLeapYear(an))
                if (zi > 29)
                    return false;
                else ;
            else 
                if (zi > 28) return false;
        }
        
        else if (luna == 4 || luna == 6 || luna == 9 || luna == 11)
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

    public Pasager(string nume, string CNP, List<Rezervare> istoric_rezervari, Cont cont)
    {
        this.nume = nume;
        this.CNP = CNP;
        this.istoric_rezervari = istoric_rezervari;
        this.cont = cont;
    }
    
    

}