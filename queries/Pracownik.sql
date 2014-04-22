INSERT INTO [dbo].[Pracownik]
           ([Imiê]
           ,[Nazwisko]
           ,[Data_urodzenia]
		   ,[Miejsce_urodzenia]
           ,[Pensja]
           ,[Funkcja_Funkcja_ID])
     VALUES
           ('Luke', 'Skywalker', '01/01/77', 'Polis Massa', 5000, 
		   (SELECT [Funkcja_ID]
				FROM [dbo].[Funkcja]
				WHERE Nazwa_funkcji = 'Pilot')
			),
		   ('Han', 'Solo', '01/01/77', 'Corelia', 6000, 
		   (SELECT [Funkcja_ID]
				FROM [dbo].[Funkcja]
				WHERE Nazwa_funkcji = 'Pilot')
			),
		   ('Jar Jar', 'Binks', '01/01/77', 'Naboo', 3000, 
		   (SELECT [Funkcja_ID]
				FROM [dbo].[Funkcja]
				WHERE Nazwa_funkcji = 'Serwisant')
			)
