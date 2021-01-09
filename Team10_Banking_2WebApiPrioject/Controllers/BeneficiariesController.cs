using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team10_Banking_2WebApiPrioject.Models;
using System.Web.Http.Cors;

namespace Team10_Banking_2WebApiPrioject.Controllers
{
    [EnableCors(origins: "http://localhost:4200", methods: "*", headers: "*")]
    public class BeneficiariesController : ApiController
    {
        dbBankEntities2 entities = new dbBankEntities2();
        [HttpGet]
        public HttpResponseMessage GetBenificiaries()
        {
            List<tblBeneficiary> beneficiaries = new List<tblBeneficiary>();
            beneficiaries = entities.tblBeneficiaries.ToList();
            if (beneficiaries == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Added Benificiaries");
            }
            else
            {
                return Request.CreateResponse<List<tblBeneficiary>>(HttpStatusCode.OK, beneficiaries);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddBenificiary(tblBeneficiary beneficiary)
        {
            tblBalance balance = entities.tblBalances.Where(b => b.account_number == beneficiary.account_number).FirstOrDefault();
            if (beneficiary == null)
            {
                entities.tblBeneficiaries.Add(beneficiary);
                entities.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, beneficiary);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Benificiary already exists");
            }
            
        }
    }
}
