namespace RussianBathHouse.Areas.Administrator.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;
    [Area(AdminRole)]
    [Authorize(Roles = AdminRole)]
    public abstract class AdministratorController : Controller
    {
    }
}
