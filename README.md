readme

DB 값 넣기

```
-- 1. 기존 데이터 깨끗이 비우기
SET FOREIGN_KEY_CHECKS = 0; 
TRUNCATE TABLE Jobs; 
SET FOREIGN_KEY_CHECKS = 1;

-- 2. 데이터 입력 (Code 컬럼 삭제됨)
-- =============================================
-- 1차 직업 (ROOT)
-- =============================================
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (1, 'Warrior', 1, 1, NULL);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (2, 'Thief', 1, 1, NULL);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (3, 'Mage', 1, 1, NULL);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (4, 'Archer', 1, 1, NULL);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (5, 'Healer', 1, 1, NULL);

-- =============================================
-- 2차 직업
-- =============================================
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (10, 'Knight', 2, 10, 1);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (11, 'Spearman', 2, 10, 1);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (12, 'Ronin', 2, 10, 1);

INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (30, 'Assassin', 2, 10, 2);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (31, 'ShadowDancer', 2, 10, 2);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (32, 'Phantom', 2, 10, 2);

INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (50, 'ArchMage', 2, 10, 3);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (51, 'DarkMage', 2, 10, 3);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (52, 'TimeMage', 2, 10, 3);

INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (70, 'Hunter', 2, 10, 4);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (71, 'Sniper', 2, 10, 4);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (72, 'TrickArcher', 2, 10, 4);

INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (90, 'Cleric', 2, 10, 5);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (91, 'Onmyoji', 2, 10, 5);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (92, 'Alchemist', 2, 10, 5);

-- =============================================
-- 3차 직업
-- =============================================
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (20, 'Paladin', 3, 30, 10);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (21, 'DragonKnight', 3, 30, 11);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (22, 'Blade', 3, 30, 12);

INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (40, 'Reaper', 3, 30, 30);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (41, 'Specter', 3, 30, 31);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (42, 'Raven', 3, 30, 32);

INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (60, 'GrandSorcerer', 3, 30, 50);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (61, 'NecroMancer', 3, 30, 51);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (62, 'ChronoTrigger', 3, 30, 52);

INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (80, 'BeastMaster', 3, 30, 70);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (81, 'DeadEye', 3, 30, 71);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (82, 'Mirage', 3, 30, 72);

INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (100, 'Priest', 3, 30, 90);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (101, 'Sorcerer', 3, 30, 91);
INSERT INTO Jobs (Id, Name, Tier, MinLevel, ParentId) VALUES (102, 'Homunculus', 3, 30, 92);
```