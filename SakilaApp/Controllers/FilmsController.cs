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
    public class FilmsController : Controller
    {
        private readonly SakilaContext _context;

        public FilmsController(SakilaContext context)
        {
            _context = context;
        }


  public async Task<IActionResult> PrimerosDiez()
        {
    var peliculas = await _context.Films
        .Include(f => f.FilmCategories)
            .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Animation")   && f.Length > 100)
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
            
            ViewBag.PaginaActual = 1;
            ViewBag.TotalPaginas = 1;
            ViewBag.Buscar = null;
            ViewBag.DuracionMinima = null;

            return View("Index", peliculas);

        }






/*





var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Animation")
             && f.Length > 100)
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();







# LINQ y SQL – 40 Ejercicios Sakila

---

### Ejercicio 1 – 10 primeras películas ordenadas alfabéticamente
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
ORDER BY title
LIMIT 10;
```

---

### Ejercicio 2 – 5 películas más largas
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .OrderByDescending(f => f.Length)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT * FROM film
ORDER BY length DESC
LIMIT 5;
```

---

### Ejercicio 3 – Título contiene LOVE
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Where(f => f.Title.Contains("LOVE"))
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
WHERE title LIKE '%LOVE%'
LIMIT 10;
```

---

### Ejercicio 4 – Título empieza con A
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Where(f => f.Title.StartsWith("A"))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
WHERE title LIKE 'A%'
ORDER BY title
LIMIT 10;
```

---

### Ejercicio 5 – Título termina con N
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Where(f => f.Title.EndsWith("N"))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
WHERE title LIKE '%N'
ORDER BY title
LIMIT 10;
```

---

### Ejercicio 6 – Duración mayor a 120 minutos
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Where(f => f.Length > 120)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
WHERE length > 120
LIMIT 10;
```

---

### Ejercicio 7 – Costo de reemplazo menor a 20
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Where(f => f.ReplacementCost < 20)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
WHERE replacement_cost < 20
LIMIT 10;
```

---

### Ejercicio 8 – Duración mayor a 100 y costo menor a 20
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Where(f => f.Length > 100 && f.ReplacementCost < 20)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
WHERE length > 100
  AND replacement_cost < 20
LIMIT 10;
```

---

### Ejercicio 9 – Título contiene LOVE o tarifa de alquiler es 4.99
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Where(f => f.Title.Contains("LOVE") || f.RentalRate == 4.99m)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
WHERE title LIKE '%LOVE%'
   OR rental_rate = 4.99
LIMIT 10;
```

---

### Ejercicio 10 – Título empieza con A o termina con N
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Where(f => f.Title.StartsWith("A") || f.Title.EndsWith("N"))
    .Take(10)
    .ToListAsync();
```

```sql
SELECT * FROM film
WHERE title LIKE 'A%'
   OR title LIKE '%N'
LIMIT 10;
```

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

---

### Ejercicio 15 – Películas con nombre de su idioma
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.Language)
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.title, l.name AS idioma
FROM film f
INNER JOIN language l ON f.language_id = l.language_id
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 16 – Idioma diferente a English
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.Language)
    .Where(f => f.Language.Name != "English")
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN language l ON f.language_id = l.language_id
WHERE l.name != 'English'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 17 – Idioma diferente a English y título empieza con A
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.Language)
    .Where(f => f.Language.Name != "English" && f.Title.StartsWith("A"))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN language l ON f.language_id = l.language_id
WHERE l.name != 'English'
  AND f.title LIKE 'A%'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 18 – Idioma diferente a English o título contiene LOVE
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.Language)
    .Where(f => f.Language.Name != "English" || f.Title.Contains("LOVE"))
    .Take(5)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN language l ON f.language_id = l.language_id
WHERE l.name != 'English'
   OR f.title LIKE '%LOVE%'
LIMIT 5;
```

---

### Ejercicio 19 – 5 películas más largas con idioma diferente a English
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.Language)
    .Where(f => f.Language.Name != "English")
    .OrderByDescending(f => f.Length)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN language l ON f.language_id = l.language_id
WHERE l.name != 'English'
ORDER BY f.length DESC
LIMIT 5;
```

---

### Ejercicio 20 – Idioma English, duración DESC, omitiendo la primera
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.Language)
    .Where(f => f.Language.Name == "English")
    .OrderByDescending(f => f.Length)
    .Skip(1)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN language l ON f.language_id = l.language_id
WHERE l.name = 'English'
ORDER BY f.length DESC
OFFSET 1
LIMIT 10;
```

---

### Ejercicio 21 – Categoría Action
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Join(_context.FilmCategories,
        film    => film.FilmId,
        filmCat => filmCat.FilmId,
        (film, filmCat) => new { film, filmCat })
    .Join(_context.Categories,
        temp     => temp.filmCat.CategoryId,
        category => category.CategoryId,
        (temp, category) => new { temp.film, category })
    .Where(x => x.category.Name == "Action")
    .OrderBy(x => x.film.Title)
    .Take(10)
    .Select(x => x.film)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Action'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 22 – 5 películas más largas de Drama
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Drama"))
    .OrderByDescending(f => f.Length)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Drama'
ORDER BY f.length DESC
LIMIT 5;
```

---

### Ejercicio 23 – Comedy con A en el título
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Comedy")
             && f.Title.Contains("A"))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Comedy'
  AND f.title LIKE '%A%'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 24 – Horror omitiendo la primera
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Horror"))
    .OrderBy(f => f.Title)
    .Skip(1)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Horror'
ORDER BY f.title
OFFSET 1
LIMIT 5;
```

---

### Ejercicio 25 – Family ordenadas por título
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Family"))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Family'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 26 – Animation con duración mayor a 100 minutos
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Animation")
             && f.Length > 100)
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Animation'
  AND f.length > 100
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 27 – Action con costo de reemplazo menor a 20
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Action")
             && f.ReplacementCost < 20)
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Action'
  AND f.replacement_cost < 20
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 28 – Comedy con duración mayor a 120 minutos
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Comedy")
             && f.Length > 120)
    .OrderByDescending(f => f.Length)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Comedy'
  AND f.length > 120
ORDER BY f.length DESC
LIMIT 5;
```

---

### Ejercicio 29 – Drama con título que empieza con M
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Drama")
             && f.Title.StartsWith("M"))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Drama'
  AND f.title LIKE 'M%'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 30 – Family ordenadas por duración descendente
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Family"))
    .OrderByDescending(f => f.Length)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
WHERE c.name = 'Family'
ORDER BY f.length DESC
LIMIT 5;
```

---

### Ejercicio 31 – Actor con apellido que empieza con S
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Join(_context.FilmActors,
        film      => film.FilmId,
        filmActor => filmActor.FilmId,
        (film, filmActor) => new { film, filmActor })
    .Join(_context.Actors,
        temp  => temp.filmActor.ActorId,
        actor => actor.ActorId,
        (temp, actor) => new { temp.film, actor })
    .Where(x => x.actor.LastName.StartsWith("S"))
    .OrderBy(x => x.film.Title)
    .Take(10)
    .Select(x => x.film)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_actor fa ON f.film_id = fa.film_id
INNER JOIN actor a       ON fa.actor_id = a.actor_id
WHERE a.last_name LIKE 'S%'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 32 – Actor con nombre que contiene JO
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmActors.Any(fa => fa.Actor.FirstName.Contains("JO")))
    .OrderBy(f => f.Title)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_actor fa ON f.film_id = fa.film_id
INNER JOIN actor a       ON fa.actor_id = a.actor_id
WHERE a.first_name LIKE '%JO%'
ORDER BY f.title
LIMIT 5;
```

---

### Ejercicio 33 – Actor con apellido que termina con N
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmActors.Any(fa => fa.Actor.LastName.EndsWith("N")))
    .OrderBy(f => f.Title)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_actor fa ON f.film_id = fa.film_id
INNER JOIN actor a       ON fa.actor_id = a.actor_id
WHERE a.last_name LIKE '%N'
ORDER BY f.title
LIMIT 5;
```

---

### Ejercicio 34 – Actor con nombre que empieza con M y título contiene A
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmActors.Any(fa => fa.Actor.FirstName.StartsWith("M"))
             && f.Title.Contains("A"))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_actor fa ON f.film_id = fa.film_id
INNER JOIN actor a       ON fa.actor_id = a.actor_id
WHERE a.first_name LIKE 'M%'
  AND f.title LIKE '%A%'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 35 – Comedy y actor con apellido que empieza con B
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Comedy")
             && f.FilmActors.Any(fa => fa.Actor.LastName.StartsWith("B")))
    .OrderBy(f => f.Title)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
INNER JOIN film_actor fa    ON f.film_id = fa.film_id
INNER JOIN actor a          ON fa.actor_id = a.actor_id
WHERE c.name = 'Comedy'
  AND a.last_name LIKE 'B%'
ORDER BY f.title
LIMIT 5;
```

---

### Ejercicio 36 – Action y actor con apellido que empieza con C
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Action")
             && f.FilmActors.Any(fa => fa.Actor.LastName.StartsWith("C")))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
INNER JOIN film_actor fa    ON f.film_id = fa.film_id
INNER JOIN actor a          ON fa.actor_id = a.actor_id
WHERE c.name = 'Action'
  AND a.last_name LIKE 'C%'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 37 – Drama y actor con nombre que contiene AN
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Drama")
             && f.FilmActors.Any(fa => fa.Actor.FirstName.Contains("AN")))
    .OrderBy(f => f.Title)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
INNER JOIN film_actor fa    ON f.film_id = fa.film_id
INNER JOIN actor a          ON fa.actor_id = a.actor_id
WHERE c.name = 'Drama'
  AND a.first_name LIKE '%AN%'
ORDER BY f.title
LIMIT 5;
```

---

### Ejercicio 38 – Horror y actor con apellido que termina con S
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Horror")
             && f.FilmActors.Any(fa => fa.Actor.LastName.EndsWith("S")))
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
INNER JOIN film_actor fa    ON f.film_id = fa.film_id
INNER JOIN actor a          ON fa.actor_id = a.actor_id
WHERE c.name = 'Horror'
  AND a.last_name LIKE '%S'
ORDER BY f.title
LIMIT 10;
```

---

### Ejercicio 39 – Family y actor con nombre que empieza con J
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Family")
             && f.FilmActors.Any(fa => fa.Actor.FirstName.StartsWith("J")))
    .OrderBy(f => f.Title)
    .Take(5)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
INNER JOIN film_actor fa    ON f.film_id = fa.film_id
INNER JOIN actor a          ON fa.actor_id = a.actor_id
WHERE c.name = 'Family'
  AND a.first_name LIKE 'J%'
ORDER BY f.title
LIMIT 5;
```

---

### Ejercicio 40 – Comedy, actor con apellido que contiene R y duración mayor a 100
**Controlador:** `FilmsController.cs`

```csharp
var peliculas = await _context.Films
    .Include(f => f.FilmCategories)
        .ThenInclude(fc => fc.Category)
    .Include(f => f.FilmActors)
        .ThenInclude(fa => fa.Actor)
    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Comedy")
             && f.FilmActors.Any(fa => fa.Actor.LastName.Contains("R"))
             && f.Length > 100)
    .OrderBy(f => f.Title)
    .Take(10)
    .ToListAsync();
```

```sql
SELECT DISTINCT f.*
FROM film f
INNER JOIN film_category fc ON f.film_id = fc.film_id
INNER JOIN category c       ON fc.category_id = c.category_id
INNER JOIN film_actor fa    ON f.film_id = fa.film_id
INNER JOIN actor a          ON fa.actor_id = a.actor_id
WHERE c.name = 'Comedy'
  AND a.last_name LIKE '%R%'
  AND f.length > 100
ORDER BY f.title
LIMIT 10;
```



*/


















































/*
      // =====================================================================
        // BÁSICOS
        // =====================================================================
 
        // 1. Todas las películas ordenadas alfabéticamente por título
        // Ruta: /Films/OrdenAlfabetico
        public async Task<IActionResult> OrdenAlfabetico()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .OrderBy(f => f.Title);
            return View("Index", await films.ToListAsync());
        }
 
        // 2. Las 10 primeras películas registradas
        // Ruta: /Films/PrimerosDiez
        public async Task<IActionResult> PrimerosDiez()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .OrderBy(f => f.FilmId)
                .Take(10);
            return View("Index", await films.ToListAsync());
        }
 
        // 3. Las 20 películas más largas
        // Ruta: /Films/MasLargas
        public async Task<IActionResult> MasLargas()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .OrderByDescending(f => f.Length)
                .Take(20);
            return View("Index", await films.ToListAsync());
        }
 
        // 4. Películas con duración mayor a 120 minutos
        // Ruta: /Films/MayorA120
        public async Task<IActionResult> MayorA120()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => f.Length > 120);
            return View("Index", await films.ToListAsync());
        }
 
        // 5. Películas entre 90 y 120 minutos
        // Ruta: /Films/Entre90y120
        public async Task<IActionResult> Entre90y120()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => f.Length >= 90 && f.Length <= 120);
            return View("Index", await films.ToListAsync());
        }
 
        // 6. Películas cuyo título contenga "LOVE"
        // Ruta: /Films/ContienenLove
        public async Task<IActionResult> ContienenLove()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => EF.Functions.ILike(f.Title, "%LOVE%"));
            return View("Index", await films.ToListAsync());
        }
 
        // 7. Películas cuyo título comience con "A"
        // Ruta: /Films/EmpiezanConA
        public async Task<IActionResult> EmpiezanConA()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => EF.Functions.ILike(f.Title, "A%"));
            return View("Index", await films.ToListAsync());
        }
 
        // 8. Películas cuyo título termine con "N"
        // Ruta: /Films/TerminanConN
        public async Task<IActionResult> TerminanConN()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => EF.Functions.ILike(f.Title, "%N"));
            return View("Index", await films.ToListAsync());
        }
 
        // 9. Películas con clasificación PG
        // Ruta: /Films/ClasificacionPG
        public async Task<IActionResult> ClasificacionPG()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => f.Rating == "PG");
            return View("Index", await films.ToListAsync());
        }
 
        // 10. Películas con clasificación PG-13 o R
        // Ruta: /Films/ClasificacionPG13oR
        public async Task<IActionResult> ClasificacionPG13oR()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => f.Rating == "PG-13" || f.Rating == "R");
            return View("Index", await films.ToListAsync());
        }
 
        // =====================================================================
        // INTERMEDIOS
        // =====================================================================
 
        // 11. Películas de la categoría Drama
        // Ruta: /Films/CategoriaDrama
        public async Task<IActionResult> CategoriaDrama()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Include(f => f.FilmCategories).ThenInclude(fc => fc.Category)
                .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Drama"));
            return View("Index", await films.ToListAsync());
        }
 
        // 12. Películas de la categoría Comedy
        // Ruta: /Films/CategoriaComedy
        public async Task<IActionResult> CategoriaComedy()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Include(f => f.FilmCategories).ThenInclude(fc => fc.Category)
                .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Comedy"));
            return View("Index", await films.ToListAsync());
        }
 
        // 13. Drama y duración mayor a 100 minutos
        // Ruta: /Films/DramaMayorA100
        public async Task<IActionResult> DramaMayorA100()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Include(f => f.FilmCategories).ThenInclude(fc => fc.Category)
                .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Drama") && f.Length > 100);
            return View("Index", await films.ToListAsync());
        }
 
        // 14. Action ordenadas de más larga a más corta
        // Ruta: /Films/ActionOrdenadas
        public async Task<IActionResult> ActionOrdenadas()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Include(f => f.FilmCategories).ThenInclude(fc => fc.Category)
                .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Action"))
                .OrderByDescending(f => f.Length);
            return View("Index", await films.ToListAsync());
        }
 
        // 15. Las 5 primeras películas de Horror ordenadas por título
        // Ruta: /Films/HorrorTop5
        public async Task<IActionResult> HorrorTop5()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Include(f => f.FilmCategories).ThenInclude(fc => fc.Category)
                .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Horror"))
                .OrderBy(f => f.Title)
                .Take(5);
            return View("Index", await films.ToListAsync());
        }
 
        // 16. Contar películas por categoría
        // Ruta: /Films/ConteoXCategoria
        // NOTA: Devuelve datos simples, usa la vista Index con ViewBag
        public async Task<IActionResult> ConteoXCategoria()
        {
            var conteo = await _context.FilmCategories
                .Include(fc => fc.Category)
                .GroupBy(fc => fc.Category.Name)
                .Select(g => new { Categoria = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .ToListAsync();
 
            // Convertimos a string para mostrarlo fácil en View
            ViewBag.Titulo = "Películas por categoría";
            ViewBag.Conteo = conteo;
            return View("Conteo");   // necesitas una vista Conteo.cshtml (ver abajo)
        }
 
        // 17. Cuántas películas tienen clasificación PG
        // Ruta: /Films/CantidadPG
        public async Task<IActionResult> CantidadPG()
        {
            var cantidad = await _context.Films
                .CountAsync(f => f.Rating == "PG");
 
            ViewBag.Titulo = "Películas con clasificación PG";
            ViewBag.Cantidad = cantidad;
            return View("Cantidad");   // vista Cantidad.cshtml (ver abajo)
        }
 
        // 18. Cantidad total de películas
        // Ruta: /Films/TotalPeliculas
        public async Task<IActionResult> TotalPeliculas()
        {
            var total = await _context.Films.CountAsync();
 
            ViewBag.Titulo = "Total de películas registradas";
            ViewBag.Cantidad = total;
            return View("Cantidad");
        }
 
        // 19. Películas con costo de reemplazo mayor a 20
        // Ruta: /Films/CostoMayorA20
        public async Task<IActionResult> CostoMayorA20()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => f.ReplacementCost > 20);
            return View("Index", await films.ToListAsync());
        }
 
        // 20. Duración > 100 min Y costo de reemplazo < 20
        // Ruta: /Films/LargasBaratas
        public async Task<IActionResult> LargasBaratas()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => f.Length > 100 && f.ReplacementCost < 20);
            return View("Index", await films.ToListAsync());
        }
 
        // =====================================================================
        // RETOS
        // =====================================================================
 
        // 21. Los 10 actores con más películas
        // Ruta: /Films/Top10Actores
        public async Task<IActionResult> Top10Actores()
        {
            var actores = await _context.FilmActors
                .Include(fa => fa.Actor)
                .GroupBy(fa => new { fa.ActorId, fa.Actor.FirstName, fa.Actor.LastName })
                .Select(g => new
                {
                    Nombre = g.Key.FirstName + " " + g.Key.LastName,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .Take(10)
                .ToListAsync();
 
            ViewBag.Titulo = "Top 10 actores con más películas";
            ViewBag.Actores = actores;
            return View("Actores");
        }
 
        // 22. Películas de un actor específico (ejemplo: "PENELOPE GUINESS")
        // Ruta: /Films/PeliculasDeActor?nombre=PENELOPE&apellido=GUINESS
        public async Task<IActionResult> PeliculasDeActor(string nombre = "PENELOPE", string apellido = "GUINESS")
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Include(f => f.FilmActors).ThenInclude(fa => fa.Actor)
                .Where(f => f.FilmActors.Any(fa =>
                    EF.Functions.ILike(fa.Actor.FirstName, nombre) &&
                    EF.Functions.ILike(fa.Actor.LastName, apellido)));
            return View("Index", await films.ToListAsync());
        }
 
        // 23. Clientes cuyo apellido contenga una cadena
        // Ruta: /Films/ClientesPorApellido?apellido=SM
        // NOTA: Este ejercicio usa clientes, no Films. Igual retorna View.
        public async Task<IActionResult> ClientesPorApellido(string apellido = "SM")
        {
            var clientes = await _context.Customers
                .Where(c => EF.Functions.ILike(c.LastName, "%" + apellido + "%"))
                .ToListAsync();
 
            ViewBag.Titulo = $"Clientes con apellido que contiene: {apellido}";
            ViewBag.Clientes = clientes;
            return View("Clientes");
        }
 
        // 24. Ciudades de un país específico
        // Ruta: /Films/CiudadesDePais?pais=Mexico
        public async Task<IActionResult> CiudadesDePais(string pais = "Mexico")
        {
            var ciudades = await _context.Cities
                .Include(c => c.Country)
                .Where(c => EF.Functions.ILike(c.Country.Country1, pais))
                .ToListAsync();
 
            ViewBag.Titulo = $"Ciudades de: {pais}";
            ViewBag.Ciudades = ciudades;
            return View("Ciudades");
        }
 
        // 25. Clientes con más de 20 alquileres
        // Ruta: /Films/ClientesMas20Alquileres
        public async Task<IActionResult> ClientesMas20Alquileres()
        {
            var clientes = await _context.Rentals
                .GroupBy(r => new { r.CustomerId, r.Customer.FirstName, r.Customer.LastName })
                .Where(g => g.Count() > 20)
                .Select(g => new
                {
                    Nombre = g.Key.FirstName + " " + g.Key.LastName,
                    TotalAlquileres = g.Count()
                })
                .OrderByDescending(x => x.TotalAlquileres)
                .ToListAsync();
 
            ViewBag.Titulo = "Clientes con más de 20 alquileres";
            ViewBag.Clientes = clientes;
            return View("Clientes");
        }
 
        // 26. Películas nunca alquiladas
        // Ruta: /Films/NuncaAlquiladas
        public async Task<IActionResult> NuncaAlquiladas()
        {
            var films = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => !f.Inventories.Any(i => i.Rentals.Any()));
            return View("Index", await films.ToListAsync());
        }
 
        // 27. Las 10 películas más alquiladas
        // Ruta: /Films/Top10MasAlquiladas
        public async Task<IActionResult> Top10MasAlquiladas()
        {
            var resultado = await _context.Rentals
                .Include(r => r.Inventory).ThenInclude(i => i.Film)
                .GroupBy(r => new { r.Inventory.FilmId, r.Inventory.Film.Title })
                .Select(g => new
                {
                    Titulo = g.Key.Title,
                    TotalAlquileres = g.Count()
                })
                .OrderByDescending(x => x.TotalAlquileres)
                .Take(10)
                .ToListAsync();
 
            ViewBag.Titulo = "Top 10 películas más alquiladas";
            ViewBag.Peliculas = resultado;
            return View("TopAlquiladas");
        }
 
        // 28. Promedio de duración por categoría
        // Ruta: /Films/PromedioDuracionXCategoria
        public async Task<IActionResult> PromedioDuracionXCategoria()
        {
            var resultado = await _context.FilmCategories
                .Include(fc => fc.Category)
                .Include(fc => fc.Film)
                .GroupBy(fc => fc.Category.Name)
                .Select(g => new
                {
                    Categoria = g.Key,
                    Promedio = g.Average(x => (double?)x.Film.Length) ?? 0
                })
                .OrderByDescending(x => x.Promedio)
                .ToListAsync();
 
            ViewBag.Titulo = "Promedio de duración por categoría";
            ViewBag.Datos = resultado;
            return View("Conteo");
        }
 
        // 29. Categoría con mayor cantidad de películas
        // Ruta: /Films/CategoriaMayorCantidad
        public async Task<IActionResult> CategoriaMayorCantidad()
        {
            var resultado = await _context.FilmCategories
                .Include(fc => fc.Category)
                .GroupBy(fc => fc.Category.Name)
                .Select(g => new { Categoria = g.Key, Cantidad = g.Count() })
                .OrderByDescending(x => x.Cantidad)
                .FirstOrDefaultAsync();
 
            ViewBag.Titulo = "Categoría con más películas";
            ViewBag.Cantidad = resultado?.Cantidad ?? 0;
            ViewBag.Extra = resultado?.Categoria ?? "N/A";
            return View("Cantidad");
        }
 
        // 30. Actor que aparece en más películas
        // Ruta: /Films/ActorConMasPeliculas
        public async Task<IActionResult> ActorConMasPeliculas()
        {
            var actor = await _context.FilmActors
                .Include(fa => fa.Actor)
                .GroupBy(fa => new { fa.ActorId, fa.Actor.FirstName, fa.Actor.LastName })
                .Select(g => new
                {
                    Nombre = g.Key.FirstName + " " + g.Key.LastName,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .FirstOrDefaultAsync();
 
            ViewBag.Titulo = "Actor con más películas";
            ViewBag.Cantidad = actor?.Cantidad ?? 0;
            ViewBag.Extra = actor?.Nombre ?? "N/A";
            return View("Cantidad");
        }
*/



        // GET: Film

//;Mostarr los 5 ctores sigueintes despues dle primer actoer que sus nombres empiexen por la letra n o terminene con la lettra n ordenados alfabeticamenter por apellido 






//Mostrar las películas cuyo idioma sea English y cuyo título empiece con la letra A, ordenadas alfabéticamente por título.



 [Authorize]
public async Task<IActionResult> Index(
    string? buscar,
    int? duracionMinima,
    int pagina = 1)
{
    int tamanioPagina = 10;

    var consulta = _context.Films.AsQueryable();

    if (!string.IsNullOrWhiteSpace(buscar))
    {
        consulta = consulta.Where(f => f.Title.Contains(buscar));
    }

    if (duracionMinima.HasValue)
    {
        consulta = consulta.Where(f => f.Length >= duracionMinima.Value);
    }

    int totalRegistros = await consulta.CountAsync();

    var peliculas = await consulta
        .OrderBy(f => f.Title)
        .Skip((pagina - 1) * tamanioPagina)
        .Take(tamanioPagina)
        .ToListAsync();

    ViewBag.Buscar = buscar;
    ViewBag.DuracionMinima = duracionMinima;
    ViewBag.PaginaActual = pagina;
    ViewBag.TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanioPagina);

    return View(peliculas);
}

