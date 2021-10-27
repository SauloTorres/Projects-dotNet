using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnviarEmail.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;

namespace EnvioEmail.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            
                ViewBag.MsgSucess = TempData["msg_sucess"] as string;
           
                ViewBag.MsgError = TempData["msg_error"] as string;
            
            
            return View();
        }

        [HttpPost]
        public ActionResult EnviaEmail(HttpPostedFileBase file)
        {
            //Baixa imagem no diretorio
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Content/pics"), fileName);
            file.SaveAs(path);
            string emailDestinatario = Request.Form["txtEmail"];
            string grupoEmail = Request.Form["grupoEmail"];
            List<Destinatario> destinatarios = new List<Destinatario>();



            Destinatario email1 = new Destinatario("ba9132@sescbahia.com.br");
            Destinatario email2 = new Destinatario("saulomeca97@gmail.com");

            if (grupoEmail == "Exemplo1")
            {
                destinatarios.Add(email1);
            }
            else
            {
                destinatarios.AddRange(new[] { email1, email2 });
            }

            SendMail(emailDestinatario, fileName, destinatarios);
            return RedirectToAction("Index");
        }

        public bool SendMail(string email, string filename, List<Destinatario> destinatarios)
        {
            try
            {
                // Estancia da Classe de Mensagem
                MailMessage _mailMessage = new MailMessage();
                // Remetente
                _mailMessage.From = new MailAddress("saulomeca97@hotmail.com");

                // Destinatario seta no metodo abaixo

                //Contrói o MailMessage
                foreach (var item in destinatarios)
                {
                    _mailMessage.To.Add(item.Email);
                }
                _mailMessage.IsBodyHtml = true;
                //var contentID = "Image";
                //var inlineLogo = new Attachment(@"C:\Users\ASTIN\source\repos\EnviarEmail\EnviarEmail\Content\pics\"+ filename);
                //inlineLogo.ContentId = contentID;
                //inlineLogo.ContentDisposition.Inline = true;
                //inlineLogo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                //_mailMessage.Attachments.Add(inlineLogo);
                //_mailMessage.Body += "<br /><br /><img src=\"cid:" + contentID + "\" ><br />";

                //Insere imagem no corpo do email
                string str = "<html><body><br/><img src=\"cid:image1\"></body></html>";
                AlternateView av =
                             AlternateView.CreateAlternateViewFromString(str,
                             null, MediaTypeNames.Text.Html);
                LinkedResource lr = new LinkedResource(@"C:\Users\ASTIN\source\repos\EnviarEmail\EnviarEmail\Content\pics\" + filename,
                             MediaTypeNames.Image.Jpeg);
                lr.ContentId = "image1";
                av.LinkedResources.Add(lr);
                _mailMessage.AlternateViews.Add(av);

                //Attachment anexar = new Attachment(body);
                //_mailMessage.Attachments.Add(anexar);

                _mailMessage.Subject = "Email Teste";

                //_mailMessage.Body = body;

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.live.com", Convert.ToInt32("25"));

                //CONFIGURAÇÃO SEM PORTA
                // SmtpClient _smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential("saulomeca97@hotmail.com", "senha");

                _smtpClient.EnableSsl = true;
                _smtpClient.Send(_mailMessage);
                TempData["msg_sucess"] = "Email enviado com sucesso";

                return true;              
            }
            catch
            {
                TempData["msg_error"] = "Um erro ocorreu, por favor contate o suporte";
                //throw ex;
                return false;
            }
        }
    }
}
