using HeroesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HeroesServer.Controllers
{
    [Authorize]
    public class AlerteController : ApiController
    {

        /*
        {
            IdInitiateur:"12312",
            IdRepondant: null,
            Position:{
                        Latitude:1213.232,
                        Longitude:32323.232,
                        LastUpdate:2017-02-04 12:00:00
                     }
            Gravite: 0,
            Type: 1
        }
            */
        [HttpPost]
        public bool CreateAlert([FromBody] Alerte alerte)
        {

            return true;
        }

        [HttpPost]
        public bool FindNewAlert([FromBody] Alerte alerte)
        {

            return true;
        }

        [HttpPost]
        public bool IsAlertAnswered([FromBody] Alerte alerte)
        {

            return true;
        }

        [HttpPost]
        public bool GetHelperDistance([FromBody] Alerte alerte)
        {

            return true;
        }
    }
}
