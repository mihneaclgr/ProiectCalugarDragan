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
}