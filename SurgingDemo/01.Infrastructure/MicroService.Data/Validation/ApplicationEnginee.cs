using MicroService.Data.Log;
using Surging.Core.ProxyGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Data.Validation
{
   public class ApplicationEnginee: ProxyServiceBase
    {
        public JsonResponse TryAction(Action action)
        {

            var jsonResponse = new JsonResponse();

            try
            {
                action();
            }
            catch (DomainException ex)
            {
                jsonResponse.Errors.AddErrors(ex.ValidationErrors.ErrorItems);
            }
            catch (Exception ex)
            {
                jsonResponse.Errors.AddSystemError();
                Error(ex);
            }

            return jsonResponse;
        }


        public async Task<JsonResponse> TryActionAsync(Func<Task> action)
        {

            var jsonResponse = new JsonResponse();

            try
            {
                await Task.Run(action);
            }
            catch (DomainException ex)
            {
                jsonResponse.Errors.AddErrors(ex.ValidationErrors.ErrorItems);
            }
            catch (Exception ex)
            {
                jsonResponse.Errors.AddSystemError();
                Error(ex);
            }

            return jsonResponse;
        }


        public async Task<JsonResponse> TryTransactionAsync(Func<Task> action)
        {

            var jsonResponse = new JsonResponse();

            try
            {
                using (var ctox = TransactionFactory.Required())
                {
                    await Task.Run(action).ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            throw task.Exception.InnerException;
                        }
                    });
                    ctox.Complete();
                }
            }
            catch (DomainException ex)
            {
                jsonResponse.Errors.AddErrors(ex.ValidationErrors.ErrorItems);
            }
            catch (AggregateException ex)
            {
                Error(ex);
            }
            catch (Exception ex)
            {
                Error(ex);
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                jsonResponse.Errors.AddSystemError("IsAlter", errorMessage);
                jsonResponse.SystemErrorMessage = errorMessage;

            }

            return jsonResponse;
        }

        protected TResponse TryAction<TResponse>(Action action) where TResponse : JsonResponse, new()
        {

            var jsonResponse = new TResponse();

            try
            {
                action();
            }
            catch (DomainException ex)
            {
                jsonResponse.Errors.AddErrors(ex.ValidationErrors.ErrorItems);
            }
            catch (Exception ex)
            {
                jsonResponse.Errors.AddSystemError();
                Error(ex);
            }

            return jsonResponse;
        }

        private void Error(Exception exception)
        {
           // Logger.GetLogger(LogContants.Repository, LogContants.Error);
           // Logger.Error(exception);
        }
    }
}
