using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Soccer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Controllers
{
  public class TournamentsController : Controller
  {
    private readonly SoccerContext _db;

    public TournamentsController(SoccerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Tournament> model = _db.Tournaments.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Tournament tournament)
    {
      _db.Tournaments.Add(tournament);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTournament = _db.Tournaments
        .Include(tournament => tournament.JoinEntities)
        .ThenInclude(join => join.Team)
        .FirstOrDefault(tournament => tournament.TournamentId == id);
      return View(thisTournament);
    }

    [HttpPost]
    public ActionResult Edit(Tournament tournament)
    {
      _db.Entry(tournament).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTournament = _db.Tournaments.FirstOrDefault(tournament => tournament.TournamentId == id);
      return View(thisTournament);
    }
    [HttpPost, ActionName("Delete")]

    public ActionResult DeleteConfirmed(int id)
    {
      var thisTournament = _db.Tournaments.FirstOrDefault(tournament => tournament.TournamentId == id);
      _db.Tournaments.Remove(thisTournament);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}