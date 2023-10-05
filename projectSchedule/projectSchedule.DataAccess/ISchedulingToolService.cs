
using projectSchedule.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace projectSchedule.Service
{
    public interface ISchedulingToolService
    {
        Task<int> CreateAsync(SchedulingToolModel schedulingTool);
        Task<SchedulingToolModel> GetByIdAsync(int id);
        Task<List<SchedulingToolModel>> GetAllAsync();
        Task<bool> UpdateAsync(SchedulingToolModel schedulingTool);
        Task<bool> DeleteAsync(int id);
    }
}
