namespace WCFServices.Infrastructure
{
    using System.IdentityModel.Selectors;
    using System.IdentityModel.Tokens;

    public class UserPasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            return;

            //throw new SecurityTokenValidationException();
        }
    }
}
