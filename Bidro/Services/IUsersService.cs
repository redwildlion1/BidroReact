using Bidro.Types;
using Microsoft.AspNetCore.Mvc;

namespace Bidro.Services;

public interface IUsersService
{
    public Task<IActionResult> AddFirmAccount(UserTypes.FirmAccount firmAccount);
    public Task<IActionResult> AddAdminAccount(UserTypes.AdminAccount adminAccount);
}