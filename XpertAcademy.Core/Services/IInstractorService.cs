using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Instractor;
using XpertAcademy.Core.DTOs.Pagination;

namespace XpertAcademy.Core.Services
{
    public interface IInstractorService
    {
        Task<InstractorToReturnDto> AddNewInstractor(CreateInstractorDto dto);
        Task<IReadOnlyList<InstractorToReturnDto>> GetAllInstractorsAsync(PaginationDto dto);
        Task<int> GetInstractorsCount();
        Task<InstractorToReturnDto> UpdateInstractorAsync(int instractorId, CreateInstractorDto dto);
        Task<bool> DeleteInstractorAsync(int instractorId);
    }
}
