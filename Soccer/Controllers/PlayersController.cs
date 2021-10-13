using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Soccer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Soccer.Controllers
{
  public class PlayersController : Controller
  {
    private readonly SoccerContext _db;

    public PlayersController(SoccerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Players.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.TeamId = new SelectList(_db.Teams, "TeamId", "TeamName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Player player, int TeamId)
    {
      _db.Players.Add(player);
      _db.SaveChanges();
      if (TeamId != 0)
      {
        _db.PlayerTeam.Add(new PlayerTeam() {TeamId = TeamId, PlayerId = player.PlayerId});
        // _db.StudentCourse.Add(new StudentCourse() { CourseId = CourseId, StudentId = student.StudentId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisPlayer = _db.Players
      .Include(player => player.JoinEntities)
      .ThenInclude(join => join.Team)
      .FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    public ActionResult Edit(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      ViewBag.TeamId = new SelectList(_db.Teams, "TeamId", "TeamName");
      return View(thisPlayer);
    }

    [HttpPost]
    public ActionResult Edit(Player player, int TeamId)
    {
      if (TeamId != 0)
      {
        _db.PlayerTeam.Add(new PlayerTeam() { TeamId = TeamId, PlayerId = player.PlayerId });
      }
      _db.Entry(player).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTeam(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      ViewBag.TeamId = new SelectList(_db.Teams, "TeamId", "TeamName");
      return View(thisPlayer);
    }

    [HttpPost]
    public ActionResult AddTEam(Player player, int TeamId)
    {
      if (TeamId != 0)
      {
      _db.PlayerTeam.Add(new PlayerTeam() { TeamId = TeamId, PlayerId = player.PlayerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      _db.Players.Remove(thisPlayer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteTeam(int joinId)
    {
      var joinEntry = _db.PlayerTeam.FirstOrDefault(entry => entry.PlayerTeamId == joinId);
      _db.PlayerTeam.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}