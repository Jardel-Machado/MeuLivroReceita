using System.Globalization;

namespace MyRecipeBook.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate next;

    public CultureMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var culture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        if (string.IsNullOrWhiteSpace(culture) || !supportedLanguages.Any(c => c.Name.Equals(culture)))
        {
            culture = "en";
        }

        var cultureInfo = new CultureInfo(culture);

        CultureInfo.CurrentCulture = cultureInfo;

        CultureInfo.CurrentUICulture = cultureInfo;          

        await next(context);
    }
}
