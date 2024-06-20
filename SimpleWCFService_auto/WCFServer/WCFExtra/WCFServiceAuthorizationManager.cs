using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer.WCFExtra
{
    public class WCFServiceAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string classMethod = operationContext.RequestContext.RequestMessage.Headers.Action;
            var operationName = operationContext.RequestContext.RequestMessage.Properties["HttpOperationName"].ToString();

            if (operationName == nameof(IMZWCFService.FileList))
            {
                throw new InvalidOperationException("Unauthorized access to resource detected."); // because someone is simply updating a client service reference
            }

            Console.WriteLine("Class Method Call: {0}", classMethod);
            // do something with operationContext here as you need to inspect stuff
            // return true if you want this class method call to succeed and go through
            // return false if you want this class method to fail on the client
            return true;
        }

        public override bool CheckAccess(OperationContext operationContext, ref Message message)
        {
            return base.CheckAccess(operationContext, ref message);
        }
    }
}
