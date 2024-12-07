using Bidro.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bidro.Users.Persistence;

public class UsersDb(DbContextOptions<EntityDbContext> options) : IUsersDb
{
    public async Task<IActionResult> AddFirmAccount(UserTypes.FirmAccount firmAccount)
    {
        await using var db = new EntityDbContext(options);
        await db.FirmAccounts.AddAsync(firmAccount);
        await db.SaveChangesAsync();
        return new OkResult();
    }

    public async Task<IActionResult> AddAdminAccount(UserTypes.AdminAccount adminAccount)
    {
        await using var db = new EntityDbContext(options);
        await db.AdminAccounts.AddAsync(adminAccount);
        await db.SaveChangesAsync();
        return new OkResult();
    }
}