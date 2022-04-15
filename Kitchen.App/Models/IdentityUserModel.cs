using Microsoft.AspNetCore.Identity;

namespace Kitchen.App.Models
{
    public class IdentityUserModel : IdentityUser
    {
        public override string ToString()
        {
            return "[\n\t{\n\t\tID: " + this.Id + "\n\t\tEmail: " + this.Email + ";\n\t\tUserName: " + this.UserName + ";\n\t}\n]";
        }
    }
}
