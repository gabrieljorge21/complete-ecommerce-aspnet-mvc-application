using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsServices _service;

        public  ActorsController(IActorsServices service)
        {
            _service = service;
        }
        public async Task <IActionResult> Index()
        {
            var allActors = await _service.GettAllAsync();
            return View(allActors);
        }

        //GET Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if(!ModelState.IsValid) {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //GET Actors/Details/1

        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if(actorDetails == null) { return View("Empty"); }
            return View(actorDetails);
        }

        //GET Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) { return View("Empty"); }
            return View(actorDetails);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }

        //GET Actors/Delete/1
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) { return View("NotFound"); }
            return View(actorDetails);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) { return View("NotFound"); }

            await _service.DeleteAsync(id);
           
            return RedirectToAction(nameof(Index));
        }
    }

}
