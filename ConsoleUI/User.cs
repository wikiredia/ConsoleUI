using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.CodeDom;


namespace ConsoleUI
{
	public class User
	{
		// Auto Properties
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		private static string fileName = "UserData.json";

		public DateTime BirthDate { get; set; } = DateTime.Now;

		public static List<User> RegisteredUsers { get; set; } = new List<User>();

		public BankAccount PersonalBankAccount { get; private set; } = null;
		public bool HasBankAccount
		{
			get
			{
				return PersonalBankAccount != null;
			}
		}

		// Full Properties
		public int Age
		{
			get
			{
				return (DateTime.Now - BirthDate).Days / 365;
			}
		}

		public User(string firstname, string lastname, string username, string password)
		{
			FirstName = firstname;
			LastName = lastname;
			Username = username;
			Password = password;

			RegisteredUsers.Add(this);
		}


		public static User Get(string username, string password)
		{
            if (!Exists(username)) { return null; }

			User necessaryUser = null;

            for (int i=0;i<RegisteredUsers.Count;i++)
			{
				if(RegisteredUsers[i].Username.ToLower() == username.ToLower())
				{
					if (RegisteredUsers[i].Password == password)
					{
						necessaryUser = RegisteredUsers[i];
					}
				}
			}
			
			return necessaryUser;
		}

		public static bool Exists(string username)
		{
			bool exists = false;

			for(int i=0;i<RegisteredUsers.Count;i++)
			{
				if(username.ToLower() == RegisteredUsers[i].Username.ToLower())
				{
					exists = true;
				}
			}
			return exists;
		}
		
		public void OpenBankAccount()
		{
			PersonalBankAccount = new BankAccount();
		}
		public void CloseBankAccount()
		{
			PersonalBankAccount = null;
		}

		public static void Save()
		{
			File.WriteAllText(fileName, $"{ConvertToJson()}\n");
		}

		public static string ConvertToJson()
		{
			return JsonConvert.SerializeObject(RegisteredUsers, Formatting.Indented);
		}

		public static void LoadData()
		{
			if(!File.Exists(fileName) || File.ReadAllText(fileName) == "")
			{
				File.WriteAllText(fileName, "[]");
			}
			RegisteredUsers = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(fileName));
		}
	}
}
