using System.Text;
using OtpNet;

var key = Guid.NewGuid().ToString();

var otp = new Totp(Encoding.UTF8.GetBytes(key), 120, OtpHashMode.Sha512, 6);

var topt =otp.ComputeTotp(DateTime.Now);
Console.WriteLine(otp.RemainingSeconds());






Console.ReadLine();