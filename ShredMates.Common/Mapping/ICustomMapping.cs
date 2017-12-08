using System;
using System.Collections.Generic;
using System.Text;

namespace ShredMates.Common.Mapping
{
    public interface ICustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
