using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.DAL.Security
{
    public interface ITokenFactory
    {
        string Token { get; }
    }
}
