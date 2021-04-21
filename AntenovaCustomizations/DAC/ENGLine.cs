using System;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.SO;

namespace AntenovaCustomizations.DAC
{
    [Serializable]
    [PXCacheName("ENGLine")]
    public class ENGLine : IBqlTable
    {

        #region FK
        public class FK
        {
            public class Nbr : ENGineering.PK.ForeignKeyOf<ENGLine>.By<engrNbr> { }
        }
        #endregion

        #region EngrNbr
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Engr Nbr")]
        [PXParent(typeof(FK.Nbr))]
        [PXDBDefault(typeof(ENGineering.engrNbr))]
        public virtual string EngrNbr { get; set; }
        public abstract class engrNbr : PX.Data.BQL.BqlString.Field<engrNbr> { }
        #endregion

        #region DeviceRcvDate
        [PXDBDate(InputMask = "g",PreserveTime = true)]
        [PXUIField(DisplayName = "Device Rcv Date")]
        public virtual DateTime? DeviceRcvDate { get; set; }
        public abstract class deviceRcvDate : PX.Data.BQL.BqlDateTime.Field<deviceRcvDate> { }
        #endregion

        #region RequstRcvDate
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Eng. Request from Receive Date")]
        public virtual DateTime? RequstRcvDate { get; set; }
        public abstract class requstRcvDate : PX.Data.BQL.BqlDateTime.Field<requstRcvDate> { }
        #endregion

