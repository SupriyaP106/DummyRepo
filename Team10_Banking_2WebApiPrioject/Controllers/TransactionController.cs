using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Team10_Banking_2WebApiPrioject.Models;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace Team10_Banking_2WebApiPrioject.Controllers
{
    [EnableCors(origins: "http://localhost:4200", methods:"*",headers:"*")]
    public class TransactionController : ApiController
    {
        dbBankEntities2 entities = new dbBankEntities2();

        //[HttpGet]
        //public HttpResponseMessage GetTransactions(int id)
        //{
        //    List<tblTransaction> transactions = new List<tblTransaction>();
        //    transactions = entities.tblTransactions.Where(t => t.customer_id == id).ToList();
        //    if (transactions == null)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No transactions yet");
        //    }
        //    else
        //    {
        //        return Request.CreateResponse<List<tblTransaction>>(HttpStatusCode.OK, transactions);
        //    }
        //}
        [HttpGet]
        public HttpResponseMessage GetTransaction(int id)
        {
            sp_DisplayTransaction_Result transaction = entities.sp_DisplayTransaction(id).FirstOrDefault();

            if (transaction == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid ID");
            else
                return Request.CreateResponse<sp_DisplayTransaction_Result>(transaction);
        }

        [HttpPost]
        public HttpResponseMessage InitiatePayment(tblTransaction transaction)
        {
            DbContextTransaction trans = entities.Database.BeginTransaction();
            try
            {
                transaction.customer_id = entities.tblBalances.Where(b => b.account_number == transaction.from_account).FirstOrDefault().customer_id;
                var beneficiary = entities.tblBeneficiaries.Where(b => b.account_number == transaction.from_account && b.beneficiary_account_number == transaction.to_account).FirstOrDefault();
            if (beneficiary == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Benificiary Account Number does not Exist");
            }
            else
            {
                var balance1 = entities.tblBalances.Where(b => b.customer_id == transaction.customer_id).FirstOrDefault();
                var balance2 = entities.tblBalances.Where(b => b.account_number == transaction.to_account).FirstOrDefault();
                if (balance1.balance < transaction.amount)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Insufficient Balance. Transaction Failed!");
                }
                else
                {
                    transaction.from_Account_balance = balance1.balance - transaction.amount;
                    transaction.to_Account_balance = balance2.balance + transaction.amount;
                }
                entities.sp_transact(transaction.customer_id, transaction.transaction_type, transaction.from_account, transaction.to_account,
                    transaction.from_Account_balance, transaction.to_Account_balance, transaction.amount, transaction.transaction_date, transaction.remarks);
                entities.SaveChanges();
                trans.Commit();
                    //tblTransaction transaction1 = entities.tblTransactions.OrderByDescending(t => t.transaction_id).Where(t => t.from_account == transaction.from_account).FirstOrDefault();
                return Request.CreateResponse<t(HttpStatusCode.Created);
            }
            }
            catch (Exception)
            {
                trans.Rollback();
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something Went Wrong. Unable to process your transaction!");
            }
        }
    }
}
