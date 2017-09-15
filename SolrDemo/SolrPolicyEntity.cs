using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolrNet.Attributes;

namespace SolrDemo
{
    public class SolrPolicyEntity
    {
        [SolrUniqueKey("PolicyID")]
        public long PolicyID { get; set; }

        [SolrField("PolicyGroupID")]
        public long PolicyGroupID { get; set; }

        [SolrField("PolicyOperatorID")]
        public long PolicyOperatorID { get; set; }

        [SolrField("PolicyOperatorName")]
        public string PolicyOperatorName { get; set; }

        [SolrField("PolicyCode")]
        public string PolicyCode { get; set; }

        [SolrField("PolicyName")]
        public string PolicyName { get; set; }

        [SolrField("PolicyType")]
        public string PolicyType { get; set; }

        [SolrField("TicketType")]
        public int TicketType { get; set; }

        [SolrField("FlightType")]
        public int FlightType { get; set; }

        [SolrField("DepartureDate")]
        public DateTime DepartureDate { get; set; }

        [SolrField("ArrivalDate")]
        public DateTime ArrivalDate { get; set; }

        [SolrField("ReturnDepartureDate")]
        public DateTime ReturnDepartureDate { get; set; }

        [SolrField("ReturnArrivalDate")]
        public DateTime ReturnArrivalDate { get; set; }

        [SolrField("DepartureCityCodes")]
        public string DepartureCityCodes { get; set; }

        [SolrField("TransitCityCodes")]
        public string TransitCityCodes { get; set; }

        [SolrField("ArrivalCityCodes")]
        public string ArrivalCityCodes { get; set; }

        [SolrField("OutTicketType")]
        public int OutTicketType { get; set; }

        [SolrField("OutTicketStart")]
        public DateTime OutTicketStart { get; set; }

        [SolrField("OutTicketEnd")]
        public DateTime OutTicketEnd { get; set; }

        [SolrField("OutTicketPreDays")]
        public int OutTicketPreDays { get; set; }

        [SolrField("Remark")]
        public string Remark { get; set; }

        [SolrField("Status")]
        public int Status { get; set; }

        [SolrField("SolrUpdatedTime")]
        public DateTime SolrUpdatedTime { get; set; }
    }
}
