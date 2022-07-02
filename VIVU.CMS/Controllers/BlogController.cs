using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VIVU.CMS.Models;

namespace VIVU.CMS.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IAuthenticateHelper authenticateHelper;

        public BlogController(IBlogService blogService,
            IAuthenticateHelper authenticateHelper)
        {
            this.blogService = blogService;
            this.authenticateHelper = authenticateHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Detail(int Id)
        {
            var model = new BlogDetailModel()
            {
                PostDate = DateTime.Now
            };

            if (Id > 0)
            {
                var remoteResponseData = await blogService.Get(Id);

                if (remoteResponseData.Data != null)
                {
                    model = remoteResponseData.Data;
                }
            }

            return View(model);
        }

        public async Task<ActionResult> List(BlogQueryModel query)

        {
            query.Keywords = query.Keywords ?? string.Empty;
            query.PageIndex = query.PageIndex > 1 ? query.PageIndex : 1;
            query.PageSize = query.PageSize > 0 ? query.PageSize : 1000;
            var remoteResponseData = await blogService.GetWithQuery(query);
            var model = remoteResponseData.Data;
            return PartialView(model);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SaveChange(BlogDetailModel model)
        {
            var response = new CommonResponseModel<BlogModel>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();

            if (model.Id == 0)
            {
                response = await blogService.Create(model, token);
            }
            else
            {
                response = await blogService.Update(model, token);
            }

            return Json(response);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var response = new CommonResponseModel<object>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();

            response = await blogService.Delete(id, token);
            return Json(response);
        }
    }
}