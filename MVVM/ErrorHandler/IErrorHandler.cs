using System;

namespace MVVM.ErrorHandler
{
    public interface IErrorHandler
    {
        public void HandleError(Exception ex);

        public void HandleError(Exception ex, string message);
    }
}
