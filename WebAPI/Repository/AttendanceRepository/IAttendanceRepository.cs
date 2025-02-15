using 這是扭蛋機系統;

namespace WebAPI.Repository.AttendanceRepository
{
    public interface IAttendanceRepository
    {
        public  Task<bool> ClockInAsync(int userId);
        public  Task<bool> ClockOutAsync(int userId);
        public Task<IEnumerable<AttendanceHistoryDto>> GetAttendanceByFilters(string name, DateTime? startDate, DateTime? endDate);
        public  Task<IEnumerable<AttendanceHistoryDto>> GetAttendanceHistoryAsync();
        public Task<Register> GetEmployeeByIdAsync(int userId);
    }
}
