-- Opretter databasen 'Cars'
CREATE DATABASE Cars;
GO

-- Skifter til Cars-databasen
USE Cars;
GO

CREATE TABLE Cars (
    CarID INT IDENTITY(1,1) PRIMARY KEY,      -- Auto-incrementerende primærnøgle
    Model NVARCHAR(100) NOT NULL,             -- Modelnavn på bilen (maks 100 tegn)
    Brand NVARCHAR(100) NOT NULL,             -- Mærke af bilen (maks 100 tegn)
    Price DECIMAL(18, 2) NOT NULL,            -- Prisen på bilen (med 2 decimaler)
    YearOfManufacture DATE NOT NULL,          -- Produktionsår for bilen
    IsElectric BIT NOT NULL                   -- Indikerer om bilen er elektrisk (1 eller 0)
);


INSERT INTO Cars (Model, Brand, Price, YearOfManufacture, IsElectric)
VALUES ('Model S', 'Tesla', 79999.99, '2020-05-15', 1);

INSERT INTO Cars (Model, Brand, Price, YearOfManufacture, IsElectric)
VALUES ('Mustang', 'Ford', 55999.50, '2018-07-20', 0);

INSERT INTO Cars (Model, Brand, Price, YearOfManufacture, IsElectric)
VALUES ('Civic', 'Honda', 22999.99, '2019-03-10', 0);

INSERT INTO Cars (Model, Brand, Price, YearOfManufacture, IsElectric)
VALUES ('Leaf', 'Nissan', 31999.99, '2021-01-25', 1);

INSERT INTO Cars (Model, Brand, Price, YearOfManufacture, IsElectric)
VALUES ('i3', 'BMW', 45999.99, '2020-10-05', 1);


select * from Cars