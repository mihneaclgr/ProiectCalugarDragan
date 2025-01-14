using System.Runtime.CompilerServices;

namespace CompanieAeriana;

public class Wrapper
{
    private static Transformari transformari = new Transformari();
    
    public static List<Zbor> CitireZboruri(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] zboruri_string = File.ReadAllText(path).Split("\n");
        List<Zbor> zboruri = new List<Zbor>();

        for (int i = 0; i < zboruri_string.Length; i++)
        {
            string s = zboruri_string[i];
            zboruri.Add(transformari.StringtoZbor(s));
        }
        
        return zboruri;
    } 
    public static List<Cont> CitireConturi(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] conturi_string = File.ReadAllText(path).Split("\n");
        List<Cont> conturi = new List<Cont>();

        for (int i = 0; i < conturi_string.Length; i++)
        {
            string s = conturi_string[i];
            conturi.Add(transformari.StringtoCont(s));
        }
        
        return conturi;
    }
    
    /*public static List<Ruta> CitireRute(string numeFisier)
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
    }*/
    
    List<Cont> conturi = CitireConturi(@"conturi.txt");
    List<Zbor> zboruri = CitireZboruri("Lista_zboruri.txt");
    //List<Ruta> rute = CitireRute("rute.txt");


    public void InitDate(Companie companie)
    {
        foreach (Cont c in conturi)
            companie.AddCont(c);
        //foreach (Ruta r in rute)
            //companie.AddRute(r);
    }
    
    public void MeniuLogin(Companie companie)
    {
        Console.Clear();
        Console.WriteLine("====  Compania Aeriana NovaGrup  ====\n" +
                          "\n1) Creeaza cont\n2) Intra in cont\n3) Continua fara cont\n\n");
        
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
                        Console.WriteLine("Nume de utilizator deja utilizat\n" +
                                          "Doriti sa reincercati?\n1) DA\n2) NU\n\n");
                        int option2 = Convert.ToInt32(Console.ReadLine());
                        if (option2 != 1)
                        {
                            MeniuLogin(companie);
                        } 
                    }
                } while (!companie.UsernameDisponibil(username));
                
                Console.WriteLine("Parola: ");
                parola = Console.ReadLine();
                
                cont = new Cont(username, parola);
                pasager = new Pasager(nume, prenume, cnp, cont);
                companie.AddCont(cont);

                Console.WriteLine("Cont creat cu succes!\nApasati orice tasta pentru a intra in meniul principal...");
                Console.ReadKey();
                MeniuLogin(companie);
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
                //Console.Clear();
                MeniuGuest(companie);
                break;
            
            default:
                //Console.WriteLine("Optiune invalida!");
                break;
        }
    }
    void MeniuGuest(Companie companie)
    {
        Console.Clear();
        Console.WriteLine("~~~~ Bine ati venit in modul Guest ~~~~\n\n");
        Console.WriteLine("Lista zborurilor disponibile:\n");
        //aici zboruri disponibile
        
    
        Console.WriteLine("\n\nApasati orice tasta pentru a reveni la meniul principal ...\n\n");
        Console.ReadKey();
        MeniuLogin(companie);
    }
    void RezervariPentruPasageri(Companie companie, Cont cont)
    {

        void VizualizareListaZboruriDisponibile()
        {
            //dragan
            //si la guest traba pus asta
            //un zbor e disponibil daca sunt locuri disponibile si ora de plecare e in viitor
        }
        void RezervaLocZbor()
        {
            //eu
        }
        void AnulareRezervare()
        {
            //eu
        }
        
        Console.Clear();
        Console.WriteLine("1) Vizualizare lista de zboruri disponibile\n" +
                          "2) Rezervare locuri pe un zbor specific\n" +
                          "3) Anulare rezervare\n" +
                          "4) Vizualizare istoricul rezervărilor proprii\n" +
                          "0) Exit");
        int optiune = Convert.ToInt32(Console.ReadLine());
        switch (optiune)
        {
            case 1:
                VizualizareListaZboruriDisponibile();
                break;
            case 2:
                RezervaLocZbor();
                break;
            case 3:
                AnulareRezervare();
                break;
            default:
                if (cont.username == "admin")
                    MeniuAdmin(companie, cont);
                else
                    Meniu(companie, cont);
                break;
        }
    }
    void Meniu(Companie companie, Cont cont)
    {
        Console.Clear();
        Console.WriteLine($"\n~~~~ Bun venit {cont.username} ~~~~\n\n");
        Console.WriteLine("1) Rezervari pentru pasagerii\n0) Exit");
        int optiune = Convert.ToInt32(Console.ReadLine());
        switch (optiune)
        {
            case 1:
                RezervariPentruPasageri(companie,cont);
                break;
            default:
                MeniuLogin(companie);
                break;
        }
        Meniu(companie, cont);
    }
    void MeniuAdmin(Companie companie, Cont cont)
    {
        int optiune;

        void GestiuneZboruri()
        {
            void AdaugareZborNou()
            {
                //dragan
                Console.WriteLine("\n~~~~~ Adaugare zboruri ~~~~~\n");
            }
            void StergereZbor()
            {
                //dragan
            }
            void VizualizareListaZboruri()
            {
                //dragan
            }
            void ActualizareInformatiiDespreZboruri()
            {
                //eu
            }
            void VizualizareRuteDisponibile()
            {
                //dragan
            }
            void AdaugareStergereRute()
            {
                //eu
            }
            
            Console.Clear();
            Console.WriteLine("\n~~~~ Gestiune Zboruri ~~~~\n\n");
            Console.WriteLine("1) Adauga zbor nou\n2) Stergere zbor\n3) Vizualizare lista completa de zboruri\n" +
                              "4) Actualizare informatii despre zboruri\n5) Vizualizare rute disponibile\n" +
                              "6) Adaugare\\stergere rute\n0) Exit");
            Console.WriteLine("Optiunea dvs...");
            optiune = Convert.ToInt32(Console.ReadLine());
            switch (optiune)
            {
                case 1:
                    AdaugareZborNou();
                    break;
                case 2:
                    StergereZbor();
                    break;
                case 3:
                    VizualizareListaZboruri();
                    break;
                case 4:
                    ActualizareInformatiiDespreZboruri();
                    break;
                case 5:
                    VizualizareRuteDisponibile();
                    break;
                case 6:
                    AdaugareStergereRute();
                    break;
                default:
                    MeniuAdmin(companie, cont);
                    break;
            }
        }
        void Rapoarte()
        {
            void VizualizareZborPopular()
            {
                //eu
            }
            void VizualizareVenituriZbor()
            {
                //dragan
            }
            void GenerareRaportZilnic()
            {
                //eu
            } 
            void PlatiPasager()
            {
                //dragan
            }
            Console.Clear();
            Console.WriteLine("\n~~~~ Rapoarte si statistici ~~~~\n\n");
            Console.WriteLine("1) Vizualizare zboruri cu cele mai multe locuri rezervate." +
                              "\n2) Vizualizare veniturile generate de un zbor." +
                              "\n3) Generare raport zilnic al veniturilor totale." +
                              "\n4) Vizualizarea tuturor plăților efectuate de un pasager." + 
                              "\n0) Exit");
            Console.WriteLine("Optiunea dvs...");
            optiune = Convert.ToInt32(Console.ReadLine());
            switch (optiune)
            {
                case 1:
                    VizualizareZborPopular();
                    break;
                case 2:
                    VizualizareVenituriZbor();
                    break;
                case 3:
                    GenerareRaportZilnic();
                    break;
                case 4:
                    PlatiPasager();
                    break;
                default:
                    MeniuAdmin(companie, cont);
                    break;
            }
        }
            
            
        Console.Clear();
        Console.WriteLine("\n~~~~ Cont Admin ~~~~\n\n");
        Console.WriteLine("1) Gestiune Zboruri\n2) Rapoarte si statistici" +
                          "\n3) Rezervari pentru pasageri\n0) Exit");
        optiune = Convert.ToInt32(Console.ReadLine());
        switch (optiune)
        {
            case 1:
                GestiuneZboruri();
                break;
            case 2:
                Rapoarte();
                break;
            case 3:
                RezervariPentruPasageri(companie, cont);
                break;
            default:
                MeniuLogin(companie);
                break;
        }
        MeniuAdmin(companie, cont);
        
        
   }
    public void SaveDate(Companie companie)
    {
        string pathZboruri, pathConturi, pathRute;
        pathZboruri = @"..\..\..\Lista_zboruri.txt";
        pathConturi = @"..\..\..\conturi.txt";
        pathRute = @"..\..\..\rute.txt";

        if (File.Exists(pathZboruri))
        {
            File.WriteAllText(pathZboruri, transformari.ZbortoString(zboruri[0])+"\r");
            for (int i = 1;i <= zboruri.Count - 1;i++)
                File.AppendText(transformari.ZbortoString(zboruri[i]));   
        }
        else
        {
            Console.WriteLine("Fisierul nu exista sau a fost mutat");
        }
        
        
        if (File.Exists(pathConturi))
        {
            File.WriteAllText(pathConturi, transformari.ConttoString(conturi[0])+"\r");
            for (int i = 1;i <= conturi.Count - 1;i++)
                File.AppendText(transformari.ConttoString(conturi[i]));   
        }
        else
        {
            Console.WriteLine("Fisierul nu exista sau a fost mutat");
        }
        
        
        /*if (File.Exists(pathRute))
        {
            File.WriteAllText(pathRute, transformari.RutatoString(rute[0])+"\r");
            for (int i = 1;i <= rute.Count - 1;i++)
                File.AppendText(pathConturi);   
        }
        else
        {
            Console.WriteLine("Fisierul nu exista sau a fost mutat");
        }*/

    }
}