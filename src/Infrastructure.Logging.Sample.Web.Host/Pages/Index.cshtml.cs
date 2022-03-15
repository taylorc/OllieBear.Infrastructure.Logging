using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Infrastructure.Logging.Sample.Web.Host.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILog _logger;

        public IndexModel(ILog logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.Info("Message Test Inherited Logger");
        }
    }
}