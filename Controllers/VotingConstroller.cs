using System.Linq;
using AndriiCoursework.DbContext;
using AndriiCoursework.Models.Static;
using AndriiCoursework.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AndriiCoursework.Controllers
{
    [Authorize]
    [Route("api/polls")]
    public class VotingConstroller : Controller
    {
        private readonly ApplicationDbContext _appDbContext;

        public VotingConstroller(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("candidates")]
        private IActionResult GetAllCandidates()
        {
            if (!_appDbContext.Candidates.Any())
            {
                var dbModels = StaticData.CandidatesList;
                _appDbContext.Candidates.AddRange(dbModels);
                _appDbContext.SaveChanges();
            }

            dynamic cadidates = _appDbContext.Candidates.Select(candidate =>
                new CandidateViewModel
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    Info = candidate.Info,
                    Id = candidate.Id
                });

            return new OkObjectResult(cadidates);
        }
    }
}