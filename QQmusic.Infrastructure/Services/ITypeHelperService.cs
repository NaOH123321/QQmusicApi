using System;
using System.Collections.Generic;
using System.Text;

namespace QQmusic.Infrastructure.Services
{
    public interface ITypeHelperService
    {
        bool TypeHasProperties<T>(string fields);
    }
}
