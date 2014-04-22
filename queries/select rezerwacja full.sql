SELECT Klient.Imiê as Imiê_Klienta, Klient.Nazwisko as Nazwisko_Klienta,
 Pracownik.Nazwisko as Nazwisko_Pracownika, Typ_statku.Nazwa_typu, Statek.Cena_za_dobê,
 Rezerwacja.Data_wypo¿yczenia, Rezerwacja.Data_zwrotu

FROM Klient 
INNER JOIN Rezerwacja ON Klient.Klient_ID = Rezerwacja.Klient_Klient_ID
INNER JOIN pilotuje ON Rezerwacja.Rezerwacja_ID =pilotuje.Rezerwacja_Rezerwacja_ID
INNER JOIN Pracownik ON pilotuje.Pracownik_Pracownik_ID = Pracownik.Pracownik_ID
INNER JOIN Statek ON Rezerwacja.Statek_Statek_ID = Statek.Statek_ID
INNER JOIN Typ_statku ON Statek.Typ_statku_Typ_statku_ID = Typ_statku.Typ_statku_ID