--[1] Table: Notice(공지사항) 테이블
CREATE TABLE [dbo].[Notices]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),	-- Serial Number
	[ParentId] Int Null,							-- ParentId
	[Name] NVarChar(100) NOT Null,					-- 작성자
	[Title] NVarChar(255) NOT Null,					-- 제목
	[Contents] NVarChar(Max) Null,					-- 내용
	[IsPinned] Bit Null Default(0),					-- 공지글로 올리기
	[CreatedBy] NVarChar(255) Null,					-- 등록자
	[Created]DateTime Default(GetDate()),			-- 생성일
	[ModifiedBy] NVarChar(255) Null,				-- 수정자
	[Modified] DateTime Null,						-- 수정일
)
Go