using WordUp.Service.Contracts.Settings;

namespace WordUp.Service.Settings
{
    public interface ISettingsService
    {
        SettingsDto GetModel();

        void Update(SettingsDto model);
    }
}