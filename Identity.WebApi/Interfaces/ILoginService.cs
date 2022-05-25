using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.WebApi.Interfaces
{
    public interface ILoginService
    {
        Models.Token Authenticate(string username, string password);
    }
}
