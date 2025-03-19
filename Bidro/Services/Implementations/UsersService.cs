using Bidro.Config;
using Bidro.Types;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services.Implementations;

public class UsersService(EntityDbContext db) : IUsersService
{
    public async Task<IActionResult> AddFirmAccount(UserTypes.FirmAccount firmAccount)
    {
        await db.FirmAccounts.AddAsync(firmAccount);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> AddAdminAccount(UserTypes.AdminAccount adminAccount)
    {
        await db.AdminAccounts.AddAsync(adminAccount);
        await db.SaveChangesAsync();
        return new OkResult();
    }
}