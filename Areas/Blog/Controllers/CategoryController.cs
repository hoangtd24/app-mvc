using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnetcoremvc.Models;
using aspnetcoremvc.Models.Blog;

namespace aspnetcoremvc.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("admin/blog/[action]")]
    public class CategoryController : Controller
    {
        private readonly AppMvcContext _context;

        public CategoryController(AppMvcContext context)
        {
            _context = context;
        }

        public void CreateSelectItem(List<Category> categories, List<Category> data, int level = 0){
            string prefix = String.Concat(Enumerable.Repeat("----", level));
            foreach(Category item in categories){
                item.Title = prefix + item.Title;
                data.Add(item);
                if(item.CategoryChildren.Count > 0){
                    CreateSelectItem(item.CategoryChildren.ToList(), data, level + 1);
                }
            }
        }
        // GET: Category
        public async Task<IActionResult> Index()
        {
            var appMvcContext = await _context.Categories
            .Include(c => c.ParentCategory)
            .Include(c => c.CategoryChildren)
            .ToListAsync();

            var result = (from c in appMvcContext where c.ParentCategoryId == null select c).ToList();
            return View(result);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public async Task<IActionResult> Create()
        {
            var appMvcContext = _context.Categories
            .Include(c => c.ParentCategory)
            .Include(c => c.CategoryChildren);

            var result = await (from c in appMvcContext where c.ParentCategoryId == null select c).ToListAsync();
            List<Category> data = new List<Category>();
            data.Insert(0, new Category()
            {
                Id = -1,
                Title = "Khong co danh muc cha"
            });
            CreateSelectItem(result,data,0);
            var selectList = new SelectList(data, "Id", "Title");
            ViewData["ParentCategoryId"] = selectList;
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Category category)
        {
            if(category.ParentCategoryId == -1) category.ParentCategoryId = null;
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

             ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Slug", category.ParentCategoryId);
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var appMvcContext = _context.Categories
            .Include(c => c.ParentCategory)
            .Include(c => c.CategoryChildren);

            var result = await (from c in appMvcContext where c.ParentCategoryId == null select c).ToListAsync();
            List<Category> data = new List<Category>();
            data.Insert(0, new Category()
            {
                Id = -1,
                Title = "Khong co danh muc cha"
            });
            CreateSelectItem(result,data,0);
            var selectList = new SelectList(data, "Id", "Title");
            ViewData["ParentCategoryId"] = selectList;
            //ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Slug", category.ParentCategoryId);
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Slug,ParentCategoryId")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(category.ParentCategoryId == -1) category.ParentCategoryId = null;
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Slug", category.ParentCategoryId);
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null){
                return NotFound();
            }

            var categories = _context.Categories.Include(c => c.CategoryChildren);

            var result = (from c in categories where c.Id == id select c).FirstOrDefault();


            foreach(var c in result.CategoryChildren){
                c.ParentCategoryId = category.ParentCategoryId;
            }
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
