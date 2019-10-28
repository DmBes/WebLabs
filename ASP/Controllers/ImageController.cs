using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.DAL.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ASP.Controllers
{
    public class ImageController : Controller
    {
        UserManager<ApplicationUser> _userManager; 
        IHostingEnvironment _env;

        public ImageController(UserManager<ApplicationUser> mngr, IHostingEnvironment env)
        { _userManager = mngr; 
            _env = env; }

        public async Task<IActionResult> GetAvatar() 
        { var user = await _userManager.GetUserAsync(User);
            if (user.AvatarImage != null) 
                return File(user.AvatarImage, user.ImageMimeType);
            else { var avatarPath = "/Images/anonymous.png";
                var extProvider = new FileExtensionContentTypeProvider();
                var mimeType = extProvider.Mappings[".png"];
                return File(_env.WebRootFileProvider.GetFileInfo(avatarPath).CreateReadStream(), mimeType); } }



        //public IActionResult Index()
        //{
        //    return View();
        //}



    }
}