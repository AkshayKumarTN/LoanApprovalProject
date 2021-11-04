namespace Repository.Repository
{
    using Experimental.System.Messaging;
    using global::Repository.Context;
    using global::Repository.Interface;
    using Microsoft.Extensions.Configuration;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    public class FormRepository:IFormRepository
    {
        private readonly UserContext userContext;
        public IConfiguration configuration { get; }

        public FormRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        public bool AddToForm(FormListModel formData)
        {
            try
            {
                FormModel formModel = new FormModel();
                formModel.Reason = formData.Reason;
                formModel.UserId = formData.UserId;
                formModel.Status = "pending";
                this.userContext.Form.Add(formModel);
                this.userContext.SaveChanges();
                var formTableData = this.userContext.Form.Where(x => x.Reason.Equals(formData.Reason) && x.UserId == formData.UserId && x.Status.Equals("pending")).SingleOrDefault();
                var userEmail = this.userContext.Users.Where(x => x.UserId == formData.UserId).SingleOrDefault();
                long totalWorth = AddProperties(formTableData.FormId, formData.UserId, formData.propertyList);
                if(totalWorth>0)
                {
                    double approvedAmount = totalWorth * 0.1;
                    this.SendToQueue(approvedAmount);
                    if(this.SendMail(userEmail.EmailId))
                    {
                        formTableData.ApprovedAmount = approvedAmount;
                        formTableData.Status = "approved";
                        this.userContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        formTableData.ApprovedAmount = 0;
                        formTableData.Status = "denied";
                        this.userContext.SaveChanges();
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public long AddProperties(int formId,int userId, List<PropertyModel> propertyList)
        {
            try
            {
                int totalWorth = 0;
                foreach(var x in propertyList)
                {
                    x.FormId = formId;
                    x.UserId = userId;
                    totalWorth += Convert.ToInt32(x.PropertyWorth);
                }
                this.userContext.Property.AddRange(propertyList);
                this.userContext.SaveChanges();
                return totalWorth>0? totalWorth:0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SendToQueue(double approvedAmount)
        {
            try
            {
                MessageQueue msgQueue;
                if (MessageQueue.Exists(@".\Private$\MyQueue"))
                {
                    msgQueue = new MessageQueue(@".\Private$\MyQueue");
                }
                else
                {
                    msgQueue = MessageQueue.Create(@".\Private$\MyQueue");
                }

                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = approvedAmount;
                msgQueue.Label = "approved amount";
                msgQueue.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ReceiveQueue()
        {
            try
            {
                var receiveQueue = new MessageQueue(@".\Private$\MyQueue");
                var receiveMsg = receiveQueue.Receive();
                receiveMsg.Formatter = new BinaryMessageFormatter();
                return receiveMsg.Body.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SendMail(string email)
        {
            try
            {
                string amount = this.ReceiveQueue();
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("cristianomessicrlm0730@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Congratulations!!!";
                mail.Body = $"Your Loan is Approved for ₹.{amount}";
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("cristianomessicrlm0730@gmail.com", "CristianoMessi0730");
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FormModel> GetFormDetails(int userId)
        {
            try
            {
                 List<FormModel> result = this.userContext.Form.Where(x => x.UserId == userId).ToList();
                 return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PropertyModel> GetPropertyDetails(int userId, int formId)
        {
            try
            {
                List<PropertyModel> result = this.userContext.Property.Where(x => x.UserId == userId && x.FormId==formId).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
