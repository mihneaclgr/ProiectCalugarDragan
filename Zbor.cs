namespace CompanieAeriana;

public class Zbor
{
    private string cod;
    private int oraPlecarii;
    private double durataZbor;
    private int capacitateAvion;
    private int locuriDisponibile;

    public Zbor(string cod, int oraPlecarii, double durataZbor, int capacitateAvion, int locuriDisponibile)
    {
        this.cod = cod;
        this.oraPlecarii = oraPlecarii;
        this.durataZbor = durataZbor;
        this.capacitateAvion = capacitateAvion;
        this.locuriDisponibile = locuriDisponibile;
    }

    static bool ValideazaCod(string cod)
    {
        int i;
        
        if (cod.Length != 5)
        {
            Console.WriteLine("Cod invalid! (codul trebuie sa aiba 5 caractere)");
            return false;
        }

        // Verificam daca primele doua caractere sunt litere
        for (i = 0; i < 2; i++)
        {
            if (!char.IsLetter(cod[i]))
            {
                Console.WriteLine("Code type: 'RO123'/'IN123'");
                return false;
            }
        }
        
        // Verificam daca ultimele trei caractere sunt cifre
        for (i = 2; i < 5; i++)
        {
            if (!char.IsDigit(cod[i]))
            {
                return false;
            }
        }

        return true;
    }

    public void PretZbor()
    {
        
    }
}