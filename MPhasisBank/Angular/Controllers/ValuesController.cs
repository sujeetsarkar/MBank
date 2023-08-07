using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using DALayer;

namespace Angular.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<SavingTransactionsEntity> Get(string pId, DateTime pFromDate, DateTime pToDate)
        {
            GeneralDAL _grnDAL = new GeneralDAL();

            IEnumerable<SavingTransactionsEntity> li;
            li = _grnDAL.GetSavingsTransaction(pId, pFromDate, pToDate);
            return li;
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
