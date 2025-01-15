namespace CompanieAeriana;
using System.Globalization;

public class Transformari
{
    public Zbor StringtoZbor(string s)
    {
        string[] parts = s.Split(' ');
        
        string cod = parts[0];
        string plecare = parts[1];
        string destinatie = parts[2];
        int distantaKm = int.Parse(parts[3]);
        DateTime ziLunaAn = DateTime.ParseExact(parts[4], "dd/MM/yyyy", CultureInfo.InvariantCulture);
        TimeSpan oraPlecarii = TimeSpan.Parse(parts[5]);
        TimeSpan durataZbor = TimeSpan.Parse(parts[6]);
        string numeAvion = parts[7];
        int capacitateAvion = int.Parse(parts[8]);
        int locuriDisponibile = int.Parse(parts[9]);

        Ruta ruta = new Ruta(plecare, destinatie, distantaKm);
        
        DateTime data = ziLunaAn + oraPlecarii;

        Avion avion = new Avion(numeAvion, capacitateAvion);

        return new Zbor(cod, ruta, data, durataZbor, avion, locuriDisponibile);
    }

    public string ZbortoString(Zbor zbor)
    {
        string format = $"{zbor.getCod()} {zbor.ruta.getPlecareDin()} {zbor.ruta.getDestinatie()} {zbor.ruta.getKm()}"
                        + $"{zbor.data:dd/MM/yyyy} {zbor.data.TimeOfDay.Hours}:{zbor.data.TimeOfDay.Minutes} {zbor.getDurataZbor()}"
                        + $"{zbor.getAvion().getNume()} {zbor.getAvion().getCapacitateAvion()} {zbor.locuriDisponibile}";
        
        return format;
    }
    public Cont StringtoCont(string s)
    {
        string username = s.Split(' ')[0];
        string password = s.Split(' ')[1];
        password = password[0..(password.Length-1)];
        
        return new Cont(username, password);
    }

    public string ConttoString(Cont cont)
    {
        string format = $"{cont.username} {cont.getPassword()}";
        return format;
    }
    public Ruta StringtoRuta(string s)
    {
        string[] parts = s.Split(' ');
        
        string plecare = parts[0];
        string destinatie = parts[1];
        int Km = int.Parse(parts[2]);
        
        return new Ruta(plecare, destinatie, Km);
    }
    public string RutatoString(Ruta ruta)
    {
        string format = $"{ruta.getPlecareDin()} {ruta.getDestinatie()} {ruta.getKm()}";
        return format;
    }

    public Rezervare StringtoRezervare(string s)
    {
        return null;
    }

    public string RezervaretoString(Rezervare rezervare)
    {
        return null;
    }

    public Avion StringtoAvion(string s)
    {
        string[] parts = s.Split(' ');
        
        string numeAvion = parts[0];
        int capacitateAvion = int.Parse(parts[1]);
        
        return new Avion(numeAvion, capacitateAvion);
    }

    public string AviontoString(Avion avion)
    {
        string format = $"{avion.getNume()} {avion.getCapacitateAvion()}";
        return format;
    }
 }