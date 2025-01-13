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

    public void AddAvion(Avion avion)
    {
        this.avioane.Add(avion);
    }

    public void AddRute(Ruta ruta)
    {
        this.rute.Add(ruta);
    }

    public void AddRezervare(Rezervare rezervare)
    {
        this.rezervari.Add(rezervare);
    }

    public void AddCont(Cont cont)
    {
        this.conturi.Add(cont);
    }

    public bool ContInLista(Cont cont)
    {
        return this.conturi.Contains(cont);
    }
}