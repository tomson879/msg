using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using Model;

namespace DAL
{
    public static class MailSend
    {
        public static void sendMail(string Guid)
        {
            Q_FlowInvest a = Q_FlowInvestService.getOne(" Guid = '" + Guid + "'");
            if (a.FundId == 57 || a.FundId == 59) //Argo 和 Prelude才发送
            {
                string Manager = AccountMgService.getManager_ByFundMgId(a.FundMgId);
                string Entity = "entity：        " + FundService.getName(a.FundId);
                string Ticker = "ticker：        " + a.StockCode;
                string Name = "name：        " + a.StockName;
                string OrderType = "type：        " + (a.TradeType == "买入" ? "buy" : (a.TradeType == "卖出" ? "sell" : (a.TradeType == "做空" ? "short" : (a.TradeType == "减空头" ? "cover short" : ""))));
                string Amount = "amount：        " + a.PositionDescribe.Replace("%", "") + a.Amount;
                string price = "price：        " + a.PriceDescribe;
                string Reason = "reason" + (Manager == "刘迅" ? "<br/>" : "：") + a.Reason;
                string MailBody = Entity + "<br/>" + Ticker + "<br/>" + Name + "<br/>" + OrderType + "<br/>" + Amount + "<br/>" + price + (Manager == "刘迅" ? "<p/>" : "<br/>") + Reason;

                sendMail(MailBody, Manager,Guid);
            }
        }
        private static bool sendMail(string MessageBody, string Manager,string Guid0)
        {
            string Mail_Host = "smtp.exmail.qq.com";
            int Mail_Port = 25;
            string Mail_Pwd = "Xtf687";
            string Mail_Name = (Manager == "刘迅" ? "PM2" : "PM1");
            string Mail_Address = (Manager == "刘迅" ? "pm2@ntf.fund" : "pm1@ntf.fund");
            string Mail_Pop = "imap.exmail.qq.com";
            string MessageSubject = "order";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(Mail_Address, Mail_Name, System.Text.Encoding.UTF8);
            message.To.Add("compliance@ntf.fund");
            message.To.Add("trader@ntf.fund");
            //message.To.Add("yangt@newtf.com");
            //message.To.Add("yangt@ntf.fund");

            message.Subject = MessageSubject;
            message.Body = MessageBody;
            message.IsBodyHtml = true; //是否为html格式 
            message.Priority = MailPriority.High; //发送邮件的优先等级 
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            SmtpClient sc = new SmtpClient();
            sc.Host = Mail_Host; //指定发送邮件的服务器地址或IP 
            sc.Port = Mail_Port; //指定发送邮件端口 
            sc.Credentials = new System.Net.NetworkCredential(Mail_Address, Mail_Pwd); //指定登录服务器的用户名和密码 


            try
            {
                sc.Send(message); //发送邮件
                Q_LogService.getLog("订单人：" + Manager,"Form","sendMail", Guid0, "每5小时自动发送", "发送邮件成功", MessageBody);
                return true;
            }
            catch
            {
                Q_LogService.getLog("订单人：" + Manager, "Form", "sendMail", Guid0, "每5小时自动发送", "发送邮件成功", MessageBody);
                return false;
            }

        }


    }
}
