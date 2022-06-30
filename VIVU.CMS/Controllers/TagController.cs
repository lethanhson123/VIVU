using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService tagService;
        private readonly IAuthenticateHelper authenticateHelper;

        public TagController(ITagService tagService,
            IAuthenticateHelper authenticateHelper)
        {
            this.tagService = tagService;
            this.authenticateHelper = authenticateHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Modal(int id)
        {
            var model = new TagModel();

            if (id > 0)
            {
                var remoteResponseData = await tagService.Get(id);

                if (remoteResponseData.Data != null)
                {
                    model = remoteResponseData.Data;
                }
            }

            return PartialView(model);
        }

        public async Task<ActionResult> List(TagQueryModel query)
        {
            var remoteResponseData = await tagService.GetAll(query.Keywords);
            var model = remoteResponseData.Data;
            return PartialView(model);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var response = new CommonResponseModel<object>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();

            response = await tagService.Delete(id, token);
            return Json(response);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SaveChange(TagModel model)
        {
            var response = new CommonResponseModel<TagModel>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();

            if (model.Id == 0)
            {
                response = await tagService.Create(model, token);
            }
            else
            {
                response = await tagService.Update(model, token);
            }

            return Json(response);
        }
    }
}
