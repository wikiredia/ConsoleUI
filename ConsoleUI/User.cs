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

		public static bool TestPassword(string username, string passwordAttempt)
		{
			if(!Exists(username)) { return false; }
			return Get(username).Password == passwordAttempt;
		}

		public static User Get(string username)
		{
			User necessaryUser = null;

			for(int i=0;i<RegisteredUsers.Count;i++)
			{
				if(RegisteredUsers[i].Username == username)
				{
					necessaryUser = RegisteredUsers[i];
				}
			}
			
			return necessaryUser;
		}
		public static bool Exists(string username)
		{
			return Get(username) != null;
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
			if(File.ReadAllText(fileName) == "")
			{
				File.WriteAllText(fileName, "[]");
			}
			RegisteredUsers = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(fileName));
		}
	}
}
