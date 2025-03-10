using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.Enums
{
    public enum ReviewType
    {
        [EnumMember(Value = "عام")]
        General,
        [EnumMember(Value = "كورس")]
        Course
    }
}
