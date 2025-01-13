namespace CompanieAeriana;

public class Rezervare
{
    private Pasager pasager;
    private Zbor zbor;
    private int nrLocuriRezervate;

    public Rezervare(Pasager pasager, Zbor zbor, int nrLocuriRezervate)
    {
        this.pasager = pasager;
        this.zbor = zbor;
        this.nrLocuriRezervate = nrLocuriRezervate;
    }

    public double CostTotal()
    {
        return zbor.PretZbor() * nrLocuriRezervate;
    }
}