using Microsoft.AspNetCore.Http;

namespace PAY_UP.Common.Helpers
{
    public class NotificationHelper
    {
        public static string EmailHtmlStringTemplate(string fullName, string routePath, Dictionary<string, string> queryParams, string templateFilename, HttpContext context)
        {
            //get the base address
            var baseUrl = UrlHelper.BaseAddress(context);

            //get the email link address
            var link = UrlHelper.GetEmailLink(queryParams, routePath, context);
            var directory = Directory.GetCurrentDirectory();
            var dr = directory.Substring(0, directory.Length - 10);

            //Read from the template file and construct the email template
            var path = dr + "PAY-UP.Common/Templates/";
            var templatePath = string.Join("", path, templateFilename);
            var htmlContent = File.ReadAllText(templatePath);
            htmlContent = htmlContent.Replace("[name]", fullName);
            htmlContent = htmlContent.Replace("[baseAddress]", baseUrl);
            htmlContent = htmlContent.Replace("[link]", link);

            return htmlContent;
        }
    }
}
