using Microsoft.AspNetCore.Mvc.Rendering;
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
      return View(_db.Teams.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.TournamentId = new SelectList(_db.Tournaments, "TournamentId", "TournamentName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Team team, int TournamentId)
    {
      _db.Teams.Add(team);
      _db.SaveChanges();
      if (TournamentId != 0)
      {
        _db.TeamTournament.Add(new TeamTournament() {TournamentId = TournamentId, TeamId = team.TeamId});
        // _db.StudentCourse.Add(new StudentCourse() { CourseId = CourseId, StudentId = student.StudentId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTeam = _db.Teams
      .Include(team => team.JoinEntities)
      .ThenInclude(join => join.Tournament)
      .FirstOrDefault(team => team.TeamId == id);
      return View(thisTeam);
    }

    public ActionResult Edit(int id)
    {
      var thisTeam = _db.Teams.FirstOrDefault(team => team.TeamId == id);
      ViewBag.TournamentId = new SelectList(_db.Tournaments, "TournamentId", "TournamentName");
      return View(thisTeam);
    }

    [HttpPost]
    public ActionResult Edit(Team team, int TournamentId)
    {
      if (TournamentId != 0)
      {
        _db.TeamTournament.Add(new TeamTournament() { TournamentId = TournamentId, TeamId = team.TeamId });
      }
      _db.Entry(team).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTournament(int id)
    {
      var thisTeam = _db.Teams.FirstOrDefault(team => team.TeamId == id);
      ViewBag.TournamentId = new SelectList(_db.Tournaments, "TournamentId", "TournamentName");
      return View(thisTeam);
    }

    [HttpPost]
    public ActionResult AddTournament(Team team, int TournamentId)
    {
      if (TournamentId != 0)
      {
      _db.TeamTournament.Add(new TeamTournament() { TournamentId = TournamentId, TeamId = team.TeamId });
      }
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

    [HttpPost]
    public ActionResult DeleteTournament(int joinId)
    {
      var joinEntry = _db.TeamTournament.FirstOrDefault(entry => entry.TeamTournamentId == joinId);
      _db.TeamTournament.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}