using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMC.Database.Models
{
    class UserLog
    {
		private static string FirstName;
		private static string LastName;
		private static string MiddleName;
		private static string UserName;
		private static int Role;
		private static int useridd;
		private static int isPasswordChanged;
		private static int isOnline;

		public UserLog(string firstName, string lastname, string middlename, int role_id, string username, int userid, int isPasword,int isOl)
		{
			FirstName = firstName;
			LastName = lastname;
			MiddleName = middlename;
			Role = role_id;
			UserName = username;
			useridd = userid;
			isPasswordChanged = isPasword;
			isOnline = isOl;
		}

		public static string getFirstName()
		{
			return FirstName;
		}

		public static string getLastName()
		{
			return LastName;
		}
		public static string getMiddleName()
		{
			return MiddleName;
		}
		public static string getUserName()
		{
			return UserName;
		}
		public static int getRole()
		{
			return Role;
		}

		public static string getFullName()
		{
			return FirstName + " "  + LastName;
		}

		public static int getUserId()
		{
			return useridd;
		}

		public static int getIsPasswordChanged()
		{
			return isPasswordChanged;
		}

		public static int getStatusOnline()
		{
			return isOnline;
		}
	}
}
