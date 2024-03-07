using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
	public class BankAccount
	{
		private double _balance { get; set; } = 0;
		public double Balance
		{
			get
			{
				return _balance;
			}
		}
		public string IBAN { get; private set; }
		public User AccountHolder { get; private set; }

		public BankAccount(User AccountHolder)
		{
			IBAN = GenerateIBAN();
			this.AccountHolder = AccountHolder;
		}

		public void Deposit(double amount)
		{
			if(amount<0) { return; }

			_balance+=amount;
		}

		public void Withdraw(double amount)
		{
			if(amount>_balance) { return; }
			if(amount<0) { return; }

			_balance-=amount;
		}

		string GenerateIBAN()
		{
			Random random = new Random();
			return $"BE30 {random.Next(1000, 10000)} {random.Next(1000, 10000)} {random.Next(1000, 10000)}";
		}

		public static void TransferFunds(User user1, User user2, double amount)
		{
			if(user1.HasBankAccount && user2.HasBankAccount)
			{
				if(user1.PersonalBankAccount.Balance > amount && amount > 0)
				{
					user1.PersonalBankAccount.Withdraw(amount);
					user2.PersonalBankAccount.Deposit(amount);
				}
			}
		}
	}
}
