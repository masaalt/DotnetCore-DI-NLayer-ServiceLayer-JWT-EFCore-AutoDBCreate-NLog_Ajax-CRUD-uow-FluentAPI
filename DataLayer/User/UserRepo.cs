using DataLayer.Repository;
using DB.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.User
{
    public class UserRepo : GenericRepository<Users>, IUserRepo
    {
        private readonly List<UserDTO> users = new List<UserDTO>();
        private IConfiguration configuration;
        public UserRepo(MVCEFCoreContext context, IConfiguration iConfig) : base(context)
        {
            configuration = iConfig;
            //string dbConn = iConfig.GetSection("ConfigOfUsers").GetSection("AdminName").Value;

            users.Add(new UserDTO
            {
                UserName = configuration.GetSection("ConfigOfUsers").GetSection("AdminName").Value,
                Password = configuration.GetSection("ConfigOfUsers").GetSection("AdminPassword").Value,
                Role = configuration.GetSection("ConfigOfUsers").GetSection("AdminRole").Value
            });
            users.Add(new UserDTO
            {
                UserName = configuration.GetSection("ConfigOfUsers").GetSection("Admin2Name").Value,
                Password = configuration.GetSection("ConfigOfUsers").GetSection("Admin2Password").Value,
                Role = configuration.GetSection("ConfigOfUsers").GetSection("Admin2Role").Value
            });
            users.Add(new UserDTO
            {
                UserName = configuration.GetSection("ConfigOfUsers").GetSection("EngineerName").Value,
                Password = configuration.GetSection("ConfigOfUsers").GetSection("EngineerPassword").Value,
                Role = configuration.GetSection("ConfigOfUsers").GetSection("EngineerRole").Value
            });

        }

        public UserDTO GetUser(Users user)
        {
            return users.Where(x => x.UserName.ToLower() == user.UserName.ToLower()
                && x.Password == user.Password).FirstOrDefault();
        }
    }
}
