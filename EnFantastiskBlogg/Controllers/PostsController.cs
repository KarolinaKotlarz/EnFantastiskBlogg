#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnFantastiskBlogg.Data;
using EnFantastiskBlogg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EnFantastiskBlogg.Models.ViewModels;

namespace EnFantastiskBlogg.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.ToListAsync());
        }

        // GET: Posts/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.Include(x => x.Comments).Include(x => x.User)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            PostDetailsViewModel p = new PostDetailsViewModel
            {
                PostId = post.PostId,
                Post = post,
                Comment = new CreateCommentViewModel()
                {
                    PostId = post.PostId
                }
            };

            return View(p);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,CreatedDate,UpdatedDate,Body,UserId")] Post post)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                post.UserId = user.Id;
                post.CreatedDate = DateTime.UtcNow;
                post.UpdatedDate = DateTime.UtcNow;
                _context.Add(post);

                user.PostCount++;

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Posts", new { id = post.PostId });
            }
            return RedirectToAction("Create");
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (post == null)
            {
                return NotFound();
            }

            if(user.Id != post.UserId)
            {
                return Forbid();
            }

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Body")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (true)
            {
                var _post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);

                _post.Title = post.Title;
                _post.UpdatedDate = DateTime.Now;
                _post.Body = post.Body;

                try
                {
                    _context.Update(_post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", new { id = post.PostId });
            }
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }

        public async Task<IActionResult> AddPost(Post post)
        {
            var user = await _userManager.GetUserAsync(User);

            Post p = new Post
            {
                Title = post.Title ?? "",
                Body = post.Body ?? "",
                User = user,
                UserId = user.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.Posts.Add(p);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = p.PostId });
        }
        public async Task<IActionResult> AddComment(Comment comment)
        {
            var user = await _userManager.GetUserAsync(User);
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == comment.PostId);

            Comment c = new Comment
            {
                Title = comment.Title ?? "",
                Body = comment.Body ?? "",
                Post = post,
                PostId = post.PostId,
                User = user,
                UserId = user.Id,
                CreatedDate = DateTime.Now
            };

            if(post.Comments == null)
            {
                post.Comments = new List<Comment>();
                post.Comments.Add(c);
            }
            else
            {
                post.Comments.Add(c);
            }

            _context.Update(post);
            _context.Comments.Add(c);
            
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
