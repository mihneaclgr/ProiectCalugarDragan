using CompanieAeriana;

Companie companie = new Companie("NovaGrup");
//companie.AddCont(new Cont("admin","adminpass123"));
Wrapper wrapper = new Wrapper();

List<Cont> conturi = wrapper.CitireConturi(@"conturi.txt");
List<Zbor> zboruri = wrapper.CitireZboruri("Lista_zboruri.txt");
List<Ruta> rute = wrapper.CitireRute("rute.txt");


wrapper.MeniuLogin(companie);
