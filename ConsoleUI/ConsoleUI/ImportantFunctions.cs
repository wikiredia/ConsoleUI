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

			Button debugBtn = new Button("Debug", new Vector2(50, 22));
			debugBtn.OnClickEvent += (bsender, args) => { debugTxt.ChangeText($"{User.Get(usernameField.text, passwordField.text).HasBankAccount}"); };

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
			Text debugTxt = new Text($"{User.Get(user.Username, user.Password).PersonalBankAccount==null}", new Vector2(50, 10));
			Button accountSettingsBtn = new Button("Account Settings", new Vector2(100, 1));
			accountSettingsBtn.OnClickEvent += (bsender, args) => { AccountSettings(); };
            
			Button logOutBtn = new Button("Log Out", new Vector2(109, 28));
            logOutBtn.OnClickEvent += (bsender, args) => { LoginScreen(); };

            if (user.HasBankAccount)
			{
				Text balanceTxt = new Text($"Balance: {user.PersonalBankAccount.Balance:F2}$", new Vector2(1, 4));
				balanceTxt.ChangeColor(user.PersonalBankAccount.Balance >= 0 ? ConsoleColor.Green : ConsoleColor.Red);

				Button depositBtn = new Button("Deposit", new Vector2(1, 10));
				depositBtn.ChangeColor(ConsoleColor.Green);
				depositBtn.OnClickEvent += (bsender, args) => { Deposit(); };

				Button withdrawBtn = new Button("Withdraw", new Vector2(14, 10));
				withdrawBtn.ChangeColor(ConsoleColor.Red);
				withdrawBtn.OnClickEvent += (bsender, args) => { Withdraw(); };
			} else
			{
				Button openAccountBtn = new Button("Open Bank Account", new Vector2(1, 4));
				openAccountBtn.ChangeColor(ConsoleColor.Green);
				openAccountBtn.OnClickEvent += (bsender, args) => { user.OpenBankAccount(); HomeScreen(user); };
			}

			Button saveChangesBtn = new Button("Save Changes", new Vector2(1, 28));
			saveChangesBtn.OnClickEvent += (bsender, args) => { User.Save(); };

			Button loadChangesBtn = new Button("Load Changes", new Vector2(20, 28));
			loadChangesBtn.OnClickEvent += (bsender, args) => { User.LoadData(); HomeScreen(user); };

			void AccountSettings()
			{
				InstantiateNewWindow();
                Text accountSettingsTitleTxt = new Text($" Account Settings ", new Vector2(2, 1), true);

				Text firstNameTxt = new Text($"First name: {user.FirstName}", new Vector2(1, 4));
				Text lastNameTxt = new Text($"Last name: {user.LastName}", new Vector2(1, 5));
				Text usernameTxt = new Text($"Username: {user.Username}", new Vector2(1, 7));
				Text passwordTxt = new Text($"Password: {user.Password}", new Vector2(1, 8));
				Text birthdayTxt = new Text($"Birthday: {user.BirthDate.ToString("dd-MM-yyyy")} | Age: {user.Age}", new Vector2(1, 10));
				string hasAccount = user.HasBankAccount ? Convert.ToString(user.PersonalBankAccount.Balance) + "$" : "Please open a bank account!";
				Text accountBalanceTxt = new Text($"Bank Account Balance: {hasAccount}", new Vector2(1, 13));
                Button backBtn = new Button("Back Home", new Vector2(107, 1));
                backBtn.OnClickEvent += (bsender, args) => { HomeScreen(user); };

            }

			void Deposit()
			{
				ImportantFunctions.InstantiateNewWindow();				

				int depositAmount = 0;

				Text depositTxt = new Text("How much money would you like to deposit? ", new Vector2(1, 1));
				depositTxt.ChangeColor(ConsoleColor.Green);
				Button plusBtn = new Button("+", new Vector2(1, 4));
				plusBtn.ChangeColor(ConsoleColor.Green);

				Text Price = new Text($"{depositAmount:F2}$", new Vector2(7, 4));

				Button minusBtn = new Button("-", new Vector2(20, 4));
				minusBtn.ChangeColor(ConsoleColor.Red);

				Button depositConfirmBtn = new Button("Deposit", new Vector2(1, 8));
				depositConfirmBtn.ChangeColor(ConsoleColor.Green);

				Button cancelButton = new Button("Cancel", new Vector2(14, 8));
				cancelButton.ChangeColor(ConsoleColor.Gray);

				minusBtn.OnClickEvent += (bsender, args) => { depositAmount-=10; ChangePrice(); };
				plusBtn.OnClickEvent += (bsender, args) => { depositAmount+=10; ChangePrice(); };
				depositConfirmBtn.OnClickEvent += (bsender, args) => { user.PersonalBankAccount.Deposit(depositAmount); HomeScreen(user); };
				cancelButton.OnClickEvent += (bsender, args) => { HomeScreen(user); };

				void ChangePrice()
				{
					Price.ChangeText($"{depositAmount:F2}$");
				}
			}

			void Withdraw()
			{
				ImportantFunctions.InstantiateNewWindow();

				int withdrawAmount = 0;

				Text withdrawTxt = new Text("How much money would you like to withdraw?", new Vector2(1, 1));
				withdrawTxt.ChangeColor(ConsoleColor.Red);
				Button plusBtn = new Button("+", new Vector2(1, 4));
				plusBtn.ChangeColor(ConsoleColor.Green);

				Text Price = new Text($"{withdrawAmount:F2}$", new Vector2(7, 4));

				Button minusBtn = new Button("-", new Vector2(20, 4));
				minusBtn.ChangeColor(ConsoleColor.Red);

				Button withdrawConfirmBtn = new Button("Withdraw", new Vector2(1, 8));
				withdrawConfirmBtn.ChangeColor(ConsoleColor.Red);

				Button cancelButton = new Button("Cancel", new Vector2(14, 8));
				cancelButton.ChangeColor(ConsoleColor.Gray);

				minusBtn.OnClickEvent += (bsender, args) => { withdrawAmount-=10; ChangePrice(); };
				plusBtn.OnClickEvent += (bsender, args) => { withdrawAmount+=10; ChangePrice(); };
				withdrawConfirmBtn.OnClickEvent += (bsender, args) => { user.PersonalBankAccount.Withdraw(withdrawAmount); HomeScreen(user); };
				cancelButton.OnClickEvent += (bsender, args) => { HomeScreen(user); };

				void ChangePrice()
				{
					Price.ChangeText($"{withdrawAmount:F2}$");
				}
			}
        }
	}
}
