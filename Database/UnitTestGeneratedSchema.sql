
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD2354E7F5AE7A3ED]') AND parent_object_id = OBJECT_ID('Links'))
alter table Links  drop constraint FKD2354E7F5AE7A3ED


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD2354E7FC11127C4]') AND parent_object_id = OBJECT_ID('Links'))
alter table Links  drop constraint FKD2354E7FC11127C4


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK171E697E5AE7A3ED]') AND parent_object_id = OBJECT_ID('Pages'))
alter table Pages  drop constraint FK171E697E5AE7A3ED


    if exists (select * from dbo.sysobjects where id = object_id(N'CrawlerSessions') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CrawlerSessions

    if exists (select * from dbo.sysobjects where id = object_id(N'Links') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Links

    if exists (select * from dbo.sysobjects where id = object_id(N'Pages') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Pages

    create table CrawlerSessions (
        CrawlerSessionId INT IDENTITY NOT NULL,
       StartUrl NVARCHAR(255) null,
       DateTime DATETIME null,
       Title NVARCHAR(255) null,
       SettingsSearchDepth INT null,
       SettingsMaxMemoryConsumption BIGINT null,
       SettingsBatchSize BIGINT null,
       primary key (CrawlerSessionId)
    )

    create table Links (
        LinkId INT IDENTITY NOT NULL,
       FromUrl NVARCHAR(255) null,
       ToUrl NVARCHAR(255) null,
       CrawlerSessionId INT null,
       PageId INT null,
       primary key (LinkId)
    )

    create table Pages (
        PageId INT IDENTITY NOT NULL,
       InnerHtml NVARCHAR(MAX) null,
       CrawlerSessionId INT null,
       primary key (PageId)
    )

    alter table Links 
        add constraint FKD2354E7F5AE7A3ED 
        foreign key (CrawlerSessionId) 
        references CrawlerSessions

    alter table Links 
        add constraint FKD2354E7FC11127C4 
        foreign key (PageId) 
        references Pages

    alter table Pages 
        add constraint FK171E697E5AE7A3ED 
        foreign key (CrawlerSessionId) 
        references CrawlerSessions
