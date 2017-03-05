using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class MaxLengthAttribute : System.ComponentModel.DataAnnotations.MaxLengthAttribute
    {
        public MaxLengthAttribute(int length)
            : base(length)
        {

        }
    }
}
