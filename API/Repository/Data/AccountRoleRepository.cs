using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository
    {
        private readonly MyContext context;

        public AccountRoleRepository(MyContext myContext)
        {
            this.context = myContext;
        }

        public int SignManager(SignManagerVM signManagerVM)
        {
            var sign = (from a in context.AccountRole
                        where a.AccountId == signManagerVM.NIK && a.RoleId == "2"
                        select a).FirstOrDefault();

            if (sign == null)
            {
                AccountRole accountRole = new AccountRole();
                accountRole.AccountId = signManagerVM.NIK;
                accountRole.RoleId = "2";
                context.AccountRole.Add(accountRole);
                context.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
