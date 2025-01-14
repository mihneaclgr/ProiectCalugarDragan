namespace CompanieAeriana;

public class Zbor
{
    private string cod;
    internal Ruta ruta;
    internal DateTime data;
    private TimeSpan durataZbor;
    private Avion avion;
    internal int locuriDisponibile;

    public Zbor(string cod, Ruta ruta, DateTime data, TimeSpan durataZbor, Avion avion, int locuriDisponibile)
    {
        this.cod = cod;
        this.ruta = ruta;
        this.data = data;
        this.durataZbor = durataZbor;
        this.avion = avion;
        this.locuriDisponibile = locuriDisponibile;
    }

    public Zbor()
    {
        
    }

    static bool ValideazaCod(string cod)
    {
        int i;
        
        if (cod.Length != 5)
        {
            Console.WriteLine("Cod invalid! (codul trebuie sa aiba 5 caractere)");
            return false;
        }

        if (cod[0..1] != "RO" || cod[0..1] != "IN")
        {
            Console.WriteLine("Invalid format!");
            return false;
        }

        for (i = 2; i < 4; i++)
        {
            if (cod[i] < '0' || cod[i] > '9')
            {
                Console.WriteLine("Invalid format!");
                return false;
            }
        }

        return true;
    }

    public double PretZbor()
    {
        if (cod[0..1] == "RO")
        {
            return 50 + 0.5 * ruta.getKM();
        }
        else if (cod[0..1] == "IN")
        {
            return 200 + ruta.getKM();
        }

        return -1;
    }

    public Ruta GetRuta()
    {
        return ruta;
    }
}
