using CompanieAeriana;

Companie companie = new Companie("NovaGrup");
companie.AddCont(new Cont("admin","adminpass123"));
Wrapper wrapper = new Wrapper();

wrapper.MeniuLogin(companie);
