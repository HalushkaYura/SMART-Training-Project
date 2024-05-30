using System.Threading.Tasks;

namespace Smart.Core.Interface.Services
{ 
    public interface ITemplateService
    {
        Task<string> GetTemplateHtmlAsStringAsync<T>(string viewName, T model) where T : class, new();
    }
}
