namespace Presentation.API.Handlers
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Threading.Tasks;

    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //LOG
        }
    }
}