// 

/*

public async Task<IActionResult> Index()
{
    var peliculas = await _context.Films
        .Join(_context.Languages,
            film => film.LanguageId,
            language => language.LanguageId,
            (film, language) => new { film, language })
        .Where(x => x.language.Name == "English" && x.film.Title.StartsWith("A"))
        .OrderBy(x => x.film.RentalRate) 
        .Take(5)
        .Select(x => x.film)
        .ToListAsync();

    return View(peliculas);
}


public async Task<IActionResult> Index()
{
    var peliculas = await _context.Films
        .Include(f => f.Language)
        .Where(f => f.Language.Name == "English" && f.Title.StartsWith("A"))
        .OrderBy(f => f.Title)
        .ToListAsync();

    return View(peliculas);
}



public async Task<IActionResult> Index()
{
    var peliculas = await _context.Films
    .Include(c => c.Categories)
        .Join(_context.Languages,
            film => film.LanguageId,
            language => language.LanguageId,
            (film, language) => new { film, language })
        .Join(_context.FilmCategories,
            temp => temp.film.FilmId,
            filmCategory => filmCategory.FilmId,
            (temp, filmCategory) => new { temp.film, temp.language, filmCategory })
        .Join(_context.Categories,
            temp => temp.filmCategory.CategoryId,
            category => category.CategoryId,
            (temp, category) => new { temp.film, temp.language, category })
        .Where(x => x.language.Name == "English" && x.category.Name == "Comedy")
        .OrderBy(x => x.film.Title)
        .Select(x => x.film)
        .ToListAsync();

    return View(peliculas);
}

public async Task<IActionResult> Index()
{
    var peliculas = await _context.Films
        .Join(_context.FilmCategories,
            film => film.FilmId,
            filmCategory => filmCategory.FilmId,
            (film, filmCategory) => new { film, filmCategory })
        .Join(_context.Categories,
            temp => temp.filmCategory.CategoryId,
            category => category.CategoryId,
            (temp, category) => new { temp.film, category })
        .Where(x => x.category.Name == "Drama")
        .OrderByDescending(x => x.film.Length)
        .Take(10)
        .Select(x => x.film)
        .ToListAsync();

    return View(peliculas);
}




    public async Task<IActionResult> Index()
{
    var peliculas = await _context.Films
       
       .Include(l=>l.Language)
       .Include(f => f.OriginalLanguage)
       .OrderBy(f=>f.Title) 
    
       .ToListAsync();  

    return View(peliculas);
}



        public async Task<IActionResult> Index()
{
    var peliculas = await _context.Films
        .Join(_context.Languages,
            film => film.LanguageId,
            language => language.LanguageId,
            (film, language) => film)
        .OrderBy(f => f.Title)
        .ToListAsync();

    return View(peliculas);
}

*/



