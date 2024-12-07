using Microsoft.AspNetCore.Identity;

namespace Bidro.Users.Persistence;

public static class UserManagerExtensions
{
    public static async Task<IdentityResult> CreateFirmAccountAsync(this UserManager<UserTypes.UserAccount> userManager, UserTypes.UserAccount userAccount, string password, UserTypes.FirmAccount firmAccount, IUsersDb usersDb)
    {
        var result = await userManager.CreateAsync(userAccount, password);
        if (!result.Succeeded) return result;

        firmAccount.UserAccountId = userAccount.Id;
        firmAccount.UserAccount = userAccount;
        await usersDb.AddFirmAccount(firmAccount);

        return IdentityResult.Success;
    }

    public static async Task<IdentityResult> CreateAdminAccountAsync(this UserManager<UserTypes.UserAccount> userManager, UserTypes.UserAccount userAccount, string password, UserTypes.AdminAccount adminAccount, IUsersDb usersDb)
    {
        var result = await userManager.CreateAsync(userAccount, password);
        if (!result.Succeeded) return result;

        adminAccount.UserAccountId = userAccount.Id;
        adminAccount.UserAccount = userAccount;
        await usersDb.AddAdminAccount(adminAccount);

        return IdentityResult.Success;
    }
}