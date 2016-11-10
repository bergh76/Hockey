using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hockey.Data;
using Hockey.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Hockey.HelperClasses;
using Hockey.ViewModels;
using Hockey.BusinessLayers;

namespace Hockey.Areas.Nhl.Controllers
{
    public class NhlPlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostEnvironment; // service that provides some useful environment information such as the current file path
        //private IFormFile _file;
        private static string _root;
        public NhlPlayersController(ApplicationDbContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: NhlPlayers
        public async Task<IActionResult> Index()
        {
            var list = await ListPlayer();           
            return View();
        }

        public async Task<ViewResult> ListPlayer()
        {
            //var applicationDbContext = _context.NhlPlayer
            //    .Include(s => s._CardManufacture)
            //    .Include(s => s._Image)
            //    .Include(s => s._Leauge)
            //    .Include(s => s._Nationality)
            //    .Include(s => s._Position)
            //    .Include(s => s._Season)
            //    .Include(s => s._Team)
            //    .Include(s => s._TeamImage);
            //IEnumerable<NhlPlayerViewModel> nhlListPlayer = await applicationDbContext.ToListAsync();
            //return View("Index",nhlListPlayer);
            //try
            //{
            var playerList = from p in _context.NhlPlayer
                             join c in _context.Conference on p.ConferenceId equals c.ConferenceId
                             join d in _context.Division on p.DivisionId equals d.DivisionId
                             join t in _context.Team on p.TeamId equals t.TeamId
                             join y in _context.Position on p.PositionId equals y.PositionId
                             join i in _context.Image on p.NhlPlayerId equals i.PlayerId
                             join n in _context.Nationality on p.NationalityId equals n.NationalityId
                             join ti in _context.TeamImage on t.TeamImageId equals ti.TeamImageId
                             join s in _context.Season on p.SeasonId equals s.SeasonId
                             join m in _context.CardManufacture on p.CardManufactureId equals m.CardManufactureId
                             //join l in _context.League on p.LeagueId equals l.LeagueId
                             select new NhlPlayerViewModel
                             {
                                 CardNumber = p.NhlPlayerCardId, //ok
                                 NationalityImgPath = n.NationalityImagePath + n.NationalityImageName, //ok
                                 Nationality = n.NationalityName, //ok
                                 FirstName = p.PlayerFirstName, //ok
                                 LastName = p.PlayerLastName, //ok
                                 Position = y.PositionShortName, //ok
                                 JersyNumber = p.PlayerJersyNumber, //ok
                                 //League = l.LeagueName,
                                 Conference = c.ConferenceName, //ok
                                 Division = d.DivisionName, //ok
                                 TeamImgPath = ti.TeamImagePath + ti.TeamImageName, //ok
                                 Team = t.TeamName, //ok
                                 CardManufacture = m.MakerName, //ok
                                 Season = s.SeasonName, //ok
                                 ImagePath = i.ImagePath + i.ImageName,//ok
                                 ISSigned = p.ISSigned, //ok
                                 PlayerId = p.NhlPlayerId,//ok

                             };
            IEnumerable<NhlPlayerViewModel> pwModel = await playerList.ToListAsync();
            return View("Index", pwModel);
        }

        //catch (Exception ex)
        //{
        //    throw new ArgumentNullException(ex.Message);
        //}
        //return View("Index");

    //}

    //// GET: NhlPlayers/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhlPlayer = await _context.NhlPlayer.SingleOrDefaultAsync(m => m.NhlPlayerId == id);
            if (nhlPlayer == null)
            {
                return NotFound();
            }

            return View(nhlPlayer);
        }

        // GET: NhlPlayers/Create
        public IActionResult Create()
        {
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "MakerName");
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceName");
            ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionName");
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityName");
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionShortName");
            ViewData["SeasonId"] = new SelectList(_context.Season.OrderByDescending(x => x.SeasonName), "SeasonId", "SeasonName");
            ViewData["TeamId"] = new SelectList(_context.Team.Where(x => x.LeagueId == 1).ToList(), "TeamId", "TeamName");
            ViewData["Team"] = BusinessLayer.teamMessage;

