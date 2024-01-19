using MVVM.ErrorHandler;
using System;
using System.Threading.Tasks;

namespace MVVM.AsyncCommand
{
    public static class TaskUtilities
    {
        public static async Task FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}
