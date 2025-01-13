using CompanieAeriana;

Companie companie = new Companie("NovaGrup");



void MeniuGuest()
{
    Console.Clear();
    Console.WriteLine("~~~~ Bine ati venit in modul Guest ~~~~\n\n");
    Console.WriteLine("Lista zborurilor disponibile:\n");
    
    foreach (Zbor zbor in )
    {
        Console.WriteLine($"Plecare din: {zbor.ruta.getPlecareDin()}, Destinatie: {zbor.ruta.getDestinatie()}");
    }
    
    Console.WriteLine("\n\nApasati orice tasta pentru a reveni la meniul principal ...\n\n");
    Console.ReadKey();
    MeniuLogin();
}

void MeniuLogin()
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
                        MeniuLogin();
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
                        MeniuLogin();
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
            Meniu();


            break; //
        
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
                        MeniuLogin();
                    }
                }
            } while (!companie.ContInLista(cont));

            if (cont.username == "admin")
            {
                MeniuAdmin();
            }
            else
            {
                Meniu();
            }
            break;
        
        //continua ca Guest
        case 3:
            Console.Clear();
            MeniuGuest();
            break;
        
        
        default:
            Console.WriteLine("Optiune invalida!");
            break;
    }
}
void Meniu()
{
    
}
void MeniuAdmin()
{
    
}



MeniuLogin();       