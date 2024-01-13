using DAL_Proj.Entities;
using System.Net;
using System.Net.Mail;

namespace PL_Proj.Utilities
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("ao8856695@gmail.com", "iiwj wbmm zrjl skfo");
			Client.Send("ao8856695@gmail.com", email.Recipient, email.Subject, email.Body);
		}
	}
}
