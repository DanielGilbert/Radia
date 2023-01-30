namespace Radia.Services
{
    public class FooterService : IFooterService
    {
        private readonly IConfigurationService configurationService;
        private readonly IDateTimeService dateTimeService;
        private readonly IVersionService versionService;

        public FooterService(IConfigurationService configurationService,
                             IDateTimeService dateTimeService,
                             IVersionService versionService)
        {
            this.configurationService = configurationService;
            this.dateTimeService = dateTimeService;
            this.versionService = versionService;
        }

        public string GetFormattedFooter()
        {
            string year = dateTimeService.UtcNow().Year.ToString();

            string formattedFooter = configurationService.GetFooterCopyright().Replace("{{CurrentYear}}", year);
            formattedFooter = $"{formattedFooter} | {this.versionService.GetVersionLinked()} | Made with ❤ &amp; ☕ using <a href=\"https://github.com/DanielGilbert/Radia\">Radia</a> "; 
            return formattedFooter;
        }
    }
}
