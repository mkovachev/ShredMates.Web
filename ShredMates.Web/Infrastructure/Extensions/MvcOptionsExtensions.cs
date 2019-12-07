using Microsoft.AspNetCore.Mvc;

namespace ShredMates.Web.Infrastructure.Extensions
{
    public static class MvcOptionsExtensions
    {
        public static MvcOptions AddAutoValidateAntiforgeryToken(this MvcOptions options)
        {
            options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            return options;
        }
    }
}
