using FinalProjectConsume.ViewModels.UI;

namespace FinalProjectConsume.Services.Interfaces
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingVM>> GetAllAsync();
    }
}
