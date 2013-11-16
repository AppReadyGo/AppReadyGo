using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Common.Mails
{
    class PublishEmailRecipients
    {
        public String[] recipients { get; protected set; }

        public PublishEmailRecipients()
        {
            recipients = new String[] {"philip.belder@appreadygo.com" };
           
        }
      
    }
}
