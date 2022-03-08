using System.Linq;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers.utils;

public static class ConnectionController
{
	/// <summary>
	/// Get the user corresponding to the given login and password if he exists.
	/// </summary>
	/// <param name="login">The login of the user to get</param>
	/// <param name="password">The password of the user to get</param>
	/// <returns>SALARIE the user if found, null otherwise.</returns>
	public static SALARIE getUser(string login, string password)
	{
		string hashedPassword = PasswordUtils.getHash(password);
		var allUsers = from s in DbContext.get().SALARIE
						  where s.LOGIN == login
						  where s.MDP == hashedPassword
						  select s;

		return allUsers.FirstOrDefault();
	}
}
