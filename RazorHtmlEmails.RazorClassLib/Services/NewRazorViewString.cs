using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Razor.Templating.Core;
namespace RazorHtmlEmails.RazorClassLib.Services
{
    public class NewRazorViewString : INewRazorViewString
    {
        public async Task<string> GetStringFromRazor<TModel>(string fileUrl, TModel model)
        {
            return await RazorTemplateEngine.RenderAsync(fileUrl, model);
        }
    }

    public interface INewRazorViewString
    {
        Task<string> GetStringFromRazor<TModel>(string fileUrl, TModel model);
    }
}
