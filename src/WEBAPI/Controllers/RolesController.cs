using App.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Controllers
{
    /// <summary>
    /// Role Managerment
    /// </summary>
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleManager"></param>
        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Produces(typeof(ApplicationRole))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(ApplicationRole))]
        public IActionResult GetAll()
        {
            return Ok(_roleManager.Roles);
        }

        /// <summary>
        /// Get Role by name
        /// </summary>
        /// <param name="name">Role Name</param>
        /// <returns>Application Role</returns>
        [HttpGet("name/{name}")]
        [Authorize]
        [Produces(typeof(ApplicationRole))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(ApplicationRole))]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return Ok(await _roleManager.FindByNameAsync(name));
            }
            return NoContent();
        }

        /// <summary>
        /// Get Role By ID
        /// </summary>
        /// <param name="id">RoleId</param>
        /// <returns>ApplicationRole</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        [Produces(typeof(ApplicationRole))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(ApplicationRole))]
        public async Task<IActionResult> GetRoleById(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    return Ok(await _roleManager.FindByIdAsync(id));
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        /// <summary>
        /// Create a role
        /// </summary>
        /// <param name="role">Application Role</param>
        /// <returns>Role</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Produces(typeof(ApplicationRole))]
        [SwaggerResponse(System.Net.HttpStatusCode.Created, Type = typeof(ApplicationRole))]
        public async Task<IActionResult> Post(ApplicationRole role)
        {
            try
            {
                if (role == null)
                {
                    return BadRequest();
                }

                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return CreatedAtRoute("Roles", role);
                }
                else
                {
                    return Forbid();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
