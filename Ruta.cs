﻿namespace CompanieAeriana;

public class Ruta
{
    private string plecare;
    private string destinatie;
    private int km;

    public Ruta(string plecare, string destinatie, int km)
    {
        this.plecare = plecare;
        this.destinatie = destinatie;
        this.km = km;
    }
    
    public string getPlecareDin()
    {
        return plecare;
    }

    public string getDestinatie()
    {
        return destinatie;
    }
    
    public int getKm()
    {
        return km;
    }
}