/*


public async Task<IActionResult> Index()
        {
            var peliculas = await _context.Films
                
                .Where(a => EF.Functions.Like(a.Actor.FirstName, "N%") || EF.Functions.Like(a.Actor.LastName, "%N"))
                .OrderBy(a => a.Actor.LastName)
                .Take(5)
                .ToListAsync();

            return View(actores);
        }
//peluclas mayor a 100  muntos y cuya tarifa de alquiler mayor o igual  3.50
public async Task<IActionResult> Index()
        {
            var peliculas = await _context.Films
                .Where(f => f.Length > 100 && f.RentalRate >= 3.50m)
                

                .ToListAsync();

            return View(peliculas);
        }

.Where(f => f.Title.EndsWith("A"))
.Where(f => f.Title.Contains("DINO"))


public async Task<IActionResult> Index()
{
    // Variables con las palabras que deseas buscar en la descripción
    var palabra1 = "Drama";
    var palabra2 = "Documentary";

    var sakilaContext = _context.Films
        .Include(f => f.Language)
        .Include(f => f.OriginalLanguage)
        .Where(f => EF.Functions.Like(f.Description, $"%{palabra1}%") 
                 && EF.Functions.Like(f.Description, $"%{palabra2}%")) 
        .OrderBy(f => f.Title) 
        .Take(10); 
        
    return View(await sakilaContext.ToListAsync());
}


 public async Task<IActionResult> Index()
        {
            var sakilaContext = _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .Where(f => EF.Functions.Like(f.Description, "%Drama%"))
                .OrderBy(f => f.Length);
                
            return View(await sakilaContext.ToListAsync());
        }
 
 public async Task<IActionResult> Index()
        {
            var peliculas = await _context.Films
                .Where(f => f.Title.EndsWith("A"))
                .ToListAsync();

            return View(peliculas);
        }

*/


          [Authorize]
        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .FirstOrDefaultAsync(m => m.FilmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

 
          [Authorize(Roles = "Administrador,Empleado")]
          ////////////////////////////
        // GET: Films/Create
        public IActionResult Create()
        {
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FilmId,Title,Description,ReleaseYear,LanguageId,OriginalLanguageId,RentalDuration,RentalRate,Length,ReplacementCost,LastUpdate,SpecialFeatures,Fulltext")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.OriginalLanguageId);
            return View(film);
        }

          [Authorize(Roles = "Administrador,Empleado")]
          ///////////////////////////////////////////////
        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.OriginalLanguageId);
            return View(film);
        }

          [Authorize(Roles = "Administrador,Empleado")]
        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmId,Title,Description,ReleaseYear,LanguageId,OriginalLanguageId,RentalDuration,RentalRate,Length,ReplacementCost,LastUpdate,SpecialFeatures,Fulltext")] Film film)
        {
            if (id != film.FilmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.FilmId))
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
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.LanguageId);
            ViewData["OriginalLanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", film.OriginalLanguageId);
            return View(film);
        }

          [Authorize(Roles = "Administrador")]
        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.Language)
                .Include(f => f.OriginalLanguage)
                .FirstOrDefaultAsync(m => m.FilmId == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

          [Authorize(Roles = "Administrador")]
        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                try
                {
                    _context.Films.Remove(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ViewBag.ErrorMessage = "No se puede eliminar esta película porque está relacionada con actores, categorías o inventario.";
                    return View(film);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Films.Any(e => e.FilmId == id);
        }
    }
}
