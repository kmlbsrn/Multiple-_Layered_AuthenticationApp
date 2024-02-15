using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentacion.Data.Context
{
    public class AuthenticationAppContext:DbContext
    {
        public AuthenticationAppContext(DbContextOptions<AuthenticationAppContext> options):base(options)
        {

        }

        public DbSet<Entities.UserEntity> Users { get; set; }
    }
}
