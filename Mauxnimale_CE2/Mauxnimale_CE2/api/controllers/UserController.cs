using System;
using System.Collections.Generic;
using System.Linq;
using Mauxnimale_CE2.api;
using Mauxnimale_CE2.api.entities;
using Mauxnimale_CE2.api.controllers.utils;

public static class UserController
{
	/* GET METHODS */

	/// <summary>
	/// Get the user corresponding to the given login and password if he exists.
	/// </summary>
	/// <param name="login">The login of the user to get</param>
	/// <param name="password">The password of the user to get</param>
	/// <returns>SALARIE the user if found, null otherwise.</returns>
	public static SALARIE getConnection(string login, string password)
	{
		string hashedPassword = PasswordUtils.getHash(password);
		var allUsers = from s in DbContext.get().SALARIE
					   where s.LOGIN == login
					   where s.MDP == hashedPassword
					   select s;

		return allUsers.FirstOrDefault();
	}

	/// <summary>
	/// Get all the employees in the database.
	/// </summary>
	/// <returns>A list of all the employees present in the database.</returns>
	public static List<SALARIE> getAllEmployees()
	{
		return DbContext.get().SALARIE.ToList();
	}


	/* UPDATES METHODS */

	public static bool updateInfos(SALARIE user,
								   string firstName,
								   string lastName,
								   string login,
								   string password,
								   string email,
								   string phoneNumber,
								   string adress)
	{
		if (firstName != null && firstName.Any())
		{
			if (InputVerification.noNumber(firstName))
				user.PRENOM = firstName;
			else
			{
				Console.WriteLine("First name: " + firstName + " invalid. Contains a number.");
				return false;
			}
		}

		if (lastName != null && lastName.Any())
		{
			if (InputVerification.noNumber(lastName))
				user.NOM = lastName;
			else
			{
				Console.WriteLine("Last name: " + lastName + " invalid. Contains a number.");
				return false;
			}
		}

		if (login != null && login.Any())
		{
			if (InputVerification.noSpecialCharacters(login))
				user.LOGIN = login;
			else
			{
				Console.WriteLine("Login: " + login + " invalid. Contains a special character.");
				return false;
			}
		}

		if (adress != null && adress.Any())
		{
			user.ADRESSE = adress;
		}

		if (password != null && password.Any())
		{
			user.MDP = PasswordUtils.getHash(password);
		}

		if (email != null && email.Any())
		{
			if (InputVerification.isEmail(email))
				user.EMAIL = email;
			else
			{
				Console.WriteLine("Email: " + email + " invalid. It does not match the email pattern.");
				return false;
			}
		}

		if (phoneNumber != null && phoneNumber.Any())
		{
			if (InputVerification.isPhoneNumber(phoneNumber))
				user.TEL = phoneNumber.Trim();
			else
			{
				Console.WriteLine("Phone number: " + phoneNumber + " invalid. It does not exclusively contains numbers.");
				return false;
			}
		}

		DbContext.get().SaveChanges();
		Console.WriteLine("All infos updated successfully.");
		return true;
	}

	/// <summary>
	/// Update the user's login if valid.
	/// </summary>
	/// <param name="user">The user to update the login to</param>
	/// <param name="newLogin">The new user's login</param>
	/// <returns>true if it succeeded, false otherwise</returns>
	public static bool updateLogin(SALARIE user, string newLogin)
	{
		return updateInfos(user, null, null, newLogin, null, null, null, null);
	}

	/// <summary>
	/// Update the user's password if valid.
	/// </summary>
	/// <param name="user">The user to update the password to</param>
	/// <param name="newLogin">The new user's password</param>
	/// <returns>true if it succeeded, false otherwise</returns>
	public static bool updatePassword(SALARIE user, string enteredPrevPassword, string newPassword)
	{
		if (user.MDP != PasswordUtils.getHash(enteredPrevPassword))
		{
			Console.WriteLine("WARNING: Entered previous password does not correspond to the user's password.");
			return false;
		}

		return updateInfos(user, null, null, null, newPassword, null, null, null);
	}

	/// <summary>
	/// Update the salary of the given employee.
	/// </summary>
	/// <param name="employee">The employee concerned.</param>
	/// <param name="newSalary">The new salary amount</param>
	public static void updateSalary(SALARIE employee, decimal newSalary)
	{
		employee.SALAIRE = newSalary;
		DbContext.get().SaveChanges();
		Console.WriteLine("Salary updated.");
	}

	/// <summary>
	/// Update the intership dates of the given intern.
	/// </summary>
	/// <param name="intern">The intern concerned.</param>
	/// <param name="beginDate">The new begin date of the internship</param>
	/// <param name="endDate">The new end date of the internship</param>
	/// <returns>true if it succeeded, false otherwise.</returns>
	public static bool updateInternshipDates(SALARIE intern, DateTime beginDate, DateTime endDate)
	{
		if (!InputVerification.isDateEarlier(beginDate, endDate))
		{
			Console.WriteLine("Begin date: " + beginDate + " is not earlier than end date: " + endDate);
			return false;
		}

		intern.DATEDEBUTSTAGE = beginDate;
		intern.DATEFINSTAGE = endDate;
		DbContext.get().SaveChanges();

		Console.WriteLine("Internship dates updated.");
		return true;
	}

	/// <summary>
	/// Set the first connection property of the user as true.
	/// </summary>
	/// <param name="user">The user concerned.</param>
	public static void setFirstConnectionDone(SALARIE user)
	{
		user.PREMIERECONNEXION = true;
		DbContext.get().SaveChanges();
		Console.WriteLine("User with login: " + user.LOGIN + " has connected for the first time.");
	}
}