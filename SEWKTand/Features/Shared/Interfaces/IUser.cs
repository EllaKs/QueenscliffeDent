using SEWKTand.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEWKTand.Features.Shared.Interfaces
{
    public interface IUser
    {
         Role Role { get; set; }
         string HashedPassword { get; set; }
        //Hash
        //Salt
    }
}
