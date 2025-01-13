using CompanieAeriana;

Companie companie = new Companie("NovaGrup");

void MeniuGuest()
{
    Console.Clear();
    Console.WriteLine("~~~~ Bine ati venit in modul Guest ~~~~\n\n");
    Console.WriteLine("Lista zborurilor disponibile:\n");

    foreach (Zbor zbor in companie.ZboruriDisponibile)
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

    switch (option)
    {
        case 1:
            break;
        case 2:
            Console.Clear();
            Console.WriteLine("\n~~~~ Accesare cont ~~~~\n\n");
            Cont cont;
            do
            {
                string username, parola;
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
            break;
        case 3:
            Console.Clear();
            MeniuGuest();
            break;
        default:
            Console.WriteLine("Optiune invalida!");
            break;
    }
}

MeniuLogin();       