namespace CompanieAeriana;

public class Avion
{
    private string nume;
    private int capacitateAvion;

    public Avion(string nume, int nr_locuri)
    {
        this.nume = nume;
        this.capacitateAvion = nr_locuri;
    }

    public string getNume()
    {
        return nume;
    }

    public int getCapacitateAvion()
    {
        return capacitateAvion;
    }
}