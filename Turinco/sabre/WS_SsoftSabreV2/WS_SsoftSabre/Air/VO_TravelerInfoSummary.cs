using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.ValueObjects;

namespace WS_SsoftSabre.Air
{
    public class VO_TravelerInfoSummary
    {
        /// <summary>
        /// Sum of all seats required by each passenger group.
        /// </summary>
        public int IntSeatsRequested { get; set; }

        /// <summary>
        /// Passenger type groupings. This element must be repeated for each passenger group, listing its passenger types in the PassengerTypeQuantity element.
        /// </summary>
        public List<VO_Passenger> Lvo_Passenger { get; set; }
    }
}
