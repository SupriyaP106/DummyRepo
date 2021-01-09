﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Team10_Banking_2WebApiPrioject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class dbBankEntities2 : DbContext
    {
        public dbBankEntities2()
            : base("name=dbBankEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbladmin> tbladmins { get; set; }
        public virtual DbSet<tblBankingCustomer> tblBankingCustomers { get; set; }
        public virtual DbSet<tblBeneficiary> tblBeneficiaries { get; set; }
        public virtual DbSet<tblCustomer> tblCustomers { get; set; }
        public virtual DbSet<tblNetBanking> tblNetBankings { get; set; }
        public virtual DbSet<tblTransaction> tblTransactions { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tblBalance> tblBalances { get; set; }
    
        public virtual int sp_transact(Nullable<int> cust_id, string mode, Nullable<int> from_acnt, Nullable<int> to_acnt, Nullable<double> from_acnt_bal, Nullable<double> to_acnt_bal, Nullable<double> amount, Nullable<System.DateTime> trans_date, string remarks)
        {
            var cust_idParameter = cust_id.HasValue ?
                new ObjectParameter("cust_id", cust_id) :
                new ObjectParameter("cust_id", typeof(int));
    
            var modeParameter = mode != null ?
                new ObjectParameter("mode", mode) :
                new ObjectParameter("mode", typeof(string));
    
            var from_acntParameter = from_acnt.HasValue ?
                new ObjectParameter("from_acnt", from_acnt) :
                new ObjectParameter("from_acnt", typeof(int));
    
            var to_acntParameter = to_acnt.HasValue ?
                new ObjectParameter("to_acnt", to_acnt) :
                new ObjectParameter("to_acnt", typeof(int));
    
            var from_acnt_balParameter = from_acnt_bal.HasValue ?
                new ObjectParameter("from_acnt_bal", from_acnt_bal) :
                new ObjectParameter("from_acnt_bal", typeof(double));
    
            var to_acnt_balParameter = to_acnt_bal.HasValue ?
                new ObjectParameter("to_acnt_bal", to_acnt_bal) :
                new ObjectParameter("to_acnt_bal", typeof(double));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("amount", amount) :
                new ObjectParameter("amount", typeof(double));
    
            var trans_dateParameter = trans_date.HasValue ?
                new ObjectParameter("trans_date", trans_date) :
                new ObjectParameter("trans_date", typeof(System.DateTime));
    
            var remarksParameter = remarks != null ?
                new ObjectParameter("remarks", remarks) :
                new ObjectParameter("remarks", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_transact", cust_idParameter, modeParameter, from_acntParameter, to_acntParameter, from_acnt_balParameter, to_acnt_balParameter, amountParameter, trans_dateParameter, remarksParameter);
        }
    
        public virtual ObjectResult<sp_DisplayTransaction_Result> sp_DisplayTransaction(Nullable<int> trans_id)
        {
            var trans_idParameter = trans_id.HasValue ?
                new ObjectParameter("trans_id", trans_id) :
                new ObjectParameter("trans_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_DisplayTransaction_Result>("sp_DisplayTransaction", trans_idParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_SelectTransactionId(Nullable<int> acnt_no)
        {
            var acnt_noParameter = acnt_no.HasValue ?
                new ObjectParameter("acnt_no", acnt_no) :
                new ObjectParameter("acnt_no", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_SelectTransactionId", acnt_noParameter);
        }
    }
}