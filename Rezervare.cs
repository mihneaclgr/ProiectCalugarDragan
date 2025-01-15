namespace CompanieAeriana;

public class Rezervare
{
    private string username;
    private string cod_zbor;
    private int nrLocuriRezervate;

    public Rezervare(string username, string cod_zbor,  int nrLocuriRezervate)
    {
        this.username = username;
        this.cod_zbor = cod_zbor;
        this.nrLocuriRezervate = nrLocuriRezervate;
    }

    public double CostTotal(List<Zbor> zboruri)
    {
        int i = 0;
        while (zboruri[i++].getCod() != cod_zbor);
        return zboruri[i-1].PretZbor() * nrLocuriRezervate;
    }
}