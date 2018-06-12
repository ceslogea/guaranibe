using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("Coin")]
    public class USD : RootCoinValues
    {
       
    }

    [Table("Coin")]
    public class EUR : RootCoinValues
    {
        
    }

    [Table("Coin")]
    public class GBP : RootCoinValues
    {
      
    }

    [Table("Coin")]
    public class ARS : RootCoinValues
    {
       
    }

    [Table("Coin")]
    public class USDT : RootCoinValues
    {
       
    }

    [Table("Coin")]
    public class CAD : RootCoinValues
    {
      
    }

    [Table("Coin")]
    public class BTC : RootCoinValues
    {
        
    }

    public class RootCoin : BaseObject
    {
        public virtual USD USD { get; set; }
        public virtual EUR EUR { get; set; }
        public virtual GBP GBP { get; set; }
        public virtual ARS ARS { get; set; }
        public virtual USDT USDT { get; set; }
        public virtual CAD CAD { get; set; }
        public virtual BTC BTC { get; set; }
    }

    public class RootCoinValues : BaseObject
    {
        public string code { get; set; }
        public string codein { get; set; }
        public string name { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string pctChange { get; set; }
        public string open { get; set; }
        public string bid { get; set; }
        public string ask { get; set; }
        public string varBid { get; set; }
        public string timestamp { get; set; }
        public string create_date { get; set; }
    }
}
