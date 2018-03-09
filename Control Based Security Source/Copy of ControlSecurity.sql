SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[WorkPhone] [varchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Controls]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Controls](
	[Page] [varchar](50) NOT NULL,
	[ControlID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ControlPermissions] PRIMARY KEY CLUSTERED 
(
	[Page] ASC,
	[ControlID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersToRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersToRoles](
	[FKUserID] [int] NOT NULL,
	[FKRoleID] [int] NOT NULL,
 CONSTRAINT [PK_UsersToRoles] PRIMARY KEY CLUSTERED 
(
	[FKUserID] ASC,
	[FKRoleID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ControlsToRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ControlsToRoles](
	[FKRole] [int] NOT NULL,
	[FKPage] [varchar](50) NOT NULL,
	[FKControlID] [varchar](50) NOT NULL,
	[Invisible] [int] NOT NULL,
	[Disabled] [int] NOT NULL,
 CONSTRAINT [PK_PermissionsToRoles] PRIMARY KEY CLUSTERED 
(
	[FKRole] ASC,
	[FKPage] ASC,
	[FKControlID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spInsertNewControlToRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



Create PROCEDURE [dbo].[spInsertNewControlToRole]
@RoleID int,
@PageName varchar(50),
@ControlID varchar(50),
@invisible int,
@disabled int
AS
BEGIN
Begin Transaction
	
	if not exists (select * from Controls where Page = @PageName and ControlID = @ControlID)
		Insert into Controls (Page, ControlID) values (@PageName, @ControlID)
	if @@Error <> 0 goto ErrorHandler

	insert into ControlsToRoles (FKRole, FKPage, FKControlID, invisible, disabled) 
		values (@RoleID, @PageName, @ControlID, @invisible, @disabled)

	if @@Error <> 0 goto ErrorHandler

	commit transaction
	return

ErrorHandler:
	rollback transaction 
	return 

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spInsertNewUserInRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[spInsertNewUserInRole]
	@UserID int,
	@RoleID int
AS
BEGIN
	insert into usersToRoles (FKUserID, FKRoleID) values (@USERID, @RoleID)
END
' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersToRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersToRoles]'))
ALTER TABLE [dbo].[UsersToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersToRoles_Roles] FOREIGN KEY([FKRoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersToRoles_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersToRoles]'))
ALTER TABLE [dbo].[UsersToRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersToRoles_Users] FOREIGN KEY([FKUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PermissionsToRoles_ControlPermissions]') AND parent_object_id = OBJECT_ID(N'[dbo].[ControlsToRoles]'))
ALTER TABLE [dbo].[ControlsToRoles]  WITH CHECK ADD  CONSTRAINT [FK_PermissionsToRoles_ControlPermissions] FOREIGN KEY([FKPage], [FKControlID])
REFERENCES [dbo].[Controls] ([Page], [ControlID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PermissionsToRoles_Roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[ControlsToRoles]'))
ALTER TABLE [dbo].[ControlsToRoles]  WITH CHECK ADD  CONSTRAINT [FK_PermissionsToRoles_Roles] FOREIGN KEY([FKRole])
REFERENCES [dbo].[Roles] ([RoleID])
