-- create schema NBA
-- go

/* Destroy dependencies and tables*/
alter table NBA.Average_Individual_Numbers drop constraint PlayerFK;
alter table NBA.Player drop constraint TeamFK3;
alter table NBA.Player drop constraint PlayerIDFK;
alter table NBA.Ticket drop constraint GameFK;
alter table NBA.Ticket drop constraint TeamFK2;
alter table NBA.Stadium drop constraint TeamFK1;
alter table NBA.Team drop constraint CoachFK;
alter table NBA.Team drop constraint OwnerFK;
alter table NBA.Game drop constraint HomeTeamFK;
alter table NBA.Game drop constraint AwayTeamFK;
alter table NBA.Game drop constraint StadiumFK;
alter table NBA.Person drop constraint ContractFK;

drop table NBA.Average_Individual_Numbers;
drop table NBA.Player;
drop table NBA.Coach;
drop table NBA.Ticket;
drop table NBA.Stadium;
drop table NBA.Team;
drop table NBA.Game;
drop table NBA.[Contract];
drop table NBA.Person;

/* Create tables */
create table NBA.[Contract] (
    ID                      int                 not null,
    [Description]           varchar(50)         not null,
    Salary                  float               not null        check(Salary > 0), 
    [Start_Date]            date                not null,
    End_Date                date                not null,   

    check (End_Date > [Start_Date]),
    primary key (ID),
    unique ([Description])
);

create table NBA.Person (
    CCNumber                int                 not null        check(len(CCNumber) = 8),
    [Name]                  varchar(50)         not null,
    Age                     int                 not null        check(Age > 0),
    Contract_ID             int,

    primary key (CCNumber)
);

create table NBA.Team (
    ID              int                 not null,
    [Name]          varchar(50)         not null,
    City            varchar(50)         not null,
    Conference      varchar(50)         not null,
    Found_Year      int                 not null        check(Found_Year > 0),
    Owner_CCNumber  int,
    Coach_CCNumber  int,

    primary key (ID),
    unique ([Name])
);



create table NBA.Stadium (
    ID                      int                 not null,
    [Name]                  varchar(50)         not null,
    [Location]              varchar(50)         not null,
    Capacity                int                 not null        check(Capacity > 0),
    Team_ID                 int                 not null,

    primary key (ID),
    unique ([Name])
);

create table NBA.Ticket (
    [Type]                  varchar(30)         not null,
    Price                   float               not null        check(Price > 0),
    Restantes               int                 not null        check(Restantes >= 0),
    Game_ID                 int                 not null,
    Team_ID                 int                 not null
);


create table NBA.Coach (
    CCNumber                int                 not null		check(len(CCNumber) = 8),

    primary key (CCNumber)
);

create table NBA.Player (
    CCNumber                int                 not null        check(len(CCNumber) = 8),
    [Number]                int					not null		check([Number] >= 0),
    Height                  varchar(5)          not null        check(Height like '[0-9]-[0-9]'),
    [Weight]                float               not null        check([Weight] > 0),
    Position                varchar(20)         not null,
    Team_ID                 int,

    primary key (CCNumber)
);

create table NBA.Average_Individual_Numbers (
    Points                float                         check([Points] >= 0),
    Assists               float                         check([Assists] >= 0),
    Rebounds              float                         check([Rebounds] >= 0),
    Blocks                float                         check([Blocks] >= 0),
    Steals                float                         check([Steals] >= 0),
    [FG%]                 float                         check([FG%] >= 0 and [FG%] <= 100),
    [3PT%]                float                         check([3PT%] >= 0 and [3PT%] <= 100),
    Player_CCNumber       int          not null,
);

create table NBA.Game (
    ID                      int                 not null,
    [Time]                  time                not null,
    [Date]                  date                not null,
    Home_Score              int                 check(Home_Score > 0),
    Away_Score              int                 check(Away_Score > 0),
    Home_Team_ID            int                 not null,
    Away_Team_ID            int                 not null,
    Stadium_ID              int                 not null,

	check (Home_Score!=Away_Score),
    primary key (ID),
);


alter table NBA.Player add constraint PlayerIDFK foreign key (CCNumber) references NBA.Person(CCNumber);
alter table NBA.Player add constraint TeamFK3 foreign key (Team_ID) references NBA.Team(ID)
    on update cascade on delete set null;


alter table NBA.Coach add constraint CoachIDFK foreign key (CCNumber) references NBA.Person(CCNumber);


alter table NBA.Team add constraint CoachFK foreign key (Coach_CCNumber) references NBA.Coach(CCNumber)
    on update cascade on delete set null;
alter table NBA.Team add constraint OwnerFK foreign key (Owner_CCNumber) references NBA.Person(CCNumber)
    on update cascade on delete set null;


alter table NBA.Stadium add constraint TeamFK1 foreign key (Team_ID) references NBA.Team(ID)
    on update cascade on delete cascade;


alter table NBA.Ticket add constraint GameFK foreign key (Game_ID) references NBA.Game(ID)
    on update cascade on delete cascade;
alter table NBA.Ticket add constraint TeamFK2 foreign key (Team_ID) references NBA.Team(ID)
    on update cascade on delete cascade;
alter table NBA.Ticket add primary key ([Type], Game_ID);


alter table NBA.Person add constraint ContractFK foreign key (Contract_ID) references NBA.[Contract](ID)
    on update cascade on delete set null;


alter table NBA.Average_Individual_Numbers add constraint PlayerFK foreign key (Player_CCNumber) references NBA.Player(CCNumber)
    on update cascade on delete cascade;
alter table NBA.Average_Individual_Numbers add primary key (Player_CCNumber);


alter table NBA.Game add constraint HomeTeamFK foreign key (Home_Team_ID) references NBA.Team(ID);
alter table NBA.Game add constraint AwayTeamFK foreign key (Away_Team_ID) references NBA.Team(ID);
alter table NBA.Game add constraint StadiumFK foreign key (Stadium_ID) references NBA.Stadium(ID);

