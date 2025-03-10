using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.DTOs.Workshop;

namespace XpertAcademy.Core.Services
{
    public interface IWorkshopService
    {
        Task<WorkshopToReturnDto> AddNewWorkshopAsync(CreateWorkshopDto dto);
        Task<IReadOnlyList<WorkshopToReturnDto>> GetAllWorkshopsAsync(PaginationDto dto);
        Task<int> GetAllWorkshopsCount();
        Task<WorkshopToReturnDto> GetWorkshopDetailsAsync(int workshopId);

        Task<bool> DeleteWorkshopAsync(int workshopId);
        Task<WorkshopToReturnDto> EditWorkshopAsync(int workshopId,CreateWorkshopDto dto);
    }
}
