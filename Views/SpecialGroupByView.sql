CREATE VIEW SpecialsGroupedByDate AS
SELECT CAST(CreatedAt AS DATE) as 'Date', Title, Price FROM Specials GROUP BY CAST(CreatedAt AS DATE), Title, Price;