USE [SolrDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Policy](
	[PolicyID] [bigint] IDENTITY(1,1) NOT NULL,	
	[PolicyGroupID] [bigint] NOT NULL,
	[PolicyOperatorID] [bigint] NOT NULL,
	[PolicyOperatorName] [varchar](50) NULL,
	[PolicyCode] [varchar](20) NOT NULL,
	[PolicyName] [varchar](50) NOT NULL,	
	[PolicyType] [varchar](50) NOT NULL,
	[TicketType] [int] NOT NULL,
	[FlightType] [int] NOT NULL,
	[DepartureDate] [datetime] NOT NULL,
	[ArrivalDate] [datetime] NOT NULL,
	[ReturnDepartureDate] [datetime] NULL,
	[ReturnArrivalDate] [datetime] NULL,
	[DepartureCityCodes] [varchar](max) NOT NULL,	
	[TransitCityCodes] [varchar](max) NULL DEFAULT (''),	
	[ArrivalCityCodes] [varchar](max) NOT NULL DEFAULT (''),
	[OutTicketType] [int] NOT NULL,
	[OutTicketStart] [datetime] NOT NULL,
	[OutTicketEnd] [datetime] NOT NULL,
	[OutTicketPreDays] [int] NOT NULL,
	[Remark] [nvarchar](400) NULL,
	[Status] [int] NOT NULL,
	[SolrUpdatedTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PolicyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'政策操作人的ID号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'PolicyOperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'政策操作人的姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'PolicyOperatorName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'政策类型：OW 单程
RT往返
多选时格式OW/RT
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'PolicyType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'票本类型：1 BSP
2 B2B
3 境外电子
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'TicketType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'航班类型：1 国内始发
2 直飞SOTO
3 国内始发Add-On
4 国外始发Add-On
5 境外到境外
6 国内始发中转
7 国外始发中转
8 境外境外境外
9 境外境内境外
10 全球到全球
11 全球中转全球
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'FlightType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出票类型：0 手动出票
1 自动出票
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'OutTicketType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出票开始日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'OutTicketStart'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'出票结束日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'OutTicketEnd'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提前开票天数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'OutTicketPreDays'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'政策的当前状态：1待审核
2 已发布
3 已挂起
4已作废
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录更新日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy', @level2type=N'COLUMN',@level2name=N'SolrUpdatedTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'政策表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Policy'
GO

