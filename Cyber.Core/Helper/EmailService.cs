using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Cyber.Core.Helper;

public class EmailService
{
    public void SendEmail(string email, string code)
    {
        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential("gelab2109@gmail.com", "wulyylslqxdqgtvi"),
            EnableSsl = true
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress("gelab2109@gmail.com"),
            Subject = $"ახალი შეტყობინება: {DateTime.Now}",
            Body = @$"<!DOCTYPE html>
            <html lang=""en"">
            <head>
              <meta charset=""UTF-8"" />
              <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
              <title>Verification Code</title>
            </head>
            <body style=""margin:0; padding:0; background:#f5f6fa; font-family:Arial, Helvetica, sans-serif;"">
              <table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f5f6fa; padding:24px 0;"">
                <tr>
                  <td align=""center"">
                    <table role=""presentation"" width=""600"" cellpadding=""0"" cellspacing=""0"" style=""width:600px; max-width:600px; background:#ffffff; border-radius:14px; overflow:hidden; box-shadow:0 6px 18px rgba(0,0,0,0.08);"">
          
                      <tr>
                        <td style=""padding:22px 28px; background:#111827;"">
                          <div style=""color:#ffffff; font-size:18px; font-weight:700; letter-spacing:0.2px;"">
                            Your Verification Code
                          </div>
                          <div style=""color:#cbd5e1; font-size:13px; margin-top:6px;"">
                            Use this code to verify your email
                          </div>
                        </td>
                      </tr>

                      <tr>
                        <td style=""padding:28px;"">
                          <div style=""font-size:15px; color:#111827; line-height:1.6;"">
                            Hi!,<br /><br />
                            Here is your 6-digit verification code:
                          </div>

                          <div style=""margin:18px 0 10px; text-align:center;"">
                            <div style=""display:inline-block; padding:14px 18px; border-radius:12px; background:#f3f4f6; border:1px solid #e5e7eb;"">
                              <span style=""font-size:28px; font-weight:800; letter-spacing:8px; color:#111827;"">
                                {code}
                              </span>
                            </div>
                          </div>

                          <div style=""font-size:13px; color:#6b7280; line-height:1.6;"">
                            This code expires in <b>5 minutes</b>. If you didn’t request this, you can ignore this email.
                          </div>

                          <hr style=""border:none; border-top:1px solid #e5e7eb; margin:22px 0;"" />

                          <div style=""font-size:12px; color:#9ca3af; line-height:1.6;"">
                            Tip: Don’t share this code with anyone — we will never ask for it.
                          </div>
                        </td>
                      </tr>

                      <tr>
                        <td style=""padding:18px 28px; background:#f9fafb; font-size:12px; color:#9ca3af;"">
                          © Step Homework • This is an automated message, please don’t reply.
                        </td>
                      </tr>

                    </table>

                    <div style=""font-size:11px; color:#9ca3af; margin-top:14px;"">
                      If you can’t see the code, make sure images are enabled or use the plain code: <b>{code}</b>
                    </div>
                  </td>
                </tr>
              </table>
            </body>
            </html>",
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        smtp.Send(mailMessage);
    }
}
