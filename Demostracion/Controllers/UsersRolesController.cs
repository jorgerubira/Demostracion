using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demostracion.Models;

namespace Demostracion.Controllers
{
    public class UsersRolesController : Controller
    {
        private readonly Contexto _context;

        public UsersRolesController(Contexto context)
        {
            _context = context;
        }

        // GET: UsersRoles
        public async Task<IActionResult> Index()
        {
            var contexto = _context.UsersRoles.Include(u => u.Users);
            return View(await contexto.ToListAsync());
        }

        // GET: UsersRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersRoles = await _context.UsersRoles
                .Include(u => u.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usersRoles == null)
            {
                return NotFound();
            }

            return View(usersRoles);
        }

        // GET: UsersRoles/Create
        public IActionResult Create()
        {
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UsersRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Rol,UsersId")] UsersRoles usersRoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersRoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", usersRoles.UsersId);
            return View(usersRoles);
        }

        // GET: UsersRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersRoles = await _context.UsersRoles.FindAsync(id);
            if (usersRoles == null)
            {
                return NotFound();
            }
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", usersRoles.UsersId);
            return View(usersRoles);
        }

        // POST: UsersRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Rol,UsersId")] UsersRoles usersRoles)
        {
            if (id != usersRoles.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersRoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersRolesExists(usersRoles.ID))
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
            ViewData["UsersId"] = new SelectList(_context.Users, "Id", "Id", usersRoles.UsersId);
            return View(usersRoles);
        }

        // GET: UsersRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersRoles = await _context.UsersRoles
                .Include(u => u.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (usersRoles == null)
            {
                return NotFound();
            }

            return View(usersRoles);
        }

        // POST: UsersRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersRoles = await _context.UsersRoles.FindAsync(id);
            _context.UsersRoles.Remove(usersRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersRolesExists(int id)
        {
            return _context.UsersRoles.Any(e => e.ID == id);
        }
    }
}
