using JassCoffeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JassCoffeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssortmentController : ControllerBase
    {
        private ApplicationContext Database;
        public AssortmentController(ApplicationContext db)
        {
            Database = db;
        }
        
        [HttpGet]
        public IEnumerable<Card> Get()
        {

            List<Card> test = Database.Cards.Select(x=>x).ToList();
           
            return test;
        }
       
       
}}
