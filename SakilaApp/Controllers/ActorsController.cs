using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SakilaApp.Data;
using SakilaApp.Models;

using Microsoft.AspNetCore.Authorization;

namespace SakilaApp.Controllers
{
    [Authorize]
    public class ActorsController : Controller
    {
        private readonly SakilaContext _context;

        public ActorsController(SakilaContext context)
        {
            _context = context;
        }



        // GET: Actors
        public async Task<IActionResult> Index()
        {
            var actores = await _context.Actors
    .OrderBy(a => a.LastName)
    .Skip(1)
    .Take(5)
    .ToListAsync();
            
            
            return View(actores);
            //return View(await _context.Actors.ToListAsync());
        }

/*
---

### Ejercicio 11 – 5 actores siguientes después del primero, ordenados por apellido
**Controlador:** `ActorsController.cs`

```csharp
var actores = await _context.Actors
    .OrderBy(a => a.LastName)
    .Skip(1)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT * FROM actor
ORDER BY last_name
OFFSET 1
LIMIT 5;
```

---

### Ejercicio 12 – Nombre del actor empieza o termina con N
**Controlador:** `ActorsController.cs`

```csharp
var actores = await _context.Actors
    .Where(a => a.FirstName.StartsWith("N") || a.FirstName.EndsWith("N"))
    .Take(5)
    .ToListAsync();
```

```sql
SELECT * FROM actor
WHERE first_name LIKE 'N%'
   OR first_name LIKE '%N'
LIMIT 5;
```

---

### Ejercicio 13 – Apellido del actor empieza con S
**Controlador:** `ActorsController.cs`

```csharp
var actores = await _context.Actors
    .Where(a => a.LastName.StartsWith("S"))
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM actor
WHERE last_name LIKE 'S%'
LIMIT 10;
```

---

### Ejercicio 14 – Nombre del actor contiene JO
**Controlador:** `ActorsController.cs`

```csharp
var actores = await _context.Actors
    .Where(a => a.FirstName.Contains("JO"))
    .Take(5)
    .ToListAsync();
```

```sql
SELECT * FROM actor
WHERE first_name LIKE '%JO%'
LIMIT 5;
```



*/












        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // GET: Actors/Create
        [Authorize(Roles = "Administrador,Operador")]/////////////////
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Operador")]
        public async Task<IActionResult> Create([Bind("ActorId,FirstName,LastName,LastUpdate")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        [Authorize(Roles = "Administrador")]
        /////////////////////////////////
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("ActorId,FirstName,LastName,LastUpdate")] Actor actor)
        {
            if (id != actor.ActorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.ActorId))
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
            return View(actor);
        }

        // GET: Actors/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ActorId == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                try
                {
                    _context.Actors.Remove(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorMessage = "No se puede eliminar este actor porque está asignado a una o más películas.";
                    return View(actor);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.ActorId == id);
        }
    }
}
