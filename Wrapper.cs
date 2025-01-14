namespace CompanieAeriana;

public class Wrapper
{
    private Transformari transformari;
    List<Zbor> CitireZboruri(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] zboruri_string = File.ReadAllText(path).Split("\n");
        List<Zbor> zboruri = new List<Zbor>();

        for (int i = 0; i < zboruri_string.Length; i++)
        {
            string s = zboruri_string[i];
            zboruri.Append(transformari.StringtoZbor(s));
        }
        
        return zboruri;
    } 
    List<Cont> CitireConturi(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] conturi_string = File.ReadAllText(path).Split("\n");
        List<Cont> conturi = new List<Cont>();

        for (int i = 0; i < conturi_string.Length; i++)
        {
            string s = conturi_string[i];
           conturi.Append(transformari.StringtoCont(s));
        }
        
        return conturi;
    }
    List<Ruta> CitireRute(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] rute_string = File.ReadAllText(path).Split("\n");
        List<Ruta> rute = new List<Ruta>();

        for (int i = 0; i < rute_string.Length; i++)
        {
            string s = rute_string[i];
            rute.Append(transformari.StringtoRuta(s));
        }
        
        return rute;
    }
    
    void MeniuGuest(Companie companie)
    {
        Console.Clear();
        Console.WriteLine("~~~~ Bine ati venit in modul Guest ~~~~\n\n");
        Console.WriteLine("Lista zborurilor disponibile:\n");
    
    
        Console.WriteLine("\n\nApasati orice tasta pentru a reveni la meniul principal ...\n\n");
        Console.ReadKey();
        MeniuLogin(companie);
    }
    void MeniuLogin(Companie companie)
    {
        Console.Clear();
        Console.WriteLine("1) Creeaza cont\n2) Intra in cont\n3) Continua fara cont\n\n");
        
        Console.WriteLine("Alegeti o optiune: ");
        int option = Convert.ToInt32(Console.ReadLine());

        Cont cont;
        string username;
        string parola;
        switch (option)
        {
            //creare cont
            case 1:
                Console.Clear();
                Console.WriteLine("\n~~~~ Creare cont ~~~~\n\n");
                

                string nume,prenume,cnp;
                
                Console.WriteLine("Nume: ");
                nume = Console.ReadLine();
                Console.WriteLine("Prenume: ");
                prenume = Console.ReadLine();

                Pasager pasager;
                do
                {
                    Console.WriteLine("CNP: ");
                    cnp = Console.ReadLine();
                    pasager = new Pasager(nume, prenume, cnp);
                    if (!pasager.CNPisValid())
                    {
                        Console.WriteLine("CNP invalid: \nReincercati?\n1)DA\n2)NU\nOptiunea aleasa: ");
                        int opt = Convert.ToInt32(Console.ReadLine());
                        if (opt != 1) 
                            MeniuLogin(companie);
                    }
                } while (!pasager.CNPisValid());

                do
                {
                    Console.WriteLine("Nume de utilizator: ");
                    username = Console.ReadLine();
                    if (!companie.UsernameDisponibil(username))
                    {
                        Console.WriteLine("Doriti sa reincercati?\n1) DA\n2) NU\n\n");
                        int option2 = Convert.ToInt32(Console.ReadLine());
                        if (option2 != 1)
                        {
                            MeniuLogin(companie);
                        } 
                    }
                } while (!companie.UsernameDisponibil(username));
                
                Console.WriteLine("Parola: ");
                parola = Console.ReadLine();
                
                cont = new Cont(nume, parola);
                pasager = new Pasager(nume, prenume, cnp, cont);
                companie.AddCont(cont);

                Console.WriteLine("Cont creat cu succes!\nApasati orice tasta pentru a intra in meniul principal...");
                Console.ReadKey();
                Meniu(companie,cont);


                break; 
            
            //intrare in cont
            case 2:
                Console.Clear();
                Console.WriteLine("\n~~~~ Accesare cont ~~~~\n\n");
                do
                {
                    Console.WriteLine("Nume de utilizator: ");
                    username = Console.ReadLine();
                    Console.WriteLine("Parola: ");
                    parola = Console.ReadLine();
                
                    cont = new Cont(username, parola);

                    if (!companie.ContInLista(cont))
                    {
                        Console.Clear();
                        Console.WriteLine("Nume de utilizator sau parola invalide!");
                        
                        Console.WriteLine("Doriti sa reincercati?\n1) DA\n2) NU\n\n");
                        int option2 = Convert.ToInt32(Console.ReadLine());
                        if (option2 != 1)
                        {
                            MeniuLogin(companie);
                        }
                    }
                } while (!companie.ContInLista(cont));

                if (cont.username == "admin")
                {
                    MeniuAdmin(companie,cont);
                }
                else
                {
                    Meniu(companie,cont);
                }
                break;
            
            //continua ca Guest
            case 3:
                Console.Clear();
                MeniuGuest(companie);
                break;
            
            
            default:
                Console.WriteLine("Optiune invalida!");
                break;
        }
    }
    void Meniu(Companie companie, Cont cont)
    {
    
    }
    void MeniuAdmin(Companie companie, Cont cont)
    {
    
    }
    
    
    
}