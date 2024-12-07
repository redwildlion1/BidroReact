using Microsoft.AspNetCore.Mvc;

namespace Bidro.Users.Persistence;

public interface IUsersDb
{
    public Task<IActionResult> AddFirmAccount(UserTypes.FirmAccount firmAccount);
    public Task<IActionResult> AddAdminAccount(UserTypes.AdminAccount adminAccount);
}