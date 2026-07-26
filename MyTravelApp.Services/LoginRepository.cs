using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTravelApp;
using MyTravelApp.Data;
using MyTravelApp.Data.Models;

namespace MyTravelApp.Services
{
    public class LoginRepository
    {
        private readonly DAL _dal;
        public LoginRepository(DAL dal)
        {
            _dal = dal;
        }


        public bool LoginRepo(LoginUser userAuth)
        {

            var parameters = new
            {
                mail = userAuth.UserId,
                pass = userAuth.PasswordHash
            };
            var data = _dal.ExecuteSelectOneRowCommand<LoginUser>("USP_Login", parameters);
            if (data != null)
            {
                return true;
            }
            else return false;
        }

    }
}
