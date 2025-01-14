using CompanieAeriana;

Companie companie = new Companie("NovaGrup");
//companie.AddCont(new Cont("admin","adminpass123"));
Wrapper wrapper = new Wrapper();

wrapper.InitDate(companie);
//wrapper.MeniuLogin(companie);
wrapper.SaveDate(companie);
