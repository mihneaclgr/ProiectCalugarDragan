﻿using System.Runtime.CompilerServices;

namespace CompanieAeriana;

public class Wrapper
{
    private Transformari transformari = new Transformari();
    //public Companie companie;
    
    public List<Zbor> CitireZboruri(string numeFisier)
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
    public List<Cont> CitireConturi(string numeFisier)
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
    public List<Ruta> CitireRute(string numeFisier)
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
                Console.WriteLine("Optiune invalida!");
                break;
        }
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
    void RezervariPentruPasageri(Companie companie, Cont cont)
    {

        void VizualizareListaZboruriDisponibile()
        {
            
        }
        void RezervaLocZbor()
        {
            
        }
        void AnulareRezervare()
        {
            
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
                Console.WriteLine("\n~~~~~ Adaugare zboruri ~~~~~\n");
            }
            void StergereZbor()
            {
                
            }
            void VizualizareListaZboruri()
            {
                
            }
            void ActualizareInformatiiDespreZboruri()
            {
                
            }
            void VizualizareRuteDisponibile()
            {
                
            }
            void AdaugareStergereRute()
            {
                
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
            }
            void VizualizareVenituriZbor()
            {
            }
            void GenerareRaportZilnic()
            {
            } 
            void PlatiPasager()
            {
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
}