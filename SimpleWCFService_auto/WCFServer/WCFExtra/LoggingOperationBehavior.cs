using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFServer.WCFExtra
{
    public class LoggingOperationBehavior : IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            //throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.Invoker = new LoggingOperationInvoker(dispatchOperation.Invoker, dispatchOperation);
        }

        public void Validate(OperationDescription operationDescription)
        {
            //throw new NotImplementedException();
        }

        // (TODO stub implementations)
    }

}
