using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    public class BaseController<T> : ControllerBase where T: class
    {
        protected ObjectResult? _result;
    }
}