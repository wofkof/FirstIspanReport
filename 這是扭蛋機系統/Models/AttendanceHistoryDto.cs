using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 這是扭蛋機系統
{
    public class AttendanceHistoryDto
    {
        public int RecordID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }
        public string ClockInStatus { get; set; }
        public string ClockOutStatus { get; set; }
    }
    public class EmployeeDto
    {
        public int UserID { get; set; }
        public string Name { get; set; }
    }
}
