using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared_List.Data;
using Shared_List.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Humanizer;
using static Humanizer.In;
using System.Numerics;
using System;
using Microsoft.EntityFrameworkCore;

namespace Shared_List.Controllers
{
    public class ListController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public ListController(UserManager<IdentityUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }


        // GET: ListController
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var userLists = await _dbContext.UserLists
                .Include(ul => ul.List)
                .Where(ul => ul.UserId == userId)
                .ToListAsync();

            return View(userLists);
        }
        
        // GET: ListController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: List/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string email)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                var list = new List
                {
                    Title = title
                };

                _dbContext.Lists.Add(list);
                await _dbContext.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(email))
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user != null)
                    {
                        var userList = new UserList
                        {
                            ListId = list.Id,
                            UserId = user.Id
                        };

                        _dbContext.UserLists.Add(userList);
                        await _dbContext.SaveChangesAsync();
                    }
                }

                return RedirectToAction("Index");
            }

            return View();
        }


        // GET: ListController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Share(int listId)
        {
            var userId = _userManager.GetUserId(User);

            var userList = new UserList
            {
                ListId = listId,
                UserId = userId
            };

            using (var db = _dbContext)
            {
                db.UserLists.Add(userList);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
       
    }
}
