namespace Presentation.API.Helpers
{
    using BLL.Services.Interfaces;
    using IdentityModel;
    using IdentityServer4.Models;
    using IdentityServer4.Validation;
    using Models.Domain;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IUserService _userService;

        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                var user = this._userService.GetByEmail(context.UserName);
                if (user == null)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                    return;
                }
                if (user.Password == context.Password)
                {
                    //set the result
                    context.Result = new GrantValidationResult(
                        subject: user.Id.ToString(),
                        authenticationMethod: "custom",
                        claims: GetUserClaims(user));

                    return;
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
            }
            catch
            {
                throw;
            }
        }
        public static Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
            new Claim("user_id", user.Id.ToString()),
            new Claim(JwtClaimTypes.Name, user.Name),
            new Claim(JwtClaimTypes.Email, user.Email),

            //roles
            new Claim(JwtClaimTypes.Role, user.Role.ToString())
            };
        }
    }
}
