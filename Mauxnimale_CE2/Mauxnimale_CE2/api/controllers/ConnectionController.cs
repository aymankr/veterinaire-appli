using System.Linq;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api;

public static class Connection
{
	public static SALARIE GetSALARIE(string login, string mdp)
	{
        var allSalaries = from s in DbContext.get().SALARIE
				  where s.LOGIN == login
				  where s.MDP == mdp
				  select s;
		foreach(var s in allSalaries)
        {
			if(s.LOGIN == login && s.MDP == mdp)
            {
				return s;
            }
        }
		return null;
	}
}