        #region IsGerber
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "Gerber Review(Y/N)")]
        public virtual bool? IsGerber { get; set; }
        public abstract class isGerber : PX.Data.BQL.BqlBool.Field<isGerber> { }
        #endregion

        #region GerberNbr
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Gerber Review No.")]
        public virtual string GerberNbr { get; set; }
        public abstract class gerberNbr : PX.Data.BQL.BqlString.Field<gerberNbr> { }
        #endregion

        #region EstStar
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Est. Star Date")]
        public virtual DateTime? EstStar { get; set; }
        public abstract class estStar : PX.Data.BQL.BqlDateTime.Field<estStar> { }
        #endregion

        #region EstComplete
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Est Complete Date")]
        public virtual DateTime? EstComplete { get; set; }
        public abstract class estComplete : PX.Data.BQL.BqlDateTime.Field<estComplete> { }
        #endregion

        #region ActStart
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Actual Start Date")]
        public virtual DateTime? ActStart { get; set; }
        public abstract class actStart : PX.Data.BQL.BqlDateTime.Field<actStart> { }
        #endregion

        #region ActComplete
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Actual Complete Date")]
        public virtual DateTime? ActComplete { get; set; }
        public abstract class actComplete : PX.Data.BQL.BqlDateTime.Field<actComplete> { }
        #endregion

        #region EngrNote
        [PXDBString(1000, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Engineer Note")]
        public virtual string EngrNote { get; set; }
        public abstract class engrNote : PX.Data.BQL.BqlString.Field<engrNote> { }
        #endregion

        #region CompleteSummary
        [PXDBString(1000, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Complete Report")]
        public virtual string CompleteSummary { get; set; }
        public abstract class completeSummary : PX.Data.BQL.BqlString.Field<completeSummary> { }
        #endregion

        #region Awaitdate From
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Await date From ")]
        public virtual DateTime? AwaitdateFrom { get; set; }
        public abstract class awaitdateFrom : PX.Data.BQL.BqlDateTime.Field<awaitdateFrom> { }
        #endregion

        #region Awaitdate TO
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Await date To")]
        public virtual DateTime? AwaitdateTo { get; set; }
        public abstract class awaitdateTo : PX.Data.BQL.BqlDateTime.Field<awaitdateTo> { }
        #endregion

        #region AwaitReason
        [PXDBString(1000, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Await Reason")]
        public virtual string AwaitReason { get; set; }
        public abstract class awaitReason : PX.Data.BQL.BqlString.Field<awaitReason> { }
        #endregion

        #region OnholdDate
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "ON Hold Date")]
        public virtual DateTime? OnholdDate { get; set; }
        public abstract class onholdDate : PX.Data.BQL.BqlDateTime.Field<onholdDate> { }
        #endregion

        #region OnholdReason
        [PXDBString(1000, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "ON Hold Reason")]
        public virtual string OnholdReason { get; set; }
        public abstract class onholdReason : PX.Data.BQL.BqlString.Field<onholdReason> { }
        #endregion

        #region CloseDate
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Close Date")]
        public virtual DateTime? CloseDate { get; set; }
        public abstract class closeDate : PX.Data.BQL.BqlDateTime.Field<closeDate> { }
        #endregion

        #region CloseReason
        [PXDBString(1000, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Close Reason")]
        public virtual string CloseReason { get; set; }
        public abstract class closeReason : PX.Data.BQL.BqlString.Field<closeReason> { }
        #endregion

        #region RecycleDate
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Recycle Date")]
        public virtual DateTime? RecycleDate { get; set; }
        public abstract class recycleDate : PX.Data.BQL.BqlDateTime.Field<recycleDate> { }
        #endregion

        #region RecyleReason
        [PXDBString(1000, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Recyle Reason")]
        public virtual string RecyleReason { get; set; }
        public abstract class recyleReason : PX.Data.BQL.BqlString.Field<recyleReason> { }
        #endregion

        #region IsNRE
        [PXDBBool]
        [PXDefault(false)]
        [PXUIField(DisplayName = "NRE(Yes/No)")]
        public virtual bool? IsNre { get; set; }
        public abstract class isNre : PX.Data.BQL.BqlBool.Field<isNre> { }
        #endregion

        #region SONbr
        [PXDBString(15)]
        [PXUIField(DisplayName = "ERP SO Nbr.")]
        [PXSelector(typeof(Search<SOOrder.orderNbr>), ValidateValue = false)]
        public virtual string SONbr { get; set; }
        public abstract class sONbr : PX.Data.BQL.BqlString.Field<sONbr> { }
        #endregion

        #region ProcessDate
        [PXDBDate(DisplayMask = "d")]
        [PXUIField(DisplayName = "Process Date", Enabled = false)]
        public virtual DateTime? ProcessDate { get; set; }
        public abstract class processDate : PX.Data.BQL.BqlDateTime.Field<processDate> { }
        #endregion

        #region EngrAgeDays
        [PXInt]
        [PXUIField(DisplayName = "ENg. Age in Days", Enabled = false)]
        public virtual int? EngrAgeDays { get; set; }
        public abstract class engrAgeDays : PX.Data.BQL.BqlInt.Field<engrAgeDays> { }
        #endregion

        #region CreatedByID
        [PXDBCreatedByID()]
        [PXUIField(DisplayName = "Created By")]
        public virtual Guid? CreatedByID { get; set; }
        public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
        #endregion

        #region CreatedByScreenID
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID { get; set; }
        public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
        #endregion

        #region CreatedDateTime
        [PXDBCreatedDateTime(DisplayMask = "d")]
        [PXUIField(DisplayName = "Created Time")]
        public virtual DateTime? CreatedDateTime { get; set; }
        public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
        #endregion

        #region LastModifiedByID
        [PXDBLastModifiedByID()]
        [PXUIField(DisplayName = "Modified By")]
        public virtual Guid? LastModifiedByID { get; set; }
        public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
        #endregion

        #region LastModifiedByScreenID
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID { get; set; }
        public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
        #endregion

        #region LastModifiedDateTime
        [PXDBLastModifiedDateTime(DisplayMask = "d")]
        [PXUIField(DisplayName = "Last Modified Time")]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
        #endregion

        #region Tstamp
        [PXDBTimestamp()]
        [PXUIField(DisplayName = "Tstamp")]
        public virtual byte[] Tstamp { get; set; }
        public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
        #endregion
    }
}