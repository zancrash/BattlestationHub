// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BattlestationHub.Data;
using BattlestationHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BattlestationHub.Areas.Identity.Pages.Account.Manage
{
    public class FavouritesModel : PageModel
    {
        private readonly UserManager<Models.ApplicationUser> _userManager;
        private readonly SignInManager<Models.ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public FavouritesModel(
            UserManager<Models.ApplicationUser> userManager,
            SignInManager<Models.ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }

        public List<Setup> UserFavourites { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // Fetch setups associated with the current user using the user's ID
            UserFavourites = await _context.Favourites
                .Where(f => f.UserId == user.Id)
                .Select(f => f.Setup)
                .ToListAsync();

            /*await LoadAsync(user);*/
            return Page();
        }

    }
}