            return View();
        }

        // POST: NhlPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhlPlayerId,CardManufactureId,ConferenceId,DivisionId,ISActive,ISSigned,ImageId,LeagueId,NationalityId,NhlPlayerCardId,PlayerAddDate,PlayerFirstName,PlayerImage,PlayerJersyNumber,PlayerLastName,PositionId,SeasonId,TeamId,TeamImageId,Value")] NhlPlayer nhlPlayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhlPlayer);
                string ext = ImageCreator._fileExtension;
                string tempFileName = string.Format("{0}_{1}_{2}.", nhlPlayer.NhlPlayerCardId, nhlPlayer.PlayerFirstName, nhlPlayer.PlayerLastName) + ext;
                var fileName = tempFileName.Replace(" ", "_");
                ImageCreator._fileName = fileName;
                string league = await _context.League.Where(x => x.LeagueId == 1).Select(x => x.LeagueName).FirstOrDefaultAsync();                
                string team = await _context.Team.Where(x => x.TeamId == nhlPlayer.TeamId).Select(x => x.TeamName).FirstOrDefaultAsync();
                string tmpYear = await _context.Season.Where(x => x.SeasonId == nhlPlayer.SeasonId).Select(x => x.SeasonName).FirstOrDefaultAsync();
                string year = tmpYear.Replace(" - ", "_");
                string tmpPosition = await _context.Position.Where(x => x.PositionId == nhlPlayer.PositionId).Select(x => x.PositionShortName).FirstOrDefaultAsync();
                string position = tmpPosition.Replace(" - ", "_");
                nhlPlayer.PlayerAddDate = DateTime.Now;
                await _context.SaveChangesAsync();
                await NewImage(nhlPlayer, fileName, league, team, year, position);
                //await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", nhlPlayer.CardManufactureId);
            //ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceId", nhlPlayer.ConferenceId);
            //ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionId", nhlPlayer.DivisionId);
            //ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", nhlPlayer.NationalityId);
            //ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", nhlPlayer.PositionId);
            //ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", nhlPlayer.SeasonId);
            //ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", nhlPlayer.TeamId);
            return View(nhlPlayer);
        }

        public async Task<IActionResult> NewImage(NhlPlayer nhlPlayer, string fileName, string leauge, string team, string year, string position)
        {
            _root = _hostEnvironment.WebRootPath;
            string imgPath = string.Format("images/{0}/{1}/{2}/{3}/{4}_{5}/", leauge, team, year, position, nhlPlayer.PlayerFirstName, nhlPlayer.PlayerLastName);
            imgPath.Replace(" ", "_");
            var uploads = _root + "/" + imgPath;
            ImageCreator._uploads = uploads;
            Directory.CreateDirectory(uploads);
            var img = new Image
            {
                ImageName = fileName,
                ImagePath = imgPath,
                PlayerId = nhlPlayer.NhlPlayerId
            };

            _context.Add(img);
            await _context.SaveChangesAsync();
            nhlPlayer.ImageId = img.ImageId;
            nhlPlayer.LeagueId = 1;
            nhlPlayer.TeamImageId = await _context.TeamImage.Where(x => x.TeamId == nhlPlayer.TeamId).Select(x => x.TeamImageId).FirstOrDefaultAsync();
            _context.Update(nhlPlayer);
            await _context.SaveChangesAsync();
            var createImage = new ImageCreator();
            createImage.ImageCreate();
            return View("Index");
        }

        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> AddTeam([Bind("TeamId, TeamName, LeagueId, TeamImageId")] Team team, BusinessLayer bLL)
        {
            if (ModelState.IsValid)
            {
                await bLL.AddTeam(_context, team);
                return RedirectToAction("Create");
            }
            return View(team);
        }

        [HttpPost]
        public string ImageData(string imageData)
        {
            var data = new ImageCreator();
            data.ImageData(imageData);
            return imageData;
        }

        // GET: NhlPlayers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhlPlayer = await _context.NhlPlayer.SingleOrDefaultAsync(m => m.NhlPlayerId == id);
            if (nhlPlayer == null)
            {
                return NotFound();
            }
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", nhlPlayer.CardManufactureId);
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceId", nhlPlayer.ConferenceId);
            ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionId", nhlPlayer.DivisionId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", nhlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", nhlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", nhlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", nhlPlayer.TeamId);
            return View(nhlPlayer);
        }

        // POST: NhlPlayers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NhlPlayerId,CardManufactureId,ConferenceId,DivisionId,ISActive,ISSigned,ImageId,LeagueId,NationalityId,NhlPlayerCardId,PlayerAddDate,PlayerFirstName,PlayerImage,PlayerJersyNumber,PlayerLastName,PositionId,SeasonId,TeamId,TeamImageId,Value")] NhlPlayer nhlPlayer)
        {
            if (id != nhlPlayer.NhlPlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhlPlayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhlPlayerExists(nhlPlayer.NhlPlayerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CardManufactureId"] = new SelectList(_context.CardManufacture, "CardManufactureId", "CardManufactureId", nhlPlayer.CardManufactureId);
            ViewData["ConferenceId"] = new SelectList(_context.Conference, "ConferenceId", "ConferenceId", nhlPlayer.ConferenceId);
            ViewData["DivisionId"] = new SelectList(_context.Division, "DivisionId", "DivisionId", nhlPlayer.DivisionId);
            ViewData["NationalityId"] = new SelectList(_context.Nationality, "NationalityId", "NationalityId", nhlPlayer.NationalityId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", nhlPlayer.PositionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "SeasonId", "SeasonId", nhlPlayer.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "TeamId", nhlPlayer.TeamId);
            return View(nhlPlayer);
        }

        // GET: NhlPlayers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhlPlayer = await _context.NhlPlayer.SingleOrDefaultAsync(m => m.NhlPlayerId == id);
            if (nhlPlayer == null)
            {
                return NotFound();
            }

            return View(nhlPlayer);
        }

        // POST: NhlPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhlPlayer = await _context.NhlPlayer.SingleOrDefaultAsync(m => m.NhlPlayerId == id);
            _context.NhlPlayer.Remove(nhlPlayer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool NhlPlayerExists(int id)
        {
            return _context.NhlPlayer.Any(e => e.NhlPlayerId == id);
        }
    }
}
