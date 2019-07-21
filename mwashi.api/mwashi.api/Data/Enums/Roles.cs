using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace mwashi.api.Data.Enums
{
    public enum Roles
    {
        [Description("Admin")]
        Admin,
        [Description("Customer")]
        Customer,
        [Description("Washer")]
        Washer
    }
}
