//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace losk_3.BasaSQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaxReporting
    {
        public int ReportID { get; set; }
        public Nullable<int> SubscriberID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> ReportDate { get; set; }
    
        public virtual Subscribers Subscribers { get; set; }
    }
}