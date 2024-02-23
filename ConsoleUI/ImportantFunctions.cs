using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleUI
{
	public static class ImportantFunctions
	{
		public static void InstantiateNewWindow()
		{
			Console.ResetColor();
			Console.Clear();
			ConsoleItems.AllTextItems.Clear();
			ConsoleItems.AllInputFieldItems.Clear();
			ConsoleItems.AllButtonItems.Clear();
			ConsoleItems.AllClickableItems.Clear();
			ConsoleItems.MainLoop();
		}

		public static void LoginScreen()
		{
			string debugMessage = "";
			InstantiateNewWindow();
            Text titleTxt = new Text($"     CS50X Hospital     ", new Vector2(49, 2), true);
            
            InputField usernameField = new InputField("Username", new Vector2(48, 8), "Enter your username...");
			InputField passwordField = new InputField("Password", new Vector2(48, 13), "Enter your password...");
            passwordField.isPasswordField = true;

			Text debugTxt = new Text(debugMessage, new Vector2(0, 29));
			
			Button signinBtn = new Button("Sign In", new Vector2(48, 18));
            signinBtn.OnClickEvent += (bsender, args) => { SignIn(); };
			Button signupBtn = new Button("Sign Up", new Vector2(63, 18));
			signupBtn.OnClickEvent += ImportantFunctions.SignUp;

			void SignIn()
			{
				debugMessage = "";

				if(User.Get(usernameField.text, passwordField.text) != null)
				{
					User loggedInUser = User.Get(usernameField.text, passwordField.text);
					debugTxt.ChangeText($"Succesful login, welcome {loggedInUser.FirstName}!");
					Thread.Sleep(1000);
					HomeScreen(loggedInUser);
				} else
				{
					debugTxt.ChangeText("Username or Password Incorrect");
				}
			}
		}

		public static void SignUp(object sender, EventArgs e)
		{
			InstantiateNewWindow();
			string debugMessage = "";
			Text test = new Text($"             Create an hospital patient account             ", new Vector2(33, 2), true);

			InputField firstnameField = new InputField("First Name", new Vector2(32, 8), "Enter your first name...");
			InputField lastnameField = new InputField("Last Name", new Vector2(62, 8), "Enter your last name...     ");

			InputField usernameField = new InputField("Username", new Vector2(32, 13), "Enter a username...");
			InputField birthdayField = new InputField("Birthday", new Vector2(57, 13), "Enter your birthday (dd-mm-yyyy) ");
			InputField passwordField = new InputField("Password", new Vector2(32, 18), "Enter a strong password...");
            passwordField.isPasswordField = true;
			InputField repetitionPasswordField = new InputField("Repeat Your Password", new Vector2(64, 18), "Repeat your password...   ");
			repetitionPasswordField.isPasswordField = true;

			Button createAccountBtn = new Button("                      Create Account                      ", new Vector2(32, 25));
			Text debugTxt = new Text(debugMessage, new Vector2(0, 29));

			createAccountBtn.OnClickEvent += (bsender, args) => { CreateAccount(); };


			bool IsValidAccountData()
			{
				bool isValid = true;

				debugMessage = "";

				for(int i=0;i<usernameField.text.Length;i++)
				{
					if(usernameField.text[i] == ' ')
					{
						isValid = false;
						debugMessage = "Username may not contain a \" \".";
					}
				}

				if(firstnameField.text.Any(char.IsDigit))
				{
					isValid = false;
					debugMessage = "First name may not contain a number.";
				}

				if(lastnameField.text.Any(char.IsDigit))
				{
					isValid = false;
					debugMessage = "Last name may not contain a number.";
				}

				if (!DateTime.TryParseExact(birthdayField.text, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
				{
					isValid = false;
					debugMessage = "Make sure your birthdate follows the following format (dd-mm-yyyy)";
				}

				if(passwordField.text != repetitionPasswordField.text)
				{
					isValid = false;
					debugMessage = "Passwords do not match up";
				}

				if(User.Exists(usernameField.text))
				{
					isValid = false;
					debugMessage = "Username already in use.";
				}

				debugTxt.ChangeColor(ConsoleColor.Red);
				debugTxt.ChangeText(debugMessage);
				return isValid;
			}

			void CreateAccount()
			{
				if (!IsValidAccountData()) { return; }

				User newUser = new User(firstnameField.text, lastnameField.text, usernameField.text, passwordField.text);
				string[] arrBirthdayValues = birthdayField.text.Split('-');
				newUser.BirthDate = new DateTime(Convert.ToInt32(arrBirthdayValues[2]), Convert.ToInt32(arrBirthdayValues[1]), Convert.ToInt32(arrBirthdayValues[0]));
			
				User.Save();

				LoginScreen();
			}
		}

		public static void HomeScreen(User user)
		{
			InstantiateNewWindow();

			Text titleTxt = new Text($" Welcome, {user.FirstName}! ", new Vector2(2, 1), true);

			Text balanceTxt = new Text($"Balance: {user.Balance:C2}", new Vector2(1, 4));
			balanceTxt.ChangeColor(user.Balance >= 0 ? ConsoleColor.Green : ConsoleColor.Red);

			Button accountSettingsBtn = new Button("Account Settings", new Vector2(100, 1));
			accountSettingsBtn.OnClickEvent += (bsender, args) => { AccountSettings(); };

			Button depositBtn = new Button("Deposit", new Vector2(1, 10));
			depositBtn.ChangeColor(ConsoleColor.Green);

			Button withdrawBtn = new Button("Withdraw", new Vector2(14, 10));
			withdrawBtn.ChangeColor(ConsoleColor.Red);

			void AccountSettings()
			{
				InstantiateNewWindow();
                Text accountSettingsTitleTxt = new Text($" Account Settings ", new Vector2(2, 1), true);

				Text firstNameTxt = new Text($"First name: {user.FirstName}", new Vector2(1, 4));
				Text lastNameTxt = new Text($"Last name: {user.LastName}", new Vector2(1, 5));
				Text usernameTxt = new Text($"Username: {user.Username}", new Vector2(1, 7));
				Text passwordTxt = new Text($"Password: {user.Password}", new Vector2(1, 8));
				Text birthdayTxt = new Text($"Birthday: {user.BirthDate.ToString("dd-MM-yyyy")} | Age: {user.Age}", new Vector2(1, 10));

                Button backBtn = new Button("Back Home", new Vector2(107, 1));
                backBtn.OnClickEvent += (bsender, args) => { HomeScreen(user); };

            }
        }
	}
}
