using System.Runtime.CompilerServices;

namespace CompanieAeriana;

public class Companie
{
    private string nume;
    private List<Avion> avioane;
    private List<Ruta> rute;
    private List<Rezervare> rezervari;
    private List<Cont> conturi;

    public Companie(string nume)
    {
        this.nume = nume;
        this.avioane = new List<Avion>();
        this.rute = new List<Ruta>();
        this.rezervari = new List<Rezervare>();
        this.conturi = new List<Cont>();
    }

    internal void AddAvion(Avion avion)
    {
        this.avioane.Add(avion);
    }
    internal void AddRute(Ruta ruta)
    {
        this.rute.Add(ruta);
    }
    internal void AddRezervare(Rezervare rezervare)
    {
        this.rezervari.Add(rezervare);
    }
    internal void AddCont(Cont cont)
    {
        this.conturi.Add(cont);
    }
    internal bool ContInLista(Cont cont)
    {
        foreach (Cont c in this.conturi)
            if (c.Egal(cont))
                return true;
        return false;
    }
    internal bool UsernameDisponibil(string username)
    {
        foreach (Cont c in this.conturi)
            if (c.username == username)
                return false;
        return true;
    }
    
    
    
}