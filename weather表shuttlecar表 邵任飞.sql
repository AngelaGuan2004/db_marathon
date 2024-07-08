CREATE TABLE EVENT 
(
  ID INTEGER NOT NULL, 
  CATEGORY VARCHAR2(20) NOT NULL, 
  NAME VARCHAR2(20) NOT NULL, 
  START_DATE DATE NOT NULL, 
  END_DATE DATE NOT NULL, 
  EVENT_DATE DATE NOT NULL, 
  CONSTRAINT EVENT_PK PRIMARY KEY 
  (
    ID 
  )
  ENABLE 
);

CREATE TABLE shuttlecar (
    event_id INTEGER NOT NULL,  
    id INTEGER NOT NULL,        
    depature_time VARCHAR(10),      
    arrival_time VARCHAR(10),       
    PRIMARY KEY (id),
FOREIGN KEY (event_id) REFERENCES event  
);
CREATE TABLE weather (
    id INTEGER NOT NULL,               
time INTEGER NOT NULL,              
    temperature INTEGER,              
condition VARCHAR(20),                
whether_to_proceed INTEGER,         
PRIMARY KEY (id, time),                
FOREIGN KEY (id) REFERENCES event  
);
