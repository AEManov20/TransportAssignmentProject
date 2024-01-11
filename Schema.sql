USE master
GO

CREATE DATABASE [TransportDatabase]
GO

USE [TransportDatabase]
GO

CREATE TABLE TransportDatabase.dbo.Users (
	Id uniqueidentifier NOT NULL,
	FirstName varchar(100) NOT NULL,
	LastName varchar(100) NOT NULL,
	Username varchar(100) NOT NULL,
	PasswordHash varchar(250) NOT NULL,
	Email varchar(320) NOT NULL,
	PhoneNumber varchar(20) NOT NULL,
	CONSTRAINT Users_PK PRIMARY KEY (Id)
);

CREATE TABLE TransportDatabase.dbo.Vehicles (
	Id uniqueidentifier NOT NULL,
	Brand varchar(100) NOT NULL,
	Model varchar(100) NOT NULL,
	RegistrationNumber varchar(100) NOT NULL,
	RegisteredInCountry varchar(2) NOT NULL,
	Seats smallint NOT NULL,
	Color varchar(50) NOT NULL,
	CONSTRAINT Vehicles_PK PRIMARY KEY (Id),
);

CREATE TABLE TransportDatabase.dbo.Drivers (
	Id uniqueidentifier NOT NULL,
	AvailabilityStatus smallint NOT NULL,
	VehicleId uniqueidentifier NOT NULL,
	CONSTRAINT Drivers_PK PRIMARY KEY (Id),
	CONSTRAINT Drivers_Users_FK FOREIGN KEY (Id) REFERENCES TransportDatabase.dbo.Users(Id),
	CONSTRAINT Drivers_Vehicles_FK FOREIGN KEY (VehicleId) REFERENCES TransportDatabase.dbo.Vehicles(Id)
);

CREATE TABLE TransportDatabase.dbo.UserReviews (
	Id uniqueidentifier NOT NULL,
	AuthorId uniqueidentifier NOT NULL,
	Rating float NOT NULL,
	Content varchar(2000) NOT NULL,
	CONSTRAINT UserReviews_PK PRIMARY KEY (Id),
	CONSTRAINT UserReviews_Users_FK FOREIGN KEY (AuthorId) REFERENCES TransportDatabase.dbo.Users(Id)
);

CREATE TABLE TransportDatabase.dbo.Rides (
	Id uniqueidentifier NOT NULL,
	RiderId uniqueidentifier NOT NULL,
	DriverId uniqueidentifier NOT NULL,
	[Status] smallint NOT NULL,
	RequestedOn datetime2(0) NOT NULL,
	UserReviewId uniqueidentifier NULL,
	CONSTRAINT Rides_PK PRIMARY KEY (Id),
	CONSTRAINT Rides_Users_FK FOREIGN KEY (RiderId) REFERENCES TransportDatabase.dbo.Users(Id),
	CONSTRAINT Rides_UserReviews_FK FOREIGN KEY (UserReviewId) REFERENCES TransportDatabase.dbo.UserReviews(Id),
	CONSTRAINT Rides_Drivers_FK FOREIGN KEY (DriverId) REFERENCES TransportDatabase.dbo.Drivers(Id)
);

CREATE TABLE TransportDatabase.dbo.RideStops (
	Id uniqueidentifier NOT NULL,
	RideId uniqueidentifier NOT NULL,
	AddressText varchar(500) NOT NULL,
	OrderingNumber smallint NOT NULL,
	CONSTRAINT RideStops_PK PRIMARY KEY (Id),
	CONSTRAINT RideStops_Rides_FK FOREIGN KEY (RideId) REFERENCES TransportDatabase.dbo.Rides(Id)
);

CREATE TABLE TransportDatabase.dbo.UserReviewsDrivers (
	UserReviewId uniqueidentifier NOT NULL,
	DriverId uniqueidentifier NOT NULL,
	CONSTRAINT UserReviewsDrivers_PK PRIMARY KEY (UserReviewId,DriverId),
	CONSTRAINT UserReviewsDrivers_UN UNIQUE (UserReviewId),
	CONSTRAINT UserReviewsDrivers_UserReviews_FK FOREIGN KEY (UserReviewId) REFERENCES TransportDatabase.dbo.UserReviews(Id),
	CONSTRAINT UserReviewsDrivers_Drivers_FK FOREIGN KEY (DriverId) REFERENCES TransportDatabase.dbo.Drivers(Id)
);
