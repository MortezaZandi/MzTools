﻿using OLTMockServer.DataStructures;
using OLTMockServer.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer
{
    public class Definitions
    {
        public delegate void OrderProcessingFeedbackEventHandler(Order order, OrderProcessingSteps processingStep, int totalSteps, int doneSteps, OrderActivityTypes orderActivity = OrderActivityTypes.None, Exception exception = null);
        public delegate void OnTestStausChangedEventHandler(TestContainerControl testContainer, int totalSteps, int doneSteps);

        public const int Order_Max_Activity_Try_Count = 15;
        public const string Order_Status_Unknown = "Unknown";
        public const string Order_Status_NotSend = "NotSend";
        public const string Order_Status_Sent = "Sent";
        public const string Order_Status_AckDone = "AckDone";
        public const string Order_Status_PickDone = "PickDone";
        public const string Order_Status_Rejected = "Rejected";
        public const string Order_Status_SendFailed = "SendFailed";


        public const string Query_Name_ReadItemsFromCMSDB = "ReadItemsFromCMSDB";


        public enum APINames
        {
            NewOrder,
        }

        public enum KnownOnlineShops
        {
            None,
            Snap,
            Digi,
        }

        public enum TestPlayStatuses
        {
            Stoped = 0,
            Playing = 1,
            Paused = 2,
            Finished = 0,
        }

        public enum PropertyValueGeneratorTypes
        {
            FixedValue,
            IntegerIncremental,
            DecimalIncremental,
            DateIncrementalMinute,
            DateIncrementalHour,
            DateIncrementalDay,
            RandomDate,
            NowDate,
            TodayDate,
            RandomStringName,
            RandomStringFName,
            RandomStringLName,
            RandomIntegerValue,
            RandomStringCode,
            RandomStringCode8,
            RandomStringCode6,
            RandomFixedList,
            RandomDecimal,
            RandomStringCode7,
        }

        public enum OrderActivityTypes
        {
            None = 0,
            Send = 1,
            Edit = 2,
            Reject = 3,
        }

        public enum OrderProcessingSteps
        {
            None = 0,
            PerformingOrderAcivity = 1,
            OrderAcivityPerformed = 2,
            OrderAcivityNotPerformed = 3,
            NewOrderCreated = 4,
            OrderSelectedForProcessing = 5,
            OrderProcessingError = 6,
            TestFinished = 7,
        }

        public enum OrderStatusResults
        {
            Unknown = 0,
            NotSend = 1,
            Sent = 2,
            AckDone = 3,
            PickDone = 4,
            //Rejected = 5,
            SendFailed = 6,
            RejectedByServer = 7,
            RejectedByVendor = 8,
            Accepted = 9,
        }

        public enum LogTypes
        {
            None = 0,
            Information = 1,
            Warining = 2,
            Error = 3,
            Debug = 4,
        }

        public class NotEditableAttribute : Attribute { }
    }
}
