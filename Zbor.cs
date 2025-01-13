﻿namespace CompanieAeriana;

public class Zbor
{
    private string cod;
    private int oraPlecarii;
    private double durataZbor;
    private int capacitateAvion;
    private int locuriDisponibile;
    private Ruta ruta;
    private Avion avion;

    public Zbor(string cod, int oraPlecarii, double durataZbor, int capacitateAvion, int locuriDisponibile, Ruta ruta, Avion avion)
    {
        this.cod = cod;
        this.oraPlecarii = oraPlecarii;
        this.durataZbor = durataZbor;
        this.capacitateAvion = capacitateAvion;
        this.locuriDisponibile = locuriDisponibile;
        this.ruta = ruta;
        this.avion = avion;
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
}