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
using System.Collections.Generic;

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

        //// post: list/create
        //[httppost]
        //[validateantiforgerytoken]
        //public async task<iactionresult> create(string title, string email)
        //{
        //    if (modelstate.isvalid)
        //    {
        //        var userid = _usermanager.getuserid(user);

        //        var list = new list
        //        {
        //            title = title
        //        };

        //        _dbcontext.lists.add(list);
        //        await _dbcontext.savechangesasync();

        //        if (!string.isnullorwhitespace(email))
        //        {
        //            var user = await _usermanager.findbyemailasync(email);
        //            if (user != null)
        //            {
        //                var userlist = new userlist
        //                {
        //                    listid = list.id,
        //                    userid = user.id
        //                };

        //                _dbcontext.userlists.add(userlist);
        //                await _dbcontext.savechangesasync();
        //            }
        //        }

        //        return redirecttoaction("index");
        //    }

        //    return view();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string title, string email)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                var list = new Note
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

                // Grant access to the creator
                var creatorList = new UserList
                {
                    ListId = list.Id,
                    UserId = userId
                };

                _dbContext.UserLists.Add(creatorList);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }


        // GET: List/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var list = await _dbContext.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            // Ensure that the current user has the necessary rights to edit the list
            var userId = _userManager.GetUserId(User);
            var userList = await _dbContext.UserLists.FirstOrDefaultAsync(ul => ul.ListId == id && ul.UserId == userId);
            if (userList == null)
            {
                return Forbid();
            }

            var viewModel = new EditListViewModel
            {
                Id = list.Id,
                Title = list.Title
            };

            return View(viewModel);
        }

        // POST: List/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Ensure that the current user has the necessary rights to edit the list
                var userId = _userManager.GetUserId(User);
                var userList = await _dbContext.UserLists.FirstOrDefaultAsync(ul => ul.ListId == viewModel.Id && ul.UserId == userId);
                if (userList == null)
                {
                    return Forbid();
                }

                var list = await _dbContext.Lists.FindAsync(viewModel.Id);
                if (list == null)
                {
                    return NotFound();
                }

                list.Title = viewModel.Title;

                _dbContext.Update(list);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }







        private bool ListExists(int id)
        {
            return _dbContext.Lists.Any(e => e.Id == id);
        }


        // GET: List/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var list = await _dbContext.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            // Ensure that the current user has the necessary rights to delete the list
            var userId = _userManager.GetUserId(User);
            var userList = await _dbContext.UserLists.FirstOrDefaultAsync(ul => ul.ListId == id && ul.UserId == userId);
            if (userList == null)
            {
                return Forbid();
            }

            return View(list);
        }

        // POST: List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var list = await _dbContext.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            // Ensure that the current user has the necessary rights to delete the list
            var userId = _userManager.GetUserId(User);
            var userList = await _dbContext.UserLists.FirstOrDefaultAsync(ul => ul.ListId == id && ul.UserId == userId);
            if (userList == null)
            {
                return Forbid();
            }

            _dbContext.Lists.Remove(list);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
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
