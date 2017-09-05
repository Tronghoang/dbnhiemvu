using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace sb_admin.web.Models
{
    public class QuenMatKhau
    {
        public static String FormAddress
        {
            get
            {
                SmtpSection cfg = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                return cfg.Network.UserName;
            }
        }

        //Hàm send mail 
        public string Send(string subject, string body, string to, bool isHtml, bool isSSL)
        {
            try
            {
            //    using (MailMessage mail = new MailMessage())
            //    {
            //        mail.From = new MailAddress("tronghoang0803@gmail.com");
            //       // DuAnPhanCungEntities db = new DuAnPhanCungEntities();
            //       //var a = from p in db.ThanhViens
            //       //     where p.TaiKhoan.Contains(to)
            //       //     select p.Email;

            //        mail.To.Add(to);
            //        mail.Subject = subject;
            //        mail.Body = body;
            //        mail.IsBodyHtml = isHtml;
            //        SmtpClient client = new SmtpClient();
            //        client.EnableSsl = isSSL;
            //        client.Send(mail);
                MailMessage message = new MailMessage();
                message.From = new MailAddress("tronghoang0803@gmail.com", "Dịch Vụ Chăm Sóc Sức Khỏe");
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.Body = body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("tronghoang0803@gmail.com", "khonghoitiec");
                smtp.EnableSsl = true;
                smtp.Send(message);
            
            }
            catch(Exception ex)
            {

                return "Gửi mail thất bại!";
            }
            return "Gửi mail thành công!";


        }
        //Lấy lại mật khẩu 
        public string BodyMail_LayLaiMatKhau(string Email, string MatKhau)
        {
            StringBuilder mailbody = new StringBuilder();
            mailbody.Append("<html><head><title>Lấy lại mật khẩu</title></head>");
            mailbody.Append("<body>");
            mailbody.Append("<p>Chào bạn</p>");
            mailbody.Append("<p>Thông tin tài khoản của bạn: </p>");
            mailbody.Append("<p><b>Email: </b>" + Email);
            mailbody.Append("</p><p><b>Mật khẩu: </b>" + MatKhau);
            mailbody.Append("<p> Chân thành cảm ơn!");
            return mailbody.ToString();
        }
    }
}