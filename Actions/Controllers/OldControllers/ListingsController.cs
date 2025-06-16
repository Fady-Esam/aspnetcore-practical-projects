using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Actions.Data;
using Microsoft.AspNetCore.Hosting;
using Actions.Models.OldModels;
using Actions.Data.Services.OldServices;

namespace Actions.Controllers.OldControllers
{
    public class ListingsController : Controller
    {
        private readonly IListingService _listingService;
        private readonly IWebHostEnvironment _host;

        public ListingsController(IListingService listingService, IWebHostEnvironment host)
        {
            _listingService = listingService;
            _host = host;
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _listingService.GetAllListings();
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _listingService.GetListingById(id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ListingVM listing)
        {

            //if (!ModelState.IsValid)
            //{
            if (listing.Image != null)
            {
                string uploadDir = Path.Combine(_host.WebRootPath, "images");
                string fileName = listing.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await listing.Image.CopyToAsync(fileStream);
                }
                var listObj = new Listing
                {
                    Title = listing.Title,
                    Description = listing.Description,
                    Price = listing.Price,
                    IdentityUserId = listing.IdentityUserId,
                    ImagePath = fileName,
                };
                await _listingService.Add(listObj);
                return RedirectToAction("Index");
            }
            // }
            return View(listing);
        }

        // GET: Listings/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

            //    var listing = await _context.Listings.FindAsync(id);
            //    if (listing == null)
            //    {
            //        return NotFound();
            //    }
            //    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", listing.IdentityUserId);
            //    return View(listing);
            //}

            //// POST: Listings/Edit/5
            //// To protect from overposting attacks, enable the specific properties you want to bind to.
            //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            //[HttpPost]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,IsSold,ImagePath,IdentityUserId")] Listing listing)
            //{
            //    if (id != listing.Id)
            //    {
            //        return NotFound();
            //    }

            //    if (ModelState.IsValid)
            //    {
            //        try
            //        {
            //            _context.Update(listing);
            //            await _context.SaveChangesAsync();
            //        }
            //        catch (DbUpdateConcurrencyException)
            //        {
            //            if (!ListingExists(listing.Id))
            //            {
            //                return NotFound();
            //            }
            //            else
            //            {
            //                throw;
            //            }
            //        }
            //        return RedirectToAction(nameof(Index));
            //    }
            //    ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", listing.IdentityUserId);
            //    return View(listing);
            //}

            //// GET: Listings/Delete/5
            //public async Task<IActionResult> Delete(int? id)
            //{
            //    if (id == null)
            //    {
            //        return NotFound();
            //    }

            //    var listing = await _context.Listings
            //        .Include(l => l.User)
            //        .FirstOrDefaultAsync(m => m.Id == id);
            //    if (listing == null)
            //    {
            //        return NotFound();
            //    }

            //    return View(listing);
            //}

            //// POST: Listings/Delete/5
            //[HttpPost, ActionName("Delete")]
            //[ValidateAntiForgeryToken]
            //public async Task<IActionResult> DeleteConfirmed(int id)
            //{
            //    var listing = await _context.Listings.FindAsync(id);
            //    if (listing != null)
            //    {
            //        _context.Listings.Remove(listing);
            //    }

            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            //private bool ListingExists(int id)
            //{
            //    return _context.Listings.Any(e => e.Id == id);
            //}
        }
    }


