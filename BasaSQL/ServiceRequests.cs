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
    
    public partial class ServiceRequests
    {
        public int RequestID { get; set; }
        public Nullable<int> SubscriberID { get; set; }
        public Nullable<int> ServiceID { get; set; }
        public Nullable<int> TechnicianID { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public string Status { get; set; }
    
        public virtual Services Services { get; set; }
        public virtual Subscribers Subscribers { get; set; }
        public virtual Technicians Technicians { get; set; }
    }
}
