using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Soccer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Controllers
{
  public class TeamsController : Controller
  {
    private readonly SoccerContext _db;

    public TeamsController(SoccerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Team> model = _db.Teams.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Team team)
    {
      _db.Teams.Add(team);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTeam = _db.Teams
        .Include(team => team.JoinEntities)
        .ThenInclude(join => join.Player)
        .FirstOrDefault(team => team.TeamId == id);
      return View(thisTeam);
    }

    [HttpPost]
    public ActionResult Edit(Team team)
    {
      _db.Entry(team).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTeam = _db.Teams.FirstOrDefault(team => team.TeamId == id);
      return View(thisTeam);
    }
    [HttpPost, ActionName("Delete")]

    public ActionResult DeleteConfirmed(int id)
    {
      var thisTeam = _db.Teams.FirstOrDefault(team => team.TeamId == id);
      _db.Teams.Remove(thisTeam);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}