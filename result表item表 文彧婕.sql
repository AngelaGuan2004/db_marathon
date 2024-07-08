Create table result_(
Result_id integer,
Gun_result interval,
Net_result interval,
Player_id integer,
Event_id integer,
Rank integer,
Gender_rank integer,
Primary key(Result_id),
FOREIGN KEY(player_id) REFERENCES Player(ID),
FOREIGN KEY(event_id) REFERENCES Event(ID)
)；

Create table Item_(
ID integer,
Name varchar(10),
Primary key(ID)
)；

