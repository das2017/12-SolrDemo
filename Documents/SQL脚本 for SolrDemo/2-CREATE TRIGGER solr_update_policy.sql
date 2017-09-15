USE [SolrDB]
GO

CREATE TRIGGER [dbo].[TR_Solr_UPDATE_Policy] ON [dbo].[Policy] 
	FOR UPDATE, INSERT
AS 
BEGIN
   IF UPDATE(PolicyID) 
	OR UPDATE(PolicyGroupID) 
	OR UPDATE(PolicyOperatorID) 
	OR UPDATE(PolicyOperatorName) 
	OR UPDATE(PolicyCode) 
	OR UPDATE(PolicyName) 
	OR UPDATE(PolicyType) 
	OR UPDATE(TicketType) 
	OR UPDATE(FlightType) 
	OR UPDATE(DepartureDate) OR UPDATE(ArrivalDate) 
	OR UPDATE(ReturnDepartureDate) OR UPDATE(ReturnArrivalDate) 
	OR UPDATE(DepartureCityCodes) 
	OR UPDATE(TransitCityCodes) 
	OR UPDATE(ArrivalCityCodes) 
	OR UPDATE(OutTicketType)
	OR UPDATE(OutTicketStart) OR UPDATE(OutTicketEnd) 
	OR UPDATE(OutTicketPreDays) 
	OR UPDATE(Remark) 
	OR UPDATE(Status)
   BEGIN 
		UPDATE dbo.Policy
		SET SolrUpdatedTime = GETDATE()
		FROM dbo.Policy p, inserted i
		WHERE p.PolicyID = i.PolicyID
   END
END
GO


