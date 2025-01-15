using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace CompanieAeriana;

public class Wrapper
{
    private static Transformari transformari = new Transformari();
    
    static List<Zbor> CitireZboruri(string numeFisier)
    {
        string path = @"..\..\..\" +numeFisier;
        string[] zboruri_string;
        using StreamReader fisier = new StreamReader(path);

        if (File.Exists(path))
        {
            zboruri_string = fisier.ReadToEnd().Split('\r');
            if (zboruri_string[zboruri_string.Length-1] == "")
                zboruri_string = zboruri_string.Take(zboruri_string.Length - 1).ToArray();

            List<Zbor> zboruri = new List<Zbor>();

            for (int i = 0; i < zboruri_string.Length; i++)
            {
                string s = zboruri_string[i];
                zboruri.Add(transformari.StringtoZbor(s));
            }

            fisier.Close();
            return zboruri;
        }
        else 
            return null;

    } 
    static List<Cont> CitireConturi(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] conturi_string;// = File.ReadAllText(path).Split("\n");
        using StreamReader fisier = new StreamReader(path);

        if (File.Exists(path))
        {
            conturi_string = fisier.ReadToEnd().Split('\r');
            List<Cont> conturi = new List<Cont>();

            for (int i = 0; i < conturi_string.Length; i++)
            {
                string s = conturi_string[i];
                conturi.Add(transformari.StringtoCont(s));
            }

            fisier.Close();
            return conturi;
        }
        else return null;

    }
    static List<Ruta> CitireRute(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] rute_string;
        StreamReader fisier = new StreamReader(path);

        if (File.Exists(path))
        {
            rute_string = fisier.ReadToEnd().Split('\r');
            List<Ruta> rute = new List<Ruta>();

            for (int i = 0; i < rute_string.Length; i++)
            {
                string s = rute_string[i];
                rute.Add(transformari.StringtoRuta(s));
            }

            fisier.Close();
            return rute;
        }
        else return null;

    }
    static List<Avion> CitireAvioane(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] avioane_string;
        StreamReader fisier = new StreamReader(path);

        if (File.Exists(path))
        {
            avioane_string = fisier.ReadToEnd().Split("\r\n");
            List<Avion> avioane = new List<Avion>();

            for (int i = 0; i < avioane_string.Length; i++)
            {
                string s = avioane_string[i];
                avioane.Add(transformari.StringtoAvion(s));
            }

            fisier.Close();
            return avioane;
        }
        else return null;

    }
    
    List<Cont> conturi = CitireConturi(@"conturi.txt");
    List<Zbor> zboruri = CitireZboruri("Lista_zboruri.txt");
    List<Ruta> rute = CitireRute("Lista_rute.txt");
    List<Avion> avioane = CitireAvioane("Lista_avioane.txt");


    public void InitDate(Companie companie)
    {
        foreach (Cont c in conturi)
            companie.AddCont(c);
        foreach (Ruta r in rute)
            companie.AddRute(r);
        foreach (Avion a in avioane)
            companie.AddAvion(a);
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
        DateTime dataCurenta = DateTime.Now;
        foreach (Zbor z in zboruri)
        {
            if (z.locuriDisponibile > 0 && z.data > dataCurenta)
            {
                Console.WriteLine(transformari.ZbortoString(z));
            }
        }
    
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
            //un zbor e disponibil daca sunt locuri disponibile si data si ora de plecare sunt in viitor
            
            DateTime dataCurenta = DateTime.Now;
            foreach (Zbor z in zboruri)
            {
                if (z.locuriDisponibile > 0 && z.data > dataCurenta)
                {
                    Console.WriteLine(transformari.ZbortoString(z));
                }
            }
            
            Console.WriteLine("Apasati orice tasta pentru a reveni...");
            Console.ReadKey();
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
                Console.Clear();
                Console.WriteLine("\n~~~~~ Adaugare zbor nou ~~~~~\n");
                
                Zbor z = new Zbor();
                
                int i;
                string cod;
                do
                {
                    Console.WriteLine("Codul zborului(ROxxx | INxxx): ");
                    cod = Console.ReadLine();
                    if (z.ValideazaCod(cod) == false)
                    {
                        Console.WriteLine("Cod invalid!\nReincercati?\n1)DA\n2)NU\n");
                        optiune = Convert.ToInt32(Console.ReadLine());
                        if (optiune != 1)
                        {
                            GestiuneZboruri();
                        }
                        else
                        {
                            AdaugareZborNou();
                            break;
                        }
                    }
                } while (z.ValideazaCod(cod) != true);
                
                Console.WriteLine("Plecarea din: ");
                string plecare = Console.ReadLine();
                
                Console.WriteLine("Destinatia: ");
                string destinatie = Console.ReadLine();
                
                Console.WriteLine("Distanta(in Km): ");
                int distantaKm = int.Parse(Console.ReadLine());
                
                /*
                DateTime dataPlecare;
                do
                {
                    Console.WriteLine("Data plecarii(dd/MM/yyyy): ");
                    string inputData = Console.ReadLine();
                    if (DateTime.TryParseExact(inputData, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataPlecare))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Data invalida!\nReincercati?\n1)DA\n2)NU\n");
                        int optiune = Convert.ToInt32(Console.ReadLine());
                        if (optiune != 1)
                        {
                            GestiuneZboruri();
                        }
                        else
                        {
                            AdaugareZborNou();
                            break;
                        }
                    }
                } while (true);
                
                DateTime oraPlecare = DateTime.Now;
                string formattedOraPlecare = oraPlecare.ToString("hh:mm");
                */
                
                Console.WriteLine("Data plecarii(dd/MM/yyyy): ");
                string dataString = Console.ReadLine();
                
                Console.WriteLine("Ora plecarii(hh:mm): ");
                string oraString = Console.ReadLine();
                
                DateTime dataPlecare;
                TimeSpan oraPlecare = default;
                DateTime data = default;
                
                if(DateTime.TryParseExact(dataString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataPlecare) &&
                   TimeSpan.TryParseExact(oraString, "hh:mm", null, out oraPlecare))
                {
                    data = new DateTime(dataPlecare.Day, dataPlecare.Month, dataPlecare.Year, oraPlecare.Hours, oraPlecare.Minutes, 0);
                }
                
                Console.WriteLine("Durata zborului(hh:mm): ");
                string durataZborString = Console.ReadLine();

                TimeSpan durataZborTimeSpan = default;
                TimeSpan durataZbor = default;
                
                if(TimeSpan.TryParseExact(durataZborString, "hh:mm", null, out durataZborTimeSpan))
                {
                    durataZbor = new TimeSpan(durataZborTimeSpan.Hours, durataZborTimeSpan.Minutes, 0);
                }
                
                Console.WriteLine("Numele avionului: ");
                string numeAvion = Console.ReadLine();
                
                Console.WriteLine("Numarul de locuri ale avionului: ");
                int numarLocuriAvion = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Numarul de locuri disponibile: ");
                int numarLocuriDisponibile = int.Parse(Console.ReadLine());
                
                Ruta ruta = new Ruta(plecare, destinatie, distantaKm);
                Avion avion = new Avion(numeAvion, numarLocuriAvion);
                Zbor zbor = new Zbor(cod, ruta, data, durataZbor, avion, numarLocuriDisponibile);
                
                zboruri.Add(zbor);
                
                Console.WriteLine("\nZborul a fost adaugat cu succes!\n");
                Console.WriteLine("Apasati orice tasta pentru a continua ...");
                Console.ReadKey();
            }
            void StergereZbor()
            {
                //dragan
                Console.Clear();
                Console.WriteLine("\n~~~~~ Stergere zbor ~~~~~\n");
                
                List<Zbor> zboruriExpirate = new List<Zbor>();
                Console.WriteLine("\nZborurile expirate sunt: \n");
                foreach (Zbor z in zboruri)
                {
                    if (z.data < DateTime.Now)
                    {
                        Console.WriteLine(transformari.ZbortoString(z));
                        zboruriExpirate.Add(z);
                    }
                }

                if (zboruriExpirate.Count == 0)
                {
                    Console.WriteLine("\nNu sunt zboruri expirate!\n");
                    Console.WriteLine("Apasati orice tasta pentru a continua ...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    int optiune;
                    Console.WriteLine("Doriti sa stergeti un singur zbor expirat sau pe toate?\n1)unul singur\n2)toate\n");
                    optiune= int.Parse(Console.ReadLine());
                    if (optiune == 1)
                    {
                        Console.WriteLine("\n\nIntroduceti codul zborului: ");
                        string cod = Console.ReadLine();
                        bool zborGasit = false;

                        for (int i = 0; i < zboruriExpirate.Count; i++)
                        {
                            if(zboruriExpirate[i].getCod() == cod)
                            {
                                zboruri.RemoveAt(i);
                                Console.WriteLine("\nZborul a fost sters cu succes!\n");
                                zborGasit = true;
                                break;
                            }
                        }
                        if (!zborGasit)
                        {
                            Console.WriteLine("\nCodul introdus nu corespunde vreounui zbor expirat!\n");
                            Console.WriteLine("Apasati orice tasta pentru a continua ...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        foreach (Zbor z in zboruriExpirate)
                        {
                            zboruri.Remove(z);
                        }
                        Console.WriteLine("\nToate zborurile expirate au fost sterse cu succes!\n");
                        Console.WriteLine("Apasati orice tasta pentru a continua ...");
                        Console.ReadKey();
                    }
                }
            }
            
            void VizualizareListaZboruri()
            {
                //dragan
                Console.Clear();
                Console.WriteLine("\n~~~~ Vizualizare lista completa de zboruri ~~~~\n\n");

                foreach (Zbor z in zboruri)
                {
                    Console.WriteLine(transformari.ZbortoString(z));
                }
                
                Console.WriteLine("\n\nApasati orice tasta pentru a continua ...\n\n");
                Console.ReadKey();
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
        pathRute = @"..\..\..\Lista_rute.txt";
        
        

        if (File.Exists(pathZboruri))
        {
            File.WriteAllText(pathZboruri, "");
            using (StreamWriter sw = File.AppendText(pathZboruri))
            {
                
                //File.WriteAllText(pathZboruri, "");
                for (int i = 0; i< zboruri.Count - 1; i++)
                    sw.Write(transformari.ZbortoString(zboruri[i]) + "\r");
                sw.Write(transformari.ZbortoString(zboruri[zboruri.Count-1]));
                
                    
            }
        }
        else
        {
            Console.WriteLine("Fisierul nu exista sau a fost mutat");
        }

        if (File.Exists(pathConturi))
        {
            File.WriteAllText(pathConturi, "");
            using (StreamWriter sw = File.AppendText(pathConturi))
            {
                for (int i = 0; i< conturi.Count - 1; i++)
                    sw.Write(transformari.ConttoString(conturi[i]) + "\r");
                sw.Write(transformari.ConttoString(conturi[conturi.Count-1]));
            }
        }
        else
        {
            Console.WriteLine("Fisierul nu exista sau a fost mutat");
        }
        
        if (File.Exists(pathRute))
        {
            File.WriteAllText(pathRute, "");
            using (StreamWriter sw = File.AppendText(pathRute))
            {
                for (int i = 0; i< rute.Count - 1; i++)
                    sw.Write(transformari.RutatoString(rute[i]) + "\r");
                sw.Write(transformari.RutatoString(rute[rute.Count-1]));
            }
        }
        else
        {
            Console.WriteLine("Fisierul nu exista sau a fost mutat");
        }
        
        
        

    }
}