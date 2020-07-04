using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebHome.Domain.Models;
using Message = WebHome.Domain.Models.Message;

namespace WebHome.Domain.IServices
{
    public interface IMessages
    {
        public List<Message> Get_Message_Apartment(ReceiverMessage receiver,string type);
    }
}
