INSERT INTO Tasks (Company, Description, Creator, Assignee, ReportedDate, DueDate, Priority, Status)
SELECT "ABC Solutions", "Implementace úvodní stránky", "Pavel Novák", "Jana Dvořáková", "2024-08-01", "2024-09-10", "Medium", "Completed"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "SecureNet", "Návrh databázového modelu", "Marek Horáček", "František Krejčí", "2024-08-10", "2024-09-02", "High", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "SecureNet", "Testování bezpečnosti", "Alena Šťastná", "Petr Svoboda", "2024-08-15", "2024-09-20", "Low", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "Optima Solutions", "Optimalizace výkonu", "Alena Šťastná", "Eva Černá", "2024-08-12", "2024-09-18", "Medium", "InProgress"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "XYZ Corp", "Vytvoření marketingové kampaně", "Tomáš Beneš", "Ivana Svobodová", "2024-08-05", "2024-09-15", "Medium", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "ABC Solutions", "Implementace API", "Pavel Novák", "Karel Urban", "2024-08-07", "2024-09-12", "High", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "SecureNet", "Auditing IT infrastruktury", "Marek Horáček", "Lenka Růžičková", "2024-08-08", "2024-09-22", "Low", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "XYZ Corp", "Návrh nového firemního loga", "Tomáš Beneš", "Eva Černá", "2024-08-09", "2024-09-18", "High", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "Optima Solutions", "Implementace CI/CD pipeline", "Alena Šťastná", "Jana Dvořáková", "2024-08-10", "2024-09-25", "Medium", "InProgress"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "SecureNet", "Bezpečnostní audit", "Alena Šťastná", "František Krejčí", "2024-08-12", "2024-09-20", "Low", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "XYZ Corp", "Vývoj mobilní aplikace", "Tomáš Beneš", "Petr Svoboda", "2024-08-14", "2024-09-28", "Low", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "Optima Solutions", "Refaktoring starého kódu", "Alena Šťastná", "Karel Urban", "2024-08-16", "2024-09-23", "Medium", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "ABC Solutions", "Vývoj e-commerce platformy", "Pavel Novák", "Jana Dvořáková", "2024-08-18", "2024-09-30", "Low", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "SecureNet", "Zajištění souladu s GDPR", "Marek Horáček", "Lenka Růžičková", "2024-08-20", "2024-09-25", "Low", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "XYZ Corp", "Optimalizace firemní sítě", "Tomáš Beneš", "Eva Černá", "2024-08-22", "2024-09-29", "Medium", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "Optima Solutions", "Nasazení nové databáze", "Alena Šťastná", "František Krejčí", "2024-08-24", "2024-09-20", "High", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "ABC Solutions", "Migrace na cloud", "Pavel Novák", "Karel Urban", "2024-08-26", "2024-09-22", "Low", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "SecureNet", "Vytvoření zálohovacího systému", "Marek Horáček", "Petr Svoboda", "2024-08-28", "2024-09-30", "Medium", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "XYZ Corp", "Revize obchodních procesů", "Tomáš Beneš", "Jana Dvořáková", "2024-08-30", "2024-09-27", "High", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks)
UNION ALL
SELECT "Optima Solutions", "Příprava QA prostředí", "Alena Šťastná", "Lenka Růžičková", "2024-09-01", "2024-09-24", "Medium", "New"
WHERE NOT EXISTS (SELECT 1 FROM Tasks);

INSERT INTO ChecklistItems (TaskId, Description, IsCompleted, DueDate)
SELECT 1, "Vytvořit základní strukturu stránky", 1, "2024-08-05"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 1, "Přidat obsah", 0, "2024-08-08"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 2, "Navrhnout ER diagram", 1, "2024-08-06"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 2, "Vytvořit SQL skripty", 1, "2024-08-07"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 3, "Provedení penetračního testu", 0, "2024-08-18"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 3, "Vyhodnocení výsledků testů", 0, "2024-08-20"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 4, "Profilování aplikace", 0, "2024-08-14"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 4, "Identifikace výkonových úzkých míst", 1, "2024-08-15"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 5, "Příprava konceptu kampaně", 0, "2024-08-10"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 5, "Prezentace návrhu", 0, "2024-08-15"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 6, "Implementace základních funkcí API", 0, "2024-08-12"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 6, "Testování API", 0, "2024-08-15"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 7, "Shromáždění potřebných dat", 0, "2024-08-09"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 7, "Analýza dat", 0, "2024-08-13"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 8, "Vytvoření návrhu loga", 0, "2024-08-12"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 8, "Prezentace loga klientovi", 0, "2024-08-16"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 9, "Nastavení prostředí pro CI/CD", 0, "2024-08-12"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 9, "Implementace CI/CD skriptů", 0, "2024-08-15"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 10, "Základní bezpečnostní audit", 0, "2024-08-13"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems)
UNION ALL
SELECT 10, "Testování zranitelností", 0, "2024-08-18"
WHERE NOT EXISTS (SELECT 1 FROM ChecklistItems);

INSERT INTO Messages (TaskId, NameUser, MessageText, Timestamp)
SELECT 1, "Jana Dvořáková", "Začínám pracovat na úvodní stránce.", "2024-08-03 10:15:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 2, "František Krejčí", "Databázový model je již téměr hotov.", "2024-08-04 14:30:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 3, "Petr Svoboda", "Bezpečnostní testy byly zahájeny.", "2024-08-16 09:00:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 4, "Eva Černá", "Pracuji na optimalizaci výkonu.", "2024-08-13 11:45:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 5, "Ivana Svobodová", "Zahajuji práci na marketingové kampani.", "2024-08-06 09:30:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 6, "Karel Urban", "Práce na API v plném proudu.", "2024-08-08 10:00:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 7, "Lenka Růžičková", "Audit IT infrastruktury probíhá podle plánu.", "2024-08-09 11:00:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 8, "Eva Černá", "Zahajuji práci na návrhu nového loga.", "2024-08-10 12:00:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 9, "Jana Dvořáková", "Pracuji na implementaci CI/CD.", "2024-08-11 13:00:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages)
UNION ALL
SELECT 10, "František Krejčí", "Audit bezpečnosti je téměř hotový.", "2024-08-12 14:00:00"
WHERE NOT EXISTS (SELECT 1 FROM Messages);
