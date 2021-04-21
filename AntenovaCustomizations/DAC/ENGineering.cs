using System;
using AntenovaCustomizations.Descriptor;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GDPR;
using PX.Objects.IN;

namespace AntenovaCustomizations.DAC
{
    [Serializable]
    [PXCacheName("ENGineering")]
    public class ENGineering : IBqlTable
    {
        #region EngrNbr
        [PXDefault]
        [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
        [PXUIField(DisplayName = "Engineering Nbr", Required = true)]
        [AutoNumber(typeof(ENGSetup.eNGSequenceID), typeof(AccessInfo.businessDate))]
        [PXSelector(typeof(SearchFor<ENGineering.engrNbr>),
                    typeof(ENGineering.engrNbr),
                    typeof(ENGineering.opprid),
                    typeof(ENGineering.description),
                    typeof(ENGineering.status),
                    typeof(ENGineering.prjtype))]
        public virtual string EngrNbr { get; set; }
        public abstract class engrNbr : PX.Data.BQL.BqlString.Field<engrNbr> { }
        #endregion

        #region Prjtype
        [PXDefault]
        [PXStringList]
        [PXDBString(15, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Project Type", Required = true)]
        public virtual string Prjtype { get; set; }
        public abstract class prjtype : PX.Data.BQL.BqlString.Field<prjtype> { }
        #endregion

        #region ENGRef
        [PXDBString(25,IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Egnineering Ref.",Required = true)]
        public virtual string Engref { get; set; }
        public abstract class engref : PX.Data.BQL.BqlString.Field<engref> { }
        #endregion

        #region Status
        [PXDBString(10)]
        [PXDefault("1")]
        [PXUIField(DisplayName = "Status",Enabled = false)]
        [GetDropDownAttribute("ENGSTATUS",true)]
        public virtual string Status { get; set; }
        public abstract class status : PX.Data.BQL.BqlString.Field<status> { }
        #endregion

        #region Opprid
        [PXDBString(20, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Opportunity Nbr")]
        public virtual string Opprid { get; set; }
        public abstract class opprid : PX.Data.BQL.BqlString.Field<opprid> { }
        #endregion

        #region Description
        [PXDefault]
        [PXDBString(1000, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Description", Required = true)]
        public virtual string Description { get; set; }
        public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
        #endregion

        #region Priority
        [PXDefault]
        [GetDropDownAttribute("PRIOLVL")]
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Priority Level", Required = true)]
        public virtual string Priority { get; set; }
        public abstract class priority : PX.Data.BQL.BqlString.Field<priority> { }
        #endregion

        #region GateStatus
        [GetDropDownAttribute("GATESTATUS")]
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Project Gate Status")]
        public virtual string GateStatus { get; set; }
        public abstract class gateStatus : PX.Data.BQL.BqlString.Field<gateStatus> { }
        #endregion

        #region OppBAccountID
        [PXDBInt()]
        [PXUIField(DisplayName = "Business Account",Required = true)]
        [PXSelector(typeof(SearchFor<Customer.bAccountID>),
            typeof(Customer.acctCD),
            typeof(Customer.acctName),
            typeof(Customer.type),
            typeof(Customer.classID),
            typeof(Customer.status),
            SubstituteKey = typeof(Customer.acctCD),
            DescriptionField = typeof(Customer.acctName))]
        //[CustomerAndProspect(DisplayName = "Business Account", BqlField = typeof(ENGineering.oppBAccountID))]
        public virtual int? OppBAccountID { get; set; }
        public abstract class oppBAccountID : PX.Data.BQL.BqlInt.Field<oppBAccountID> { }
        #endregion

        #region EndCust
        [PXDBString(20, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "End Customer")]
        public virtual string EndCust { get; set; }
        public abstract class endCust : PX.Data.BQL.BqlString.Field<endCust> { }
        #endregion

        #region SalesRegion
        [GetDropDownAttribute("REGION")]
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Sales Region",Required = true)]
        public virtual string SalesRegion { get; set; }
        public abstract class salesRegion : PX.Data.BQL.BqlString.Field<salesRegion> { }
        #endregion

        #region SalesPerson
        [PXDBInt]
        [PXUIField(DisplayName = "Sales Person")]
        [PXSelector(typeof(Search<PX.Objects.AR.SalesPerson.salesPersonID>),
                    typeof(PX.Objects.AR.SalesPerson.salesPersonCD),
                    typeof(PX.Objects.AR.SalesPerson.isActive),
                    typeof(PX.Objects.AR.SalesPerson.commnPct),
                    SubstituteKey = typeof(PX.Objects.AR.SalesPerson.salesPersonCD))]
        public virtual int? SalesPerson { get; set; }
        public abstract class salesPerson : PX.Data.BQL.BqlInt.Field<salesPerson> { }
        #endregion

        #region Msh
        [PXDBString(30, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "MSH Nbr")]
        public virtual string Msh { get; set; }
        public abstract class msh : PX.Data.BQL.BqlString.Field<msh> { }
        #endregion

        #region Repeat
        [GetDropDownAttribute("ENGREPEAT")]
        [PXDBString(10, IsUnicode = true, InputMask = "")]
        [PXUIField(DisplayName = "Engineer Repeat",Required = true)]
        public virtual string Repeat { get; set; }
        public abstract class repeat : PX.Data.BQL.BqlString.Field<repeat> { }
        #endregion

        #region Engineer
        [PXDBInt]
        [PXUIField(DisplayName = "Assign Engineer", Required = true)]
        [PXSelector(typeof(
            SelectFrom<EPEmployee>
            .InnerJoin<BAccount2>.On<EPEmployee.bAccountID.IsEqual<BAccount2.bAccountID>>
            .LeftJoin<Contact>.On<BAccount2.defContactID.IsEqual<Contact.contactID>>
            .SearchFor<EPEmployee.bAccountID>),
            typeof(EPEmployee.acctCD),
            typeof(EPEmployee.acctName),
            typeof(EPEmployee.routeEmails),
            typeof(Contact.eMail),
            typeof(EPEmployee.status),
            typeof(EPEmployee.classID),
            SubstituteKey = typeof(EPEmployee.acctCD),
            DescriptionField = typeof(EPEmployee.acctName))]
        public virtual int? Engineer { get; set; }
        public abstract class engineer : PX.Data.BQL.BqlInt.Field<engineer> { }
        #endregion

        #region ProductCategory
        [PXDBInt]
        [PXUIField(DisplayName = "Product Category")]
        [PXSelector(typeof(SearchFor<INItemClass.itemClassID>),
                    typeof(INItemClass.itemClassCD),
                    typeof(INItemClass.descr),
                    SubstituteKey = typeof(INItemClass.itemClassCD),
                    DescriptionField = typeof(INItemClass.descr))]
        public virtual int? ProductCategory { get; set; }
        public abstract class productCategory : PX.Data.BQL.BqlInt.Field<productCategory> { }

        #endregion

        #region ReveCntr
        [PXDBInt]
        [PXDefault(0)]
        public virtual int? ReveCntr { get; set; }
        public abstract class reveCntr : PX.Data.BQL.BqlInt.Field<reveCntr> { } 
        #endregion

        #region CreatedByID
        [PXDBCreatedByID()]
        public virtual Guid? CreatedByID { get; set; }
        public abstract class createdByID : PX.Data.BQL.BqlGuid.Field<createdByID> { }
        #endregion

        #region CreatedByScreenID
        [PXDBCreatedByScreenID()]
        public virtual string CreatedByScreenID { get; set; }
        public abstract class createdByScreenID : PX.Data.BQL.BqlString.Field<createdByScreenID> { }
        #endregion

        #region CreatedDateTime
        [PXDBCreatedDateTime()]
        public virtual DateTime? CreatedDateTime { get; set; }
        public abstract class createdDateTime : PX.Data.BQL.BqlDateTime.Field<createdDateTime> { }
        #endregion

        #region LastModifiedByID
        [PXDBLastModifiedByID()]
        public virtual Guid? LastModifiedByID { get; set; }
        public abstract class lastModifiedByID : PX.Data.BQL.BqlGuid.Field<lastModifiedByID> { }
        #endregion

        #region LastModifiedByScreenID
        [PXDBLastModifiedByScreenID()]
        public virtual string LastModifiedByScreenID { get; set; }
        public abstract class lastModifiedByScreenID : PX.Data.BQL.BqlString.Field<lastModifiedByScreenID> { }
        #endregion

        #region LastModifiedDateTime
        [PXDBLastModifiedDateTime()]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        public abstract class lastModifiedDateTime : PX.Data.BQL.BqlDateTime.Field<lastModifiedDateTime> { }
        #endregion

        #region Tstamp
        [PXDBTimestamp()]
        [PXUIField(DisplayName = "Tstamp")]
        public virtual byte[] Tstamp { get; set; }
        public abstract class tstamp : PX.Data.BQL.BqlByteArray.Field<tstamp> { }
        #endregion


        public class PK : PrimaryKeyOf<ENGineering>.By<engrNbr>
        {
            public static ENGineering Find(PXGraph graph, string engrNbr) => FindBy(graph, engrNbr);
        }

    }
}