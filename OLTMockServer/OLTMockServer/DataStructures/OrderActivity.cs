using OLTMockServer.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer
{
    [Serializable]
    public class OrderActivity
    {
        //
        //Does we need to keep the instance of order used for each activity in the activity object? //for debug perpuses
        //

        public Order OrderInstance { get; set; }

        /// <summary>
        /// When action command issued?
        /// </summary>
        public DateTime ActivityDate { get; set; }


        public Definitions.OrderActivityTypes ActivityType { get; set; }

        /// <summary>
        /// It is a automatic action or manual action
        /// </summary>
        public bool IsCreatedByAuto { get; set; }

        /// <summary>
        /// When action take done
        /// </summary>
        public DateTime ProcessDate { get; set; }

        /// <summary>
        /// When we tried to perform this action?
        /// </summary>
        public DateTime LastTryDate { get; set; }

        /// <summary>
        /// What happened durring the last process?
        /// </summary>
        public string LastTryExceptionMessage { get; set; }

        /// <summary>
        /// How many times we tried to process this activity?
        /// </summary>
        public int TryCount { get; set; }

        public bool IsDone
        {
            get
            {
                return ProcessDate != DateTime.MinValue;
            }
        }

        public override string ToString()
        {
            return $"{this.ActivityType}-{this.TryCount}/{Definitions.Order_Max_Activity_Try_Count} ({(IsDone ? "Done" : (TryCount>0?  "Failed": "New"))})";
        }
    }
}
