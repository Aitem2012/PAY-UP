using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PAY_UP.Common.Config;

namespace PAY_UP.Common.Helpers
{
    public class NotificationHelper
    {
        private readonly IOptions<WebAppConfig> _options;
        public NotificationHelper(IOptions<WebAppConfig> options)
        {
            _options = options;
        }

        public string EmailHtmlStringTemplate(string fullName, string routePath, Dictionary<string, string> queryParams, string templateFilename, HttpContext context)
        {
            //get the base address
            var baseUrl = UrlHelper.BaseAddress(context);

            //get the email link address
            var link = UrlHelper.GetEmailLink(queryParams, routePath, context, _options.Value.BaseUrl);
            var directory = Directory.GetCurrentDirectory();

            //Read from the template file and construct the email template
            var path = $"{directory.Substring(0, directory.Length - 10)}PAY-UP.Common/Templates/";
            var templatePath = string.Join("", path, templateFilename);
            var htmlContent = File.ReadAllText(templatePath);
            htmlContent = htmlContent.Replace("[name]", fullName);
            htmlContent = htmlContent.Replace("[baseAddress]", _options.Value.BaseUrl);
            htmlContent = htmlContent.Replace("[link]", link);

            return htmlContent;
        }
    }
}
