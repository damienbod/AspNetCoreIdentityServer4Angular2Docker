﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ResourceFileServer.Providers;

namespace ResourceFileServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FileExplorerController : Controller
    {
        private readonly ISecuredFileProvider _securedFileProvider;

        public FileExplorerController(ISecuredFileProvider securedFileProvider)
        {
            _securedFileProvider = securedFileProvider;
        }

        // TODO MUST FIX, user claims validation no longer work since identityserver4 rc4
        //[Authorize("securedFilesUser")]
        [HttpGet]
        public IActionResult Get()
        {
            //  TODO MUST FIX, rc4 update does not work, claims are missing
            var adminClaim = User.Claims.FirstOrDefault(x => x.Type == "role" && x.Value == "securedFiles.admin");
            var files = _securedFileProvider.GetFilesForUser(adminClaim != null);

            return Ok(files);
        }
    }
}
