//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinemaTicketSystemDisplayApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Screen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Screen()
        {
            this.Screenings = new HashSet<Screening>();
        }
    
        public int Id { get; set; }
        public int Number { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Screening> Screenings { get; set; }
    }
}
