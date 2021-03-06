﻿using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClubAdministration.Web.ApiControllers
{
    /// <summary>
    /// API-Controller für die Abfrage von Mitgliedern
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor mit DI
        /// </summary>
        /// <param name="unitOfWork"></param>
        public MembersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Liefert alle Namen der Mitglieder
        /// </summary>
        /// <response code="200">Die Abfrage war erfolgreich.</response>
        // GET: api/Categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string[]>> GetAllMemberNames()
        => await _unitOfWork.MemberRepository
                            .GetAllNamesAsync();


        /// <summary>
        /// Spezialroute zum Abfragen von Sektionen eines Mitglieds
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{lastName}/{firstName}/sections")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string lastName, string firstName)
        {
            var sections = await _unitOfWork.MemberSectionRepository
                                            .GetSectionsByMemberName(lastName, firstName);

            if (sections == null)
            {
                return NotFound();
            }

            return Ok(sections);
        }

        [HttpGet]
        [Route("/AllMembers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Member[]>> GetAllMembers()
        => await _unitOfWork.MemberRepository
                    .GetAllAsync();
    }
}