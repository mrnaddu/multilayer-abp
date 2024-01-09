using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Localization;
using Volo.Abp.OpenIddict.Applications;

namespace IVP.AuthServer.Pages;

public class IndexModel: AbpPageModel
{
    public List<OpenIddictApplication> Applications { get; protected set; }

    public IReadOnlyList<LanguageInfo> Languages { get; protected set; }

    public string CurrentLanguage { get; protected set; }

    protected IOpenIddictApplicationRepository OpenIdApplicationRepository { get; }

    protected ILanguageProvider LanguageProvider { get; }

    public IndexModel(
        IOpenIddictApplicationRepository openIdApplicationRepository,
        ILanguageProvider languageProvider)
    {
        OpenIdApplicationRepository = openIdApplicationRepository;
        LanguageProvider = languageProvider;
    }

    public ActionResult OnGetAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return Redirect("~/Account/Login");
        }
        else
        {
            Applications = OpenIdApplicationRepository.GetListAsync().Result;

            Languages = LanguageProvider.GetLanguagesAsync().Result;
            CurrentLanguage = CultureInfo.CurrentCulture.DisplayName;
            return Page();
        }       
    }
}
