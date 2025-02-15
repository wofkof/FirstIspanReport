using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統.Services.AttendanceService
{
    internal interface IAttendanceService
    {
        Task<List<AttendanceHistoryDto>> GetAttendanceRecordsAsync();
        Task<bool> ClockInAsync(int userId);
        Task<bool> ClockOutAsync(int userId);
        Task<EmployeeDto> GetEmployeeInfoAsync(int userId);
    }
}
