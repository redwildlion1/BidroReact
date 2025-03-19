using Bidro.Types;
using Microsoft.AspNetCore.Identity;

namespace Bidro.Services.Implementations;

public static class UserExtensionsService
{
    public static async Task<IdentityResult> CreateFirmAccountAsync(this UserManager<UserTypes.UserAccount> userManager,
        UserTypes.UserAccount userAccount, string password, UserTypes.FirmAccount firmAccount,
        IUsersService usersService)
    {
        var result = await userManager.CreateAsync(userAccount, password);
        if (!result.Succeeded) return result;

        firmAccount.UserAccountId = userAccount.Id;
        firmAccount.UserAccount = userAccount;
        await usersService.AddFirmAccount(firmAccount);

        return IdentityResult.Success;
    }

    public static async Task<IdentityResult> CreateAdminAccountAsync(
        this UserManager<UserTypes.UserAccount> userManager, UserTypes.UserAccount userAccount, string password,
        UserTypes.AdminAccount adminAccount, IUsersService usersService)
    {
        var result = await userManager.CreateAsync(userAccount, password);
        if (!result.Succeeded) return result;

        adminAccount.UserAccountId = userAccount.Id;
        adminAccount.UserAccount = userAccount;
        await usersService.AddAdminAccount(adminAccount);

        return IdentityResult.Success;
    }
}