namespace Presentation.API.Helpers
{
    using BLL.Services.Interfaces;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProfileService : IProfileService
    {
        private IUserService _userService;

        public ProfileService(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    //get user from db (in my case this is by email)
                    var user = this._userService.GetByEmail(context.Subject.Identity.Name);

                    if (user != null)
                    {
                        var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                        //set issued claims to return
                        context.IssuedClaims.AddRange(claims.ToList());
                    }
                }
                else
                {
                    //get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
                    //where and subject was set to my user id.
                    var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value))
                    {
                        //get user from db (find user by user id)
                        var user = this._userService.Get(userId.Value);

                        // issue the claims for the user
                        if (user != null)
                        {
                            var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                            context.IssuedClaims.AddRange(claims.ToList());
                        }
                    }
                }
            }
            catch
            {
                //log your error
                throw;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value))
                {
                    var user = this._userService.Get(userId.Value);

                    if (user != null)
                    {
                        context.IsActive = user.IsActive;
                    }
                }
            }
            catch
            {
                //handle error logging
                throw;
            }
        }
    }
}
