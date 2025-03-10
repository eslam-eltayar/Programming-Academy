using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.Enums
{
    public enum CourseStatus
    {
        [EnumMember(Value = "متاح")]
        Active,
        [EnumMember(Value ="غير متاح")]
        Inactive
    }
}
