using System.Net.Mail;
using System.Net;

namespace ProspercityApiUsuarios.ClasesAux
{
    public class Correos
    {
        public void enviarEMail(string correo, string mensaje, string asunto)
        {
            MailMessage Correo = new MailMessage();
            Correo.From = new MailAddress(correo);
            Correo.To.Add(correo);
            Correo.Subject = (asunto);
            Correo.Body = mensaje;
            Correo.Priority = MailPriority.Normal;

            SmtpClient ServerEmail = new SmtpClient();
            ServerEmail.Credentials = new NetworkCredential("elpantera142@gmail.com", "cdcxwtkislxjstmv");
            ServerEmail.Host = "smtp.gmail.com";
            ServerEmail.Port = 587;
            ServerEmail.EnableSsl = true;
            try
            {
                ServerEmail.Send(Correo);
            }
            catch (Exception e)
            {
                Console.Write("eroor al enviar el correo: " + e);
            }
            Correo.Dispose();
        }
    }
}
