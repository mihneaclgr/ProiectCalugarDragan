namespace CompanieAeriana;

public class Wrapper
{
    private Transformari transformariZbor;
    List<Zbor> CitireZboruri(string numeFisier)
    {
        string path = @"..\..\..\" + numeFisier;
        string[] zboruri_string = File.ReadAllText(path).Split("\n");
        List<Zbor> zboruri = new List<Zbor>();

        for (int i = 0; i < zboruri_string.Length; i++)
        {
            string s = zboruri_string[i];
            zboruri.Append(transformariZbor.StringtoZbor(s));
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
           conturi.Append(transformariZbor.StringtoCont(s));
        }
        
        return conturi;
    }
}