using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentacion.Data.Entities;
using Authentacion.Data.Repositories;
using Authentication.Business.Dtos;
using Authentication.Business.Services;
using Authentication.Business.Types;
using Microsoft.AspNetCore.DataProtection;

namespace Authentication.Business.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IDataProtector _dataProtector;


        public UserManager(IUserRepository userRepository,IDataProtectionProvider dataProtectionProvider)
        {
            _userRepository = userRepository;
            _dataProtector = dataProtectionProvider.CreateProtector("security");
        }


        public ServiceMessage AddUser(UserAddDto userAddDto)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == userAddDto.Email.ToLower()).ToList();

            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu mail adresi kullanılmaktadır."
                };
            }


            var entity= new UserEntity
            {
                Email = userAddDto.Email,
                Password = _dataProtector.Protect(userAddDto.Password),
                FirstName = userAddDto.FirstName,
                LastName = userAddDto.LastName
            };

            _userRepository.AddUser(entity);

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Kullanıcı başarıyla eklendi."
            };
        }

        public UserInfoDto SignInUser(SignInDto signInDto)
        {
            var userEntity= _userRepository.Get(x => x.Email.ToLower() == signInDto.Email.ToLower());

            if (userEntity is null)
            {
                return null;
            }


            var rawPassword= _dataProtector.Unprotect(userEntity.Password);

            if (rawPassword == signInDto.Password)
            {
                return new UserInfoDto
                {
                    Id = userEntity.Id,
                    Email = userEntity.Email,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName
                };
            }
            else
            {
                return null;
            }
        }
    }
}
