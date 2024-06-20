using System;
using System.ServiceModel.Dispatcher;

namespace WCFServer.WCFExtra
{
    public class LoggingOperationInvoker : IOperationInvoker
    {
        IOperationInvoker _baseInvoker;
        string _operationName;

        public LoggingOperationInvoker(IOperationInvoker baseInvoker, DispatchOperation operation)
        {
            _baseInvoker = baseInvoker;
            _operationName = operation.Name;
        }

        public bool IsSynchronous
        {
            get
            {
                return _baseInvoker.IsSynchronous;
            }
        }

        public object[] AllocateInputs()
        {
            return _baseInvoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            //Call start...
            OnNewLog?.Invoke($"{DateTime.Now:HH:mm:ss} - Call -> {_operationName}");

            object result = null;
            try
            {
                result = _baseInvoker.Invoke(instance, inputs, out outputs);
            }
            catch (Exception ex)
            {
                //Call error...
                OnNewLog?.Invoke($"{DateTime.Now:HH:mm:ss} - Error in {_operationName}- {ex.Message}");
                outputs = null;
                result = ex.Message;
            }

            //Call end...
            //OnNewLog?.Invoke($"{DateTime.Now:HH:mm:ss} - Call end to {_operationName}");

            return result;
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            return _baseInvoker.InvokeBegin(instance, inputs, callback, state);
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            return _baseInvoker.InvokeEnd(instance, out outputs, result);
        }

        public delegate void OnNewLogEventHandler(string message);
        public static event OnNewLogEventHandler OnNewLog;
    }

}
