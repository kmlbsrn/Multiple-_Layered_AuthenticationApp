using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Business.Dtos;
using Authentication.Business.Types;

namespace Authentication.Business.Services
{
    public interface IUserService
    {
        ServiceMessage AddUser(UserAddDto userAddDto);

        UserInfoDto SignInUser(SignInDto signInDto);
    }
}
