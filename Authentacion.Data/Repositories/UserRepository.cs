using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Authentacion.Data.Context;
using Authentacion.Data.Entities;

namespace Authentacion.Data.Repositories
{
    public class UserRepository:IUserRepository
    {

        private readonly AuthenticationAppContext _context;

        public UserRepository(AuthenticationAppContext context)
        {
            _context = context;
        }
        public void AddUser(UserEntity userEntity)
        {
            userEntity.CreatedDate=DateTime.Now;
            _context.Users.Add(userEntity);
            _context.SaveChanges();
            
        }

        public IQueryable<UserEntity> GetAll(Expression<Func<UserEntity, bool>> predicate = null)
        {
            return predicate is not null ? _context.Users.Where(predicate) : _context.Users;
        }

        public UserEntity Get(Expression<Func<UserEntity, bool>> predicate)
        {
            return _context.Users.FirstOrDefault(predicate);
        }
    }
}
